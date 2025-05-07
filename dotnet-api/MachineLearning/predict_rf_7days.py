import pandas as pd
import numpy as np
import requests
import joblib
import json
from datetime import timedelta, datetime
from sklearn.metrics import r2_score, mean_squared_error, mean_absolute_error
from sklearn.preprocessing import StandardScaler
import os
import sys
import pathlib
from typing import Tuple, Dict, List, Optional
import logging
import hashlib
import time
from math import radians, sin, cos, sqrt, atan2

# Cấu hình logging
logging.basicConfig(
    level=logging.ERROR,  # Chỉ hiển thị ERROR
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# Constants
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
HISTORY_DIR = os.path.join(BASE_DIR, "weather_history")
MAX_HISTORY_DAYS = 5 * 365  # 5 năm
WINDOW_SIZE = 7  # Kích thước cửa sổ dự báo
API_URL = "https://archive-api.open-meteo.com/v1/archive"

# Danh sách các thành phố biển để tính khoảng cách
COASTAL_CITIES = [
    {"name": "Hai Phong", "lat": 20.8449, "lng": 106.6880},
    {"name": "Da Nang", "lat": 16.0544, "lng": 108.2022},
    {"name": "Nha Trang", "lat": 12.2388, "lng": 109.1967},
    {"name": "Vung Tau", "lat": 10.3461, "lng": 107.0842},
    {"name": "Phan Thiet", "lat": 10.9333, "lng": 108.1000},
    {"name": "Quy Nhon", "lat": 13.7829, "lng": 109.2196},
    {"name": "Ha Long", "lat": 20.9515, "lng": 107.0748}
]

# Feature và target columns
FEATURE_COLS = [
    "tmax", "tmin", "prcp", "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours", "wind_speed_max", "wind_gusts_max",
    "wind_dir_sin", "wind_dir_cos", "radiation_sum", "evapo_et0",
    "sunshine_duration", "dayofyear", "month", "year", "dayofweek",
    "season", "latitude", "longitude", "elevation", "coast_distance"
]

TARGET_COLS = [
    "tmax", "tmin", "prcp", "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours", "wind_speed_max", "wind_gusts_max",
    "wind_dir_sin", "wind_dir_cos", "radiation_sum", "evapo_et0",
    "sunshine_duration"
]

def mean_absolute_percentage_error(y_true: np.ndarray, y_pred: np.ndarray) -> float:
    """Tính toán Mean Absolute Percentage Error (MAPE).
    
    Args:
        y_true (np.ndarray): Giá trị thực tế
        y_pred (np.ndarray): Giá trị dự đoán
        
    Returns:
        float: Giá trị MAPE
    """
    y_true, y_pred = np.array(y_true), np.array(y_pred)
    mask = y_true != 0
    return np.mean(np.abs((y_true[mask] - y_pred[mask]) / y_true[mask])) * 100 if np.any(mask) else np.nan

def get_location_hash(lat: float, lng: float) -> str:
    """Tạo hash cho vị trí dựa trên kinh độ và vĩ độ.
    
    Args:
        lat (float): Vĩ độ
        lng (float): Kinh độ
        
    Returns:
        str: Hash của vị trí
    """
    # Làm tròn đến 4 chữ số thập phân để nhóm các vị trí gần nhau
    lat_rounded = round(lat, 4)
    lng_rounded = round(lng, 4)
    location_str = f"{lat_rounded}_{lng_rounded}"
    return hashlib.md5(location_str.encode()).hexdigest()

def get_history_file_path(lat: float, lng: float) -> str:
    """Lấy đường dẫn file lịch sử cho vị trí.
    
    Args:
        lat (float): Vĩ độ
        lng (float): Kinh độ
        
    Returns:
        str: Đường dẫn file lịch sử
    """
    # Tạo thư mục nếu chưa tồn tại
    os.makedirs(HISTORY_DIR, exist_ok=True)
    
    # Tạo tên file dựa trên hash của vị trí
    location_hash = get_location_hash(lat, lng)
    return os.path.join(HISTORY_DIR, f"weather_history_{location_hash}.csv")

def load_history_csv(lat: float, lng: float) -> Optional[pd.DataFrame]:
    """Tải dữ liệu lịch sử từ file CSV.
    
    Args:
        lat (float): Vĩ độ
        lng (float): Kinh độ
        
    Returns:
        Optional[pd.DataFrame]: DataFrame chứa dữ liệu lịch sử
    """
    try:
        history_file = get_history_file_path(lat, lng)
        if not os.path.exists(history_file):
            return None
            
        df = pd.read_csv(history_file)
        df['date'] = pd.to_datetime(df['date'])
        
        # Kiểm tra xem dữ liệu có phải của cùng vị trí không
        if 'latitude' in df.columns and 'longitude' in df.columns:
            stored_lat = df['latitude'].iloc[0]
            stored_lng = df['longitude'].iloc[0]
            if abs(stored_lat - lat) > 0.0001 or abs(stored_lng - lng) > 0.0001:
                logger.warning(f"Location mismatch for {history_file}")
                return None
                
        return df
    except Exception as e:
        logger.error(f"Error loading history CSV: {e}")
        return None

def save_history_csv(df: pd.DataFrame, lat: float, lng: float) -> None:
    """Lưu dữ liệu lịch sử vào file CSV.
    
    Args:
        df (pd.DataFrame): DataFrame cần lưu
        lat (float): Vĩ độ
        lng (float): Kinh độ
    """
    try:
        history_file = get_history_file_path(lat, lng)
        
        # Thêm thông tin vị trí vào DataFrame
        df['latitude'] = lat
        df['longitude'] = lng
        
        # Lưu file với mode phù hợp
        df.to_csv(
            history_file,
            index=False,
            mode='w',  # Luôn ghi đè file cũ
            float_format='%.4f'  # Format số thập phân
        )
    except Exception as e:
        logger.error(f"Error saving history CSV: {e}")
        raise

def fetch_weather_data(lat: float, lng: float) -> Tuple[Optional[pd.DataFrame], Optional[Dict]]:
    """Lấy dữ liệu thời tiết từ API.
    
    Args:
        lat (float): Vĩ độ
        lng (float): Kinh độ
        
    Returns:
        Tuple[Optional[pd.DataFrame], Optional[Dict]]: DataFrame chứa dữ liệu thời tiết và đơn vị đo
    """
    try:
        today = datetime.now().date()
        five_years_ago = today - timedelta(days=MAX_HISTORY_DAYS)
        
        params = {
            "latitude": lat,
            "longitude": lng,
            "start_date": five_years_ago.strftime('%Y-%m-%d'),
            "end_date": today.strftime('%Y-%m-%d'),
            "daily": ",".join([
                "temperature_2m_max", "temperature_2m_min", "apparent_temperature_max",
                "apparent_temperature_min", "precipitation_sum", "rain_sum",
                "precipitation_hours", "windspeed_10m_max", "windgusts_10m_max",
                "winddirection_10m_dominant", "shortwave_radiation_sum",
                "et0_fao_evapotranspiration", "sunshine_duration"
            ]),
            "timezone": "auto"
        }

        # Thêm retry logic
        max_retries = 3
        retry_delay = 2  # seconds
        
        for attempt in range(max_retries):
            try:
                response = requests.get(API_URL, params=params, timeout=30)
                response.raise_for_status()
                data = response.json()
                
                if "daily" not in data:
                    logger.error("No daily data in API response")
                    if attempt < max_retries - 1:
                        time.sleep(retry_delay)
                        continue
                    return None, None
                
                # Kiểm tra dữ liệu trả về có đủ không
                required_fields = [
                    "temperature_2m_max", "temperature_2m_min", "apparent_temperature_max",
                    "apparent_temperature_min", "precipitation_sum", "rain_sum",
                    "precipitation_hours", "windspeed_10m_max", "windgusts_10m_max",
                    "winddirection_10m_dominant", "shortwave_radiation_sum",
                    "et0_fao_evapotranspiration", "sunshine_duration"
                ]
                
                if not all(field in data["daily"] for field in required_fields):
                    logger.error("Missing required fields in API response")
                    if attempt < max_retries - 1:
                        time.sleep(retry_delay)
                        continue
                    return None, None
                
                df = pd.DataFrame({
                    "date": data["daily"]["time"],
                    "tmax": data["daily"]["temperature_2m_max"],
                    "tmin": data["daily"]["temperature_2m_min"],
                    "prcp": data["daily"]["precipitation_sum"],
                    "apparent_tmax": data["daily"]["apparent_temperature_max"],
                    "apparent_tmin": data["daily"]["apparent_temperature_min"],
                    "rain_sum": data["daily"]["rain_sum"],
                    "precip_hours": data["daily"]["precipitation_hours"],
                    "wind_speed_max": data["daily"]["windspeed_10m_max"],
                    "wind_gusts_max": data["daily"]["windgusts_10m_max"],
                    "wind_dir_dominant": data["daily"]["winddirection_10m_dominant"],
                    "radiation_sum": data["daily"]["shortwave_radiation_sum"],
                    "evapo_et0": data["daily"]["et0_fao_evapotranspiration"],
                    "sunshine_duration": data["daily"]["sunshine_duration"]
                })
                
                # Kiểm tra dữ liệu có đủ số lượng không
                if len(df) < 30:  # Ít nhất cần 30 ngày dữ liệu
                    logger.error(f"Insufficient data points: {len(df)}")
                    if attempt < max_retries - 1:
                        time.sleep(retry_delay)
                        continue
                    return None, None
                
                return df, data.get("daily_units", {})
                
            except requests.exceptions.RequestException as e:
                logger.error(f"API request failed (attempt {attempt + 1}/{max_retries}): {e}")
                if attempt < max_retries - 1:
                    time.sleep(retry_delay)
                    continue
                return None, None
                
    except Exception as e:
        logger.error(f"Error fetching weather data: {e}")
        return None, None

def update_history_csv(lat: float, lng: float) -> Tuple[Optional[pd.DataFrame], Optional[Dict]]:
    """Cập nhật dữ liệu lịch sử.
    
    Args:
        lat (float): Vĩ độ
        lng (float): Kinh độ
        
    Returns:
        Tuple[Optional[pd.DataFrame], Optional[Dict]]: DataFrame chứa dữ liệu thời tiết và đơn vị đo
    """
    try:
        today = datetime.now().date()
        df = load_history_csv(lat, lng)
        
        if df is None:
            # Nếu chưa có file lịch sử, tải toàn bộ dữ liệu
            df, daily_units = fetch_weather_data(lat, lng)
            if df is not None and len(df) > 0:
                # Xử lý dữ liệu thiếu
                df = handle_missing_data(df)
                # Chỉ lưu file khi có dữ liệu hợp lệ
                save_history_csv(df, lat, lng)
                return df, daily_units
            else:
                logger.error("Failed to fetch valid weather data")
                return None, None

        last_date = df['date'].max().date()
        if last_date >= today:
            # Nếu đã có dữ liệu của ngày hôm nay, sử dụng dữ liệu cũ
            return df, None

        # Cập nhật dữ liệu mới
        new_df, _ = fetch_weather_data(lat, lng)
        if new_df is not None and len(new_df) > 0:
            # Xử lý dữ liệu thiếu trong DataFrame mới
            new_df = handle_missing_data(new_df)
            
            # Đảm bảo các cột giống nhau trước khi nối
            common_columns = list(set(df.columns) & set(new_df.columns))
            df = df[common_columns]
            new_df = new_df[common_columns]
            
            # Nối dữ liệu mới vào DataFrame cũ
            df = pd.concat([
                df.dropna(how='all', axis=1),
                new_df.dropna(how='all', axis=1)
            ], ignore_index=True)
            
            # Loại bỏ các dòng trùng lặp theo ngày
            df = df.drop_duplicates(subset=['date'], keep='last')
            
            # Sắp xếp theo ngày
            df = df.sort_values('date').reset_index(drop=True)
            
            # Giới hạn số lượng dữ liệu lịch sử
            if len(df) > MAX_HISTORY_DAYS:
                df = df.iloc[-MAX_HISTORY_DAYS:]
            
            # Chỉ lưu file khi có dữ liệu hợp lệ
            if len(df) > 0:
                save_history_csv(df, lat, lng)
                return df, None
            else:
                logger.error("No valid data after processing")
                return None, None
        else:
            logger.error("Failed to fetch new weather data")
            return None, None
            
    except Exception as e:
        logger.error(f"Error updating history CSV: {e}")
        return None, None

def handle_missing_data(df: pd.DataFrame) -> pd.DataFrame:
    """Xử lý dữ liệu thiếu trong DataFrame.
    
    Args:
        df (pd.DataFrame): DataFrame cần xử lý
        
    Returns:
        pd.DataFrame: DataFrame đã được xử lý
    """
    try:
        # Đảm bảo cột date là datetime
        df['date'] = pd.to_datetime(df['date'])
        
        # Xử lý các giá trị null trong các cột số
        numeric_columns = df.select_dtypes(include=[np.number]).columns
        for col in numeric_columns:
            if col in ['prcp', 'rain_sum', 'precip_hours']:
                # Với các cột liên quan đến mưa, thay thế null bằng 0
                df[col] = df[col].fillna(0)
            else:
                # Với các cột khác, thay thế null bằng giá trị trung bình
                df[col] = df[col].fillna(df[col].mean())
        
        # Xử lý các giá trị ngoại lai
        for col in numeric_columns:
            if col not in ['wind_dir_sin', 'wind_dir_cos']:  # Bỏ qua các cột đã được chuẩn hóa
                # Tính Q1 và Q3
                Q1 = df[col].quantile(0.25)
                Q3 = df[col].quantile(0.75)
                IQR = Q3 - Q1
                
                # Xác định giới hạn
                lower_bound = Q1 - 1.5 * IQR
                upper_bound = Q3 + 1.5 * IQR
                
                # Thay thế các giá trị ngoại lai bằng giới hạn
                df[col] = df[col].clip(lower_bound, upper_bound)
        
        return df
    except Exception as e:
        logger.error(f"Error handling missing data: {e}")
        raise

def haversine_distance(lat1: float, lon1: float, lat2: float, lon2: float) -> float:
    """Tính khoảng cách giữa hai điểm trên mặt đất sử dụng công thức haversine."""
    R = 6371  # Bán kính trái đất tính bằng km

    lat1, lon1, lat2, lon2 = map(radians, [lat1, lon1, lat2, lon2])
    dlat = lat2 - lat1
    dlon = lon2 - lon1

    a = sin(dlat/2)**2 + cos(lat1) * cos(lat2) * sin(dlon/2)**2
    c = 2 * atan2(sqrt(a), sqrt(1-a))
    distance = R * c

    return distance

def get_elevation(lat: float, lng: float) -> float:
    """Lấy độ cao của một điểm từ Open-Meteo API."""
    try:
        url = f"https://api.open-meteo.com/v1/elevation?latitude={lat}&longitude={lng}"
        response = requests.get(url)
        data = response.json()
        
        # Handle different response formats
        if isinstance(data.get('elevation'), list):
            elevation = data['elevation'][0]
        else:
            elevation = data.get('elevation', 0)
            
        return float(elevation)
    except Exception as e:
        print(f"Error getting elevation: {e}")
        return 0

def calculate_distance_to_coast(lat: float, lng: float) -> float:
    """Tính khoảng cách đến thành phố biển gần nhất."""
    min_distance = float('inf')
    for city in COASTAL_CITIES:
        dist = haversine_distance(lat, lng, city['lat'], city['lng'])
        min_distance = min(min_distance, dist)
    return min_distance

# Thay thế hàm preprocess_data bằng phiên bản tối ưu hơn
def preprocess_data(df: pd.DataFrame, lat: float, lng: float) -> pd.DataFrame:
    """Tiền xử lý dữ liệu với tốc độ nhanh hơn."""
    try:
        # Sử dụng vectorization thay vì apply
        df['date'] = pd.to_datetime(df['date'], errors='coerce', format='%Y-%m-%d')
        
        # Tính toán các features thời gian cùng lúc
        dt = df['date'].dt
        df['dayofyear'] = dt.dayofyear
        df['month'] = dt.month
        df['year'] = dt.year
        df['dayofweek'] = dt.dayofweek
        df['season'] = (df['month'] % 12 + 3) // 3
        
        # Chuyển đổi hướng gió bằng vectorization
        wind_dir_rad = np.deg2rad(df['wind_dir_dominant'])
        df['wind_dir_sin'] = np.sin(wind_dir_rad)
        df['wind_dir_cos'] = np.cos(wind_dir_rad)
        
        # Thêm thông tin địa lý (cache lại nếu cùng vị trí)
        cache_key = f"{lat:.4f}_{lng:.4f}"
        if not hasattr(preprocess_data, 'geo_cache'):
            preprocess_data.geo_cache = {}
            
        if cache_key not in preprocess_data.geo_cache:
            elevation = get_elevation(lat, lng)
            coast_distance = calculate_distance_to_coast(lat, lng)
            preprocess_data.geo_cache[cache_key] = (elevation, coast_distance)
            
        elevation, coast_distance = preprocess_data.geo_cache[cache_key]
        
        df['latitude'] = lat
        df['longitude'] = lng
        df['elevation'] = elevation
        df['coast_distance'] = coast_distance

        return df
    except Exception as e:
        logger.error(f"Error preprocessing data: {e}")
        raise

def prepare_features(df: pd.DataFrame, lat: float, lng: float) -> Tuple[pd.DataFrame, pd.DataFrame, pd.Series]:
    """Chuẩn bị features và targets."""
    try:
        # Thêm thông tin vị trí
        df['latitude'] = lat
        df['longitude'] = lng
        
        df[FEATURE_COLS] = df[FEATURE_COLS].apply(pd.to_numeric, errors="coerce")
        return df[FEATURE_COLS], df[TARGET_COLS], df['date']
    except Exception as e:
        logger.error(f"Error preparing features: {e}")
        raise

def create_sliding_window_features(
    df: pd.DataFrame,
    feature_cols: List[str],
    target_cols: List[str],
    window: int = WINDOW_SIZE
) -> Tuple[np.ndarray, np.ndarray]:
    """Tạo sliding window features."""
    try:
        features = []
        targets = []
        for i in range(window, len(df)):
            feat = df.iloc[i-window:i][feature_cols].values.flatten()
            features.append(feat)
            targets.append(df.iloc[i][target_cols].values)
        return np.array(features), np.array(targets)
    except Exception as e:
        logger.error(f"Error creating sliding window features: {e}")
        raise

def forecast_next_7_days(
    model,
    df: pd.DataFrame,
    feature_cols: List[str],
    scaler: StandardScaler,
    window: int = WINDOW_SIZE
) -> List[Dict]:
    """Dự báo 7 ngày tiếp theo."""
    try:
        # Lấy dữ liệu 7 ngày gần nhất
        last_days = df.iloc[-window:].copy()
        last_days_features = last_days[feature_cols].values.flatten().reshape(1, -1)
        last_days_scaled = scaler.transform(last_days_features)
        
        last_date = datetime.now().date()
        predictions = []

        for _ in range(7):
            pred = model.predict(last_days_scaled)[0]
            next_date = last_date + timedelta(days=1)
            
            predictions.append({
                'date': next_date.strftime('%Y-%m-%d'),
                **dict(zip(TARGET_COLS, pred))
            })

            # Tính toán features cho ngày tiếp theo
            dayofyear = next_date.timetuple().tm_yday
            month = next_date.month
            year = next_date.year
            dayofweek = next_date.weekday()
            season = (month % 12 + 3) // 3

            # Tạo features cho ngày tiếp theo
            next_features = np.concatenate([
                pred,
                [dayofyear, month, year, dayofweek, season],
                [df['latitude'].iloc[0], df['longitude'].iloc[0], 
                 df['elevation'].iloc[0], df['coast_distance'].iloc[0]]
            ])

            # Cập nhật sliding window
            last_days_unscaled = np.concatenate([
                last_days_features.flatten()[len(feature_cols):],
                next_features
            ]).reshape(1, -1)
            
            last_days_scaled = scaler.transform(last_days_unscaled)
            last_date = next_date

        return predictions
    except Exception as e:
        logger.error(f"Error forecasting next 7 days: {e}")
        raise

def main():
    try:
        # Kiểm tra tham số
        if len(sys.argv) < 3:
            logger.error("Usage: python predict_rf_7days.py <latitude> <longitude>")
            sys.exit(1)

        try:
            lat = float(sys.argv[1])
            lng = float(sys.argv[2])
        except ValueError:
            logger.error("Error: Latitude and longitude must be valid numbers")
            sys.exit(1)

        # Tải dữ liệu
        current_dir = os.path.dirname(os.path.abspath(__file__))
        df_raw, daily_units = update_history_csv(lat, lng)
        
        if df_raw is None:
            logger.error("Failed to load or update weather data")
            sys.exit(1)

        if daily_units is None:
            daily_units = {
                "tmax": "°C", "tmin": "°C", "prcp": "mm",
                "apparent_tmax": "°C", "apparent_tmin": "°C",
                "rain_sum": "mm", "precip_hours": "h",
                "wind_speed_max": "km/h", "wind_gusts_max": "km/h",
                "wind_dir_dominant": "°", "radiation_sum": "W/m²",
                "evapo_et0": "mm", "sunshine_duration": "h"
            }

        # Tiền xử lý
        df = preprocess_data(df_raw, lat, lng)
        df = df.dropna().reset_index(drop=True)
        
        if len(df) < 30:  # Kiểm tra lại sau khi xử lý
            logger.error("Insufficient data after preprocessing")
            sys.exit(1)

        # Load model và scaler
        scaler = joblib.load(os.path.join(current_dir, "scaler.pkl"))
        model = joblib.load(os.path.join(current_dir, "rf_model_7days.joblib"))

        # Chuẩn bị features
        X, y, _ = prepare_features(df, lat, lng)
        X, y = create_sliding_window_features(df, FEATURE_COLS, TARGET_COLS)
        X = scaler.transform(X)

        # Đánh giá mô hình
        y_pred = model.predict(X)
        metrics = {
            'accuracy': round(r2_score(y, y_pred) * 100, 2),
            'mse': round(mean_squared_error(y, y_pred), 4),
            'rmse': round(np.sqrt(mean_squared_error(y, y_pred)), 4),
            'mae': round(mean_absolute_error(y, y_pred), 4),
            'mape': round(mean_absolute_percentage_error(y, y_pred), 2)
        }

        # Dự báo
        predictions = forecast_next_7_days(model, df, FEATURE_COLS, scaler)

        # Xuất kết quả
        result = {
            'metrics': metrics,
            'forecast': predictions,
            'daily_units': daily_units
        }

        print(json.dumps(result, ensure_ascii=False, indent=2))

    except Exception as e:
        logger.error(f"Error in main: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()

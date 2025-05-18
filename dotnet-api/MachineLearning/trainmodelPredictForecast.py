import pandas as pd
import numpy as np
from sklearn.ensemble import RandomForestRegressor
from sklearn.multioutput import MultiOutputRegressor
from sklearn.model_selection import train_test_split
from sklearn.impute import SimpleImputer
from sklearn.preprocessing import StandardScaler
import json
from datetime import timedelta
from sklearn.metrics import r2_score, mean_squared_error, mean_absolute_error
import requests
import joblib
import os
from math import radians, sin, cos, sqrt, atan2
import time
from typing import Dict, List, Tuple

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
        elevation = data.get('elevation', [0])[0] if isinstance(data.get('elevation'), list) else data.get('elevation', 0)
        return float(elevation)
    except Exception as e:
        print(f"Error getting elevation: {e}")
        return 0

def calculate_distance_to_coast(lat: float, lng: float, coastal_cities: List[Dict]) -> float:
    """Tính khoảng cách đến thành phố biển gần nhất."""
    min_distance = float('inf')
    for city in coastal_cities:
        dist = haversine_distance(lat, lng, city['lat'], city['lng'])
        min_distance = min(min_distance, dist)
    return min_distance

def fetch_weather_data(lat: float, lng: float, start_date: str, end_date: str, location_name: str) -> pd.DataFrame:
    """Lấy dữ liệu thời tiết từ API cho một vị trí cụ thể."""
    url = "https://archive-api.open-meteo.com/v1/archive"
    params = {
        "latitude": lat,
        "longitude": lng,
        "start_date": start_date,
        "end_date": end_date,
        "daily": "temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_sum,rain_sum,precipitation_hours,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration,sunshine_duration",
        "timezone": "auto"
    }
    
    # Thêm retry logic với thời gian chờ tăng dần
    max_retries = 3
    base_delay = 5  # Tăng thời gian chờ cơ bản
    
    for attempt in range(max_retries):
        try:
            response = requests.get(url, params=params)
            response.raise_for_status()
            data = response.json()
            
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
            
            # Thêm tên thành phố
            df['name'] = location_name
            
            return df
            
        except Exception as e:
            if attempt < max_retries - 1:
                delay = base_delay * (2 ** attempt)  # Tăng thời gian chờ theo cấp số nhân
                print(f"Attempt {attempt + 1} failed. Retrying in {delay} seconds...")
                time.sleep(delay)
            else:
                raise Exception(f"Failed to fetch weather data after {max_retries} attempts: {e}")

def prepare_features(df: pd.DataFrame, lat: float, lng: float, elevation: float, coast_distance: float) -> pd.DataFrame:
    """Chuẩn bị features cho mô hình."""
    df['date'] = pd.to_datetime(df['date'], errors='coerce')
    df = df.sort_values('date').reset_index(drop=True)

    # Tạo đặc trưng thời gian
    df['dayofyear'] = df['date'].dt.dayofyear
    df['month'] = df['date'].dt.month
    df['year'] = df['date'].dt.year
    df['dayofweek'] = df['date'].dt.dayofweek
    df['season'] = (df['month'] % 12 + 3) // 3  # 1: Xuân, 2: Hạ, 3: Thu, 4: Đông

    # Chuyển đổi hướng gió
    df['wind_dir_sin'] = np.sin(np.deg2rad(df['wind_dir_dominant']))
    df['wind_dir_cos'] = np.cos(np.deg2rad(df['wind_dir_dominant']))
    
    # Thêm thông tin địa lý
    df['latitude'] = lat
    df['longitude'] = lng
    df['elevation'] = elevation
    df['coast_distance'] = coast_distance
    
    return df

# Danh sách các thành phố để huấn luyện (mở rộng)
locations = [
    # Các thành phố lớn
    {"name": "Ha Noi", "lat": 21.0285, "lng": 105.8542},
    {"name": "Ho Chi Minh", "lat": 10.7757, "lng": 106.7019},
    {"name": "Da Nang", "lat": 16.0544, "lng": 108.2022},
    {"name": "Hai Phong", "lat": 20.8449, "lng": 106.6880},
    {"name": "Can Tho", "lat": 10.0452, "lng": 105.7469},
    {"name": "Quy Nhon", "lat": 13.7829, "lng": 109.2196},
    # Miền Bắc
    {"name": "Sapa", "lat": 22.3364, "lng": 103.8438},
    {"name": "Ha Long", "lat": 20.9515, "lng": 107.0748},
    {"name": "Nam Dinh", "lat": 20.4333, "lng": 106.1833},
    {"name": "Thai Nguyen", "lat": 21.5667, "lng": 105.8250},
    # Miền Trung
    {"name": "Hue", "lat": 16.4637, "lng": 107.5909},
    {"name": "Nha Trang", "lat": 12.2388, "lng": 109.1967},
    {"name": "Phan Thiet", "lat": 10.9333, "lng": 108.1000},
    {"name": "Pleiku", "lat": 13.9833, "lng": 108.0000},
    # Miền Nam
    {"name": "Vung Tau", "lat": 10.3461, "lng": 107.0842},
    {"name": "Ca Mau", "lat": 9.1769, "lng": 105.1500},
    {"name": "Rach Gia", "lat": 10.0125, "lng": 105.0808},
    {"name": "Long Xuyen", "lat": 10.3864, "lng": 105.4351}
]

# Danh sách các thành phố biển để tính khoảng cách
coastal_cities = [
    {"name": "Hai Phong", "lat": 20.8449, "lng": 106.6880},
    {"name": "Da Nang", "lat": 16.0544, "lng": 108.2022},
    {"name": "Nha Trang", "lat": 12.2388, "lng": 109.1967},
    {"name": "Vung Tau", "lat": 10.3461, "lng": 107.0842},
    {"name": "Phan Thiet", "lat": 10.9333, "lng": 108.1000},
    {"name": "Quy Nhon", "lat": 13.7829, "lng": 109.2196},
    {"name": "Ha Long", "lat": 20.9515, "lng": 107.0748}
]

# Thời gian lấy dữ liệu
start_date = "2022-01-01"
end_date = "2024-04-28"

# Lấy dữ liệu cho tất cả các vị trí
all_data = []
for location in locations:
    try:
        print(f"Fetching data for {location['name']}...")
        
        # Lấy độ cao và khoảng cách đến biển
        elevation = get_elevation(location['lat'], location['lng'])
        time.sleep(3)  # Tăng thời gian chờ giữa các lần gọi API
        
        coast_distance = calculate_distance_to_coast(
            location['lat'], 
            location['lng'], 
            coastal_cities
        )
        
        # Lấy và chuẩn bị dữ liệu thời tiết
        df = fetch_weather_data(location['lat'], location['lng'], start_date, end_date, location['name'])
        time.sleep(5)  # Tăng thời gian chờ giữa các lần gọi API
        
        df = prepare_features(df, location['lat'], location['lng'], elevation, coast_distance)
        
        all_data.append(df)
        print(f"Successfully processed data for {location['name']}")
        time.sleep(3)  # Tăng thời gian chờ trước khi xử lý vị trí tiếp theo
        
    except Exception as e:
        print(f"Error processing data for {location['name']}: {e}")
        time.sleep(10)  # Tăng thời gian chờ khi có lỗi
        continue

# Kết hợp dữ liệu từ tất cả các vị trí
df = pd.concat(all_data, ignore_index=True)

# Danh sách đầy đủ các feature
feature_cols = [
    "tmax", "tmin", "prcp",
    "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours",
    "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
    "radiation_sum", "evapo_et0", "sunshine_duration",
    "dayofyear", "month", "year", "dayofweek", "season",
    "latitude", "longitude", "elevation", "coast_distance"
]

target_cols = [
    "tmax", "tmin", "prcp",
    "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours",
    "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
    "radiation_sum", "evapo_et0", "sunshine_duration"
]

# Convert hết sang số
df[feature_cols] = df[feature_cols].apply(pd.to_numeric, errors="coerce")

# Tạo dữ liệu dạng sliding window
window = 7
features = []
targets = []
dates = []

for location_name in df['name'].unique():
    location_data = df[df['name'] == location_name].copy()
    
    for i in range(window, len(location_data)):
        feat = location_data.iloc[i-window:i][feature_cols].values.flatten()
        features.append(feat)
        targets.append(location_data.iloc[i][target_cols].values)
        dates.append(location_data.iloc[i]['date'])

X = np.array(features)
y = np.array(targets)

# Chuẩn hóa features
scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# Train/test split
X_train, X_test, y_train, y_test = train_test_split(
    X_scaled, y,
    test_size=0.2,
    shuffle=True,  # Xáo trộn dữ liệu để đảm bảo tính đại diện
    random_state=42
)

# Huấn luyện mô hình với các tham số tối ưu
rf = MultiOutputRegressor(
    RandomForestRegressor(
        n_estimators=200,  # Tăng số lượng cây
        max_depth=20,      # Giới hạn độ sâu để tránh overfitting
        min_samples_split=5,
        min_samples_leaf=2,
        max_features='sqrt',  # Số features được xem xét ở mỗi split
        n_jobs=-1,  # Sử dụng tất cả CPU cores
        random_state=42
    )
)

print("Training model...")
rf.fit(X_train, y_train)

# Đánh giá mô hình
print("\nEvaluating model...")
pred_test = rf.predict(X_test)
r2 = r2_score(y_test, pred_test)
accuracy = round(r2 * 100, 2)

mse = mean_squared_error(y_test, pred_test)
rmse = np.sqrt(mse)
mae = mean_absolute_error(y_test, pred_test)

def mean_absolute_percentage_error(y_true, y_pred):
    y_true, y_pred = np.array(y_true), np.array(y_pred)
    mask = y_true != 0
    return np.mean(np.abs((y_true[mask] - y_pred[mask]) / y_true[mask])) * 100 if np.any(mask) else np.nan

mape = mean_absolute_percentage_error(y_test, pred_test)

# In kết quả đánh giá
print(f"\nModel Evaluation Results:")
print(f"Accuracy (R²): {accuracy}%")
print(f"MSE: {mse:.4f}")
print(f"RMSE: {rmse:.4f}")
print(f"MAE: {mae:.4f}")
print(f"MAPE: {mape:.2f}%")

# Lưu mô hình và scaler
print("\nSaving model and scaler...")
current_dir = os.path.dirname(os.path.abspath(__file__))
joblib.dump(rf, os.path.join(current_dir, "rf_model_7days.joblib"))
joblib.dump(scaler, os.path.join(current_dir, "scaler.pkl"))

print("\nModel and scaler have been saved successfully!")


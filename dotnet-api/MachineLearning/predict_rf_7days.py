import pandas as pd
import numpy as np
import requests
import joblib
import json
from datetime import timedelta, datetime
from sklearn.metrics import r2_score, mean_squared_error, mean_absolute_error
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
import os
import sys
import pathlib
sys.stdout.reconfigure(encoding='utf-8')

# --- 1. HÀM TIỆN ÍCH ---
def mean_absolute_percentage_error(y_true, y_pred):
    y_true, y_pred = np.array(y_true), np.array(y_pred)
    mask = y_true != 0
    return np.mean(np.abs((y_true[mask] - y_pred[mask]) / y_true[mask])) * 100 if np.any(mask) else np.nan

# --- 2. TẢI DỮ LIỆU TỪ OPEN-METEO ---
HISTORY_CSV = os.path.join(os.path.dirname(os.path.abspath(__file__)), "weather_history.csv")

def load_history_csv():
    if not os.path.exists(HISTORY_CSV):
        return None
    df = pd.read_csv(HISTORY_CSV)
    df['date'] = pd.to_datetime(df['date'])
    return df

def save_history_csv(df):
    df.to_csv(HISTORY_CSV, index=False)

def update_history_csv():
    today = datetime.now().date()
    df = load_history_csv()
    if df is None:
        # Chưa có file, tải toàn bộ lịch sử
        df, daily_units = fetch_weather_data()
        save_history_csv(df)
        return df, daily_units
    else:

        last_date = df['date'].max().date()
        if last_date >= today:
            # Đã cập nhật đến hôm nay
            return df, None
        else:
            # Cần cập nhật thêm ngày mới nhất
            url = "https://archive-api.open-meteo.com/v1/archive"
            params = {
                "latitude": 13.782,
                "longitude": 109.219,
                "start_date": (last_date + timedelta(days=1)).strftime('%Y-%m-%d'),
                "end_date": today.strftime('%Y-%m-%d'),
                "daily": "temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,"
                         "precipitation_sum,rain_sum,precipitation_hours,windspeed_10m_max,windgusts_10m_max,"
                         "winddirection_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration,sunshine_duration",
                "timezone": "auto"
            }
            response = requests.get(url, params=params)
            data = response.json()
            if "daily" not in data or len(data["daily"]["time"]) == 0:
                return df, None  # Không có dữ liệu mới
            new_df = pd.DataFrame({
                "date": data["daily"]["time"],
                "tmax": data["daily"]["temperature_2m_max"],
                "tmin": data["daily"]["temperature_2m_min"],
                "prcp": data["daily"]["precipitation_sum"],
                "apparent_tmax": data["daily"]["apparent_temperature_max"],
                "apparent_tmin": data["daily"]["apparent_temperature_min"],
                "rain_sum": data["daily"]["rain_sum"],
                "precip_hours": data["daily"]["precipitation_hours"],
                "wind_speed_max": data["daily"]["windspeed_10m_max"],
                "wind_gusts_max": data["daily"]["wind_gusts_max"],
                "wind_dir_dominant": data["daily"]["winddirection_10m_dominant"],
                "radiation_sum": data["daily"]["shortwave_radiation_sum"],
                "evapo_et0": data["daily"]["et0_fao_evapotranspiration"],
                "sunshine_duration": data["daily"]["sunshine_duration"]
            })
            new_df['date'] = pd.to_datetime(new_df['date'])
            df = pd.concat([df, new_df], ignore_index=True)
            # Nếu số dòng vượt quá số ngày mong muốn (ví dụ 5 năm), xóa dòng đầu
            max_days = 5 * 365
            if len(df) > max_days:
                df = df.iloc[-max_days:]
            save_history_csv(df)
            return df, None

def fetch_weather_data():
    url = "https://archive-api.open-meteo.com/v1/archive"
    today = datetime.now().date()
    five_years_ago = today - timedelta(days=5*365)
    params = {
        "latitude": 13.782,
        "longitude": 109.219,
        "start_date": five_years_ago.strftime('%Y-%m-%d'),
        "end_date": today.strftime('%Y-%m-%d'),
        "daily": "temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,"
                 "precipitation_sum,rain_sum,precipitation_hours,windspeed_10m_max,windgusts_10m_max,"
                 "winddirection_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration,sunshine_duration",
        "timezone": "auto"
    }
    try:
        response = requests.get(url, params=params)
        response.raise_for_status()
        data = response.json()
    except requests.exceptions.RequestException as e:
        print("Lỗi khi gọi API:", e)
        return None

    daily_units = data.get("daily_units", {})
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
    return df, daily_units

# --- 3. TIỀN XỬ LÝ DỮ LIỆU ---
def preprocess_data(df):
    df['date'] = pd.to_datetime(df['date'], errors='coerce')
    df = df.sort_values('date').reset_index(drop=True)

    df['dayofyear'] = df['date'].dt.dayofyear
    df['month'] = df['date'].dt.month
    df['year'] = df['date'].dt.year
    df['dayofweek'] = df['date'].dt.dayofweek

    df['wind_dir_sin'] = np.sin(np.deg2rad(df['wind_dir_dominant']))
    df['wind_dir_cos'] = np.cos(np.deg2rad(df['wind_dir_dominant']))

    return df

# --- 4. CHUẨN BỊ FEATURE & TARGET ---
def prepare_features(df):
    feature_cols = [
        "tmax", "tmin", "prcp",
        "apparent_tmax", "apparent_tmin",
        "rain_sum", "precip_hours",
        "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
        "radiation_sum", "evapo_et0", "sunshine_duration",
        "dayofyear", "month", "year", "dayofweek"
    ]
    target_cols = [
        "tmax", "tmin", "prcp",
        "apparent_tmax", "apparent_tmin",
        "rain_sum", "precip_hours",
        "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
        "radiation_sum", "evapo_et0", "sunshine_duration"
    ]

    df[feature_cols] = df[feature_cols].apply(pd.to_numeric, errors="coerce")
    return df[feature_cols], df[target_cols], df['date']

# --- 5. DỰ BÁO 7 NGÀY TỚI ---
def forecast_next_7_days(model, df, feature_cols, scaler, window=7):
    last_days = df.iloc[-window:][feature_cols].values.flatten().reshape(1, -1)
    last_days_scaled = scaler.transform(last_days)
    last_date = datetime.now().date()
    predictions = []

    for i in range(7):
        pred = model.predict(last_days_scaled)[0]
        next_date = last_date + timedelta(days=1)
        predictions.append({
            'date': next_date.strftime('%Y-%m-%d'),
            **dict(zip([
                "tmax", "tmin", "prcp", "apparent_tmax", "apparent_tmin",
                "rain_sum", "precip_hours", "wind_speed_max", "wind_gusts_max",
                "wind_dir_sin", "wind_dir_cos", "radiation_sum", "evapo_et0", "sunshine_duration"
            ], pred))
        })

        # Tính các đặc trưng thời gian cho ngày tiếp theo
        dayofyear = next_date.timetuple().tm_yday
        month = next_date.month
        year = next_date.year
        dayofweek = next_date.weekday()

        # Tạo feature mới cho ngày tiếp theo (14 giá trị dự báo + 4 đặc trưng thời gian)
        next_features = np.concatenate([
            pred,  # 14 giá trị dự báo
            [dayofyear, month, year, dayofweek]  # 4 đặc trưng thời gian
        ])

        # Cập nhật sliding window: bỏ 18 features đầu, thêm 18 features mới
        last_days_unscaled = np.concatenate([
            last_days.flatten()[18:],  # bỏ 1 ngày cũ nhất (18 features)
            next_features  # thêm ngày mới nhất
        ]).reshape(1, -1)
        last_days_scaled = scaler.transform(last_days_unscaled)
        last_date = next_date

    return predictions

def create_sliding_window_features(df, feature_cols, target_cols, window=7):
    features = []
    targets = []
    for i in range(window, len(df)):
        feat = df.iloc[i-window:i][feature_cols].values.flatten()
        features.append(feat)
        targets.append(df.iloc[i][target_cols].values)
    X = np.array(features)
    y = np.array(targets)
    return X, y

# --- 6. MAIN FLOW ---
def main():
    current_dir = os.path.dirname(os.path.abspath(__file__))
    # 1. Tải dữ liệu (ưu tiên từ file lịch sử)
    df_raw, daily_units = update_history_csv()
    if daily_units is None:
        # Nếu không có daily_units (do chỉ cập nhật 1 ngày), lấy từ file cũ hoặc hardcode
        daily_units = {
            "tmax": "°C", "tmin": "°C", "prcp": "mm", "apparent_tmax": "°C", "apparent_tmin": "°C",
            "rain_sum": "mm", "precip_hours": "h", "wind_speed_max": "km/h", "wind_gusts_max": "km/h",
            "wind_dir_dominant": "°", "radiation_sum": "W/m²", "evapo_et0": "mm", "sunshine_duration": "h"
        }

    # 2. Tiền xử lý
    df = preprocess_data(df_raw)

    # 2.1 Loại bỏ các dòng chứa NaN
    df = df.dropna().reset_index(drop=True)

    scaler = joblib.load(os.path.join(current_dir, "scaler.pkl"))
    # 3. Feature & target
    X, y, date_col = prepare_features(df)

    # 4. Load mô hình
    model = joblib.load(os.path.join(current_dir, "rf_model_7days.joblib"))

    # 5. Đánh giá mô hình
    # Cần có sẵn X_test và y_test nếu chưa có thì tách ở đây (giả sử tách 80-20)

    feature_cols = [
        "tmax", "tmin", "prcp",
        "apparent_tmax", "apparent_tmin",
        "rain_sum", "precip_hours",
        "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
        "radiation_sum", "evapo_et0", "sunshine_duration",
        "dayofyear", "month", "year", "dayofweek"
    ]
    target_cols = [
        "tmax", "tmin", "prcp",
        "apparent_tmax", "apparent_tmin",
        "rain_sum", "precip_hours",
        "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
        "radiation_sum", "evapo_et0", "sunshine_duration"
    ]

    X, y = create_sliding_window_features(df, feature_cols, target_cols, window=7)
    X = scaler.transform(X)

    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, shuffle=False)

    y_pred = model.predict(X_test)
    r2 = r2_score(y_test, y_pred)
    mse = mean_squared_error(y_test, y_pred)
    rmse = np.sqrt(mse)
    mae = mean_absolute_error(y_test, y_pred)
    mape = mean_absolute_percentage_error(y_test, y_pred)

    # 6. Dự báo
    predictions = forecast_next_7_days(model, df, feature_cols, scaler, window=7)

    # 7. Xuất JSON
    result = {
        'metrics': {
            'accuracy': round(r2 * 100, 2),
            'mse': round(mse, 4),
            'rmse': round(rmse, 4),
            'mae': round(mae, 4),
            'mape': round(mape, 2)
        },
        'forecast': predictions,
        'daily_units': daily_units
    }

    print(json.dumps(result, ensure_ascii=False, indent=2))

    # 8. Ghi file
    pd.DataFrame(predictions).to_csv("forecast_result.csv", index=False, mode='a', header=False)

# --- RUN ---
if __name__ == "__main__":
    main()

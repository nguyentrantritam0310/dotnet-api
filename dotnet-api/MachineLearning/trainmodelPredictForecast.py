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

# Gọi Open-Meteo
url = "https://archive-api.open-meteo.com/v1/archive"
params = {
    "latitude": 13.782,  # Quy Nhơn
    "longitude": 109.219,
    "start_date": "2022-01-01",
    "end_date": "2025-04-28",
    "daily": "temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_sum,rain_sum,precipitation_hours,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration,sunshine_duration",
    "timezone": "auto"
}

response = requests.get(url, params=params)
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

df['date'] = pd.to_datetime(df['date'], errors='coerce')
df = df.sort_values('date').reset_index(drop=True)

# Tạo đặc trưng thời gian
# (có thể giúp mô hình học được tính mùa vụ)
df['dayofyear'] = df['date'].dt.dayofyear
df['month'] = df['date'].dt.month
df['year'] = df['date'].dt.year
df['dayofweek'] = df['date'].dt.dayofweek

# Danh sách đầy đủ các feature
df['wind_dir_sin'] = np.sin(np.deg2rad(df['wind_dir_dominant']))
df['wind_dir_cos'] = np.cos(np.deg2rad(df['wind_dir_dominant']))
feature_cols = [
    "tmax", "tmin", "prcp",
    "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours",
    "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
    "radiation_sum", "evapo_et0", "sunshine_duration",
    "dayofyear", "month", "year", "dayofweek"
]

target_cols = ["tmax", "tmin", "prcp",
    "apparent_tmax", "apparent_tmin",
    "rain_sum", "precip_hours",
    "wind_speed_max", "wind_gusts_max", "wind_dir_sin", "wind_dir_cos",
    "radiation_sum", "evapo_et0", "sunshine_duration"]

# Convert hết sang số
df[feature_cols] = df[feature_cols].apply(pd.to_numeric, errors="coerce")

# Tạo dữ liệu dạng sliding window (dùng 7 ngày gần nhất để dự báo ngày tiếp theo)
window = 7
features = []
targets = []
dates = []
for i in range(window, len(df)):
    # Lấy 7 ngày trước làm feature
    feat = df.iloc[i-window:i][feature_cols].values.flatten()
    features.append(feat)
    # Target là giá trị ngày tiếp theo
    targets.append(df.iloc[i][target_cols].values)
    dates.append(df.iloc[i]['date'])

X = np.array(features)
y = np.array(targets)

scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# Train/test split (dùng gần hết để train, 7 ngày cuối để test)
X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, shuffle=False)

# Huấn luyện mô hình
rf = MultiOutputRegressor(RandomForestRegressor(n_estimators=100, random_state=42))
rf.fit(X_train, y_train)

# Đánh giá mô hình trên tập test
pred_test = rf.predict(X_test)
r2 = r2_score(y_test, pred_test)
accuracy = round(r2 * 100, 2)

mse = mean_squared_error(y_test, pred_test)
rmse = np.sqrt(mse)
mae = mean_absolute_error(y_test, pred_test)
# MAPE (Mean Absolute Percentage Error)
def mean_absolute_percentage_error(y_true, y_pred):
    y_true, y_pred = np.array(y_true), np.array(y_pred)
    # Tránh chia cho 0
    mask = y_true != 0
    return np.mean(np.abs((y_true[mask] - y_pred[mask]) / y_true[mask])) * 100 if np.any(mask) else np.nan
mape = mean_absolute_percentage_error(y_test, pred_test)


joblib.dump((rf), "rf_model_7days.joblib")
joblib.dump(scaler, "scaler.pkl")


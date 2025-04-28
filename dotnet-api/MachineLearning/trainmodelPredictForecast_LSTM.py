import pandas as pd
import numpy as np
from sklearn.preprocessing import MinMaxScaler
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import LSTM, Dense
from tensorflow.keras.callbacks import EarlyStopping
import joblib

def load_and_preprocess(file_path):
    df = pd.read_csv(file_path, index_col="DATE", parse_dates=True)
    df.dropna(how='all', inplace=True)
    df.drop_duplicates(inplace=True)
    df['PRCP'].fillna(0,inplace=True)
    df['TMAX'].fillna(df['TMAX'].median(),inplace=True)
    df['TMIN'].fillna(df['TMIN'].median(),inplace=True)
    df['TAVG'].fillna(df[['TMAX', 'TMIN']].mean(axis=1),inplace=True)
    df['TMAX_ATTRIBUTES'].fillna(',,S',inplace=True)
    df['TMIN_ATTRIBUTES'].fillna(',,S',inplace=True)
    df['PRCP_ATTRIBUTES'].fillna(',,S',inplace=True)
    # Chỉ lấy các cột cần thiết
    df = df[['PRCP', 'TAVG', 'TMAX', 'TMIN']]
    return df

def create_sequences(data, n_past, n_future):
    X, y = [], []
    for i in range(n_past, len(data) - n_future + 1):
        X.append(data[i - n_past:i, :])
        y.append(data[i:i + n_future, :])
    return np.array(X), np.array(y)

def main():
    file_path = 'localweather.csv'
    df = load_and_preprocess(file_path)

    scaler = MinMaxScaler()
    scaled_data = scaler.fit_transform(df.values)

    n_past = 30   # Sử dụng 30 ngày quá khứ để dự báo
    n_future = 7  # Dự báo 7 ngày tiếp theo

    X, y = create_sequences(scaled_data, n_past, n_future)

    # Chia train/test
    split = int(0.8 * len(X))
    X_train, X_test = X[:split], X[split:]
    y_train, y_test = y[:split], y[split:]

    # Xây dựng model LSTM
    model = Sequential([
        LSTM(64, activation='relu', input_shape=(n_past, X.shape[2]), return_sequences=False),
        Dense(n_future * X.shape[2])
    ])
    model.compile(optimizer='adam', loss='mse')
    es = EarlyStopping(monitor='val_loss', patience=10, restore_best_weights=True)
    model.fit(X_train, y_train.reshape(y_train.shape[0], -1), 
              validation_data=(X_test, y_test.reshape(y_test.shape[0], -1)),
              epochs=100, batch_size=32, callbacks=[es])

    # Lưu model và scaler
    model.save('lstm_weather_model.h5')
    joblib.dump(scaler, 'scaler.save')

    print("Model and scaler saved.")

if __name__ == "__main__":
    main()
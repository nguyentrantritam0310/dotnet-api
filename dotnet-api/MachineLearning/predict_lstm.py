import pandas as pd
import numpy as np
from tensorflow.keras.models import load_model
import joblib

def load_and_preprocess(file_path):
    df = pd.read_csv(file_path, index_col="DATE", parse_dates=True)
    df.dropna(how='all', inplace=True)
    df.drop_duplicates(inplace=True)
    df['PRCP'].fillna(0, inplace=True)
    df['TMAX'].fillna(df['TMAX'].median(), inplace=True)
    df['TMIN'].fillna(df['TMIN'].median(), inplace=True)
    df['TAVG'].fillna(df[['TMAX', 'TMIN']].mean(axis=1), inplace=True)
    df = df[['PRCP', 'TAVG', 'TMAX', 'TMIN']]
    return df

def predict_next_7_days():
    scaler = joblib.load('scaler.save')
    model = load_model('lstm_weather_model.h5', compile=False)
    df = load_and_preprocess('localweather.csv')
    last_30 = df.values[-30:]
    last_30_scaled = scaler.transform(last_30)
    X_input = np.expand_dims(last_30_scaled, axis=0)
    y_pred = model.predict(X_input)
    y_pred = y_pred.reshape(7, 4)
    y_pred_inv = scaler.inverse_transform(y_pred)

    last_date = df.index[-1]
    future_dates = pd.date_range(start=last_date + pd.Timedelta(days=1), periods=7)

    df_pred = pd.DataFrame(y_pred_inv, columns=['PRCP', 'TAVG', 'TMAX', 'TMIN'], index=future_dates)
    return df_pred.to_json(orient='records', date_format='iso')

if __name__ == "__main__":
    print(predict_next_7_days())
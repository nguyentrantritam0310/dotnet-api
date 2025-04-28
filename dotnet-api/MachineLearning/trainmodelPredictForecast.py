import pandas as pd
from sklearn.linear_model import Ridge
from sklearn.metrics import mean_absolute_error
from sklearn.model_selection import train_test_split
import joblib

def load_data(file_path):
    return pd.read_csv(file_path, index_col="DATE", header=0, delimiter=",", encoding="utf-8")

def data_preprocessing(weather):
     # xóa dòng dữ liệu rỗng
    weather.dropna(how='all', inplace=True)
    # xóa các dòng bị trùng
    weather.drop_duplicates(inplace=True)
    weather['PRCP'].fillna(0,inplace=True)
    weather['TMAX'].fillna(weather['TMAX'].median(),inplace=True)
    weather['TMIN'].fillna(weather['TMIN'].median(),inplace=True)
    weather['TMAX_ATTRIBUTES'].fillna(',,S',inplace=True)
    weather['TMIN_ATTRIBUTES'].fillna(',,S',inplace=True)
    weather['PRCP_ATTRIBUTES'].fillna(',,S',inplace=True)
    return weather

def create_predictors(predictors, core_weather, reg):
    train = core_weather.loc[:'2022-12-31']
    test = core_weather.loc['2023-01-01':]
    reg.fit(train[predictors], train['target'])
    predictions = reg.predict(test[predictors])
    error = mean_absolute_error(test['target'], predictions)
    combined = pd.concat([test["target"], pd.Series(predictions, index=test.index)], axis=1)
    combined.columns = ['Actual', 'Predicted']
    return error, combined

def save_model(model, model_path='ridge_model.pkl'):
    # Lưu mô hình
    joblib.dump(model, model_path)
    print(f"Model has been saved to {model_path}")

# Main function
def main():
    # Đường dẫn file dữ liệu
    file_path = 'localweather.csv'

    # Đọc dữ liệu
    weather = load_data(file_path)
    # Làm sạch dữ liệu
    data_preprocessing(weather)

    # Lấy dữ liệu cần thiết cho mô hình
    core_weather = weather[['PRCP', 'TAVG', 'TMAX', 'TMIN']].copy()
    core_weather.columns = ['Rainfall', 'Average Temperature', 'Max Temperature', 'Min Temperature']
    core_weather.index = pd.to_datetime(core_weather.index, format='%Y-%m-%d')

    # Thêm cột "target" cho giá trị cần dự báo (nhiệt độ tối đa)
    core_weather["target"] = core_weather.shift(-1)["Max Temperature"]
    core_weather = core_weather.iloc[: -1, :].copy()
    reg = Ridge(alpha=0.1)
    predictors = ['Rainfall', 'Average Temperature', 'Max Temperature', 'Min Temperature']
    
    # Huấn luyện mô hình Ridge
    error, combined = create_predictors(predictors, core_weather, reg)

    # In ra lỗi và kết quả dự báo
    print(f"Mean Absolute Error: {error}")
    print("Combined actual and predicted values:")
    print(combined.head())

    # Lưu mô hình và scaler
    save_model(reg)

if __name__ == "__main__":
    main()
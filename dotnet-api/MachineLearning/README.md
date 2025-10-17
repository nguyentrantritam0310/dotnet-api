# Face Recognition System với YOLO và FaceNet

Hệ thống nhận dạng khuôn mặt sử dụng YOLOv8 cho phát hiện khuôn mặt và FaceNet cho nhận dạng khuôn mặt.

## 🚀 Tính năng

- **Phát hiện khuôn mặt**: Sử dụng YOLOv8 để phát hiện khuôn mặt trong ảnh
- **Nhận dạng khuôn mặt**: Sử dụng FaceNet để nhận dạng và so sánh khuôn mặt
- **Đăng ký khuôn mặt**: Cho phép đăng ký khuôn mặt nhân viên mới
- **API RESTful**: Tích hợp với .NET Core API
- **Mobile App**: Tích hợp với React Native app

## 📋 Yêu cầu hệ thống

- Python 3.8+
- RAM: Tối thiểu 4GB (khuyến nghị 8GB+)
- GPU: Không bắt buộc nhưng khuyến nghị để tăng tốc độ
- .NET 6.0+

## 🛠️ Cài đặt

### 1. Cài đặt Python dependencies

```bash
# Chạy script setup tự động
python setup.py

# Hoặc cài đặt thủ công
pip install -r requirements.txt
```

### 2. Cài đặt trên Windows

Chạy file `setup.bat` để tự động cài đặt:

```cmd
setup.bat
```

### 3. Kiểm tra cài đặt

```bash
python face_recognition.py --help
```

## 📖 Sử dụng

### 1. Đăng ký khuôn mặt nhân viên

```bash
python face_recognition.py register path/to/image.jpg employee_001
```

### 2. Nhận dạng khuôn mặt

```bash
python face_recognition.py recognize path/to/image.jpg
```

### 3. API Endpoints

#### Đăng ký khuôn mặt
```http
POST /api/FaceRecognition/register/{employeeId}
Content-Type: multipart/form-data

image: [file]
```

#### Nhận dạng khuôn mặt (FormData)
```http
POST /api/FaceRecognition/recognize
Content-Type: multipart/form-data

image: [file]
```

#### Nhận dạng khuôn mặt (Base64)
```http
POST /api/FaceRecognition/recognize-base64
Content-Type: application/json

{
  "ImageBase64": "base64_encoded_image",
  "UserId": "employee_id"
}
```

#### Lấy danh sách nhân viên đã đăng ký
```http
GET /api/FaceRecognition/registered-employees
```

#### Xóa khuôn mặt đã đăng ký
```http
DELETE /api/FaceRecognition/unregister/{employeeId}
```

## 🏗️ Kiến trúc hệ thống

```
Face Recognition System
├── Backend (.NET Core)
│   ├── FaceRecognitionController.cs
│   ├── FaceRecognitionService.cs
│   └── DTOs/
├── Python ML Engine
│   ├── face_recognition.py
│   ├── requirements.txt
│   └── setup.py
└── Mobile App (React Native)
    ├── faceRecognitionService.js
    ├── face-registration.js
    └── checkin.js (updated)
```

## 🔧 Cấu hình

### Backend Configuration

Trong `appsettings.json`:

```json
{
  "PythonPath": "python",
  "FaceRecognition": {
    "FaceDetectionConfidence": 0.5,
    "FaceRecognitionThreshold": 0.6,
    "ModelDirectory": "MachineLearning/models"
  }
}
```

### Mobile App Configuration

Trong `faceRecognitionService.js`:

```javascript
const API_BASE_URL = 'http://localhost:5244/api'; // Thay đổi theo URL backend
```

## 📊 Quy trình nhận dạng

1. **Phát hiện khuôn mặt**: YOLOv8 phát hiện vùng khuôn mặt trong ảnh
2. **Alignment**: MTCNN căn chỉnh và chuẩn hóa khuôn mặt
3. **Feature Extraction**: FaceNet trích xuất embedding vector
4. **Comparison**: So sánh với database embeddings
5. **Decision**: Quyết định nhận dạng dựa trên threshold

## 🎯 Độ chính xác

- **Face Detection**: ~95% với YOLOv8
- **Face Recognition**: ~98% với FaceNet
- **Threshold**: 0.6 (có thể điều chỉnh)

## 🚨 Xử lý lỗi

### Lỗi thường gặp:

1. **"Không phát hiện được khuôn mặt"**
   - Kiểm tra chất lượng ảnh
   - Đảm bảo khuôn mặt rõ ràng, đủ ánh sáng
   - Không đeo kính râm hoặc che khuôn mặt

2. **"Độ tin cậy quá thấp"**
   - Tăng threshold trong config
   - Cải thiện chất lượng ảnh đăng ký

3. **"Lỗi Python script"**
   - Kiểm tra Python dependencies
   - Chạy lại setup.py

## 🔒 Bảo mật

- Face embeddings được lưu trữ local
- Không lưu trữ ảnh gốc
- API có timeout 30 giây
- Validation đầu vào nghiêm ngặt

## 📈 Hiệu suất

- **CPU**: ~2-3 giây/ảnh
- **GPU**: ~0.5-1 giây/ảnh
- **Memory**: ~2GB RAM khi chạy

## 🤝 Đóng góp

1. Fork repository
2. Tạo feature branch
3. Commit changes
4. Push to branch
5. Tạo Pull Request

## 📝 License

MIT License

## 📞 Hỗ trợ

Nếu gặp vấn đề, vui lòng tạo issue hoặc liên hệ team phát triển.

---

**Lưu ý**: Hệ thống này được thiết kế cho môi trường development/testing. Để production, cần thêm các biện pháp bảo mật và tối ưu hiệu suất.

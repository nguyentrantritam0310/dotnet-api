#!/usr/bin/env python3
"""
Setup script for Face Recognition System
Cài đặt dependencies và khởi tạo models
"""

import subprocess
import sys
import os
from pathlib import Path

def install_requirements():
    """Cài đặt các dependencies từ requirements.txt"""
    print("Đang cài đặt Python dependencies...")
    
    try:
        subprocess.check_call([
            sys.executable, "-m", "pip", "install", "-r", "requirements.txt"
        ])
        print("✅ Cài đặt dependencies thành công!")
        return True
    except subprocess.CalledProcessError as e:
        print(f"❌ Lỗi khi cài đặt dependencies: {e}")
        return False

def create_directories():
    """Tạo các thư mục cần thiết"""
    print("Đang tạo thư mục...")
    
    directories = [
        "models",
        "data",
        "logs"
    ]
    
    for dir_name in directories:
        Path(dir_name).mkdir(exist_ok=True)
        print(f"✅ Tạo thư mục: {dir_name}")

def download_models():
    """Tải các models cần thiết"""
    print("Đang tải models...")
    
    try:
        # Import sau khi đã cài đặt dependencies
        from ultralytics import YOLO
        from facenet_pytorch import MTCNN, InceptionResnetV1
        
        # Tải YOLO model
        print("Đang tải YOLO model...")
        yolo_model = YOLO('yolov8n.pt')
        print("✅ YOLO model đã sẵn sàng")
        
        # Tải FaceNet model
        print("Đang tải FaceNet model...")
        facenet_model = InceptionResnetV1(pretrained='vggface2')
        print("✅ FaceNet model đã sẵn sàng")
        
        # Tải MTCNN model
        print("Đang tải MTCNN model...")
        mtcnn = MTCNN(
            image_size=160,
            margin=0,
            min_face_size=20,
            thresholds=[0.6, 0.7, 0.7],
            factor=0.709,
            post_process=True
        )
        print("✅ MTCNN model đã sẵn sàng")
        
        return True
        
    except Exception as e:
        print(f"❌ Lỗi khi tải models: {e}")
        return False

def test_installation():
    """Kiểm tra cài đặt"""
    print("Đang kiểm tra cài đặt...")
    
    try:
        # Test imports
        import torch
        import cv2
        import numpy as np
        from ultralytics import YOLO
        from facenet_pytorch import MTCNN, InceptionResnetV1
        
        print("✅ Tất cả imports thành công!")
        print(f"PyTorch version: {torch.__version__}")
        print(f"CUDA available: {torch.cuda.is_available()}")
        print(f"OpenCV version: {cv2.__version__}")
        
        return True
        
    except ImportError as e:
        print(f"❌ Lỗi import: {e}")
        return False

def main():
    """Main setup function"""
    print("🚀 Bắt đầu setup Face Recognition System...")
    print("=" * 50)
    
    # Tạo thư mục
    create_directories()
    
    # Cài đặt dependencies
    if not install_requirements():
        print("❌ Setup thất bại!")
        sys.exit(1)
    
    # Tải models
    if not download_models():
        print("⚠️  Models chưa được tải, sẽ được tải khi chạy lần đầu")
    
    # Kiểm tra cài đặt
    if not test_installation():
        print("❌ Setup thất bại!")
        sys.exit(1)
    
    print("=" * 50)
    print("🎉 Setup hoàn tất thành công!")
    print("\nHướng dẫn sử dụng:")
    print("1. Đăng ký khuôn mặt: python face_recognition.py register <image_path> <employee_id>")
    print("2. Nhận dạng khuôn mặt: python face_recognition.py recognize <image_path>")
    print("\nLưu ý:")
    print("- Đảm bảo Python >= 3.8")
    print("- Cần ít nhất 4GB RAM")
    print("- GPU được khuyến nghị để tăng tốc độ")

if __name__ == "__main__":
    main()

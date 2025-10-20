#!/usr/bin/env python3
"""
Test script để kiểm tra face detection
"""

import sys
import os
import cv2
import numpy as np
from pathlib import Path

# Add current directory to path
sys.path.append(os.path.dirname(os.path.abspath(__file__)))

try:
    from face_recognition import FaceRecognitionSystem
    print("✅ Import face_recognition thành công")
except ImportError as e:
    print(f"❌ Lỗi import: {e}")
    sys.exit(1)

def test_face_detection():
    """Test face detection với ảnh mẫu"""
    try:
        print("🔧 Khởi tạo FaceRecognitionSystem...")
        face_system = FaceRecognitionSystem()
        print("✅ Khởi tạo thành công")
        
        # Tạo ảnh test đơn giản (màu xanh)
        test_image = np.zeros((300, 300, 3), dtype=np.uint8)
        test_image[:] = (0, 255, 0)  # Màu xanh
        
        # Lưu ảnh test
        test_path = "test_image.jpg"
        cv2.imwrite(test_path, test_image)
        print(f"✅ Đã tạo ảnh test: {test_path}")
        
        # Test detect face
        print("🔍 Bắt đầu test detect face...")
        result = face_system.detect_face(test_path)
        
        print(f"📊 Kết quả: {result}")
        
        # Cleanup
        if os.path.exists(test_path):
            os.remove(test_path)
            print("🧹 Đã xóa ảnh test")
            
    except Exception as e:
        print(f"❌ Lỗi trong test: {e}")
        import traceback
        traceback.print_exc()

if __name__ == "__main__":
    test_face_detection()



#!/usr/bin/env python3
"""
Face Recognition System using MTCNN and FaceNet
Phát hiện và align khuôn mặt bằng MTCNN, nhận dạng bằng FaceNet
Note: Client (React Native) đã dùng ML Kit để detect face trước khi gửi ảnh lên server.
MTCNN ở server chỉ dùng để align face tốt hơn và validate face có trong ảnh.
"""

import sys
import json
import cv2
import numpy as np
import os
from pathlib import Path
import logging
from typing import List, Tuple, Optional, Dict
import pickle
import base64
from io import BytesIO
from PIL import Image

# Machine Learning imports
try:
    import torch
    from facenet_pytorch import MTCNN, InceptionResnetV1
    import torch.nn.functional as F
except ImportError as e:
    print(f"Lỗi import: {e}")
    print("Vui lòng cài đặt: pip install torch facenet-pytorch opencv-python pillow")
    sys.exit(1)

# Setup logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class FaceRecognitionSystem:
    def __init__(self, model_dir: str = "models"):
        """
        Khởi tạo hệ thống nhận dạng khuôn mặt
        
        Args:
            model_dir: Thư mục chứa các model
        """
        self.model_dir = Path(model_dir)
        self.model_dir.mkdir(exist_ok=True)
        
        # Đường dẫn các file
        self.face_db_path = self.model_dir / "face_database.pkl"
        
        # Thresholds
        self.face_detection_confidence = 0.5
        self.face_recognition_threshold = 0.6
        
        # Initialize models
        # Note: YOLO không được dùng vì client đã detect face bằng ML Kit trước khi gửi ảnh
        self.mtcnn = None
        self.facenet_model = None
        
        self.load_models()
        self.load_face_database()
    
    def load_models(self):
        """Load các model MTCNN và FaceNet"""
        try:
            # Note: YOLO không cần vì client (React Native) đã detect face bằng ML Kit
            # MTCNN chỉ dùng để align face tốt hơn và validate face trong ảnh
            
            logger.info("Đang tải MTCNN model...")
            # MTCNN cho face detection và alignment
            self.mtcnn = MTCNN(
                image_size=160,
                margin=0,
                min_face_size=20,
                thresholds=[0.6, 0.7, 0.7],
                factor=0.709,
                post_process=True,
                device='cpu' if not torch.cuda.is_available() else 'cuda'
            )
            
            logger.info("Đang tải FaceNet model...")
            # FaceNet cho face recognition
            self.facenet_model = InceptionResnetV1(pretrained='vggface2').eval()
            
            device = 'cuda' if torch.cuda.is_available() else 'cpu'
            self.facenet_model = self.facenet_model.to(device)
            
            logger.info(f"Models loaded successfully on {device}")
            
        except Exception as e:
            logger.error(f"Lỗi khi load models: {e}")
            raise
    
    def load_face_database(self):
        """Load database khuôn mặt đã đăng ký"""
        if self.face_db_path.exists():
            try:
                with open(self.face_db_path, 'rb') as f:
                    self.face_database = pickle.load(f)
                logger.info(f"Loaded {len(self.face_database)} faces from database")
            except Exception as e:
                logger.error(f"Lỗi khi load face database: {e}")
                self.face_database = {}
        else:
            self.face_database = {}
            logger.info("Khởi tạo face database mới")
    
    def save_face_database(self):
        """Lưu database khuôn mặt"""
        try:
            with open(self.face_db_path, 'wb') as f:
                pickle.dump(self.face_database, f)
            logger.info("Face database saved successfully")
        except Exception as e:
            logger.error(f"Lỗi khi save face database: {e}")
    
    # Removed detect_faces_yolo - không cần vì client đã detect face bằng ML Kit
    
    def detect_and_align_faces_mtcnn(self, image: np.ndarray) -> List[Tuple[np.ndarray, float]]:
        """
        Phát hiện và align khuôn mặt bằng MTCNN
        
        Args:
            image: Ảnh input
            
        Returns:
            List of (aligned_face, confidence)
        """
        try:
            # Convert BGR to RGB
            rgb_image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
            
            # Detect faces
            boxes, probs, landmarks = self.mtcnn.detect(rgb_image, landmarks=True)
            
            if boxes is None:
                return []
            
            aligned_faces = []
            for i, (box, prob) in enumerate(zip(boxes, probs)):
                if prob > self.face_detection_confidence:
                    # Extract face using MTCNN
                    x1, y1, x2, y2 = box.astype(int)
                    
                    # Ensure coordinates are within image bounds
                    h, w = image.shape[:2]
                    x1 = max(0, x1)
                    y1 = max(0, y1)
                    x2 = min(w, x2)
                    y2 = min(h, y2)
                    
                    face_img = rgb_image[y1:y2, x1:x2]
                    if face_img.size > 0:
                        aligned_faces.append((face_img, float(prob)))
            
            return aligned_faces
            
        except Exception as e:
            logger.error(f"Lỗi trong MTCNN face detection: {e}")
            return []
    
    def extract_face_embedding(self, face_image: np.ndarray) -> np.ndarray:
        """
        Trích xuất face embedding bằng FaceNet
        
        Args:
            face_image: Ảnh khuôn mặt đã được align
            
        Returns:
            Face embedding vector
        """
        try:
            # Resize to 160x160 (required by FaceNet)
            face_img = cv2.resize(face_image, (160, 160))
            
            # Normalize to [0, 1]
            face_img = face_img.astype(np.float32) / 255.0
            
            # Convert to tensor and add batch dimension
            face_tensor = torch.from_numpy(face_img).permute(2, 0, 1).unsqueeze(0)
            
            device = next(self.facenet_model.parameters()).device
            face_tensor = face_tensor.to(device)
            
            # Extract embedding
            with torch.no_grad():
                embedding = self.facenet_model(face_tensor)
                embedding = F.normalize(embedding, p=2, dim=1)
            
            return embedding.cpu().numpy().flatten()
            
        except Exception as e:
            logger.error(f"Lỗi khi extract face embedding: {e}")
            return None
    
    def calculate_face_similarity(self, embedding1: np.ndarray, embedding2: np.ndarray) -> float:
        """
        Tính độ tương đồng giữa 2 face embeddings
        
        Args:
            embedding1: Face embedding 1
            embedding2: Face embedding 2
            
        Returns:
            Similarity score (0-1, higher is more similar)
        """
        try:
            # Cosine similarity
            similarity = np.dot(embedding1, embedding2) / (np.linalg.norm(embedding1) * np.linalg.norm(embedding2))
            return float(similarity)
        except Exception as e:
            logger.error(f"Lỗi khi tính similarity: {e}")
            return 0.0
    
    def register_face(self, image_path: str, employee_id: str) -> Dict:
        """
        Đăng ký khuôn mặt mới
        
        Args:
            image_path: Đường dẫn ảnh
            employee_id: ID nhân viên
            
        Returns:
            Kết quả đăng ký
        """
        try:
            logger.info(f"Đăng ký khuôn mặt cho nhân viên: {employee_id}")
            
            # Load image
            image = cv2.imread(image_path)
            if image is None:
                return {
                    "success": False,
                    "message": "Không thể đọc ảnh"
                }
            
            # Detect faces using MTCNN (better for registration)
            aligned_faces = self.detect_and_align_faces_mtcnn(image)
            
            if not aligned_faces:
                return {
                    "success": False,
                    "message": "Không phát hiện được khuôn mặt trong ảnh"
                }
            
            # Lấy khuôn mặt có confidence cao nhất
            best_face, best_confidence = max(aligned_faces, key=lambda x: x[1])
            
            if best_confidence < self.face_detection_confidence:
                return {
                    "success": False,
                    "message": f"Độ tin cậy phát hiện khuôn mặt quá thấp: {best_confidence:.2f}"
                }
            
            # Extract embedding
            embedding = self.extract_face_embedding(best_face)
            
            if embedding is None:
                return {
                    "success": False,
                    "message": "Không thể trích xuất đặc trưng khuôn mặt"
                }
            
            # Save to database
            self.face_database[employee_id] = {
                "embedding": embedding,
                "confidence": best_confidence,
                "registered_date": str(np.datetime64('now'))
            }
            
            self.save_face_database()
            
            logger.info(f"Đăng ký thành công cho nhân viên: {employee_id}")
            return {
                "success": True,
                "message": "Đăng ký khuôn mặt thành công",
                "confidence": best_confidence,
                "embedding": embedding.tolist()
            }
            
        except Exception as e:
            logger.error(f"Lỗi khi đăng ký khuôn mặt: {e}")
            return {
                "success": False,
                "message": f"Lỗi: {str(e)}"
            }
    
    def recognize_face(self, image_path: str) -> Dict:
        """
        Nhận dạng khuôn mặt
        
        Args:
            image_path: Đường dẫn ảnh
            
        Returns:
            Kết quả nhận dạng
        """
        try:
            logger.info("Bắt đầu nhận dạng khuôn mặt")
            
            # Load image
            image = cv2.imread(image_path)
            if image is None:
                return {
                    "success": False,
                    "message": "Không thể đọc ảnh"
                }
            
            # Detect faces using MTCNN
            aligned_faces = self.detect_and_align_faces_mtcnn(image)
            
            if not aligned_faces:
                return {
                    "success": False,
                    "message": "Không phát hiện được khuôn mặt trong ảnh"
                }
            
            # Lấy khuôn mặt có confidence cao nhất
            best_face, best_confidence = max(aligned_faces, key=lambda x: x[1])
            
            if best_confidence < self.face_detection_confidence:
                return {
                    "success": False,
                    "message": f"Độ tin cậy phát hiện khuôn mặt quá thấp: {best_confidence:.2f}"
                }
            
            # Extract embedding
            query_embedding = self.extract_face_embedding(best_face)
            
            if query_embedding is None:
                return {
                    "success": False,
                    "message": "Không thể trích xuất đặc trưng khuôn mặt"
                }
            
            # Compare with database
            best_match = None
            best_similarity = 0.0
            
            for employee_id, face_data in self.face_database.items():
                similarity = self.calculate_face_similarity(query_embedding, face_data["embedding"])
                
                if similarity > best_similarity:
                    best_similarity = similarity
                    best_match = employee_id
            
            # Check if similarity is above threshold
            if best_similarity >= self.face_recognition_threshold:
                logger.info(f"Nhận dạng thành công: {best_match} (similarity: {best_similarity:.3f})")
                return {
                    "success": True,
                    "message": "Nhận dạng thành công",
                    "employee_id": best_match,
                    "confidence": best_similarity
                }
            else:
                logger.info(f"Nhận dạng thất bại (similarity: {best_similarity:.3f})")
                return {
                    "success": False,
                    "message": "Không tìm thấy khuôn mặt khớp trong database",
                    "confidence": best_similarity
                }
                
        except Exception as e:
            logger.error(f"Lỗi khi nhận dạng khuôn mặt: {e}")
            return {
                "success": False,
                "message": f"Lỗi: {str(e)}"
            }

    def detect_face(self, image_path: str) -> Dict:
        """
        Chỉ phát hiện khuôn mặt (không nhận dạng)
        
        Args:
            image_path: Đường dẫn ảnh
            
        Returns:
            Kết quả phát hiện
        """
        try:
            logger.info("Bắt đầu phát hiện khuôn mặt")
            
            # Load image
            image = cv2.imread(image_path)
            if image is None:
                logger.error(f"Không thể đọc ảnh từ: {image_path}")
                return {
                    "success": False,
                    "message": "Không thể đọc ảnh"
                }
            
            # Kiểm tra MTCNN đã được khởi tạo chưa
            if self.mtcnn is None:
                logger.error("MTCNN chưa được khởi tạo")
                return {
                    "success": False,
                    "message": "MTCNN chưa được khởi tạo"
                }
            
            # Detect faces using MTCNN (client đã detect bằng ML Kit, nhưng MTCNN align tốt hơn)
            aligned_faces = self.detect_and_align_faces_mtcnn(image)
            
            if not aligned_faces:
                logger.info("Không phát hiện được khuôn mặt trong ảnh")
                return {
                    "success": False,
                    "message": "Không phát hiện được khuôn mặt trong ảnh"
                }
            
            # Lấy khuôn mặt có confidence cao nhất
            best_face, best_confidence = max(aligned_faces, key=lambda x: x[1])
            
            if best_confidence < self.face_detection_confidence:
                logger.info(f"Độ tin cậy phát hiện khuôn mặt quá thấp: {best_confidence:.2f}")
                return {
                    "success": False,
                    "message": f"Độ tin cậy phát hiện khuôn mặt quá thấp: {best_confidence:.2f}"
                }
            
            logger.info(f"Phát hiện khuôn mặt thành công (confidence: {best_confidence:.3f})")
            return {
                "success": True,
                "message": "Phát hiện khuôn mặt thành công",
                "confidence": best_confidence
            }
                
        except Exception as e:
            logger.error(f"Lỗi khi phát hiện khuôn mặt: {e}")
            import traceback
            logger.error(f"Traceback: {traceback.format_exc()}")
            return {
                "success": False,
                "message": f"Lỗi: {str(e)}"
            }

def main():
    """Main function để xử lý command line arguments"""
    if len(sys.argv) < 3:
        print("Usage: python face_recognition.py <command> <image_path> [employee_id]")
        print("Commands:")
        print("  register <image_path> <employee_id> - Đăng ký khuôn mặt")
        print("  recognize <image_path> - Nhận dạng khuôn mặt")
        print("  detect <image_path> - Phát hiện khuôn mặt")
        print("  extract_embedding <image_path> - Extract FaceNet embedding từ ảnh")
        sys.exit(1)
    
    command = sys.argv[1]
    image_path = sys.argv[2]
    
    # Initialize face recognition system
    face_system = FaceRecognitionSystem()
    
    if command == "register":
        if len(sys.argv) < 4:
            print("Error: employee_id required for register command")
            sys.exit(1)
        
        employee_id = sys.argv[3]
        result = face_system.register_face(image_path, employee_id)
        
    elif command == "recognize":
        result = face_system.recognize_face(image_path)
        
    elif command == "detect":
        result = face_system.detect_face(image_path)
        
    elif command == "extract_embedding":
        # Extract embedding từ ảnh (không cần employee_id)
        image = cv2.imread(image_path)
        if image is None:
            result = {
                "success": False,
                "message": "Không thể đọc ảnh"
            }
        else:
            # Detect và align face
            aligned_faces = face_system.detect_and_align_faces_mtcnn(image)
            if not aligned_faces:
                result = {
                    "success": False,
                    "message": "Không phát hiện được khuôn mặt"
                }
            else:
                # Lấy khuôn mặt tốt nhất
                best_face, best_confidence = max(aligned_faces, key=lambda x: x[1])
                # Extract embedding
                embedding = face_system.extract_face_embedding(best_face)
                if embedding is not None:
                    result = {
                        "success": True,
                        "message": "Extract embedding thành công",
                        "embedding": embedding.tolist(),  # Convert numpy array to list for JSON
                        "confidence": float(best_confidence)
                    }
                else:
                    result = {
                        "success": False,
                        "message": "Không thể extract embedding"
                    }
        
    else:
        result = {
            "success": False,
            "message": f"Unknown command: {command}"
        }
    
    # Output result as JSON
    print(json.dumps(result, indent=2))

if __name__ == "__main__":
    main()

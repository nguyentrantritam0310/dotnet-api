using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IFaceRecognitionService
    {
        /// <summary>
        /// Đăng ký khuôn mặt cho nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="imageBytes">Dữ liệu ảnh dưới dạng byte array</param>
        /// <returns>Kết quả đăng ký</returns>
        Task<FaceRegistrationResult> RegisterFaceAsync(string employeeId, byte[] imageBytes);

        /// <summary>
        /// Nhận dạng khuôn mặt từ ảnh
        /// </summary>
        /// <param name="imageBytes">Dữ liệu ảnh dưới dạng byte array</param>
        /// <returns>Kết quả nhận dạng</returns>
        Task<FaceRecognitionResult> RecognizeFaceAsync(byte[] imageBytes);

        /// <summary>
        /// Xóa khuôn mặt đã đăng ký
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>True nếu xóa thành công</returns>
        Task<bool> UnregisterFaceAsync(string employeeId);

        /// <summary>
        /// Lấy danh sách nhân viên đã đăng ký khuôn mặt
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        Task<List<RegisteredEmployee>> GetRegisteredEmployeesAsync();

        /// <summary>
        /// Kiểm tra xem nhân viên đã đăng ký khuôn mặt chưa
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>True nếu đã đăng ký</returns>
        Task<bool> IsEmployeeRegisteredAsync(string employeeId);

        /// <summary>
        /// Phát hiện khuôn mặt trong ảnh (chỉ kiểm tra có khuôn mặt hay không)
        /// </summary>
        /// <param name="imageBytes">Dữ liệu ảnh dưới dạng byte array</param>
        /// <returns>True nếu phát hiện khuôn mặt</returns>
        Task<bool> DetectFaceAsync(byte[] imageBytes);
    }
}

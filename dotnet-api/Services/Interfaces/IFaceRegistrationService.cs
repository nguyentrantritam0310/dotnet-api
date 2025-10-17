using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IFaceRegistrationService
    {
        /// <summary>
        /// Đăng ký khuôn mặt cho nhân viên
        /// </summary>
        /// <param name="dto">Thông tin đăng ký khuôn mặt</param>
        /// <returns>Kết quả đăng ký</returns>
        Task<FaceRegistrationDTO> CreateFaceRegistrationAsync(FaceRegistrationDTO dto);

        /// <summary>
        /// Lấy thông tin đăng ký khuôn mặt theo Employee ID
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Thông tin đăng ký khuôn mặt</returns>
        Task<FaceRegistrationDTO?> GetFaceRegistrationByEmployeeIdAsync(string employeeId);

        /// <summary>
        /// Lấy danh sách tất cả nhân viên đã đăng ký khuôn mặt
        /// </summary>
        /// <returns>Danh sách nhân viên đã đăng ký</returns>
        Task<List<RegisteredEmployee>> GetAllRegisteredEmployeesAsync();

        /// <summary>
        /// Cập nhật thông tin đăng ký khuôn mặt
        /// </summary>
        /// <param name="id">ID đăng ký</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <returns>Thông tin đã cập nhật</returns>
        Task<FaceRegistrationDTO> UpdateFaceRegistrationAsync(int id, FaceRegistrationDTO dto);

        /// <summary>
        /// Xóa (soft delete) đăng ký khuôn mặt
        /// </summary>
        /// <param name="id">ID đăng ký</param>
        /// <returns>True nếu xóa thành công</returns>
        Task<bool> DeleteFaceRegistrationAsync(int id);

        /// <summary>
        /// Kiểm tra nhân viên đã đăng ký khuôn mặt chưa
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>True nếu đã đăng ký</returns>
        Task<bool> IsEmployeeRegisteredAsync(string employeeId);
    }
}


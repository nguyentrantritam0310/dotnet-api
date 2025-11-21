using dotnet_api.DTOs;
using dotnet_api.Data.Entities;

namespace dotnet_api.Services.Interfaces
{
    public interface IAttendanceService
    {
        /// <summary>
        /// Chấm công vào (check-in)
        /// </summary>
        /// <param name="request">Thông tin chấm công</param>
        /// <returns>Kết quả chấm công</returns>
        Task<AttendanceCheckInResult> CheckInAsync(AttendanceCheckInRequest request);

        /// <summary>
        /// Chấm công ra (check-out)
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="checkOutDateTime">Thời gian check-out</param>
        /// <param name="imageBase64">Ảnh check-out</param>
        /// <returns>Kết quả check-out</returns>
        Task<bool> CheckOutAsync(string employeeId, DateTime checkOutDateTime, string? imageBase64 = null);

        /// <summary>
        /// Lấy lịch sử chấm công của nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>Danh sách chấm công</returns>
        Task<List<Attendance>> GetEmployeeAttendanceAsync(string employeeId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Lấy thông tin chấm công hiện tại của nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Thông tin chấm công hôm nay</returns>
        Task<Attendance?> GetTodayAttendanceAsync(string employeeId);

        /// <summary>
        /// Kiểm tra nhân viên đã chấm công vào chưa
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>True nếu đã chấm công vào</returns>
        Task<bool> HasCheckedInTodayAsync(string employeeId);

        /// <summary>
        /// Lấy tất cả chấm công theo ngày
        /// </summary>
        /// <param name="date">Ngày cần lấy</param>
        /// <returns>Danh sách chấm công</returns>
        Task<List<Attendance>> GetAttendanceByDateAsync(DateTime date);

        /// <summary>
        /// Lấy danh sách WorkShiftID đã chấm công hôm nay (để ẩn khỏi dropdown)
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Danh sách WorkShiftID đã check-in/checkout hôm nay</returns>
        Task<List<int>> GetTodayCheckedShiftsAsync(string employeeId);
    }
}
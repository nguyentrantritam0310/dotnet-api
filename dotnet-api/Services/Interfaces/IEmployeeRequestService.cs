using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IEmployeeRequestService
    {
        Task<EmployeeRequestDTO> CreateEmployeeRequestAsync(EmployeeRequestDTO employeeRequestDTO);
        Task<EmployeeRequestDTO> GetEmployeeRequestByIdAsync(string voucherNo);
        Task<IEnumerable<EmployeeRequestDTO>> GetAllEmployeeRequestsAsync();
        Task<EmployeeRequestDTO> UpdateEmployeeRequestAsync(EmployeeRequestDTO employeeRequestDTO);
        Task<EmployeeRequestDTO> DeleteEmployeeRequestAsync(string voucherno);

        // Leave Request specific methods
        Task<LeaveRequestDTO> CreateLeaveRequestAsync(LeaveRequestDTOPOST leaveRequestDTO);
        Task<LeaveRequestDTO> GetLeaveRequestByIdAsync(string voucherCode);
        Task<IEnumerable<LeaveRequestDTO>> GetAllLeaveRequestsAsync();
        Task<LeaveRequestDTO> UpdateLeaveRequestAsync(LeaveRequestDTOPUT leaveRequestDTO);
        Task<LeaveRequestDTO> DeleteLeaveRequestAsync(string voucherCode);

        // Overtime Request specific methods
        Task<OvertimeRequestDTO> CreateOvertimeRequestAsync(OvertimeRequestDTOPOST overtimeRequestDTO);
        Task<OvertimeRequestDTO> GetOvertimeRequestByIdAsync(string voucherCode);
        Task<IEnumerable<OvertimeRequestDTO>> GetAllOvertimeRequestsAsync();
        Task<OvertimeRequestDTO> UpdateOvertimeRequestAsync(OvertimeRequestDTOPUT overtimeRequestDTO);
        Task<OvertimeRequestDTO> DeleteOvertimeRequestAsync(string voucherCode);

        // Approval workflow methods for Leave Requests
        Task<LeaveRequestDTO> SubmitLeaveRequestForApprovalAsync(string voucherCode, string submitterId, string? notes);
        Task<LeaveRequestDTO> ApproveLeaveRequestAsync(string voucherCode, string approverId, string? notes);
        Task<LeaveRequestDTO> RejectLeaveRequestAsync(string voucherCode, string approverId, string? notes);
        Task<LeaveRequestDTO> ReturnLeaveRequestAsync(string voucherCode, string approverId, string? notes);

        // Approval workflow methods for Overtime Requests
        Task<OvertimeRequestDTO> SubmitOvertimeRequestForApprovalAsync(string voucherCode, string submitterId, string? notes);
        Task<OvertimeRequestDTO> ApproveOvertimeRequestAsync(string voucherCode, string approverId, string? notes);
        Task<OvertimeRequestDTO> RejectOvertimeRequestAsync(string voucherCode, string approverId, string? notes);
        Task<OvertimeRequestDTO> ReturnOvertimeRequestAsync(string voucherCode, string approverId, string? notes);
    }
}

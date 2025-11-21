using dotnet_api.Data.Enums;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IApprovalHistoryService
    {
        Task<ApprovalHistoryDTO> CreateHistoryAsync(
            string requestType,
            string requestID,
            string approverID,
            string approverName,
            string action,
            ApproveStatusEnum? oldStatus,
            ApproveStatusEnum newStatus,
            string? notes = null);

        Task<IEnumerable<ApprovalHistoryDTO>> GetHistoryAsync(string requestType, string requestID);
        
        Task<bool> DeleteLatestHistoryAsync(string requestType, string requestID);
    }
}


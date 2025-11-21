using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ApprovalHistoryService : IApprovalHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ApprovalHistoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApprovalHistoryDTO> CreateHistoryAsync(
            string requestType,
            string requestID,
            string approverID,
            string approverName,
            string action,
            ApproveStatusEnum? oldStatus,
            ApproveStatusEnum newStatus,
            string? notes = null)
        {
            var history = new ApprovalHistory
            {
                RequestType = requestType,
                RequestID = requestID,
                ApproverID = approverID,
                ApproverName = approverName,
                Action = action,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Notes = notes,
                CreatedAt = DateTime.Now
            };

            _context.ApprovalHistories.Add(history);
            await _context.SaveChangesAsync();

            return _mapper.Map<ApprovalHistoryDTO>(history);
        }

        public async Task<IEnumerable<ApprovalHistoryDTO>> GetHistoryAsync(string requestType, string requestID)
        {
            var histories = await _context.ApprovalHistories
                .Where(h => h.RequestType == requestType && h.RequestID == requestID)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ApprovalHistoryDTO>>(histories);
        }

        public async Task<bool> DeleteLatestHistoryAsync(string requestType, string requestID)
        {
            var latestHistory = await _context.ApprovalHistories
                .Where(h => h.RequestType == requestType && h.RequestID == requestID)
                .OrderByDescending(h => h.CreatedAt)
                .FirstOrDefaultAsync();

            if (latestHistory == null) return false;

            // Restore old status based on request type
            if (requestType == "LeaveRequest" || requestType == "OvertimeRequest")
            {
                var entity = await _context.EmployeeRequests
                    .FirstOrDefaultAsync(e => e.VoucherCode == requestID);
                if (entity != null)
                {
                    entity.ApproveStatus = latestHistory.OldStatus ?? ApproveStatusEnum.Created;
                }
            }
            else if (requestType == "PayrollAdjustment")
            {
                var entity = await _context.PayrollAdjustments
                    .FirstOrDefaultAsync(e => e.voucherNo == requestID);
                if (entity != null)
                {
                    entity.ApproveStatus = latestHistory.OldStatus ?? ApproveStatusEnum.Created;
                }
            }
            else if (requestType == "Contract")
            {
                if (int.TryParse(requestID, out int contractId))
                {
                    var entity = await _context.Contracts
                        .FirstOrDefaultAsync(e => e.ID == contractId);
                    if (entity != null)
                    {
                        entity.ApproveStatus = latestHistory.OldStatus ?? ApproveStatusEnum.Created;
                    }
                }
            }

            _context.ApprovalHistories.Remove(latestHistory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}


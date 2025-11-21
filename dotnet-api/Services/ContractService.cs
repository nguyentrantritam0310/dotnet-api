using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IApprovalHistoryService _approvalHistoryService;

        public ContractService(ApplicationDbContext context, IMapper mapper, IApprovalHistoryService approvalHistoryService)
        {
            _context = context;
            _mapper = mapper;
            _approvalHistoryService = approvalHistoryService;
        }

        public async Task<IEnumerable<ContractDTO>> GetAllContractsAsync()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Employee)
                .Include(c => c.ContractType)
                .Include(c => c.ContractAllowances)
                    .ThenInclude(ca => ca.Allowance)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ContractDTO>>(contracts);
        }

        public async Task<ContractDTO> GetContractByIdAsync(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Employee)
                .Include(c => c.ContractType)
                .Include(c => c.ContractAllowances)
                    .ThenInclude(ca => ca.Allowance)
                .FirstOrDefaultAsync(c => c.ID == id);

            return contract == null ? null : _mapper.Map<ContractDTO>(contract);
        }

        public async Task<ContractDTO> CreateContractAsync(ContractDTOPOST contractDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Check if employee exists
                var employee = await _context.ApplicationUsers.FindAsync(contractDTO.EmployeeID);
                if (employee == null)
                {
                    throw new Exception("Không tìm thấy nhân viên với ID đã cho");
                }

                // Check if contract type exists
                var contractType = await _context.ContractTypes.FindAsync(contractDTO.ContractTypeID);
                if (contractType == null)
                {
                    throw new Exception("Không tìm thấy loại hợp đồng với ID đã cho");
                }

                // Validate probation contract duration (should be 2 months max)
                if (contractType.ContractTypeName.ToLower().Contains("thử việc"))
                {
                    var startDate = contractDTO.StartDate;
                    var endDate = contractDTO.EndDate;
                    var durationInMonths = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
                    
                    if (durationInMonths > 2)
                    {
                        throw new Exception("Hợp đồng thử việc không được vượt quá 2 tháng");
                    }
                }

                // Check if contract number already exists
                var existingContract = await _context.Contracts
                    .FirstOrDefaultAsync(c => c.ContractNumber == contractDTO.ContractNumber);
                if (existingContract != null)
                {
                    throw new Exception("Số hợp đồng đã tồn tại");
                }

                // Validate allowances
                foreach (var allowance in contractDTO.Allowances)
                {
                    var allowanceEntity = await _context.Allowances.FindAsync(allowance.AllowanceID);
                    if (allowanceEntity == null)
                    {
                        throw new Exception($"Không tìm thấy phụ cấp với ID: {allowance.AllowanceID}");
                    }
                }

                // Create contract manually to avoid AutoMapper tracking issues
                var contract = new Contract
                {
                    ContractNumber = contractDTO.ContractNumber,
                    ContractTypeID = contractDTO.ContractTypeID,
                    EmployeeID = contractDTO.EmployeeID,
                    StartDate = contractDTO.StartDate,
                    EndDate = contractDTO.EndDate,
                    ContractSalary = contractDTO.ContractSalary,
                    InsuranceSalary = contractDTO.InsuranceSalary,
                    ApproveStatus = contractDTO.ApproveStatusEnum
                };

                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();

                // Add allowances
                foreach (var allowanceDTO in contractDTO.Allowances)
                {
                    var contractAllowance = new Contract_Allowance
                    {
                        ContractID = contract.ID,
                        AllowanceID = allowanceDTO.AllowanceID,
                        Value = allowanceDTO.Value
                    };
                    _context.Contract_Allowances.Add(contractAllowance);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetContractByIdAsync(contract.ID);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ContractDTO> UpdateContractAsync(ContractDTOPUT contractDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var contract = await _context.Contracts
                .Include(c => c.ContractAllowances)
                .FirstOrDefaultAsync(c => c.ID == contractDTO.ID);

            if (contract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng với ID đã cho");
            }

            // Check if employee exists
            var employee = await _context.ApplicationUsers.FindAsync(contractDTO.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            // Check if contract type exists
            var contractType = await _context.ContractTypes.FindAsync(contractDTO.ContractTypeID);
            if (contractType == null)
            {
                throw new Exception("Không tìm thấy loại hợp đồng với ID đã cho");
            }

            // Validate probation contract duration (should be 2 months max)
            if (contractType.ContractTypeName.ToLower().Contains("thử việc"))
            {
                var startDate = contractDTO.StartDate;
                var endDate = contractDTO.EndDate;
                var durationInMonths = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
                
                if (durationInMonths > 2)
                {
                    throw new Exception("Hợp đồng thử việc không được vượt quá 2 tháng");
                }
            }

            // Check if contract number already exists (excluding current contract)
            var existingContract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractNumber == contractDTO.ContractNumber && c.ID != contractDTO.ID);
            if (existingContract != null)
            {
                throw new Exception("Số hợp đồng đã tồn tại");
            }

            // Validate allowances
            foreach (var allowance in contractDTO.Allowances)
            {
                var allowanceEntity = await _context.Allowances.FindAsync(allowance.AllowanceID);
                if (allowanceEntity == null)
                {
                    throw new Exception($"Không tìm thấy phụ cấp với ID: {allowance.AllowanceID}");
                }
            }

            // Update contract properties
            contract.ContractNumber = contractDTO.ContractNumber;
            contract.ContractTypeID = contractDTO.ContractTypeID;
            contract.EmployeeID = contractDTO.EmployeeID;
            contract.StartDate = contractDTO.StartDate;
            contract.EndDate = contractDTO.EndDate;
            contract.ContractSalary = contractDTO.ContractSalary;
            contract.InsuranceSalary = contractDTO.InsuranceSalary;
            contract.ApproveStatus = contractDTO.ApproveStatusEnum;

            // Remove existing allowances
            _context.Contract_Allowances.RemoveRange(contract.ContractAllowances);
            await _context.SaveChangesAsync();

            // Clear change tracker to avoid conflicts
            _context.ChangeTracker.Clear();

            // Add new allowances
            foreach (var allowanceDTO in contractDTO.Allowances)
            {
                var contractAllowance = new Contract_Allowance
                {
                    ContractID = contract.ID,
                    AllowanceID = allowanceDTO.AllowanceID,
                    Value = allowanceDTO.Value
                };
                _context.Contract_Allowances.Add(contractAllowance);
            }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetContractByIdAsync(contract.ID);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteContractAsync(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.ContractAllowances)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (contract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng với ID đã cho");
            }

            // Remove allowances first
            _context.Contract_Allowances.RemoveRange(contract.ContractAllowances);
            
            // Remove contract
            _context.Contracts.Remove(contract);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ContractTypeDTO>> GetContractTypesAsync()
        {
            var contractTypes = await _context.ContractTypes.ToListAsync();
            return _mapper.Map<IEnumerable<ContractTypeDTO>>(contractTypes);
        }


        public async Task<IEnumerable<AllowanceDTO>> GetAllowancesAsync()
        {
            var allowances = await _context.Allowances.ToListAsync();
            return _mapper.Map<IEnumerable<AllowanceDTO>>(allowances);
        }

        // Approval workflow methods
        public async Task<ContractDTO> SubmitContractForApprovalAsync(int contractId, string submitterId, string? notes)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null) throw new Exception("Không tìm thấy hợp đồng");
            if (contract.ApproveStatus != ApproveStatusEnum.Created) throw new Exception("Hợp đồng đã được gửi duyệt hoặc đã xử lý");

            var submitter = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == submitterId);
            if (submitter == null) throw new Exception("Không tìm thấy người gửi");

            var oldStatus = contract.ApproveStatus;
            contract.ApproveStatus = ApproveStatusEnum.Pending;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "Contract",
                contractId.ToString(),
                submitterId,
                $"{submitter.FirstName} {submitter.LastName}",
                "Submit",
                oldStatus,
                ApproveStatusEnum.Pending,
                notes
            );

            return await GetContractByIdAsync(contractId);
        }

        public async Task<ContractDTO> ApproveContractAsync(int contractId, string approverId, string? notes)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null) throw new Exception("Không tìm thấy hợp đồng");
            if (contract.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Hợp đồng không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "Contract" && h.RequestID == contractId.ToString() && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể duyệt đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            // Check if approver has permission based on workflow
            // Contract/Adjustment: HREmployee (6) → HRManager (5) → Director (3)
            var canApprove = await CanApproveContractAsync(contract, approver);
            if (!canApprove) throw new Exception("Bạn không có quyền duyệt hợp đồng này ở giai đoạn hiện tại");

            var oldStatus = contract.ApproveStatus;
            var nextStatus = await GetNextApprovalStatusForContractAsync(contract, approver);
            
            contract.ApproveStatus = nextStatus;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "Contract",
                contractId.ToString(),
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Approve",
                oldStatus,
                nextStatus,
                notes
            );

            return await GetContractByIdAsync(contractId);
        }

        public async Task<ContractDTO> RejectContractAsync(int contractId, string approverId, string? notes)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null) throw new Exception("Không tìm thấy hợp đồng");
            if (contract.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Hợp đồng không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "Contract" && h.RequestID == contractId.ToString() && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể từ chối đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = contract.ApproveStatus;
            contract.ApproveStatus = ApproveStatusEnum.Rejected;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "Contract",
                contractId.ToString(),
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Reject",
                oldStatus,
                ApproveStatusEnum.Rejected,
                notes
            );

            return await GetContractByIdAsync(contractId);
        }

        public async Task<ContractDTO> ReturnContractAsync(int contractId, string approverId, string? notes)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null) throw new Exception("Không tìm thấy hợp đồng");
            if (contract.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Hợp đồng không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "Contract" && h.RequestID == contractId.ToString() && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể trả lại đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = contract.ApproveStatus;
            contract.ApproveStatus = ApproveStatusEnum.Created; // Return về "Tạo mới"

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "Contract",
                contractId.ToString(),
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Return",
                oldStatus,
                ApproveStatusEnum.Created,
                notes
            );

            return await GetContractByIdAsync(contractId);
        }

        // Helper methods for approval workflow
        private async Task<bool> CanApproveContractAsync(Contract contract, ApplicationUser approver)
        {
            var approverRoleId = approver.RoleID;

            // Check approval history to determine current level
            var history = await _context.ApprovalHistories
                .Where(h => h.RequestType == "Contract" && h.RequestID == contract.ID.ToString())
                .OrderByDescending(h => h.CreatedAt)
                .FirstOrDefaultAsync();

            // If no history, check who created it (must be HREmployee or HRManager)
            if (history == null)
            {
                // First approval: HREmployee (6) → HRManager (5), or HRManager (5) → Director (3)
                // We need to check who created it, but for simplicity, allow HRManager to approve
                return approverRoleId == 5; // HRManager can approve first level
            }

            // If last approver was HREmployee (6), next is HRManager (5)
            if (history != null)
            {
                var lastApprover = await _context.ApplicationUsers
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == history.ApproverID);
                
                if (lastApprover?.RoleID == 6 && approverRoleId == 5) return true; // HREmployee → HRManager
                if (lastApprover?.RoleID == 5 && approverRoleId == 3) return true; // HRManager → Director
            }

            return false;
        }

        private async Task<ApproveStatusEnum> GetNextApprovalStatusForContractAsync(Contract contract, ApplicationUser approver)
        {
            var approverRoleId = approver.RoleID;

            // If Director approves, it's final approval
            if (approverRoleId == 3) return ApproveStatusEnum.Approved;

            // Otherwise, still pending for next level
            return ApproveStatusEnum.Pending;
        }
    }
}
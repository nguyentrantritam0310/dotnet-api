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

        public ContractService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        // Approve/Reject Contract methods
        public async Task<ContractDTO> ApproveContractAsync(int contractId)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng với ID đã cho");
            }

            contract.ApproveStatus = ApproveStatusEnum.Approved;
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await GetContractByIdAsync(contractId);
        }

        public async Task<ContractDTO> RejectContractAsync(int contractId)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng với ID đã cho");
            }

            contract.ApproveStatus = ApproveStatusEnum.Rejected;
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await GetContractByIdAsync(contractId);
        }

        public async Task<ContractDTO> PendingContractAsync(int contractId)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng với ID đã cho");
            }

            contract.ApproveStatus = ApproveStatusEnum.Pending;
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await GetContractByIdAsync(contractId);
        }
    }
}
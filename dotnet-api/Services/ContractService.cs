using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
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
                .Include(c => c.ContractFormEntity)
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
                .Include(c => c.ContractFormEntity)
                .Include(c => c.ContractAllowances)
                    .ThenInclude(ca => ca.Allowance)
                .FirstOrDefaultAsync(c => c.ID == id);

            return contract == null ? null : _mapper.Map<ContractDTO>(contract);
        }

        public async Task<ContractDTO> CreateContractAsync(ContractDTOPOST contractDTO)
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

            // Check if contract form exists
            var contractForm = await _context.ContractForms.FindAsync(contractDTO.ContractFormID);
            if (contractForm == null)
            {
                throw new Exception("Không tìm thấy hình thức hợp đồng với ID đã cho");
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

            // Create contract
            var contract = _mapper.Map<Contract>(contractDTO);
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

            return await GetContractByIdAsync(contract.ID);
        }

        public async Task<ContractDTO> UpdateContractAsync(ContractDTOPUT contractDTO)
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

            // Check if contract form exists
            var contractForm = await _context.ContractForms.FindAsync(contractDTO.ContractFormID);
            if (contractForm == null)
            {
                throw new Exception("Không tìm thấy hình thức hợp đồng với ID đã cho");
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
            contract.ContractFormID = contractDTO.ContractFormID;
            contract.EmployeeID = contractDTO.EmployeeID;
            contract.Status = contractDTO.Status;
            contract.StartDate = contractDTO.StartDate;
            contract.EndDate = contractDTO.EndDate;
            contract.ContractSalary = contractDTO.ContractSalary;
            contract.InsuranceSalary = contractDTO.InsuranceSalary;
            contract.ApproveStatus = contractDTO.ApproveStatus;

            // Remove existing allowances
            _context.Contract_Allowances.RemoveRange(contract.ContractAllowances);

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

            return await GetContractByIdAsync(contract.ID);
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

        public async Task<IEnumerable<ContractFormDTO>> GetContractFormsAsync()
        {
            var contractForms = await _context.ContractForms.ToListAsync();
            return _mapper.Map<IEnumerable<ContractFormDTO>>(contractForms);
        }

        public async Task<IEnumerable<AllowanceDTO>> GetAllowancesAsync()
        {
            var allowances = await _context.Allowances.ToListAsync();
            return _mapper.Map<IEnumerable<AllowanceDTO>>(allowances);
        }
    }
}
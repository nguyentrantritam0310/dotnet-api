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
    public class FamilyRelationService : IFamilyRelationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FamilyRelationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FamilyRelationDTO>> GetAllFamilyRelationsAsync()
        {
            var familyRelations = await _context.Employee_FamilyRelations
                .Include(efr => efr.FamilyRelation)
                .Include(efr => efr.Employee)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FamilyRelationDTO>>(familyRelations);
        }

        public async Task<IEnumerable<FamilyRelationDTO>> GetFamilyRelationsByEmployeeIdAsync(string employeeId)
        {
            var familyRelations = await _context.Employee_FamilyRelations
                .Include(efr => efr.FamilyRelation)
                .Include(efr => efr.Employee)
                .Where(efr => efr.EmployeeID == employeeId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FamilyRelationDTO>>(familyRelations);
        }

        public async Task<FamilyRelationDTO> GetFamilyRelationByIdAsync(int id)
        {
            var familyRelation = await _context.Employee_FamilyRelations
                .Include(efr => efr.FamilyRelation)
                .Include(efr => efr.Employee)
                .FirstOrDefaultAsync(efr => efr.FamilyRelationID == id);

            return familyRelation == null ? null : _mapper.Map<FamilyRelationDTO>(familyRelation);
        }

        public async Task<FamilyRelationDTO> CreateFamilyRelationAsync(FamilyRelationDTOPOST familyRelationDTO)
        {
            // Check if employee exists
            var employee = await _context.ApplicationUsers.FindAsync(familyRelationDTO.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            // Create FamilyRelation
            var familyRelation = _mapper.Map<FamilyRelation>(familyRelationDTO);
            _context.FamilyRelations.Add(familyRelation);
            await _context.SaveChangesAsync();

            // Create Employee_FamilyRelation
            var employeeFamilyRelation = new Employee_FamilyRelation
            {
                FamilyRelationID = familyRelation.ID,
                EmployeeID = familyRelationDTO.EmployeeID,
                RelationShipName = familyRelationDTO.RelationShipName
            };

            _context.Employee_FamilyRelations.Add(employeeFamilyRelation);
            await _context.SaveChangesAsync();

            return await GetFamilyRelationByIdAsync(familyRelation.ID);
        }

        public async Task<FamilyRelationDTO> UpdateFamilyRelationAsync(FamilyRelationDTOPUT familyRelationDTO)
        {
            // Check if employee exists
            var employee = await _context.ApplicationUsers.FindAsync(familyRelationDTO.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            // Update FamilyRelation
            var familyRelation = await _context.FamilyRelations.FindAsync(familyRelationDTO.ID);
            if (familyRelation == null)
            {
                throw new Exception("Không tìm thấy quan hệ gia đình với ID đã cho");
            }

            familyRelation.RelativeName = familyRelationDTO.RelativeName;
            familyRelation.StartDate = familyRelationDTO.StartDate;
            familyRelation.EndDate = familyRelationDTO.EndDate;

            // Update Employee_FamilyRelation
            var employeeFamilyRelation = await _context.Employee_FamilyRelations
                .FirstOrDefaultAsync(efr => efr.FamilyRelationID == familyRelationDTO.ID);

            if (employeeFamilyRelation != null)
            {
                employeeFamilyRelation.EmployeeID = familyRelationDTO.EmployeeID;
                employeeFamilyRelation.RelationShipName = familyRelationDTO.RelationShipName;
            }

            await _context.SaveChangesAsync();

            return await GetFamilyRelationByIdAsync(familyRelationDTO.ID);
        }

        public async Task<bool> DeleteFamilyRelationAsync(int id)
        {
            // Find Employee_FamilyRelation
            var employeeFamilyRelation = await _context.Employee_FamilyRelations
                .FirstOrDefaultAsync(efr => efr.FamilyRelationID == id);

            if (employeeFamilyRelation == null)
            {
                throw new Exception("Không tìm thấy quan hệ gia đình với ID đã cho");
            }

            // Find FamilyRelation
            var familyRelation = await _context.FamilyRelations.FindAsync(id);
            if (familyRelation == null)
            {
                throw new Exception("Không tìm thấy quan hệ gia đình với ID đã cho");
            }

            // Remove Employee_FamilyRelation first
            _context.Employee_FamilyRelations.Remove(employeeFamilyRelation);
            
            // Remove FamilyRelation
            _context.FamilyRelations.Remove(familyRelation);
            
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

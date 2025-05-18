using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_api.Data;
using dotnet_api.DTOs;
using dotnet_api.Data.Entities;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using dotnet_api.Data.Enums;

namespace dotnet_api.Services
{
    public class ImportOrderEmployeeService : IImportOrderEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImportOrderEmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<ImportOrderEmployeeDTO>> GetAllImportOrderEmployeesByManager()
        //{
        //    var ImportOrderEmployees = await _context.ImportOrderEmployees
        //        .Include(io => io.ImportOrderEmployeeEmployees)
        //        .ThenInclude(io => io.Employee)
        //        .Where(ImportOrderEmployee => ImportOrderEmployee.Status == ImportOrderEmployeeStatusEnum.Approved || ImportOrderEmployee.Status == ImportOrderEmployeeStatusEnum.Completed)
        //        .ToListAsync();
        //    return _mapper.Map<IEnumerable<ImportOrderEmployeeDTO>>(ImportOrderEmployees);
        //}

        public async Task<ImportOrderEmployeeDTO> GetImportOrderEmployeeById(int importOrderID, string employeeID)
        {
            var ImportOrderEmployee = await _context.ImportOrderEmployees
                .Include(io => io.Employee)
                .FirstOrDefaultAsync(e => e.ImportOrderId == importOrderID && e.EmployeeID == employeeID);
            return _mapper.Map<ImportOrderEmployeeDTO>(ImportOrderEmployee);
        }

        public async Task<ImportOrderEmployeeDTOPOST> CreateImportOrderEmployee(ImportOrderEmployeeDTOPOST ImportOrderEmployeeDTO)
        {
            var ImportOrderEmployee = _mapper.Map<ImportOrderEmployee>(ImportOrderEmployeeDTO);
            _context.ImportOrderEmployees.Add(ImportOrderEmployee);
            await _context.SaveChangesAsync();
            return _mapper.Map<ImportOrderEmployeeDTOPOST>(ImportOrderEmployee);
        }

        //public async Task<ImportOrderEmployeeDTO> UpdateImportOrderEmployee(int id, ImportOrderEmployeeDTO ImportOrderEmployeeDTO)
        //{
        //    var existingOrder = await _context.ImportOrderEmployees
        //        .Include(e => e.Material_ImportOrderEmployees)
        //        .FirstOrDefaultAsync(e => e.ID == id);

        //    if (existingOrder == null)
        //        return null;

        //    _mapper.Map(ImportOrderEmployeeDTO, existingOrder);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<ImportOrderEmployeeDTO>(existingOrder);
        //}

        //public async Task<bool> DeleteImportOrderEmployee(int id)
        //{
        //    var ImportOrderEmployee = await _context.ImportOrderEmployees.FindAsync(id);
        //    if (ImportOrderEmployee == null)
        //        return false;

        //    _context.ImportOrderEmployees.Remove(ImportOrderEmployee);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
} 
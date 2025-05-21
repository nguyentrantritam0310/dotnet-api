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
    public class ImportOrderService : IImportOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImportOrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImportOrderDTO>> GetAllImportOrdersByManager()
        {
            var importOrders = await _context.ImportOrders
                .Include(io => io.ImportOrderEmployees)
                .ThenInclude(io => io.Employee)
                .Where(ImportOrder => ImportOrder.Status == ImportOrderStatusEnum.Approved || ImportOrder.Status == ImportOrderStatusEnum.Completed)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ImportOrderDTO>>(importOrders);
        }

        public async Task<IEnumerable<ImportOrderDTO>> GetAllImportOrdersByDirector()
        {
            var importOrders = await _context.ImportOrders
                .Include(io => io.ImportOrderEmployees)
                .ThenInclude(io => io.Employee)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ImportOrderDTO>>(importOrders);
        }

        public async Task<ImportOrderDTO> GetImportOrderById(int id)
        {
            var ImportOrder = await _context.ImportOrders
                .Include(io => io.ImportOrderEmployees)
                .FirstOrDefaultAsync(e => e.ID == id);
            return _mapper.Map<ImportOrderDTO>(ImportOrder);
        }

        public async Task<ImportOrderDTOPOST> CreateImportOrder(ImportOrderDTOPOST ImportOrderDTO)
        {
            var ImportOrder = _mapper.Map<ImportOrder>(ImportOrderDTO);
            _context.ImportOrders.Add(ImportOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<ImportOrderDTOPOST>(ImportOrder);
        }
        public async Task<ImportOrderDTO> UpdateImportOrderStatusAsync(int id, int status)
        {
            var importOrder = await _context.ImportOrders.FirstOrDefaultAsync(io => io.ID == id);
            if (importOrder == null)
                return null;

            // Chuyển đổi int sang enum
            if (!Enum.IsDefined(typeof(ImportOrderStatusEnum), status))
                throw new ArgumentException("Invalid status value");

            importOrder.Status = (ImportOrderStatusEnum)status;
            await _context.SaveChangesAsync();

            return _mapper.Map<ImportOrderDTO>(importOrder);
        }

        //public async Task<ImportOrderDTO> UpdateImportOrder(int id, ImportOrderDTO ImportOrderDTO)
        //{
        //    var existingOrder = await _context.ImportOrders
        //        .Include(e => e.Material_ImportOrders)
        //        .FirstOrDefaultAsync(e => e.ID == id);

        //    if (existingOrder == null)
        //        return null;

        //    _mapper.Map(ImportOrderDTO, existingOrder);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<ImportOrderDTO>(existingOrder);
        //}

        //public async Task<bool> DeleteImportOrder(int id)
        //{
        //    var ImportOrder = await _context.ImportOrders.FindAsync(id);
        //    if (ImportOrder == null)
        //        return false;

        //    _context.ImportOrders.Remove(ImportOrder);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
} 
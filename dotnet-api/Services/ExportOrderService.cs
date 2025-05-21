using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_api.Data;
using dotnet_api.DTOs;
using dotnet_api.Data.Entities;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dotnet_api.Services
{
    public class ExportOrderService : IExportOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportOrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExportOrderDTO>> GetAllExportOrders()
        {
            var exportOrders = await _context.ExportOrders
                .Include(e => e.Material_ExportOrders)
                .ThenInclude(me => me.Material)
                .Include(e => e.Employee)
                .Include(cp => cp.ConstructionItem)
                .ThenInclude(ci => ci.Construction)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ExportOrderDTO>>(exportOrders);
        }

        public async Task<ExportOrderDTO> GetExportOrderById(int id)
        {
            var exportOrder = await _context.ExportOrders
                .Include(e => e.Material_ExportOrders)
                .ThenInclude(me => me.Material)
                .FirstOrDefaultAsync(e => e.ID == id);
            return _mapper.Map<ExportOrderDTO>(exportOrder);
        }

        public async Task<ExportOrderDTO> CreateExportOrder(ExportOrderDTOPOST exportOrderDTO)
        {
            var exportOrder = _mapper.Map<ExportOrder>(exportOrderDTO);
            _context.ExportOrders.Add(exportOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExportOrderDTO>(exportOrder);
        }

        public async Task<ExportOrderDTO> UpdateExportOrder(int id, ExportOrderDTO exportOrderDTO)
        {
            var existingOrder = await _context.ExportOrders
                .Include(e => e.Material_ExportOrders)
                .FirstOrDefaultAsync(e => e.ID == id);

            if (existingOrder == null)
                return null;

            _mapper.Map(exportOrderDTO, existingOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExportOrderDTO>(existingOrder);
        }

        public async Task<bool> DeleteExportOrder(int id)
        {
            var exportOrder = await _context.ExportOrders.FindAsync(id);
            if (exportOrder == null)
                return false;

            _context.ExportOrders.Remove(exportOrder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 
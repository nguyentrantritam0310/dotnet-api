using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class Material_ExportOrderService : IMaterial_ExportOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Material_ExportOrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Material_ExportOrderDTO> CreateMaterial_ExportOrderAsync(Material_ExportOrderDTO material_ExportOrderDTO)
        {
            var material_ExportOrder = _mapper.Map<Material_ExportOrder>(material_ExportOrderDTO);
            _context.Material_ExportOrders.Add(material_ExportOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<Material_ExportOrderDTO>(material_ExportOrder);
        }

        public async Task<IEnumerable<Material_ExportOrderDTO>> GetMaterial_ExportOrderById(int id)
        {
            var material_exportOrder = await _context.Material_ExportOrders
                .Where(c => c.ExportOrderID == id)
            .ToListAsync();
            return material_exportOrder == null ? null : _mapper.Map<IEnumerable<Material_ExportOrderDTO>>(material_exportOrder);


        }
    }
}

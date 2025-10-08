using AutoMapper;
using dotnet_api.Data;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public class OvertimeFormService : IOvertimeFormService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OvertimeFormService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OvertimeFormDTO>> GetAllOvertimeFormsAsync()
        {
            var overtimeForms = await _context.OvertimeForms.ToListAsync();
            return _mapper.Map<IEnumerable<OvertimeFormDTO>>(overtimeForms);
        }

        public async Task<OvertimeFormDTO> GetOvertimeFormByIdAsync(int id)
        {
            var overtimeForm = await _context.OvertimeForms.FirstOrDefaultAsync(of => of.ID == id);
            return _mapper.Map<OvertimeFormDTO>(overtimeForm);
        }
    }
}

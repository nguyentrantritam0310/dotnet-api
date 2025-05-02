using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReportDTO> CreateReportAsync(ReportDTO ReportDTO)
        {
            var Report = _mapper.Map<Report>(ReportDTO);
            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReportDTO>(Report);
        }

        public async Task<ReportDTO> GetReportByIdAsync(int id)
        {
            var Report = await _context.Reports
                .Include(c => c.Construction)
                .Include(c => c.Employee)
                //.Include(c => c.ReportType)

                .FirstOrDefaultAsync(c => c.ID == id);

            return Report == null ? null : _mapper.Map<ReportDTO>(Report);
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReportAsync()
        {
            var Reports = await _context.Reports
                .Include(c => c.Construction)
                .Include(c => c.Employee)
                //.Include(c => c.ReportType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReportDTO>>(Reports);
        }

        public async Task<ReportDTO> UpdateReportAsync(ReportDTO ReportDTO)
        {
            var existingReport = await _context.Reports.FindAsync(ReportDTO.ID);
            if (existingReport == null)
            {
                return null;
            }

            _mapper.Map(ReportDTO, existingReport);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReportDTO>(existingReport);
        }

        public async Task<bool> DeleteReportAsync(int id)
        {
            var Report = await _context.Reports.FindAsync(id);
            if (Report == null)
            {
                return false;
            }

            _context.Reports.Remove(Report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

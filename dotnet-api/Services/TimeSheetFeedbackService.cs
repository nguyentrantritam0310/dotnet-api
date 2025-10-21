using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public class TimeSheetFeedbackService : ITimeSheetFeedbackService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TimeSheetFeedbackService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeSheetFeedbackDto>> GetTimeSheetFeedbacksByEmployeeAsync(string employeeId)
        {
            var feedbacks = await _context.TimeSheetFeedbacks
                .Where(f => f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .OrderByDescending(f => f.TimeSheetFeedbackDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TimeSheetFeedbackDto>>(feedbacks);
        }

        public async Task<TimeSheetFeedbackDto> GetTimeSheetFeedbackAsync(int timeSheetId, string employeeId)
        {
            var feedback = await _context.TimeSheetFeedbacks
                .Where(f => f.TimeSheetID == timeSheetId && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            if (feedback == null)
                return null;

            return _mapper.Map<TimeSheetFeedbackDto>(feedback);
        }

        public async Task<TimeSheetFeedbackDto> CreateTimeSheetFeedbackAsync(CreateTimeSheetFeedbackDto createDto, string employeeId)
        {
            // Check if feedback already exists for this timesheet and employee
            var existingFeedback = await _context.TimeSheetFeedbacks
                .FirstOrDefaultAsync(f => f.TimeSheetID == createDto.TimeSheetID && f.EmployeeID == employeeId);

            if (existingFeedback != null)
            {
                throw new InvalidOperationException("Phản ánh cho bảng công này đã tồn tại.");
            }

            // Check if TimeSheet exists
            var timeSheet = await _context.TimeSheets
                .FirstOrDefaultAsync(t => t.ID == createDto.TimeSheetID);

            if (timeSheet == null)
            {
                throw new InvalidOperationException("Bảng công không tồn tại.");
            }

            var feedback = _mapper.Map<TimeSheetFeedback>(createDto);
            feedback.EmployeeID = employeeId;

            _context.TimeSheetFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var createdFeedback = await _context.TimeSheetFeedbacks
                .Where(f => f.TimeSheetID == createDto.TimeSheetID && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            return _mapper.Map<TimeSheetFeedbackDto>(createdFeedback);
        }

        public async Task<TimeSheetFeedbackDto> UpdateTimeSheetFeedbackAsync(UpdateTimeSheetFeedbackDto updateDto, string employeeId)
        {
            var feedback = await _context.TimeSheetFeedbacks
                .FirstOrDefaultAsync(f => f.TimeSheetID == updateDto.TimeSheetID && f.EmployeeID == employeeId);

            if (feedback == null)
            {
                throw new InvalidOperationException("Phản ánh không tồn tại.");
            }

            feedback.Title = updateDto.Title;
            feedback.Content = updateDto.Content;

            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var updatedFeedback = await _context.TimeSheetFeedbacks
                .Where(f => f.TimeSheetID == updateDto.TimeSheetID && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            return _mapper.Map<TimeSheetFeedbackDto>(updatedFeedback);
        }

        public async Task<bool> DeleteTimeSheetFeedbackAsync(int timeSheetId, string employeeId)
        {
            var feedback = await _context.TimeSheetFeedbacks
                .FirstOrDefaultAsync(f => f.TimeSheetID == timeSheetId && f.EmployeeID == employeeId);

            if (feedback == null)
            {
                return false;
            }

            _context.TimeSheetFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TimeSheetFeedbackDto>> GetTimeSheetFeedbacksByTimeSheetAsync(int timeSheetId)
        {
            var feedbacks = await _context.TimeSheetFeedbacks
                .Where(f => f.TimeSheetID == timeSheetId)
                .Include(f => f.Employee)
                .OrderByDescending(f => f.TimeSheetFeedbackDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TimeSheetFeedbackDto>>(feedbacks);
        }
    }
}
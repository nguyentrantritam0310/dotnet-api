using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public interface IClosingService
    {
        Task<bool> IsTimeSheetClosedAsync(string employeeId, int year, int month);
        Task<bool> IsPayrollClosedAsync(string employeeId, int year, int month);
        Task<bool> IsOvertimeSheetClosedAsync(string employeeId, int year, int month);
        Task<ClosingResultDTO> CloseTimeSheetAsync(string employeeId, int year, int month, string closedBy);
        Task<ClosingResultDTO> ClosePayrollAsync(string employeeId, int year, int month, string closedBy);
        Task<ClosingResultDTO> CloseOvertimeSheetAsync(string employeeId, int year, int month, string closedBy);
        Task<ClosingResultDTO> CloseAllSheetsAsync(string employeeId, int year, int month, string closedBy);
        Task<ClosingResultDTO> CloseAllPayrollsAsync(int year, int month, string closedBy);
        Task<List<TimeSheetDTO>> GetClosedTimeSheetsAsync(int year, int month);
        Task<List<PayrollDTO>> GetClosedPayrollsAsync(int year, int month);
        Task<List<OvertimeSheetDTO>> GetClosedOvertimeSheetsAsync(int year, int month);
    }

    public class ClosingService : IClosingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeSheetService _timeSheetService;
        private readonly IPayrollService _payrollService;
        private readonly IOvertimeService _overtimeService;

        public ClosingService(
            ApplicationDbContext context,
            ITimeSheetService timeSheetService,
            IPayrollService payrollService,
            IOvertimeService overtimeService)
        {
            _context = context;
            _timeSheetService = timeSheetService;
            _payrollService = payrollService;
            _overtimeService = overtimeService;
        }

        public async Task<bool> IsTimeSheetClosedAsync(string employeeId, int year, int month)
        {
            return await _context.TimeSheets
                .AnyAsync(ts => ts.EmployeeID == employeeId && 
                               ts.TimeSheetClosingDate.Year == year && 
                               ts.TimeSheetClosingDate.Month == month && 
                               ts.IsClosed);
        }

        public async Task<bool> IsPayrollClosedAsync(string employeeId, int year, int month)
        {
            return await _context.Payrolls
                .AnyAsync(p => p.EmployeeID == employeeId && 
                              p.PayrollClosingDate.Year == year && 
                              p.PayrollClosingDate.Month == month && 
                              p.IsClosed);
        }

        public async Task<bool> IsOvertimeSheetClosedAsync(string employeeId, int year, int month)
        {
            return await _context.OvertimeSheets
                .AnyAsync(os => os.EmployeeID == employeeId && 
                               os.OvertimeClosingDate.Year == year && 
                               os.OvertimeClosingDate.Month == month && 
                               os.IsClosed);
        }

        public async Task<ClosingResultDTO> CloseTimeSheetAsync(string employeeId, int year, int month, string closedBy)
        {
            try
            {
                // Kiểm tra xem đã chốt chưa
                if (await IsTimeSheetClosedAsync(employeeId, year, month))
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Bảng công đã được chốt cho tháng này"
                    };
                }

                // Lấy dữ liệu động từ service
                var timeSheetData = await _timeSheetService.GetTimeSheetDataAsync(employeeId, year, month);
                if (timeSheetData == null)
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu bảng công"
                    };
                }

                // Tạo TimeSheet entity và lưu vào DB
                var timeSheet = new TimeSheet
                {
                    EmployeeID = employeeId,
                    EmployeeName = timeSheetData.EmployeeName,
                    TotalStandardWorkdays = timeSheetData.TotalStandardWorkdays,
                    TotalUnpaidLeave = timeSheetData.TotalUnpaidLeave,
                    TotalPaidLeave = timeSheetData.TotalPaidLeave,
                    TotalWorkdays = timeSheetData.TotalWorkdays,
                    CompensatedOvertime = timeSheetData.CompensatedOvertime,
                    PayableOvertime = timeSheetData.PayableOvertime,
                    TotalActualWorkdays = timeSheetData.TotalActualWorkdays,
                    LateArrivalCount = timeSheetData.LateArrivalCount,
                    EarlyLeaveCount = timeSheetData.EarlyLeaveCount,
                    UnexcusedAbsenceCount = timeSheetData.UnexcusedAbsenceCount,
                    TimeSheetClosingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                    TimeSheetNotes = $"Chốt công tháng {month}/{year}",
                    IsClosed = true,
                    ClosedAt = DateTime.Now,
                    ClosedBy = closedBy
                };

                _context.TimeSheets.Add(timeSheet);
                await _context.SaveChangesAsync();

                return new ClosingResultDTO
                {
                    Success = true,
                    Message = "Chốt bảng công thành công",
                    ClosedAt = timeSheet.ClosedAt.Value
                };
            }
            catch (Exception ex)
            {
                return new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng công: {ex.Message}"
                };
            }
        }

        public async Task<ClosingResultDTO> ClosePayrollAsync(string employeeId, int year, int month, string closedBy)
        {
            try
            {
                // Kiểm tra xem đã chốt chưa
                if (await IsPayrollClosedAsync(employeeId, year, month))
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Bảng lương đã được chốt cho tháng này"
                    };
                }

                // Lấy dữ liệu động từ service
                var payrollData = await _payrollService.GetPayrollDataAsync(employeeId, year, month);
                if (payrollData == null)
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu bảng lương"
                    };
                }

                // Tạo Payroll entity và lưu vào DB
                var payroll = new Payroll
                {
                    EmployeeID = employeeId,
                    ContractType = payrollData.ContractType,
                    ContractSalary = payrollData.ContractSalary,
                    InsuranceSalary = payrollData.InsuranceSalary,
                    TotalContractSalary = payrollData.TotalContractSalary,
                    DailySalary = payrollData.DailySalary,
                    LeaveSalary = payrollData.LeaveSalary,
                    ActualSalary = payrollData.ActualSalary,
                    OvertimeSalary = payrollData.OvertimeSalary,
                    EatAllowance = payrollData.EatAllowance,
                    PetrolAllowance = payrollData.PetrolAllowance,
                    MealAllowance = payrollData.MealAllowance,
                    TotalAllowance = payrollData.TotalAllowance,
                    SocialInsuranceEmployee = payrollData.SocialInsuranceEmployee,
                    HealthInsuranceEmployee = payrollData.HealthInsuranceEmployee,
                    UnemploymentInsuranceEmployee = payrollData.UnemploymentInsuranceEmployee,
                    SocialInsuranceEmployer = payrollData.SocialInsuranceEmployer,
                    HealthInsuranceEmployer = payrollData.HealthInsuranceEmployer,
                    UnemploymentInsuranceEmployer = payrollData.UnemploymentInsuranceEmployer,
                    UnionFee = payrollData.UnionFee,
                    GrossIncome = payrollData.GrossIncome,
                    TaxableIncome = payrollData.TaxableIncome,
                    PersonalDeduction = payrollData.PersonalDeduction,
                    DependentDeduction = payrollData.DependentDeduction,
                    Bonus = payrollData.Bonus,
                    OtherIncome = payrollData.OtherIncome,
                    PersonalIncomeTax = payrollData.PersonalIncomeTax,
                    NetIncome = payrollData.NetIncome,
                    TotalDeduction = payrollData.TotalDeduction,
                    NetPay = payrollData.NetPay,
                    PayrollClosingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                    PayrollNotes = $"Chốt lương tháng {month}/{year}",
                    IsClosed = true,
                    ClosedAt = DateTime.Now,
                    ClosedBy = closedBy
                };

                _context.Payrolls.Add(payroll);
                await _context.SaveChangesAsync();

                return new ClosingResultDTO
                {
                    Success = true,
                    Message = "Chốt bảng lương thành công",
                    ClosedAt = payroll.ClosedAt.Value
                };
            }
            catch (Exception ex)
            {
                return new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng lương: {ex.Message}"
                };
            }
        }

        public async Task<ClosingResultDTO> CloseOvertimeSheetAsync(string employeeId, int year, int month, string closedBy)
        {
            try
            {
                // Kiểm tra xem đã chốt chưa
                if (await IsOvertimeSheetClosedAsync(employeeId, year, month))
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Bảng tăng ca đã được chốt cho tháng này"
                    };
                }

                // Lấy dữ liệu động từ service
                var overtimeData = await _overtimeService.GetOvertimeDataAsync(employeeId, year, month);
                if (overtimeData == null)
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu tăng ca"
                    };
                }

                // Tạo OvertimeSheet entity và lưu vào DB
                var overtimeSheet = new OvertimeSheet
                {
                    EmployeeID = employeeId,
                    EmployeeName = overtimeData.EmployeeName,
                    TotalOvertimeDays = overtimeData.TotalOvertimeDays,
                    TotalOvertimeHours = overtimeData.TotalOvertimeHours,
                    OvertimeSalary = overtimeData.OvertimeSalary,
                    OvertimeCoefficient = overtimeData.OvertimeCoefficient,
                    OvertimeClosingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                    OvertimeNotes = $"Chốt tăng ca tháng {month}/{year}",
                    IsClosed = true,
                    ClosedAt = DateTime.Now,
                    ClosedBy = closedBy
                };

                _context.OvertimeSheets.Add(overtimeSheet);
                await _context.SaveChangesAsync();

                return new ClosingResultDTO
                {
                    Success = true,
                    Message = "Chốt bảng tăng ca thành công",
                    ClosedAt = overtimeSheet.ClosedAt.Value
                };
            }
            catch (Exception ex)
            {
                return new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng tăng ca: {ex.Message}"
                };
            }
        }

        public async Task<ClosingResultDTO> CloseAllSheetsAsync(string employeeId, int year, int month, string closedBy)
        {
            var results = new List<string>();
            var success = true;

            // Chốt bảng công
            var timeSheetResult = await CloseTimeSheetAsync(employeeId, year, month, closedBy);
            results.Add($"Bảng công: {timeSheetResult.Message}");
            if (!timeSheetResult.Success) success = false;

            // Chốt bảng tăng ca
            var overtimeResult = await CloseOvertimeSheetAsync(employeeId, year, month, closedBy);
            results.Add($"Bảng tăng ca: {overtimeResult.Message}");
            if (!overtimeResult.Success) success = false;

            // Chốt bảng lương
            var payrollResult = await ClosePayrollAsync(employeeId, year, month, closedBy);
            results.Add($"Bảng lương: {payrollResult.Message}");
            if (!payrollResult.Success) success = false;

            return new ClosingResultDTO
            {
                Success = success,
                Message = success ? "Chốt tất cả bảng thành công" : "Một số bảng chốt không thành công",
                Details = results
            };
        }

        public async Task<ClosingResultDTO> CloseAllPayrollsAsync(int year, int month, string closedBy)
        {
            try
            {
                Console.WriteLine($"CloseAllPayrollsAsync started - Year: {year}, Month: {month}, ClosedBy: {closedBy}");
                
                // Lấy danh sách tất cả nhân viên
                var employees = await _context.Users
                    .Where(u => u.Status == dotnet_api.Data.Enums.EmployeeStatusEnum.Active)
                    .Select(u => new { u.Id, FullName = u.FirstName + " " + u.LastName })
                    .ToListAsync();
                
                Console.WriteLine($"Found {employees.Count} active employees");

                if (!employees.Any())
                {
                    return new ClosingResultDTO
                    {
                        Success = false,
                        Message = "Không tìm thấy nhân viên nào"
                    };
                }

                var successCount = 0;
                var errorCount = 0;
                var errors = new List<string>();

                // Chốt lương cho từng nhân viên
                foreach (var employee in employees)
                {
                    try
                    {
                        Console.WriteLine($"Processing employee: {employee.FullName} (ID: {employee.Id})");
                        
                        // Kiểm tra xem đã chốt chưa
                        if (await IsPayrollClosedAsync(employee.Id, year, month))
                        {
                            Console.WriteLine($"Payroll already closed for {employee.FullName}");
                            continue; // Bỏ qua nếu đã chốt
                        }

                        // Lấy dữ liệu động từ service
                        Console.WriteLine($"Getting payroll data for {employee.FullName}");
                        var payrollData = await _payrollService.GetPayrollDataAsync(employee.Id, year, month);
                        if (payrollData == null)
                        {
                            Console.WriteLine($"No payroll data found for {employee.FullName}");
                            errorCount++;
                            errors.Add($"Không tìm thấy dữ liệu lương cho nhân viên {employee.FullName}");
                            continue;
                        }
                        
                        Console.WriteLine($"Payroll data retrieved for {employee.FullName}: ContractSalary={payrollData.ContractSalary}");

                        // Tạo Payroll entity và lưu vào DB
                        var payroll = new Payroll
                        {
                            EmployeeID = employee.Id,
                            ContractType = payrollData.ContractType,
                            ContractSalary = payrollData.ContractSalary,
                            InsuranceSalary = payrollData.InsuranceSalary,
                            TotalContractSalary = payrollData.TotalContractSalary,
                            DailySalary = payrollData.DailySalary,
                            LeaveSalary = payrollData.LeaveSalary,
                            ActualSalary = payrollData.ActualSalary,
                            OvertimeSalary = payrollData.OvertimeSalary,
                            EatAllowance = payrollData.EatAllowance,
                            PetrolAllowance = payrollData.PetrolAllowance,
                            MealAllowance = payrollData.MealAllowance,
                            TotalAllowance = payrollData.TotalAllowance,
                            SocialInsuranceEmployee = payrollData.SocialInsuranceEmployee,
                            HealthInsuranceEmployee = payrollData.HealthInsuranceEmployee,
                            UnemploymentInsuranceEmployee = payrollData.UnemploymentInsuranceEmployee,
                            SocialInsuranceEmployer = payrollData.SocialInsuranceEmployer,
                            HealthInsuranceEmployer = payrollData.HealthInsuranceEmployer,
                            UnemploymentInsuranceEmployer = payrollData.UnemploymentInsuranceEmployer,
                            UnionFee = payrollData.UnionFee,
                            GrossIncome = payrollData.GrossIncome,
                            TaxableIncome = payrollData.TaxableIncome,
                            PersonalDeduction = payrollData.PersonalDeduction,
                            DependentDeduction = payrollData.DependentDeduction,
                            Bonus = payrollData.Bonus,
                            OtherIncome = payrollData.OtherIncome,
                            PersonalIncomeTax = payrollData.PersonalIncomeTax,
                            NetIncome = payrollData.NetIncome,
                            TotalDeduction = payrollData.TotalDeduction,
                            NetPay = payrollData.NetPay,
                            PayrollClosingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                            PayrollNotes = $"Chốt lương tháng {month}/{year}",
                            IsClosed = true,
                            ClosedAt = DateTime.Now,
                            ClosedBy = closedBy
                        };

                        _context.Payrolls.Add(payroll);
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        errors.Add($"Lỗi khi chốt lương cho nhân viên {employee.FullName}: {ex.Message}");
                    }
                }

                // Lưu tất cả thay đổi
                await _context.SaveChangesAsync();

                var message = $"Chốt lương thành công cho {successCount} nhân viên";
                if (errorCount > 0)
                {
                    message += $", {errorCount} nhân viên gặp lỗi";
                }

                return new ClosingResultDTO
                {
                    Success = successCount > 0,
                    Message = message,
                    ClosedAt = DateTime.Now,
                    Details = errors.Any() ? errors : new List<string>()
                };
            }
            catch (Exception ex)
            {
                return new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt lương hàng loạt: {ex.Message}"
                };
            }
        }

        public async Task<List<TimeSheetDTO>> GetClosedTimeSheetsAsync(int year, int month)
        {
            var timeSheets = await _context.TimeSheets
                .Where(ts => ts.TimeSheetClosingDate.Year == year && 
                            ts.TimeSheetClosingDate.Month == month && 
                            ts.IsClosed)
                .Select(ts => new TimeSheetDTO
                {
                    ID = ts.ID,
                    EmployeeID = ts.EmployeeID,
                    EmployeeName = ts.EmployeeName,
                    TotalStandardWorkdays = ts.TotalStandardWorkdays,
                    TotalUnpaidLeave = ts.TotalUnpaidLeave,
                    TotalPaidLeave = ts.TotalPaidLeave,
                    TotalWorkdays = ts.TotalWorkdays,
                    CompensatedOvertime = ts.CompensatedOvertime,
                    PayableOvertime = ts.PayableOvertime,
                    TotalActualWorkdays = ts.TotalActualWorkdays,
                    LateArrivalCount = ts.LateArrivalCount,
                    EarlyLeaveCount = ts.EarlyLeaveCount,
                    UnexcusedAbsenceCount = ts.UnexcusedAbsenceCount,
                    TimeSheetClosingDate = ts.TimeSheetClosingDate,
                    TimeSheetNotes = ts.TimeSheetNotes,
                    IsClosed = ts.IsClosed,
                    ClosedAt = ts.ClosedAt,
                    ClosedBy = ts.ClosedBy
                })
                .ToListAsync();

            return timeSheets;
        }

        public async Task<List<PayrollDTO>> GetClosedPayrollsAsync(int year, int month)
        {
            var payrolls = await _context.Payrolls
                .Where(p => p.PayrollClosingDate.Year == year && 
                           p.PayrollClosingDate.Month == month && 
                           p.IsClosed)
                .Select(p => new PayrollDTO
                {
                    ID = p.ID,
                    EmployeeID = p.EmployeeID,
                    ContractType = p.ContractType,
                    ContractSalary = p.ContractSalary,
                    InsuranceSalary = p.InsuranceSalary,
                    TotalContractSalary = p.TotalContractSalary,
                    DailySalary = p.DailySalary,
                    LeaveSalary = p.LeaveSalary,
                    ActualSalary = p.ActualSalary,
                    OvertimeSalary = p.OvertimeSalary,
                    EatAllowance = p.EatAllowance,
                    PetrolAllowance = p.PetrolAllowance,
                    MealAllowance = p.MealAllowance,
                    TotalAllowance = p.TotalAllowance,
                    SocialInsuranceEmployee = p.SocialInsuranceEmployee,
                    HealthInsuranceEmployee = p.HealthInsuranceEmployee,
                    UnemploymentInsuranceEmployee = p.UnemploymentInsuranceEmployee,
                    SocialInsuranceEmployer = p.SocialInsuranceEmployer,
                    HealthInsuranceEmployer = p.HealthInsuranceEmployer,
                    UnemploymentInsuranceEmployer = p.UnemploymentInsuranceEmployer,
                    UnionFee = p.UnionFee,
                    GrossIncome = p.GrossIncome,
                    TaxableIncome = p.TaxableIncome,
                    PersonalDeduction = p.PersonalDeduction,
                    DependentDeduction = p.DependentDeduction,
                    Bonus = p.Bonus,
                    OtherIncome = p.OtherIncome,
                    PersonalIncomeTax = p.PersonalIncomeTax,
                    NetIncome = p.NetIncome,
                    TotalDeduction = p.TotalDeduction,
                    NetPay = p.NetPay,
                    PayrollClosingDate = p.PayrollClosingDate,
                    PayrollNotes = p.PayrollNotes,
                    IsClosed = p.IsClosed,
                    ClosedAt = p.ClosedAt,
                    ClosedBy = p.ClosedBy
                })
                .ToListAsync();

            return payrolls;
        }

        public async Task<List<OvertimeSheetDTO>> GetClosedOvertimeSheetsAsync(int year, int month)
        {
            var overtimeSheets = await _context.OvertimeSheets
                .Where(os => os.OvertimeClosingDate.Year == year && 
                            os.OvertimeClosingDate.Month == month && 
                            os.IsClosed)
                .Select(os => new OvertimeSheetDTO
                {
                    ID = os.ID,
                    EmployeeID = os.EmployeeID,
                    EmployeeName = os.EmployeeName,
                    TotalOvertimeDays = os.TotalOvertimeDays,
                    TotalOvertimeHours = os.TotalOvertimeHours,
                    OvertimeSalary = os.OvertimeSalary,
                    OvertimeCoefficient = os.OvertimeCoefficient,
                    OvertimeClosingDate = os.OvertimeClosingDate,
                    OvertimeNotes = os.OvertimeNotes,
                    IsClosed = os.IsClosed,
                    ClosedAt = os.ClosedAt,
                    ClosedBy = os.ClosedBy
                })
                .ToListAsync();

            return overtimeSheets;
        }
    }
}

using dotnet_api.DTOs;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public interface IPayrollService
    {
        Task<PayrollDTO> GetPayrollDataAsync(string employeeId, int year, int month);
    }

    public class PayrollService : IPayrollService
    {
        private readonly ApplicationDbContext _context;

        public PayrollService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PayrollDTO> GetPayrollDataAsync(string employeeId, int year, int month)
        {
            try
            {
                Console.WriteLine($"GetPayrollDataAsync called for EmployeeID: {employeeId}, Year: {year}, Month: {month}");
                
                // Lấy thông tin hợp đồng hiện tại của nhân viên
                var contract = await _context.Contracts
                    .Include(c => c.ContractType)
                    .Include(c => c.ContractAllowances)
                        .ThenInclude(ca => ca.Allowance)
                    .Where(c => c.EmployeeID == employeeId && c.ApproveStatus == dotnet_api.Data.Enums.ApproveStatusEnum.Approved)
                    .OrderByDescending(c => c.StartDate)
                    .FirstOrDefaultAsync();

                if (contract == null)
                {
                    Console.WriteLine($"No contract found for employee {employeeId}");
                    return null; // Không có hợp đồng
                }
                
                Console.WriteLine($"Contract found for employee {employeeId}: Salary={contract.ContractSalary}");

            // Lấy dữ liệu shift assignments để tính ngày công chuẩn
            var shiftAssignments = await _context.ShiftAssignments
                .Where(sa => sa.EmployeeID == employeeId && 
                           sa.WorkDate.Year == year && 
                           sa.WorkDate.Month == month)
                .ToListAsync();

            // Lấy dữ liệu attendance của tháng
            var attendanceData = await _context.Attendances
                .Where(a => a.EmployeeId == employeeId && 
                           a.CheckInDateTime.HasValue &&
                           a.CheckInDateTime.Value.Year == year && 
                           a.CheckInDateTime.Value.Month == month)
                .ToListAsync();

            // Lấy dữ liệu overtime requests của tháng (thay vì OvertimeSheets)
            var overtimeRequests = await _context.EmployeeRequests
                .Where(o => o.EmployeeID == employeeId && 
                           o.RequestType == "Overtime" &&
                           o.ApproveStatus == dotnet_api.Data.Enums.ApproveStatusEnum.Approved &&
                           o.StartDateTime.Year == year && 
                           o.StartDateTime.Month == month)
                .ToListAsync();

            // Lấy dữ liệu leave của tháng
            var leaveData = await _context.EmployeeRequests
                .Where(l => l.EmployeeID == employeeId && 
                           l.RequestType == "Leave" &&
                           l.ApproveStatus == dotnet_api.Data.Enums.ApproveStatusEnum.Approved &&
                           l.StartDateTime.Year == year && 
                           l.StartDateTime.Month == month)
                .ToListAsync();

            // Lấy dữ liệu payroll adjustments
            var payrollAdjustments = await _context.PayrollAdjustments
                .Include(pa => pa.applicationUser)
                .Where(pa => pa.decisionDate.Year == year && 
                           pa.decisionDate.Month == month &&
                           pa.ApproveStatus == dotnet_api.Data.Enums.ApproveStatusEnum.Approved &&
                           pa.applicationUser.Any(e => e.EmployeeID == employeeId))
                .ToListAsync();

            // Tính toán các khoản lương theo logic frontend
            var contractSalary = contract.ContractSalary;
            var insuranceSalary = contract.InsuranceSalary;
            
            // Tính tổng ngày công chuẩn từ số ngày được phân ca trong tháng
            var standardDays = shiftAssignments.Count;
            
            // Tính tổng ngày công thực tế = đếm số ngày duy nhất có dữ liệu chấm công hợp lệ
            var validAttendanceDays = attendanceData.Where(a => 
                a.CheckInDateTime.HasValue && a.CheckOutDateTime.HasValue &&
                !string.IsNullOrEmpty(a.CheckInDateTime.ToString()) &&
                !string.IsNullOrEmpty(a.CheckOutDateTime.ToString())
            ).ToList();
            
            var uniqueWorkDays = validAttendanceDays
                .Select(a => a.CheckInDateTime.Value.Date)
                .Distinct()
                .Count();
            
            var totalDays = uniqueWorkDays;
            
            // Tính tổng nghỉ có lương = tổng số ngày nghỉ phép
            var approvedLeaveRequests = leaveData.Where(leave => 
                leave.LeaveType != null && 
                leave.LeaveType.LeaveTypeName.ToLower().Contains("phép")
            ).ToList();
            
            var totalPaidLeaveDays = approvedLeaveRequests.Sum(leave => 
                (leave.EndDateTime - leave.StartDateTime).Days + 1
            );
            
            var paidLeaveDays = totalPaidLeaveDays;
            
            // Tính công tăng ca và lương tăng ca từ overtime requests
            var totalOvertimeHours = 0m;
            var totalOvertimeDayUnits = 0m;
            var totalOvertimeHoursWithCoeff = 0m;
            var totalOvertimeDaysWithCoeff = 0m;
            
            foreach (var ot in overtimeRequests)
            {
                var hours = Math.Max(0, (decimal)(ot.EndDateTime - ot.StartDateTime).TotalHours);
                var dayUnits = hours / 8;
                var coeff = ot.OvertimeType?.coefficient ?? 1;
                
                totalOvertimeHours += hours;
                totalOvertimeDayUnits += dayUnits;
                totalOvertimeHoursWithCoeff += hours * (decimal)coeff;
                totalOvertimeDaysWithCoeff += dayUnits * (decimal)coeff;
            }
            
            var otDays = Math.Round(totalOvertimeDayUnits, 2);
            var otHours = Math.Round(totalOvertimeHours, 2);
            var otHoursWithCoeff = Math.Round(totalOvertimeHoursWithCoeff, 2);
            var otDaysWithCoeff = Math.Round(totalOvertimeDaysWithCoeff, 2);
            var otSalary = standardDays > 0 ? (contractSalary * totalOvertimeDaysWithCoeff / standardDays) : 0;
            
            // Tính lương theo ngày công = lương hợp đồng * tổng ngày công / tổng ngày công chuẩn
            var salaryByDays = standardDays > 0 ? contractSalary * (totalDays / standardDays) : 0;
            
            // Tính tổng lương phép = lương hợp đồng * tổng nghỉ có lương / tổng ngày công chuẩn
            var leaveSalary = standardDays > 0 ? contractSalary * (paidLeaveDays / standardDays) : 0;
            
            // Tính tổng lương thực tế = lương theo ngày công + tổng lương phép
            var actualSalary = salaryByDays + leaveSalary;
            
            // Tính các khoản phụ cấp từ hợp đồng theo logic frontend
            var mealAllowance = contract.ContractAllowances
                .Where(ca => ca.Allowance.AllowanceName.ToLower().Contains("ăn") || 
                           ca.Allowance.AllowanceName.ToLower().Contains("meal") ||
                           ca.Allowance.AllowanceName.ToLower().Contains("trưa") ||
                           ca.Allowance.AllowanceName.ToLower().Contains("ca"))
                .Sum(ca => ca.Value);
            
            var fuelAllowance = contract.ContractAllowances
                .Where(ca => ca.Allowance.AllowanceName.ToLower().Contains("xăng") || 
                           ca.Allowance.AllowanceName.ToLower().Contains("xe") ||
                           ca.Allowance.AllowanceName.ToLower().Contains("fuel") ||
                           ca.Allowance.AllowanceName.ToLower().Contains("điện thoại"))
                .Sum(ca => ca.Value);
            
            var responsibilityAllowance = contract.ContractAllowances
                .Where(ca => ca.Allowance.AllowanceName.ToLower().Contains("trách nhiệm") ||
                           ca.Allowance.AllowanceName.ToLower().Contains("responsibility"))
                .Sum(ca => ca.Value);
            
            var totalSupport = mealAllowance + fuelAllowance + responsibilityAllowance;
            
            // Tính bảo hiểm nhân viên đóng (10.5%)
            var insuranceEmployee = insuranceSalary * 0.105m;
            var unionFee = insuranceSalary * 0.01m;
            
            // Tính số người phụ thuộc từ quan hệ gia đình
            var monthStartDate = new DateTime(year, month, 1);
            var monthEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            
            var dependents = await _context.Employee_FamilyRelations
                .Where(relation => relation.EmployeeID == employeeId &&
                                 relation.FamilyRelation.StartDate <= monthEndDate &&
                                 relation.FamilyRelation.EndDate >= monthStartDate &&
                                 (relation.RelationShipName.Contains("Con") ||
                                  relation.RelationShipName.Contains("Vợ") ||
                                  relation.RelationShipName.Contains("Chồng") ||
                                  relation.RelationShipName.Contains("Cha") ||
                                  relation.RelationShipName.Contains("Mẹ")))
                .CountAsync();
            
            // Tính các khoản trừ từ payroll adjustments
            var adjustmentDeductions = payrollAdjustments
                .Where(adj => adj.AdjustmentType.AdjustmentTypeName.Contains("Kỷ luật") ||
                            adj.AdjustmentType.AdjustmentTypeName.Contains("Truy thu") ||
                            adj.AdjustmentType.AdjustmentTypeName.Contains("Tạm ứng"))
                .SelectMany(adj => adj.applicationUser)
                .Where(emp => emp.EmployeeID == employeeId)
                .Sum(emp => Math.Abs(emp.Value));
            
            // Tính thuế TNCN theo logic frontend
            var personalDeduction = 11000000m;
            var dependentDeduction = dependents * 4400000m;
            
            var totalIncome = actualSalary + mealAllowance + fuelAllowance + responsibilityAllowance + otSalary;
            
            // 1. Tổng thu nhập chịu thuế = Tổng thu nhập (không trừ gì)
            var taxableIncome = totalIncome;
            
            // 2. Tổng thu nhập tính thuế = IF(tổng thu nhập - bảo hiểm NV đóng - giảm trừ bản thân - giảm trừ người phụ thuộc > 0, ..., 0)
            var pitIncome = Math.Max(0, totalIncome - insuranceEmployee - personalDeduction - dependentDeduction);
            
            // 3. Tính thuế TNCN theo thuế luỹ tiến từ pitIncome
            var pitTax = CalculatePersonalIncomeTax(pitIncome);
            
            var totalDeduction = insuranceEmployee + unionFee + pitTax + (decimal)adjustmentDeductions;
            var netSalary = Math.Max(0, totalIncome - totalDeduction); // Thực lãnh không được âm

            return new PayrollDTO
            {
                EmployeeID = employeeId,
                ContractType = contract.ContractType.ContractTypeName,
                ContractSalary = contractSalary,
                InsuranceSalary = insuranceSalary,
                TotalContractSalary = contractSalary + insuranceSalary,
                DailySalary = standardDays > 0 ? contractSalary / standardDays : 0,
                LeaveSalary = leaveSalary,
                ActualSalary = actualSalary,
                OvertimeSalary = otSalary,
                EatAllowance = mealAllowance,
                PetrolAllowance = fuelAllowance,
                MealAllowance = responsibilityAllowance,
                TotalAllowance = totalSupport,
                SocialInsuranceEmployee = insuranceSalary * 0.08m,
                HealthInsuranceEmployee = insuranceSalary * 0.015m,
                UnemploymentInsuranceEmployee = insuranceSalary * 0.01m,
                SocialInsuranceEmployer = insuranceSalary * 0.17m,
                HealthInsuranceEmployer = insuranceSalary * 0.03m,
                UnemploymentInsuranceEmployer = insuranceSalary * 0.01m,
                UnionFee = unionFee,
                GrossIncome = totalIncome,
                TaxableIncome = taxableIncome,
                PersonalDeduction = personalDeduction,
                DependentDeduction = dependentDeduction,
                Bonus = 0,
                OtherIncome = 0,
                PersonalIncomeTax = pitTax,
                NetIncome = totalIncome - pitTax,
                TotalDeduction = totalDeduction,
                NetPay = netSalary,
                PayrollClosingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                PayrollNotes = $"Tính lương tháng {month}/{year} - {totalDays} ngày làm việc, {standardDays} ngày chuẩn",
                IsClosed = false
            };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPayrollDataAsync for employee {employeeId}: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw để controller có thể catch
            }
        }

        private decimal CalculatePersonalIncomeTax(decimal taxableIncome)
        {
            if (taxableIncome <= 0) return 0;
            
            // Bậc thuế thu nhập cá nhân (2024)
            if (taxableIncome <= 5000000) return taxableIncome * 0.05m;
            if (taxableIncome <= 10000000) return 250000 + (taxableIncome - 5000000) * 0.1m;
            if (taxableIncome <= 18000000) return 750000 + (taxableIncome - 10000000) * 0.15m;
            if (taxableIncome <= 32000000) return 1950000 + (taxableIncome - 18000000) * 0.2m;
            if (taxableIncome <= 52000000) return 4750000 + (taxableIncome - 32000000) * 0.25m;
            if (taxableIncome <= 80000000) return 9750000 + (taxableIncome - 52000000) * 0.3m;
            return 18150000 + (taxableIncome - 80000000) * 0.35m;
        }
    }
}

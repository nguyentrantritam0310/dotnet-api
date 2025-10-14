using AutoMapper;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Helpers;

namespace dotnet_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contract, ContractDTO>()
                .ForMember(dest => dest.ContractTypeName, opt => opt.MapFrom(src => src.ContractType.ContractTypeName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.ApproveStatus, opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ApproveStatus)))
                .ForMember(dest => dest.Allowances, opt => opt.MapFrom(src => src.ContractAllowances));

            // Contract type, allowance mappings
            CreateMap<ContractType, ContractTypeDTO>();
            CreateMap<Allowance, AllowanceDTO>();
            CreateMap<Contract_Allowance, ContractAllowanceDTO>()
                .ForMember(dest => dest.ContractID, opt => opt.MapFrom(src => src.ContractID))
                .ForMember(dest => dest.AllowanceName, opt => opt.MapFrom(src => src.Allowance.AllowanceName));

            // Contract POST/PUT mappings
            CreateMap<ContractDTOPOST, Contract>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.ContractType, opt => opt.Ignore())
                .ForMember(dest => dest.ContractAllowances, opt => opt.MapFrom(src => src.Allowances))
                .ForMember(dest => dest.ApproveStatus, opt => opt.MapFrom(src => ApproveStatusEnum.Created));

            CreateMap<ContractDTOPUT, Contract>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.ContractType, opt => opt.Ignore())
                .ForMember(dest => dest.ContractAllowances, opt => opt.MapFrom(src => src.Allowances));

            CreateMap<ContractAllowanceDTOPOST, Contract_Allowance>()
                .ForMember(dest => dest.ContractID, opt => opt.Ignore())
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Allowance, opt => opt.Ignore());

            CreateMap<ContractAllowanceDTOPUT, Contract_Allowance>()
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Allowance, opt => opt.Ignore());
            CreateMap<ShiftAssignment, ShiftAssignmentDTO>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.WorkShiftName, opt => opt.MapFrom(src => src.WorkShift.ShiftName))
                .ForMember(dest => dest.WorkDateFormatted, opt => opt.MapFrom(src => src.WorkDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.WorkShift.ShiftDetails.FirstOrDefault() != null ? src.WorkShift.ShiftDetails.FirstOrDefault().StartTime.ToString(@"hh\:mm") : ""))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.WorkShift.ShiftDetails.FirstOrDefault() != null ? src.WorkShift.ShiftDetails.FirstOrDefault().EndTime.ToString(@"hh\:mm") : ""));

            CreateMap<ShiftAssignmentDTOPOST, ShiftAssignment>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.Attendance, opt => opt.Ignore());

            CreateMap<ShiftAssignmentDTOPUT, ShiftAssignment>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.Attendance, opt => opt.Ignore());

            CreateMap<ShiftAssignment, AttendanceDTO>();
            CreateMap<Construction, ConstructionDTO>()
            .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
            .ForMember(dest => dest.ConstructionItems,
                    opt => opt.MapFrom(src => src.ConstructionItems
                    .ToList()))
             .ForMember(dest => dest.ConstructionTypeName,
                opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionType.ConstructionTypeName)));
            CreateMap<ConstructionItem, ConstructionItemDTO>()
                .ForMember(dest => dest.UnitName,
        opt => opt.MapFrom(src => src.UnitOfMeasurement.ShortName))
                .ForMember(dest => dest.ConstructionItemStatusID,
        opt => opt.MapFrom(src => src.ConstructionStatus.ID))
    .ForMember(dest => dest.ConstructionItemStatusName,
        opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
                ;
            CreateMap<ConstructionPlan, ConstructionPlanDTO>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                .ForMember(dest => dest.ConstructionItemName,
                    opt => opt.MapFrom(src => src.ConstructionItem.ConstructionItemName))
                .ForMember(dest => dest.ConstructionName,
                    opt => opt.MapFrom(src => src.ConstructionItem.Construction.ConstructionName))
                .ForMember(dest => dest.ConstructionID,
                    opt => opt.MapFrom(src => src.ConstructionItem.Construction.ID))
                .ForMember(dest => dest.StatusName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
                .ForMember(dest => dest.UnitOfMeasurementName,
                    opt => opt.MapFrom(src => src.ConstructionItem.UnitOfMeasurement.ShortName))
                .ForMember(dest => dest.TotalWorkload,
                    opt => opt.MapFrom(src => src.ConstructionItem.TotalVolume))
                ;

            CreateMap<ConstructionPlanDTOPOST, ConstructionPlan>().ReverseMap();
            CreateMap<ConstructionPlanDTOPUT, ConstructionPlan>().ReverseMap();
            CreateMap<ConstructionStatus, ConstructionStatusDTO>();
            CreateMap<ConstructionTemplateItem, ConstructionTemplateItemDTO>()
                .ForMember(dest => dest.ConstructionTypeName,
                    opt => opt.MapFrom(src => src.ConstructionType.ConstructionTypeName))
                .ForMember(dest => dest.WorkSubTypeVariantName,
                    opt => opt.MapFrom(src => src.WorkSubTypeVariant.Description))
                ;
            CreateMap<ConstructionTask, ConstructionTaskDTO>()
                .ForMember(dest => dest.StatusName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
                .ForMember(dest => dest.UnitOfMeasurementName,
                    opt => opt.MapFrom(src => src.ConstructionPlan.ConstructionItem.UnitOfMeasurement.ShortName))
                .ForMember(dest => dest.totalWorkload,
                    opt => opt.MapFrom(src => src.ConstructionPlan.ConstructionItem.TotalVolume))
                .ForMember(dest => dest.ConstructionItemID,
                    opt => opt.MapFrom(src => src.ConstructionPlan.ConstructionItem.ID))
                ;
            ;
            CreateMap<ConstructionTaskDTOPOST, ConstructionTask>().ReverseMap();
            CreateMap<ConstructionTaskDTOPUT, ConstructionTask>();

            CreateMap<ConstructionType, ConstructionTypeDTO>();
            CreateMap<ApplicationUser, EmployeeDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.StatusName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.Status)))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.birthday))
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.joinDate));
            CreateMap<ExportOrder, ExportOrderDTO>()
                 .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                 .ForMember(dest => dest.ConstructionItemName,
                    opt => opt.MapFrom(src => src.ConstructionItem.ConstructionItemName))
                    .ForMember(dest => dest.ConstructionName,
                    opt => opt.MapFrom(src => src.ConstructionItem.Construction.ConstructionName))
                    .ForMember(dest => dest.materialName,
                    opt => opt.MapFrom(src => src.Material_ExportOrders.Select(m => m.Material.MaterialName)))
                ;
            CreateMap<ImportOrder, ImportOrderDTO>()
                .ForMember(dest => dest.ImportOrderEmployee,
                    opt => opt.MapFrom(src => src.ImportOrderEmployees
                    .ToList()))
                ;
            CreateMap<ImportOrderEmployee, ImportOrderEmployeeDTO>()
            .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
            ;
            CreateMap<ImportOrderDTOPOST, ImportOrder>().ReverseMap();

            CreateMap<ImportOrderEmployeeDTOPOST, ImportOrderEmployee>().ReverseMap();

            CreateMap<Material_ExportOrder, Material_ExportOrderDTO>();
            CreateMap<Material, MaterialDTO>()
                .ForMember(dest => dest.MaterialTypeName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialType.MaterialTypeName)))
             .ForMember(dest => dest.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfMeasurement.ShortName));
            CreateMap<MaterialUpdateStockQuantityDTO, Material>().ReverseMap();
            CreateMap<MaterialNorm, MaterialNormDTO>()
                    .ForMember(dest => dest.MaterialName,
                        opt => opt.MapFrom(src => src.Material.MaterialName))
                    .ForMember(dest => dest.StockQuantity,
                        opt => opt.MapFrom(src => src.Material.StockQuantity))
                    .ForMember(dest => dest.Price,
                        opt => opt.MapFrom(src => src.Material.UnitPrice))
                    .ForMember(dest => dest.UnitOfMeasurement,
                        opt => opt.MapFrom(src => src.Material.UnitOfMeasurement.ShortName))
                     .ForMember(dest => dest.ConstructionId,
                        opt => opt.MapFrom(src => src.WorkSubTypeVariant.ConstructionItems.Select(ci => ci.Construction.ID).FirstOrDefault()))
                     .ForMember(dest => dest.ConstructionItemName,
                        opt => opt.MapFrom(src => src.WorkSubTypeVariant.ConstructionItems.Select(ci => ci.ConstructionItemName).FirstOrDefault()))
                     .ForMember(dest => dest.ConstructionItemID,
                        opt => opt.MapFrom(src => src.WorkSubTypeVariant.ConstructionItems.Select(ci => ci.ID).FirstOrDefault()))
                    ;

            CreateMap<MaterialNorm, MaterialNormItemDTO>()
    .ForMember(dest => dest.MaterialName,
        opt => opt.MapFrom(src => src.Material.MaterialName))
    .ForMember(dest => dest.StockQuantity,
                    opt => opt.MapFrom(src => src.Material.StockQuantity))
    .ForMember(dest => dest.UnitOfMeasurement,
        opt => opt.MapFrom(src => src.Material.UnitOfMeasurement.ShortName))
     .ForMember(dest => dest.ConstructionItemName,
        opt => opt.MapFrom(src => src.WorkSubTypeVariant.ConstructionItems.Select(ci => ci.ConstructionItemName).FirstOrDefault()))
     .ForMember(dest => dest.ConstructionItemID,
        opt => opt.MapFrom(src => src.WorkSubTypeVariant.ConstructionItems.Select(ci => ci.ID).FirstOrDefault()))
    ;

            CreateMap<MaterialPlan, MaterialPlanDTO>()
                .ForMember(dest => dest.MaterialName,
                    opt => opt.MapFrom(src => src.Material.MaterialName))
                .ForMember(dest => dest.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.Material.UnitOfMeasurement.ShortName))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Material.UnitPrice))
                ;
            CreateMap<MaterialPlanDTOPOST, MaterialPlan>().ReverseMap();

            CreateMap<MaterialType, MaterialTypeDTO>();
            CreateMap<ReportAttachment, ReportAttachmentDTO>();
            CreateMap<Report, ReportDTO>()
                 .ForMember(dest => dest.StatusLogs,
                    opt => opt.MapFrom(src => src.ReportStatusLogs
                    .OrderByDescending(log => log.ReportDate)
                    .ToList()))
                 .ForMember(dest => dest.Attachments,
                    opt => opt.MapFrom(src => src.ReportAttachments
                    .OrderByDescending(log => log.UploadDate)
                    .ToList()))
            .ForMember(dest => dest.constructionName,
                    opt => opt.MapFrom(src => src.Construction.ConstructionName));
            CreateMap<ReportStatusLog, ReportStatusLogDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UnitofMeasurement, UnitofMeasurementDTO>();
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role.RoleName))
                ;

            CreateMap<ConstructionDTOPOST, Construction>()
                .ForMember(dest => dest.ConstructionItems,
                    opt => opt.MapFrom(src => src.ConstructionItems));

            CreateMap<ConstructionItemDTOPOST, ConstructionItem>()
                .ForMember(dest => dest.UnitOfMeasurement,
                    opt => opt.MapFrom(src => new UnitofMeasurement
                    {
                        UnitName = src.UnitOfMeasurement
                    }))
                .ForMember(dest => dest.ConstructionStatus,
                    opt => opt.MapFrom(src => new ConstructionStatus
                    {
                        Name = ConstructionStatusEnum.Pending
                    }));

            CreateMap<Material_ExportOrderDTO, Material_ExportOrder>().ReverseMap();
            CreateMap<ExportOrderDTOPOST, ExportOrder>().ReverseMap();
            CreateMap<MaterialDTOPOST, Material>().ReverseMap();
            CreateMap<MaterialType, MaterialTypeDTO>()
                    .ForMember(dest => dest.MaterialTypeName,
            opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialTypeName)));
            ;
            CreateMap<UnitofMeasurement, UnitofMeasurementDTO>();

            CreateMap<ShiftAssignment, AttendanceDTO>();

            CreateMap<AttendanceDTO, ShiftAssignment>().ReverseMap();
            CreateMap<WorkSubTypeVariant, WorkSubTypeVariantDTO>();

            CreateMap<ConstructionItemCreateDTO, ConstructionItem>();

            CreateMap<ConstructionItemUpdateDTO, ConstructionItem>();

            // Add mapping for ConstructionCreateDTO to ConstructionDTOPOST
            CreateMap<ConstructionCreateDTO, ConstructionDTOPOST>()
                .ForMember(dest => dest.ConstructionTypeID, opt => opt.MapFrom(src => src.ConstructionTypeID))
                .ForMember(dest => dest.ConstructionName, opt => opt.MapFrom(src => src.ConstructionName))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.TotalArea, opt => opt.MapFrom(src => src.TotalArea))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpectedCompletionDate, opt => opt.MapFrom(src => src.ExpectedCompletionDate));

            // Add mapping for ConstructionUpdateDTO to ConstructionDTO
            CreateMap<ConstructionUpdateDTO, ConstructionDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ConstructionTypeID, opt => opt.MapFrom(src => src.ConstructionTypeID))
                .ForMember(dest => dest.ConstructionName, opt => opt.MapFrom(src => src.ConstructionName))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.TotalArea, opt => opt.MapFrom(src => src.TotalArea))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpectedCompletionDate, opt => opt.MapFrom(src => src.ExpectedCompletionDate));

            CreateMap<ConstructionDTO, Construction>()
                .ForMember(dest => dest.ConstructionType,
                    opt => opt.MapFrom(src => new ConstructionType { ID = src.ConstructionTypeID }))
                .ForMember(dest => dest.ConstructionStatus,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ConstructionItems,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DesignBlueprint,
                    opt => opt.Ignore());

            CreateMap<ConstructionCreateDTO, Construction>()
                .ForMember(dest => dest.ConstructionStatus,
                    opt => opt.MapFrom(src => new ConstructionStatus { ID = src.ConstructionStatusID }))
                .ForMember(dest => dest.ConstructionType,
                    opt => opt.MapFrom(src => new ConstructionType { ID = src.ConstructionTypeID }))
                .ForMember(dest => dest.ConstructionItems,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DesignBlueprint,
                    opt => opt.Ignore());
            CreateMap<WorkShift, WorkShiftDTO>();
            CreateMap<WorkShiftDTOPOST, WorkShift>()
                  .ForMember(dest => dest.ShiftDetails,
                    opt => opt.MapFrom(src => src.ShiftDetails));
            CreateMap<WorkShiftDTOPUT, WorkShift>()
            .ForMember(dest => dest.ShiftDetails, opt => opt.MapFrom(src => src.ShiftDetails));
            CreateMap<ShiftDetail, ShiftDetailDTO>();
            CreateMap<ShiftDetailDTO, ShiftDetail>().ReverseMap();
            CreateMap<AttendanceMachine, AttendanceMachineDTO>();
            CreateMap<AttendanceMachineDTO, AttendanceMachine>().ReverseMap();
            CreateMap<EmployeeRequests, EmployeeRequestDTO>()
            .ForMember(dest => dest.LeaveTypeName,
                opt => opt.MapFrom(src => src.LeaveType.LeaveTypeName))
             .ForMember(dest => dest.OvertimeTypeName,
                opt => opt.MapFrom(src => src.OvertimeType.OvertimeTypeName))
             .ForMember(dest => dest.OvertimeFormName,
                opt => opt.MapFrom(src => src.OvertimeForm.OvertimeFormName))
             .ForMember(dest => dest.coefficient,
                opt => opt.MapFrom(src => src.OvertimeType.coefficient))
             .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
               .ForMember(dest => dest.ApproveStatus,
                opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ApproveStatus)));

            CreateMap<PayrollAdjustment, PayrollAdjustmentDTO>()
            .ForMember(dest => dest.decisionDate,
                opt => opt.MapFrom(src => src.decisionDate.ToString("yyyy-MM-ddTHH:mm:ss")))
            .ForMember(dest => dest.AdjustmentTypeName,
                opt => opt.MapFrom(src => src.AdjustmentType.AdjustmentTypeName))
             .ForMember(dest => dest.AdjustmentItemID,
                opt => opt.MapFrom(src => src.AdjustmentItemID ?? 0))
                         .ForMember(dest => dest.AdjustmentItemName,
                opt => opt.MapFrom(src => src.AdjustmentItem != null ? src.AdjustmentItem.AdjustmentItemName : ""))
             .ForMember(dest => dest.Employees,
                opt => opt.MapFrom(src => src.applicationUser_PayrollAdjustment.
                Select(a => new PayrollAdjustmentEmployeeDTO
                {
                    EmployeeID = a.EmployeeID,
                    EmployeeName = a.applicationUser.FirstName + " " + a.applicationUser.LastName,  // hoặc UserName tuỳ model
                    Value = a.Value
                })))
               .ForMember(dest => dest.ApproveStatus,
                opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ApproveStatus)));

            // Leave Request Mappings
            CreateMap<EmployeeRequests, LeaveRequestDTO>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.LeaveTypeName,
                    opt => opt.MapFrom(src => src.LeaveType != null ? src.LeaveType.LeaveTypeName : ""))
                .ForMember(dest => dest.WorkShiftName,
                    opt => opt.MapFrom(src => src.WorkShift != null ? src.WorkShift.ShiftName : ""))
                .ForMember(dest => dest.ApproveStatus,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ApproveStatus)));

            CreateMap<LeaveRequestDTOPOST, EmployeeRequests>()
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => "Nghỉ phép"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ApproveStatus, opt => opt.MapFrom(src => ApproveStatusEnum.Created))
                .ForMember(dest => dest.OvertimeTypeID, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeFormID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeType, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeForm, opt => opt.Ignore());

            CreateMap<LeaveRequestDTOPUT, EmployeeRequests>()
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => "Nghỉ phép"))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ApproveStatus, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeTypeID, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeFormID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeType, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeForm, opt => opt.Ignore());

            // LeaveType mapping
            CreateMap<LeaveType, LeaveTypeDTO>();

            // OvertimeType mapping
            CreateMap<OvertimeType, OvertimeTypeDTO>();

            // OvertimeForm mapping
            CreateMap<OvertimeForm, OvertimeFormDTO>();

            // Overtime Request Mappings
            CreateMap<EmployeeRequests, OvertimeRequestDTO>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.OvertimeTypeName,
                    opt => opt.MapFrom(src => src.OvertimeType != null ? src.OvertimeType.OvertimeTypeName : ""))
                .ForMember(dest => dest.OvertimeFormName,
                    opt => opt.MapFrom(src => src.OvertimeForm != null ? src.OvertimeForm.OvertimeFormName : ""))
                .ForMember(dest => dest.Coefficient,
                    opt => opt.MapFrom(src => src.OvertimeType != null ? src.OvertimeType.coefficient : 0))
                .ForMember(dest => dest.ApproveStatus,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ApproveStatus)));

            CreateMap<OvertimeRequestDTOPOST, EmployeeRequests>()
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => "Tăng ca"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ApproveStatus, opt => opt.MapFrom(src => ApproveStatusEnum.Created))
                .ForMember(dest => dest.LeaveTypeID, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShiftID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeType, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeForm, opt => opt.Ignore());

            CreateMap<OvertimeRequestDTOPUT, EmployeeRequests>()
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => "Tăng ca"))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ApproveStatus, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveTypeID, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShiftID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShift, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeType, opt => opt.Ignore())
                .ForMember(dest => dest.OvertimeForm, opt => opt.Ignore());

            // Employee mappings
            CreateMap<EmployeeDTOPOST, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.joinDate, opt => opt.MapFrom(src => src.JoinDate))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.ShiftAssignments, opt => opt.Ignore())
                .ForMember(dest => dest.ExportOrders, opt => opt.Ignore())
                .ForMember(dest => dest.Reports, opt => opt.Ignore())
                .ForMember(dest => dest.ConstructionPlans, opt => opt.Ignore())
                .ForMember(dest => dest.ImportOrders, opt => opt.Ignore())
                .ForMember(dest => dest.ImportOrderEmployees, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeRequests, opt => opt.Ignore())
                .ForMember(dest => dest.applicationUser_PayrollAdjustment, opt => opt.Ignore())
                .ForMember(dest => dest.Contracts, opt => opt.Ignore())
                .ForMember(dest => dest.Payrolls, opt => opt.Ignore())
                .ForMember(dest => dest.PayrollFeedbacks, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSheets, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSheetFeedbacks, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.Ignore());

            CreateMap<EmployeeDTOPUT, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.joinDate, opt => opt.MapFrom(src => src.JoinDate))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.ShiftAssignments, opt => opt.Ignore())
                .ForMember(dest => dest.ExportOrders, opt => opt.Ignore())
                .ForMember(dest => dest.Reports, opt => opt.Ignore())
                .ForMember(dest => dest.ConstructionPlans, opt => opt.Ignore())
                .ForMember(dest => dest.ImportOrders, opt => opt.Ignore())
                .ForMember(dest => dest.ImportOrderEmployees, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeRequests, opt => opt.Ignore())
                .ForMember(dest => dest.applicationUser_PayrollAdjustment, opt => opt.Ignore())
                .ForMember(dest => dest.Contracts, opt => opt.Ignore())
                .ForMember(dest => dest.Payrolls, opt => opt.Ignore())
                .ForMember(dest => dest.PayrollFeedbacks, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSheets, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSheetFeedbacks, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore());

            // Add mapping for EmployeeDTOPUT to EmployeeDTO
            CreateMap<EmployeeDTOPUT, EmployeeDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.RoleID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                .ForMember(dest => dest.StatusName, opt => opt.Ignore());

            // AdjustmentType and AdjustmentItem mappings
            CreateMap<AdjustmentType, AdjustmentTypeDTO>();
            CreateMap<AdjustmentItem, AdjustmentItemDTO>()
                .ForMember(dest => dest.AdjustmentTypeName, 
                    opt => opt.MapFrom(src => src.adjustmentType.AdjustmentTypeName));

            // PayrollAdjustment POST/PUT mappings
            CreateMap<PayrollAdjustmentDTOPOST, PayrollAdjustmentDTO>()
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.month))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.year))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.reason))
                .ForMember(dest => dest.AdjustmentTypeID, opt => opt.MapFrom(src => src.adjustmentTypeID))
                .ForMember(dest => dest.AdjustmentItemID, opt => opt.MapFrom(src => src.adjustmentItemID))
                .ForMember(dest => dest.AdjustmentTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.AdjustmentItemName, opt => opt.Ignore())
                .ForMember(dest => dest.ApproveStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.employees));

            CreateMap<PayrollAdjustmentDTOPUT, PayrollAdjustmentDTO>()
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.month))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.year))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.reason))
                .ForMember(dest => dest.AdjustmentTypeID, opt => opt.MapFrom(src => src.adjustmentTypeID))
                .ForMember(dest => dest.AdjustmentItemID, opt => opt.MapFrom(src => src.adjustmentItemID))
                .ForMember(dest => dest.AdjustmentTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.AdjustmentItemName, opt => opt.Ignore())
                .ForMember(dest => dest.ApproveStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.employees));

            CreateMap<PayrollAdjustmentEmployeeDTOPOST, PayrollAdjustmentEmployeeDTO>()
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.employeeID))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.employeeName))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.value));

            CreateMap<PayrollAdjustmentEmployeeDTOPUT, PayrollAdjustmentEmployeeDTO>()
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.employeeID))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.employeeName))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.value));

            // FamilyRelation mappings
            CreateMap<FamilyRelation, FamilyRelationDTO>()
                .ForMember(dest => dest.EmployeeID, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeName, opt => opt.Ignore())
                .ForMember(dest => dest.RelationShipName, opt => opt.Ignore());

            CreateMap<Employee_FamilyRelation, FamilyRelationDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.FamilyRelationID))
                .ForMember(dest => dest.RelativeName, opt => opt.MapFrom(src => src.FamilyRelation.RelativeName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.FamilyRelation.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.FamilyRelation.EndDate))
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.RelationShipName, opt => opt.MapFrom(src => src.RelationShipName));

            CreateMap<FamilyRelationDTOPOST, FamilyRelation>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeFamilyRelations, opt => opt.Ignore());

            CreateMap<FamilyRelationDTOPUT, FamilyRelation>()
                .ForMember(dest => dest.EmployeeFamilyRelations, opt => opt.Ignore());

            CreateMap<FamilyRelationDTOPOST, Employee_FamilyRelation>()
                .ForMember(dest => dest.FamilyRelationID, opt => opt.Ignore())
                .ForMember(dest => dest.FamilyRelation, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            CreateMap<FamilyRelationDTOPUT, Employee_FamilyRelation>()
                .ForMember(dest => dest.FamilyRelationID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.FamilyRelation, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

        }
    }
}

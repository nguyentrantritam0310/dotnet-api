using AutoMapper;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Helpers;

namespace dotnet_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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
            CreateMap<ApplicationUser, EmployeeDTO>();
            CreateMap<ExportOrder, ExportOrderDTO>()
                 .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " +  src.Employee.LastName))
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
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<ConstructionDTOPOST, Construction>()
                .ForMember(dest => dest.ConstructionItems, 
                    opt => opt.MapFrom(src => src.ConstructionItems));

            CreateMap<ConstructionItemDTOPOST, ConstructionItem>()
                .ForMember(dest => dest.UnitOfMeasurement, 
                    opt => opt.MapFrom(src => new UnitofMeasurement { UnitName = src.UnitOfMeasurement }))
                .ForMember(dest => dest.ConstructionStatus, 
                    opt => opt.MapFrom(src => new ConstructionStatus { Name = ConstructionStatusEnum.Pending }));

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
            CreateMap<EmployeeRequest, WorkShiftDTO>();
            CreateMap<ShiftDetail, ShiftDetailDTO>();
            CreateMap<AttendanceMachine, AttendanceMachineDTO>();
            CreateMap<AttendanceMachineDTO, AttendanceMachine>().ReverseMap();


        }
    }
}

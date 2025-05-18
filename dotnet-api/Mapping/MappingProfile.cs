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
            CreateMap<Attendance, AttendanceDTO>();
            CreateMap<Construction, ConstructionDTO>()
            .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
            .ForMember(dest => dest.ConstructionItems,
                    opt => opt.MapFrom(src => src.ConstructionItems
                    .ToList()));
            //.ForMember(dest => dest.ItemStatusName,
            //    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)))
            //.ForMember(dest => dest.unitName,
            //    opt => opt.MapFrom(src => src.Unit));
            CreateMap<ConstructionItem, ConstructionItemDTO>();
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
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)));

            CreateMap<ConstructionPlanDTOPOST, ConstructionPlan>().ReverseMap();
            CreateMap<ConstructionPlanDTOPUT, ConstructionPlan>().ReverseMap();
            CreateMap<ConstructionStatus, ConstructionStatusDTO>();
            CreateMap<ConstructionTemplateItem, ConstructionTemplateItemDTO>()
                .ForMember(dest => dest.ConstructionTypeName,
                    opt => opt.MapFrom(src => src.ConstructionType.ConstructionTypeName))
                .ForMember(dest => dest.WorkSubTypeVariantName,
                    opt => opt.MapFrom(src => src.WorkSubTypeVariant.Description))
                ;
            CreateMap<ConstructionTask, ConstructionTaskDTO>();
            CreateMap<ConstructionType, ConstructionTypeDTO>();
            CreateMap<ApplicationUser, EmployeeDTO>();
            CreateMap<ExportOrder, ExportOrderDTO>()
                 .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " +  src.Employee.LastName))
                 .ForMember(dest => dest.ConstructionItemName,
                    opt => opt.MapFrom(src => src.ConstructionPlan.ConstructionItem.ConstructionItemName))
                    .ForMember(dest => dest.ConstructionName,
                    opt => opt.MapFrom(src => src.ConstructionPlan.ConstructionItem.Construction.ConstructionName))
                    .ForMember(dest => dest.materialName,
                    opt => opt.MapFrom(src => src.Material_ExportOrders.Select(m => m.Material.MaterialName)))
                ;
            CreateMap<ImportOrder, ImportOrderDTO>();
            CreateMap<Material_ExportOrder, Material_ExportOrderDTO>();
            CreateMap<Material, MaterialDTO>()
                .ForMember(dest => dest.MaterialTypeName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialType.MaterialTypeName)));

            CreateMap<MaterialNorm, MaterialNormDTO>();
            CreateMap<MaterialPlan, MaterialPlanDTO>();
            CreateMap<MaterialType, MaterialTypeDTO>()
                                .ForMember(dest => dest.MaterialTypeName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialTypeName)));
            ;
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
        }
    }
}

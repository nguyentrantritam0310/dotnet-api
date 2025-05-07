using AutoMapper;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
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
            CreateMap<ConstructionItem, ConstructionItemDTO>();
            CreateMap<ConstructionPlan, ConstructionPlanDTO>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                .ForMember(dest => dest.ConstructionItemName,
                    opt => opt.MapFrom(src => src.ConstructionItem.ConstructionItemName))
                .ForMember(dest => dest.ConstructionName,
                    opt => opt.MapFrom(src => src.ConstructionItem.Construction.ConstructionName))
                .ForMember(dest => dest.StatusName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.ConstructionStatus.Name)));
            CreateMap<ConstructionStatus, ConstructionStatusDTO>();
            CreateMap<ConstructionTask, ConstructionTaskDTO>();
            CreateMap<ConstructionType, ConstructionTypeDTO>();
            CreateMap<ApplicationUser, EmployeeDTO>();
            CreateMap<ExportOrder, ExportOrderDTO>();
            CreateMap<ImportOrder, ImportOrderDTO>();
            CreateMap<Material_ExportOrder, Material_ExportOrderDTO>();
            CreateMap<Material, MaterialDTO>()
                .ForMember(dest => dest.MaterialTypeName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialType.MaterialTypeName)));

            CreateMap<MaterialNorm, MaterialNormDTO>();
            CreateMap<MaterialPlan, MaterialPlanDTO>();
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
                    .ToList()));
            CreateMap<ReportStatusLog, ReportStatusLogDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UnitofMeasurement, UnitofMeasurementDTO>();
        }
    }
}

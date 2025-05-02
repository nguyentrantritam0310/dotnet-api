using AutoMapper;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.Helpers;

namespace dotnet_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>();
            CreateMap<Construction, ConstructionDTO>();
            CreateMap<ConstructionItem, ConstructionItemDTO>();
            CreateMap<ConstructionPlan, ConstructionPlanDTO>();
            CreateMap<ConstructionStatus, ConstructionStatusDTO>();
            CreateMap<ConstructionTask, ConstructionTaskDTO>();
            CreateMap<ConstructionType, ConstructionTypeDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<ExportOrder, ExportOrderDTO>();
            CreateMap<ImportOrder, ImportOrderDTO>();
            CreateMap<Material_ExportOrder, Material_ExportOrderDTO>();
            //CreateMap<Material, MaterialDTO>();
            //        CreateMap<Material, MaterialDTO>()
            //.ForMember(dest => dest.MaterialTypeName,
            //           opt => opt.MapFrom(src => src.MaterialType.ToString()));

            CreateMap<Material, MaterialDTO>()
                .ForMember(dest => dest.MaterialTypeName,
                    opt => opt.MapFrom(src => EnumHelper.GetDisplayName(src.MaterialType.MaterialTypeName)));


            CreateMap<MaterialNorm, MaterialNormDTO>();
            CreateMap<MaterialPlan, MaterialPlanDTO>();
            CreateMap<MaterialType, MaterialTypeDTO>();
            CreateMap<ReportAttachment, ReportAttachmentDTO>();
            CreateMap<Report, ReportDTO>();
            CreateMap<ReportStatusLog, ReportStatusLogDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UnitofMeasurement, UnitofMeasurementDTO>();
        }
    }
}

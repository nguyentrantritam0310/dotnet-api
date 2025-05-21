using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IUnitofMeasurementService
    {
        Task<IEnumerable<UnitofMeasurementDTO>> GetAllUnitofMeasurementAsync();

    }
}
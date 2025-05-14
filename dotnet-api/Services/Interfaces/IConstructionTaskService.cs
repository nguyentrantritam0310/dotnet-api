using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionTaskService
    {
        //Task<ConstructionTaskDTO> CreateConstructionTaskAsync(ConstructionTaskDTOPOST constructionTaskDTO);
        //Task<ConstructionTaskDTO> GetConstructionTaskByIdAsync(int id);
        Task<IEnumerable<ConstructionTaskDTO>> GetAllConstructionsTaskByPlanAsync(int id);
        //Task<ConstructionTaskDTO> UpdateConstructionTaskAsync(ConstructionTaskDTOPUT constructionTaskDTO);
        //Task<ConstructionTaskDTO> UpdateConstructionTaskStatusAsync(int id, int status);
    }
}

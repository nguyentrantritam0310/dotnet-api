using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IFaceRegistrationService
    {
        Task<FaceRegistrationResultDTO> RegisterFaceAsync(CreateFaceRegistrationDTO request);
        Task<FaceVerificationResultDTO> VerifyFaceAsync(FaceVerificationRequestDTO request);
        Task<List<FaceRegistrationListDTO>> GetUserFaceRegistrationsAsync(string employeeId);
        Task<bool> DeleteFaceRegistrationAsync(int faceRegistrationId, string employeeId);
        Task<FaceRegistrationDTO> GetFaceRegistrationByIdAsync(int id);
        Task<bool> UpdateFaceRegistrationAsync(int id, string notes, string employeeId);
    }
}



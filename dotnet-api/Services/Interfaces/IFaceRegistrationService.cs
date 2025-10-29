using dotnet_api.DTOs;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IFaceRegistrationService
    {
        Task<FaceRegistrationResultDTO> RegisterFaceAsync(CreateFaceRegistrationDTO request, IFormFile imageFile);
        Task<FaceVerificationResultDTO> VerifyFaceAsync(FaceVerificationRequestDTO request);
        Task<List<FaceRegistrationListDTO>> GetUserFaceRegistrationsAsync(string employeeId);
        Task<bool> DeleteFaceRegistrationAsync(int faceRegistrationId, string employeeId);
        Task<FaceRegistrationDTO> GetFaceRegistrationByIdAsync(int id);
        Task<bool> UpdateFaceRegistrationAsync(int id, string notes, string employeeId);
    }
}



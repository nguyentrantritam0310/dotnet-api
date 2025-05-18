using dotnet_api.DTOs.POST;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using dotnet_api.Data.Entities;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitofMeasurementController : ControllerBase
    {
        private readonly IUnitofMeasurementService _UnitofMeasurementService;

        public UnitofMeasurementController(IUnitofMeasurementService UnitofMeasurementService)
        {
            _UnitofMeasurementService = UnitofMeasurementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var UnitofMeasurement = await _UnitofMeasurementService.GetAllUnitofMeasurementAsync();
            return Ok(UnitofMeasurement);
        }



    }
}
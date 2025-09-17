using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkShiftController : ControllerBase
    {
        private readonly IWorkShiftService _workShiftService;
        private readonly IMapper _mapper;

        public WorkShiftController(IWorkShiftService workShiftService, IMapper mapper)
        {
            _workShiftService = workShiftService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workShift = await _workShiftService.GetAllWorkShiftsAsync();
            return Ok(workShift);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var construction = await _WorkShiftService.GetConstructionByIdAsync(id);
        //    if (construction == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(construction);
        //}

       
    }
}

using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimeFormController : ControllerBase
    {
        private readonly IOvertimeFormService _overtimeFormService;

        public OvertimeFormController(IOvertimeFormService overtimeFormService)
        {
            _overtimeFormService = overtimeFormService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var overtimeForms = await _overtimeFormService.GetAllOvertimeFormsAsync();
            return Ok(overtimeForms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var overtimeForm = await _overtimeFormService.GetOvertimeFormByIdAsync(id);
            if (overtimeForm == null)
            {
                return NotFound(new { message = $"OvertimeForm with ID {id} not found." });
            }
            return Ok(overtimeForm);
        }
    }
}





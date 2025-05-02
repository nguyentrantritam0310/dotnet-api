using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _ReportService;

        public ReportController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Reports = await _ReportService.GetAllReportAsync();
            return Ok(Reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Report = await _ReportService.GetReportByIdAsync(id);
            if (Report == null)
            {
                return NotFound();
            }
            return Ok(Report);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReportDTO ReportDTO)
        {
            var createdReport = await _ReportService.CreateReportAsync(ReportDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdReport.ID }, createdReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportDTO ReportDTO)
        {
            if (id != ReportDTO.ID)
            {
                return BadRequest();
            }

            var updatedReport = await _ReportService.UpdateReportAsync(ReportDTO);
            if (updatedReport == null)
            {
                return NotFound();
            }

            return Ok(updatedReport);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ReportService.DeleteReportAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetAllReportAsync();
            return Ok(reports);
        }

        [HttpGet("thicong")]
        public async Task<IActionResult> GetAllByThiCong()
        {
            var reports = await _reportService.GetAllReportByThiCongAsync();
            return Ok(reports);
        }

        [HttpGet("kithuat")]
        public async Task<IActionResult> GetAllByKiThuat()
        {
            var reports = await _reportService.GetAllReportByKiThuatAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ReportCreateDTO reportDTO)
        {
            try
            {
                //var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //if (string.IsNullOrEmpty(employeeId))
                //{
                //    return Unauthorized();
                //}

                var createdReport = await _reportService.CreateReportAsync(reportDTO, "tech1-id");
                return CreatedAtAction(nameof(GetById), new { id = createdReport.ID }, createdReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ReportUpdateDTO reportDTO)
        {
            if (id != reportDTO.ID)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            if (string.IsNullOrEmpty(reportDTO.Content) || string.IsNullOrEmpty(reportDTO.Level))
            {
                return BadRequest(new { message = "Content and Level are required fields" });
            }

            try
            {
                var updatedReport = await _reportService.UpdateReportAsync(reportDTO);
                if (updatedReport == null)
                {
                    return NotFound(new { message = "Report not found" });
                }

                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ReportUpdateStatusDTO statusDTO)
        {
            if (id != statusDTO.ID)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                var updatedReport = await _reportService.UpdateReportStatusAsync(id, statusDTO);
                if (updatedReport == null)
                {
                    return NotFound(new { message = "Report not found" });
                }

                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reportService.DeleteReportAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

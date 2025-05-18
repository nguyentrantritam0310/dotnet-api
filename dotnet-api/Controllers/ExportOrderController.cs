 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_api.Services;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportOrderController : ControllerBase
    {
        private readonly IExportOrderService _exportOrderService;

        public ExportOrderController(IExportOrderService exportOrderService)
        {
            _exportOrderService = exportOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportOrderDTO>>> GetAllExportOrders()
        {
            var exportOrders = await _exportOrderService.GetAllExportOrders();
            return Ok(exportOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExportOrderDTO>> GetExportOrderById(int id)
        {
            var exportOrder = await _exportOrderService.GetExportOrderById(id);
            if (exportOrder == null)
                return NotFound();
            return Ok(exportOrder);
        }

        [HttpPost]
        public async Task<ActionResult<ExportOrderDTO>> CreateExportOrder(ExportOrderDTOPOST exportOrderDTO)
        {
            try
            {
                var createdOrder = await _exportOrderService.CreateExportOrder(exportOrderDTO);
                return CreatedAtAction(nameof(GetExportOrderById), new { id = createdOrder.ID }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExportOrderDTO>> UpdateExportOrder(int id, ExportOrderDTO exportOrderDTO)
        {
            var updatedOrder = await _exportOrderService.UpdateExportOrder(id, exportOrderDTO);
            if (updatedOrder == null)
                return NotFound();
            return Ok(updatedOrder);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportOrder(int id)
        {
            var result = await _exportOrderService.DeleteExportOrder(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
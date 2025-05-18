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
    public class ImportOrderController : ControllerBase
    {
        private readonly IImportOrderService _ImportOrderService;

        public ImportOrderController(IImportOrderService ImportOrderService)
        {
            _ImportOrderService = ImportOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportOrderDTO>>> GetAllImportOrdersByManager()
        {
            var importOrders = await _ImportOrderService.GetAllImportOrdersByManager();
            return Ok(importOrders);
        }

        [HttpGet("director")]
        public async Task<ActionResult<IEnumerable<ImportOrderDTO>>> GetAllImportOrdersByDirector()
        {
            var importOrders = await _ImportOrderService.GetAllImportOrdersByDirector();
            return Ok(importOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImportOrderDTO>> GetImportOrderById(int id)
        {
            var ImportOrder = await _ImportOrderService.GetImportOrderById(id);
            if (ImportOrder == null)
                return NotFound();
            return Ok(ImportOrder);
        }

        [HttpPost]
        public async Task<ActionResult<ImportOrderDTOPOST>> CreateImportOrder(ImportOrderDTOPOST importOrderDTO)
        {
            try
            {
                var createdOrder = await _ImportOrderService.CreateImportOrder(importOrderDTO);
                return CreatedAtAction(nameof(GetImportOrderById), new { id = createdOrder.ID }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            var updatedOrder = await _ImportOrderService.UpdateImportOrderStatusAsync(id, statusDTO.Status);
            if (updatedOrder == null)
                return NotFound();

            return Ok(updatedOrder);
        }
    }
}
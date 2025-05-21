using dotnet_api.DTOs;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialExportOrderController : ControllerBase
    {
        private readonly IMaterial_ExportOrderService _Material_ExportOrderService;

        public MaterialExportOrderController(IMaterial_ExportOrderService Material_ExportOrderService)
        {
            _Material_ExportOrderService = Material_ExportOrderService;
        }


        [HttpGet("{exportOrderId}")]
        public async Task<IActionResult> GetById(int exportOrderId)
        {
            var MaterialExportOrder = await _Material_ExportOrderService.GetMaterial_ExportOrderById(exportOrderId);
            if (MaterialExportOrder == null)
            {
                return NotFound();
            }
            return Ok(MaterialExportOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Material_ExportOrderDTO Material_ExportOrderDTO)
        {
            var createdMaterial_ExportOrder = await _Material_ExportOrderService.CreateMaterial_ExportOrderAsync(Material_ExportOrderDTO);
            return CreatedAtAction(nameof(GetById), new { exportOrderId = createdMaterial_ExportOrder.ExportOrderID }, createdMaterial_ExportOrder);
        }
    }
}

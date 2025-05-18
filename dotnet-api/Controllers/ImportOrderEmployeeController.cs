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
    public class ImportOrderEmployeeController : ControllerBase
    {
        private readonly IImportOrderEmployeeService _ImportOrderEmployeeService;

        public ImportOrderEmployeeController(IImportOrderEmployeeService ImportOrderEmployeeService)
        {
            _ImportOrderEmployeeService = ImportOrderEmployeeService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ImportOrderEmployeeDTO>>> GetAllImportOrderEmployeesByManager()
        //{
        //    var ImportOrderEmployees = await _ImportOrderEmployeeService.GetAllImportOrderEmployeesByManager();
        //    return Ok(ImportOrderEmployees);
        //}

        [HttpGet("{importOrderID}/{employeeID}")]
        public async Task<ActionResult<ImportOrderEmployeeDTO>> GetImportOrderEmployeeById(int importOrderID, string employeeID)
        {
            var ImportOrderEmployee = await _ImportOrderEmployeeService.GetImportOrderEmployeeById(importOrderID, employeeID);
            if (ImportOrderEmployee == null)
                return NotFound();
            return Ok(ImportOrderEmployee);
        }

        [HttpPost]
        public async Task<ActionResult<ImportOrderEmployeeDTOPOST>> CreateImportOrderEmployee(ImportOrderEmployeeDTOPOST ImportOrderEmployeeDTO)
        {
            try
            {
                var createdOrder = await _ImportOrderEmployeeService.CreateImportOrderEmployee(ImportOrderEmployeeDTO);
                return CreatedAtAction(
            nameof(GetImportOrderEmployeeById),
            new { importOrderId = createdOrder.ImportOrderId, employeeId = createdOrder.EmployeeID },
            createdOrder
        );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
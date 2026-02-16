using JK_Inventory.Application.DTOs;
using JK_Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JK_Inventory.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JK_VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;

        public JK_VehiclesController(IVehicleService service)
        {
            _service = service;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var vehicles = await _service.GetAllVehicles();
			return Ok(vehicles);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var vehicle = await _service.GetVehicleById(id);
			return Ok(vehicle);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] JK_CreateVehicleDto dto)
		{
			await _service.CreateVehicle(dto);
			return Ok();
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateStatus(int id, [FromBody] JK_UpdateVehicleStatusDto dto)
		{
			var vehicle = await _service.UpdateVehicleStatus(id, dto);
			return Ok(vehicle);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.DeleteVehicle(id);
			return NoContent();
		}
	}
}

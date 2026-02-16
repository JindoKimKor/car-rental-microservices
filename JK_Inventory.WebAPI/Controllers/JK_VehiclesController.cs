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
			throw new NotImplementedException();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] JK_CreateVehicleDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateStatus(int id, [FromBody] JK_UpdateVehicleStatusDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}

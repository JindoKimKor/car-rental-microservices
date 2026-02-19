using Maintenance.WebAPI.Models;
using Maintenance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairHistoryController : ControllerBase
    {
		private readonly IRepairHistoryService _repairHistoryService;
		private readonly Dictionary<string, int> _usageCounts;

		public RepairHistoryController(IRepairHistoryService repairHistoryService, Dictionary<string, int> usageCounts)
		{
			_repairHistoryService = repairHistoryService;
			_usageCounts = usageCounts;
		}

		[HttpGet("{vehicleId}")]
		public IActionResult GetRepairHistory(int vehicleId)
		{
			var history = _repairHistoryService.GetByVehicleId(vehicleId);
			return Ok(history);
		}

		[HttpPost]
		public IActionResult AddRepair([FromBody] RepairHistoryDto repair)
		{
			if (repair.VehicleId <= 0)
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "VehicleId must be greater than zero."
				});
			}

			if (string.IsNullOrWhiteSpace(repair.Description))
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "Description must not be empty."
				});
			}

			if (repair.Cost < 0)
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "Cost cannot be negative."
				});
			}

			var created = _repairHistoryService.AddRepair(repair);

			return CreatedAtAction(
				nameof(GetRepairHistory),
				new { vehicleId = created.VehicleId },
				created
			);
		}

		[HttpGet("crash")]
		public IActionResult Crash()
		{
			int x = 0;
			int y = 5 / x;
			return Ok();
		}

		[HttpGet("usage")]
		public IActionResult Usage()
		{
			var key = Request.Headers["X-Api-Key"].ToString();

			if (!_usageCounts.ContainsKey(key))
				_usageCounts[key] = 0;

			_usageCounts[key]++;

			return Ok(new
			{
				clientId = key,
				callCount = _usageCounts[key]
			});
		}
	}
}

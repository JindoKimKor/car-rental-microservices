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

		[HttpPost("transfer")]
		public IActionResult Transfer(int fromId, int toId, decimal amount)
		{
			if (fromId <= 0 || toId <= 0)
				return BadRequest(new { error = "InvalidParameter", message = "Repair IDs must be greater than zero." });

			if (amount <= 0)
				return BadRequest(new { error = "InvalidParameter", message = "Amount must be greater than zero." });

			var fromRepair = _repairHistoryService.GetById(fromId);
			var toRepair = _repairHistoryService.GetById(toId);

			if (fromRepair == null)
				return NotFound(new { error = "NotFound", message = $"Repair record {fromId} not found." });

			if (toRepair == null)
				return NotFound(new { error = "NotFound", message = $"Repair record {toId} not found." });

			if (amount > fromRepair.Cost)
				return BadRequest(new { error = "InsufficientFunds", message = $"Repair {fromId} only has ${fromRepair.Cost:F2}." });

			fromRepair.Cost -= amount;
			toRepair.Cost += amount;

			return Ok(new
			{
				message = $"Transferred ${amount:F2} from repair {fromId} to repair {toId}.",
				fromRepairId = fromId,
				toRepairId = toId,
				amount
			});
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

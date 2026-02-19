using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services
{
	public class FakeRepairHistoryService : IRepairHistoryService
	{
		private int _nextId = 3;

		// In-memory store with seed data
		private readonly List<RepairHistoryDto> _repairs = new()
		{
			new RepairHistoryDto
			{
				Id = 1,
				VehicleId = 1,
				RepairDate = DateTime.Now.AddDays(-10),
				Description = "Oil change",
				Cost = 89.99m,
				PerformedBy = "Quick Lube"
			},
			new RepairHistoryDto
			{
				Id = 2,
				VehicleId = 1,
				RepairDate = DateTime.Now.AddDays(-40),
				Description = "Brake pad replacement",
				Cost = 350.00m,
				PerformedBy = "Auto Repair Pro"
			}
		};

		public List<RepairHistoryDto> GetByVehicleId(int vehicleId)
		{
			return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
		}

		public RepairHistoryDto AddRepair(RepairHistoryDto repair)
		{
			repair.Id = _nextId++;
			_repairs.Add(repair);
			return repair;
		}
	}
}

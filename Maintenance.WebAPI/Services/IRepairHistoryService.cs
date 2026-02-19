using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services
{
    public interface IRepairHistoryService
    {
		List<RepairHistoryDto> GetByVehicleId(int vehicleId);
		RepairHistoryDto? GetById(int id);
		RepairHistoryDto AddRepair(RepairHistoryDto repair);
	}
}

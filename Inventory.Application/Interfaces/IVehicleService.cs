using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces
{
	public interface IVehicleService
	{
		Task<VehicleDto?> GetByIdAsync(int id);
		Task<IEnumerable<VehicleDto>> GetAllAsync();
		Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
		Task<VehicleDto> UpdateStatusAsync(int id, UpdateVehicleStatusDto dto);
		Task DeleteAsync(int id);
	}
}

using JK_Inventory.Application.DTOs;

namespace JK_Inventory.Application.Interfaces
{
	public interface IVehicleService
	{
		Task<JK_VehicleDto?> GetByIdAsync(int id);
		Task<IEnumerable<JK_VehicleDto>> GetAllAsync();
		Task<JK_VehicleDto> CreateAsync(JK_CreateVehicleDto dto);
		Task<JK_VehicleDto> UpdateStatusAsync(int id, JK_UpdateVehicleStatusDto dto);
		Task DeleteAsync(int id);
	}
}

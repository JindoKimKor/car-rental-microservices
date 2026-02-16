using JK_Inventory.Application.DTOs;

namespace JK_Inventory.Application.Interfaces
{
	public interface IVehicleService
	{
		Task<JK_VehicleDto?> GetVehicleById(int id);
		Task<IEnumerable<JK_VehicleDto>> GetAllVehicles();
		Task CreateVehicle(JK_CreateVehicleDto dto);
		Task<JK_VehicleDto> UpdateVehicleStatus(int id, JK_UpdateVehicleStatusDto dto);
		Task DeleteVehicle(int id);
	}
}

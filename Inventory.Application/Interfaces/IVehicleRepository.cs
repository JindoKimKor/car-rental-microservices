using Inventory.Domain.Entities;

namespace JK_Inventory.Application.Interfaces
{
	public interface IVehicleRepository
	{
		Task<Vehicle?> FindByIdAsync(int id);
		Task<IEnumerable<Vehicle>> FindAllAsync();
		Task SaveAsync(Vehicle vehicle);
		Task RemoveAsync(int id);
	}
}

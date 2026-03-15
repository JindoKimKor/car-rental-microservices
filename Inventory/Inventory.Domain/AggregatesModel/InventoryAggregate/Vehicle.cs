using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class Vehicle : Entity
	{
		public VehicleCode VehicleCode { get; private set; }

		public Vehicle(VehicleCode vehicleCode)
		{
			VehicleCode = vehicleCode;
		}

		public Vehicle(int id, VehicleCode vehicleCode)
		{
			Id = id;
			VehicleCode = vehicleCode;
		}
	}
}

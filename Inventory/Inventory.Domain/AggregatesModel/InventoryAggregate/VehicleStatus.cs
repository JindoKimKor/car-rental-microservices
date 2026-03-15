using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleStatus : Entity
	{
		public string Name { get; private set; }

		public static VehicleStatus Available = new(1, "Available");
		public static VehicleStatus Reserved = new(2, "Reserved");
		public static VehicleStatus Rented = new(3, "Rented");
		public static VehicleStatus Maintenance = new(4, "Maintenance");

		public VehicleStatus(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

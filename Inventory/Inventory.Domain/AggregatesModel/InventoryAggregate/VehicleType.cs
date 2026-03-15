using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleType : Entity
	{
		public string Name { get; private set; }

		public static VehicleType Sedan = new(1, "Sedan");
		public static VehicleType SUV = new(2, "SUV");
		public static VehicleType Truck = new(3, "Truck");
		public static VehicleType Van = new(4, "Van");

		public VehicleType(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

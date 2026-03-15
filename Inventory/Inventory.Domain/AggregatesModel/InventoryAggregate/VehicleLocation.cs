using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleLocation : Entity
	{
		public string Name { get; private set; }

		public static VehicleLocation Kitchener = new(1, "Kitchener");
		public static VehicleLocation Waterloo = new(2, "Waterloo");
		public static VehicleLocation Cambridge = new(3, "Cambridge");
		public static VehicleLocation Guelph = new(4, "Guelph");

		public VehicleLocation(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

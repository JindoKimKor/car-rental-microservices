using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleLocation : Entity
	{
		public string Name { get; private set; }

		public VehicleLocation(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

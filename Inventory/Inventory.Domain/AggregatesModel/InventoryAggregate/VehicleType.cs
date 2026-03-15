using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleType : Entity
	{
		public string Name { get; private set; }

		public VehicleType(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

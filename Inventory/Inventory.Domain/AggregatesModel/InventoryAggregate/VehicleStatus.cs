using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleStatus : Entity
	{
		public string Name { get; private set; }

		public VehicleStatus(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

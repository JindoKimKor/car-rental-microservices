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

		/// <summary>
		/// Validates whether a location ID corresponds to a valid VehicleLocationEnum value.
		/// </summary>
		public static bool IsValid(int id)
		{
			return Enum.IsDefined(typeof(VehicleLocationEnum), id);
		}
	}
}

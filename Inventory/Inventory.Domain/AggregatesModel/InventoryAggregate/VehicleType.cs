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

		/// <summary>
		/// Validates whether a type ID corresponds to a valid VehicleTypeEnum value.
		/// </summary>
		public static bool IsValid(int id)
		{
			return Enum.IsDefined(typeof(VehicleTypeEnum), id);
		}
	}
}

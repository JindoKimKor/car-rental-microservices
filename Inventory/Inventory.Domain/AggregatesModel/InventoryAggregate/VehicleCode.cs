using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleCode : ValueObject
	{
		public string Make { get; private set; }
		public string Model { get; private set; }
		public VehicleTypeEnum Type { get; private set; }

		private VehicleCode() { } // EF Core

		public VehicleCode(string make, string model, VehicleTypeEnum type)
		{
			Make = make;
			Model = model;
			Type = type;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Make;
			yield return Model;
			yield return Type;
		}

		// Checks if two vehicle codes represent the same vehicle type
		public bool IsSameVehicle(VehicleCode other) => Equals(other);

		public override string ToString() => $"{Make}-{Model}-{Type}";
	}
}

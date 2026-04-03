using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class VehicleCode : ValueObject
	{
		public string Make { get; private set; }
		public string Model { get; private set; }

		private VehicleCode() { } // EF Core

		/// <summary>
		/// ValueObject validates its own values on creation.
		/// Once created, it is immutable — invalid state cannot exist.
		/// </summary>
		/// <summary>
		/// ValueObject validates its own values on creation.
		/// Once created, it is immutable — invalid state cannot exist.
		/// VehicleCode = Make + Model only. Type is Vehicle's responsibility (FK).
		/// </summary>
		public VehicleCode(string make, string model)
		{
			if (string.IsNullOrWhiteSpace(make))
				throw new ArgumentException("Make is required.");
			if (string.IsNullOrWhiteSpace(model))
				throw new ArgumentException("Model is required.");

			Make = make;
			Model = model;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Make;
			yield return Model;
		}

		public bool IsSameVehicle(VehicleCode other) => Equals(other);

		public override string ToString() => $"{Make}-{Model}";
	}
}

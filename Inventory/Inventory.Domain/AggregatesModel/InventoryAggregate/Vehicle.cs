using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class Vehicle : Entity
	{
		public VehicleCode VehicleCode { get; private set; }
		public int VehicleTypeId { get; private set; }

		// Navigation — for domain access (EF Core populates on read)
		public VehicleType Type { get; private set; }

		private Vehicle() { } // EF Core

		/// <summary>
		/// Vehicle owns:
		///   - VehicleCode (ValueObject) — Make + Model, self-validates
		///   - VehicleTypeId (FK int) — category, Entity validates
		/// </summary>
		public Vehicle(string make, string model, VehicleTypeEnum type)
		{
			// Entity validates its own attribute
			if (!VehicleType.IsValid((int)type))
				throw new Exceptions.InvalidVehicleStateException($"Invalid vehicle type: {type}");

			VehicleCode = new VehicleCode(make, model);
			VehicleTypeId = (int)type;
		}
	}
}

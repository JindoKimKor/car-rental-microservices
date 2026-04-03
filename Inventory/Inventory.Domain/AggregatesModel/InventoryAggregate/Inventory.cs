using Inventory.Domain.Exceptions;
using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class Inventory : Entity, IAggregateRoot
	{
		// FK int — for persistence
		public int VehicleLocationId { get; private set; }
		public int VehicleStatusId { get; private set; }

		// Navigation — internal to Aggregate, EF Core populates on read
		public Vehicle Vehicle { get; private set; }
		public VehicleLocation Location { get; private set; }
		public VehicleStatus Status { get; private set; }

		// Aggregate Root exposes flat read-only properties.
		// External layers access these instead of navigating into Child Entities.
		// This prevents deep traversal like inventory.Vehicle.VehicleCode.Make
		public string Make => Vehicle.VehicleCode.Make;
		public string Model => Vehicle.VehicleCode.Model;
		public int VehicleTypeId => Vehicle.VehicleTypeId;

		private Inventory() { } // EF Core

		/// <summary>
		/// Constructor initializes Inventory's own attributes only.
		/// Vehicle is added separately via AddVehicle() — domain behavior.
		/// Ubiquitous Language: "Create inventory at location" → "Add vehicle to it"
		/// </summary>
		public Inventory(int locationId)
		{
			if (!VehicleLocation.IsValid(locationId))
				throw new InvalidVehicleStateException($"Invalid location: {locationId}");

			VehicleLocationId = locationId;
		}

		/// <summary>
		/// Domain behavior: Add a vehicle to this inventory.
		/// Aggregate Root controls Child Entity creation.
		/// Vehicle validates VehicleType + creates VehicleCode internally.
		/// Status automatically set to Available.
		/// </summary>
		public void AddVehicle(string make, string model, VehicleTypeEnum vehicleType)
		{
			Vehicle = new Vehicle(make, model, vehicleType);
			VehicleStatusId = (int)VehicleStatusEnum.Available;
		}

		/// <summary>
		/// Aggregate Root delegates status transition validation to VehicleStatus Entity.
		/// Root is the entry point; Entity owns the business rules.
		/// </summary>
		public void UpdateStatus(VehicleStatusEnum newStatus)
		{
			VehicleStatus.ValidateTransition(VehicleStatusId, newStatus);
			VehicleStatusId = (int)newStatus;
		}
	}
}

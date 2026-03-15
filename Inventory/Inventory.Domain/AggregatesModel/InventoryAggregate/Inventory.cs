using Inventory.Domain.Exceptions;
using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class Inventory : Entity, IAggregateRoot
	{
		// FK int — for persistence
		public int VehicleLocationId { get; private set; }
		public int VehicleStatusId { get; private set; }

		// Navigation — for domain access (populated on read)
		public Vehicle Vehicle { get; private set; }
		public VehicleLocation Location { get; private set; }
		public VehicleStatus Status { get; private set; }

		private Inventory() { } // EF Core

		public Inventory(Vehicle vehicle, int locationId)
		{
			Vehicle = vehicle;
			VehicleLocationId = locationId;
			VehicleStatusId = (int)VehicleStatusEnum.Available;
		}

		public void UpdateStatus(VehicleStatusEnum newStatus)
		{
			switch (newStatus)
			{
				case VehicleStatusEnum.Available:   MarkAvailable(); break;
				case VehicleStatusEnum.Rented:      MarkRented(); break;
				case VehicleStatusEnum.Reserved:    MarkReserved(); break;
				case VehicleStatusEnum.Maintenance: MarkServiced(); break;
				default: throw new InvalidVehicleStateException($"Invalid status: {newStatus}");
			}

			VehicleStatusId = (int)newStatus;
		}

		private void MarkAvailable()
		{
			if (VehicleStatusId == (int)VehicleStatusEnum.Reserved)
				throw new InvalidVehicleStateException(
					"A reserved vehicle cannot be marked as available without explicit release.");
		}

		private void MarkRented()
		{
			if (VehicleStatusId == (int)VehicleStatusEnum.Rented)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is already rented.");

			if (VehicleStatusId == (int)VehicleStatusEnum.Reserved)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is reserved.");

			if (VehicleStatusId == (int)VehicleStatusEnum.Maintenance)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is under service.");
		}

		private void MarkReserved()
		{
			if (VehicleStatusId != (int)VehicleStatusEnum.Available)
				throw new InvalidVehicleStateException(
					"A vehicle can only be reserved if it is available.");
		}

		private void MarkServiced()
		{
			if (VehicleStatusId == (int)VehicleStatusEnum.Rented)
				throw new InvalidVehicleStateException(
					"A rented vehicle cannot be sent to service.");
		}
	}
}

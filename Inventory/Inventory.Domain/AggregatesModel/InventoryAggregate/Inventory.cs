using Inventory.Domain.Exceptions;
using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	public class Inventory : Entity, IAggregateRoot
	{
		public Vehicle Vehicle { get; private set; }
		public VehicleLocation Location { get; private set; }
		public VehicleStatus Status { get; private set; }

		public Inventory(Vehicle vehicle, VehicleLocation location)
		{
			Vehicle = vehicle;
			Location = location;
			Status = VehicleStatus.Available;
		}

		public Inventory(int id, Vehicle vehicle, VehicleLocation location, VehicleStatus status)
		{
			Id = id;
			Vehicle = vehicle;
			Location = location;
			Status = status;
		}

		public void UpdateStatus(VehicleStatus newStatus)
		{
			if (newStatus == VehicleStatus.Available) MarkAvailable();
			else if (newStatus == VehicleStatus.Rented) MarkRented();
			else if (newStatus == VehicleStatus.Reserved) MarkReserved();
			else if (newStatus == VehicleStatus.Maintenance) MarkServiced();
			else throw new InvalidVehicleStateException($"Invalid status: {newStatus.Name}");

			Status = newStatus;
		}

		private void MarkAvailable()
		{
			if (Status == VehicleStatus.Reserved)
				throw new InvalidVehicleStateException(
					"A reserved vehicle cannot be marked as available without explicit release.");
		}

		private void MarkRented()
		{
			if (Status == VehicleStatus.Rented)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is already rented.");

			if (Status == VehicleStatus.Reserved)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is reserved.");

			if (Status == VehicleStatus.Maintenance)
				throw new InvalidVehicleStateException(
					"A vehicle cannot be rented if it is under service.");
		}

		private void MarkReserved()
		{
			if (Status != VehicleStatus.Available)
				throw new InvalidVehicleStateException(
					"A vehicle can only be reserved if it is available.");
		}

		private void MarkServiced()
		{
			if (Status == VehicleStatus.Rented)
				throw new InvalidVehicleStateException(
					"A rented vehicle cannot be sent to service.");
		}
	}
}

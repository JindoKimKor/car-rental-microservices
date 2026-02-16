using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities
{
	public class Vehicle : Entity, IAggregateRoot
	{
		public VehicleCode VehicleCode { get; private set; }
		public int LocationId { get; private set; }
		public int VehicleTypeId { get; private set; }
		public VehicleStatus Status { get; private set; }

		public Vehicle(VehicleCode vehicleCode, int locationId, int vehicleTypeId)
		{
			VehicleCode = vehicleCode;
			LocationId = locationId;
			VehicleTypeId = vehicleTypeId;
			Status = VehicleStatus.Available;
		}

		public void MarkAvailable()
		{
			if (Status == VehicleStatus.Reserved)
				throw new InvalidVehicleStateException(
					"A reserved vehicle cannot be marked as available without explicit release.");

			Status = VehicleStatus.Available;
		}

		public void MarkRented()
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

			Status = VehicleStatus.Rented;
		}

		public void MarkReserved()
		{
			if (Status != VehicleStatus.Available)
				throw new InvalidVehicleStateException(
					"A vehicle can only be reserved if it is available.");

			Status = VehicleStatus.Reserved;
		}

		public void MarkServiced()
		{
			if (Status == VehicleStatus.Rented)
				throw new InvalidVehicleStateException(
					"A rented vehicle cannot be sent to service.");

			Status = VehicleStatus.Maintenance;
		}
	}
}

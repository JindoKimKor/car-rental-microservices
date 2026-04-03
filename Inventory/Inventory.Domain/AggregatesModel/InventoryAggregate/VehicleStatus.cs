using Inventory.Domain.Exceptions;
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

		/// <summary>
		/// Validates whether a status transition is allowed.
		/// Entity owns its own domain logic — not the Aggregate Root.
		/// </summary>
		public static void ValidateTransition(int currentStatusId, VehicleStatusEnum newStatus)
		{
			switch (newStatus)
			{
				case VehicleStatusEnum.Available:
					if (currentStatusId == (int)VehicleStatusEnum.Reserved)
						throw new InvalidVehicleStateException(
							"A reserved vehicle cannot be marked as available without explicit release.");
					break;

				case VehicleStatusEnum.Rented:
					if (currentStatusId == (int)VehicleStatusEnum.Rented)
						throw new InvalidVehicleStateException(
							"A vehicle cannot be rented if it is already rented.");
					if (currentStatusId == (int)VehicleStatusEnum.Reserved)
						throw new InvalidVehicleStateException(
							"A vehicle cannot be rented if it is reserved.");
					if (currentStatusId == (int)VehicleStatusEnum.Maintenance)
						throw new InvalidVehicleStateException(
							"A vehicle cannot be rented if it is under service.");
					break;

				case VehicleStatusEnum.Reserved:
					if (currentStatusId != (int)VehicleStatusEnum.Available)
						throw new InvalidVehicleStateException(
							"A vehicle can only be reserved if it is available.");
					break;

				case VehicleStatusEnum.Maintenance:
					if (currentStatusId == (int)VehicleStatusEnum.Rented)
						throw new InvalidVehicleStateException(
							"A rented vehicle cannot be sent to service.");
					break;

				default:
					throw new InvalidVehicleStateException($"Invalid status: {newStatus}");
			}
		}

		/// <summary>
		/// Validates whether a status ID corresponds to a valid VehicleStatusEnum value.
		/// </summary>
		public static bool IsValid(int id)
		{
			return Enum.IsDefined(typeof(VehicleStatusEnum), id);
		}
	}
}

namespace Inventory.Domain.Exceptions
{
	public class InvalidVehicleStateException : Exception
	{
		public InvalidVehicleStateException() { }

		public InvalidVehicleStateException(string message)
			: base(message) { }

		public InvalidVehicleStateException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}

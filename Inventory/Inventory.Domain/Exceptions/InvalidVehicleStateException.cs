namespace Inventory.Domain.Exceptions
{
	public class InvalidVehicleStateException : ArgumentException
	{
		public InvalidVehicleStateException() { }

		public InvalidVehicleStateException(string message)
			: base(message) { }

		public InvalidVehicleStateException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}

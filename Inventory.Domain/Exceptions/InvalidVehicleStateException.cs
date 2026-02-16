namespace Inventory.Domain.Exceptions
{
	public class InvalidVehicleStateException : Exception
	{
		public InvalidVehicleStateException(string message) : base(message) { }
	}
}

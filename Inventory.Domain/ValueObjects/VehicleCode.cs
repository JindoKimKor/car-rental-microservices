namespace Inventory.Domain.ValueObjects
{
	public class VehicleCode
	{
		public string Value { get; }

		public VehicleCode(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Vehicle code cannot be empty.");

			Value = value;
		}

		public override bool Equals(object? obj)
		{
			if (obj is not VehicleCode other)
				return false;

			return Value == other.Value;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override string ToString() => Value;
	}
}

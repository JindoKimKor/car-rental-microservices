using Inventory.Domain.Common;

namespace Inventory.Domain.ValueObjects
{
	public class VehicleTypeId : ValueObject
	{
		public int Value { get; private set; }

		public VehicleTypeId(int value)
		{
			if (value <= 0)
				throw new ArgumentException("Invalid VehicleTypeId.", nameof(value));

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}

using Inventory.Domain.Common;

namespace Inventory.Domain.ValueObjects
{
	public class LocationId : ValueObject
	{
		public int Value { get; private set; }

		public LocationId(int value)
		{
			if (value <= 0)
				throw new ArgumentException("Invalid LocationId.", nameof(value));

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}

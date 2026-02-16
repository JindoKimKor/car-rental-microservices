using Inventory.Domain.Common;

namespace Inventory.Domain.ValueObjects
{
	public class VehicleCode : ValueObject
	{
		public string Make { get; private set; }
		public string Model { get; private set; }

		public VehicleCode(string make, string model)
		{
			if (string.IsNullOrWhiteSpace(make))
				throw new ArgumentException("Make cannot be empty.");
			if (string.IsNullOrWhiteSpace(model))
				throw new ArgumentException("Model cannot be empty.");

			Make = make;
			Model = model;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Make;
			yield return Model;
		}

		public override string ToString() => $"{Make}-{Model}";
	}
}

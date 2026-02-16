using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Common
{
	public abstract class Entity
	{
		public int Id { get; protected set; }

		public override bool Equals(object? obj)
		{
			if (obj is not Entity other)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (GetType() != other.GetType())
				return false;

			return Id == other.Id;
		}

		public override int GetHashCode() => Id.GetHashCode();
	}
}

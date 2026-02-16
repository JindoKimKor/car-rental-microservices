using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace JK_Inventory.Domain.Tests
{
	public class JK_VehicleTests
	{
		private Vehicle CreateDefaultVehicle()
		{
			return new Vehicle(
				new VehicleCode("VEH-001"),
				locationId: 1,
				VehicleType.Sedan
			);
		}

		// Creation
		[Fact]
		public void NewVehicle_ShouldHaveAvailableStatus()
		{
			var vehicle = CreateDefaultVehicle();

			Assert.Equal(VehicleStatus.Available, vehicle.Status);
		}

		// MarkRented
		[Fact]
		public void MarkRented_WhenAvailable_ShouldChangeToRented()
		{
			var vehicle = CreateDefaultVehicle();

			vehicle.MarkRented();

			Assert.Equal(VehicleStatus.Rented, vehicle.Status);
		}

		[Fact]
		public void MarkRented_WhenAlreadyRented_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkRented();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkRented());
		}

		[Fact]
		public void MarkRented_WhenReserved_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkReserved();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkRented());
		}

		[Fact]
		public void MarkRented_WhenServiced_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkServiced();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkRented());
		}

		// MarkReserved
		[Fact]
		public void MarkReserved_WhenAvailable_ShouldChangeToReserved()
		{
			var vehicle = CreateDefaultVehicle();

			vehicle.MarkReserved();

			Assert.Equal(VehicleStatus.Reserved, vehicle.Status);
		}

		[Fact]
		public void MarkReserved_WhenNotAvailable_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkRented();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkReserved());
		}

		// MarkAvailable
		[Fact]
		public void MarkAvailable_WhenRented_ShouldChangeToAvailable()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkRented();

			vehicle.MarkAvailable();

			Assert.Equal(VehicleStatus.Available, vehicle.Status);
		}

		[Fact]
		public void MarkAvailable_WhenReserved_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkReserved();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkAvailable());
		}

		// MarkServiced
		[Fact]
		public void MarkServiced_WhenAvailable_ShouldChangeToServiced()
		{
			var vehicle = CreateDefaultVehicle();

			vehicle.MarkServiced();

			Assert.Equal(VehicleStatus.Serviced, vehicle.Status);
		}

		[Fact]
		public void MarkServiced_WhenRented_ShouldThrowException()
		{
			var vehicle = CreateDefaultVehicle();
			vehicle.MarkRented();

			Assert.Throws<InvalidVehicleStateException>(() => vehicle.MarkServiced());
		}
	}
}

using Inventory.Domain.AggregatesModel.InventoryAggregate;
using Inventory.Domain.Exceptions;
using Inventory.Domain.SeedWork;
using InventoryEntity = Inventory.Domain.AggregatesModel.InventoryAggregate.Inventory;

namespace JK_Inventory.Domain.Tests
{
	public class JK_InventoryTests
	{
		private Vehicle CreateDefaultVehicle()
		{
			return new Vehicle(
				new VehicleCode("Toyota", "Camry", VehicleType.Sedan)
			);
		}

		private InventoryEntity CreateDefaultInventory()
		{
			return new InventoryEntity(
				CreateDefaultVehicle(),
				VehicleLocation.Kitchener
			);
		}

		// ============================================
		// SeedWork Verification
		// ============================================

		[Fact]
		public void Inventory_Should_Implement_IAggregateRoot()
		{
			var inventory = CreateDefaultInventory();
			Assert.IsAssignableFrom<IAggregateRoot>(inventory);
		}

		[Fact]
		public void Vehicle_Should_Not_Implement_IAggregateRoot()
		{
			var vehicle = CreateDefaultVehicle();
			Assert.False(vehicle is IAggregateRoot);
		}

		[Fact]
		public void Inventory_Should_Inherit_Entity()
		{
			var inventory = CreateDefaultInventory();
			Assert.IsAssignableFrom<Entity>(inventory);
		}

		[Fact]
		public void Vehicle_Should_Inherit_Entity()
		{
			var vehicle = CreateDefaultVehicle();
			Assert.IsAssignableFrom<Entity>(vehicle);
		}

		// ============================================
		// Inventory Creation
		// ============================================

		[Fact]
		public void NewInventory_ShouldHaveAvailableStatus()
		{
			var inventory = CreateDefaultInventory();
			Assert.Equal(VehicleStatus.Available, inventory.Status);
		}

		[Fact]
		public void NewInventory_ShouldHaveVehicle()
		{
			var inventory = CreateDefaultInventory();
			Assert.NotNull(inventory.Vehicle);
			Assert.Equal("Toyota", inventory.Vehicle.VehicleCode.Make);
		}

		[Fact]
		public void NewInventory_ShouldHaveLocation()
		{
			var inventory = CreateDefaultInventory();
			Assert.Equal(VehicleLocation.Kitchener, inventory.Location);
		}

		// ============================================
		// UpdateStatus — Rented
		// ============================================

		[Fact]
		public void UpdateStatus_ToRented_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Rented);
			Assert.Equal(VehicleStatus.Rented, inventory.Status);
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenAlreadyRented_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Rented));
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenReserved_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Reserved);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Rented));
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenMaintenance_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Maintenance);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Rented));
		}

		// ============================================
		// UpdateStatus — Reserved
		// ============================================

		[Fact]
		public void UpdateStatus_ToReserved_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Reserved);
			Assert.Equal(VehicleStatus.Reserved, inventory.Status);
		}

		[Fact]
		public void UpdateStatus_ToReserved_WhenNotAvailable_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Reserved));
		}

		// ============================================
		// UpdateStatus — Available
		// ============================================

		[Fact]
		public void UpdateStatus_ToAvailable_WhenRented_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Rented);
			inventory.UpdateStatus(VehicleStatus.Available);
			Assert.Equal(VehicleStatus.Available, inventory.Status);
		}

		[Fact]
		public void UpdateStatus_ToAvailable_WhenReserved_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Reserved);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Available));
		}

		// ============================================
		// UpdateStatus — Maintenance
		// ============================================

		[Fact]
		public void UpdateStatus_ToMaintenance_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Maintenance);
			Assert.Equal(VehicleStatus.Maintenance, inventory.Status);
		}

		[Fact]
		public void UpdateStatus_ToMaintenance_WhenRented_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatus.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatus.Maintenance));
		}

		// ============================================
		// VehicleCode Value Object
		// ============================================

		[Fact]
		public void VehicleCode_SameValues_ShouldBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleType.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleType.Sedan);
			Assert.Equal(code1, code2);
		}

		[Fact]
		public void VehicleCode_DifferentValues_ShouldNotBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleType.Sedan);
			var code2 = new VehicleCode("Honda", "Civic", VehicleType.Sedan);
			Assert.NotEqual(code1, code2);
		}

		[Fact]
		public void VehicleCode_SameMakeModel_DifferentType_ShouldNotBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleType.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleType.SUV);
			Assert.NotEqual(code1, code2);
		}
	}
}

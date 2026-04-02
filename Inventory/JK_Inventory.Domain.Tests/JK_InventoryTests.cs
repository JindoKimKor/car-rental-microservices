using Inventory.Domain.AggregatesModel.InventoryAggregate;
using Inventory.Domain.Exceptions;
using Inventory.Domain.SeedWork;
using InventoryEntity = Inventory.Domain.AggregatesModel.InventoryAggregate.Inventory;

namespace JK_Inventory.Domain.Tests
{
	public class JK_InventoryTests
	{
		// Aggregate Root creates Vehicle internally — no direct Vehicle construction needed
		private InventoryEntity CreateDefaultInventory()
		{
			return new InventoryEntity(
				"Toyota", "Camry",
				VehicleTypeEnum.Sedan,
				(int)VehicleLocationEnum.Kitchener
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
			// Access Vehicle through Aggregate Root — not created directly
			var inventory = CreateDefaultInventory();
			Assert.False(inventory.Vehicle is IAggregateRoot);
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
			// Access Vehicle through Aggregate Root — not created directly
			var inventory = CreateDefaultInventory();
			Assert.IsAssignableFrom<Entity>(inventory.Vehicle);
		}

		// ============================================
		// Inventory Creation
		// ============================================

		[Fact]
		public void NewInventory_ShouldHaveAvailableStatus()
		{
			var inventory = CreateDefaultInventory();
			Assert.Equal((int)VehicleStatusEnum.Available, inventory.VehicleStatusId);
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
			Assert.Equal((int)VehicleLocationEnum.Kitchener, inventory.VehicleLocationId);
		}

		// ============================================
		// UpdateStatus — Rented
		// ============================================

		[Fact]
		public void UpdateStatus_ToRented_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Rented);
			Assert.Equal((int)VehicleStatusEnum.Rented, inventory.VehicleStatusId);
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenAlreadyRented_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Rented));
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenReserved_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Reserved);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Rented));
		}

		[Fact]
		public void UpdateStatus_ToRented_WhenMaintenance_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Maintenance);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Rented));
		}

		// ============================================
		// UpdateStatus — Reserved
		// ============================================

		[Fact]
		public void UpdateStatus_ToReserved_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Reserved);
			Assert.Equal((int)VehicleStatusEnum.Reserved, inventory.VehicleStatusId);
		}

		[Fact]
		public void UpdateStatus_ToReserved_WhenNotAvailable_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Reserved));
		}

		// ============================================
		// UpdateStatus — Available
		// ============================================

		[Fact]
		public void UpdateStatus_ToAvailable_WhenRented_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Rented);
			inventory.UpdateStatus(VehicleStatusEnum.Available);
			Assert.Equal((int)VehicleStatusEnum.Available, inventory.VehicleStatusId);
		}

		[Fact]
		public void UpdateStatus_ToAvailable_WhenReserved_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Reserved);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Available));
		}

		// ============================================
		// UpdateStatus — Maintenance
		// ============================================

		[Fact]
		public void UpdateStatus_ToMaintenance_WhenAvailable_ShouldSucceed()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Maintenance);
			Assert.Equal((int)VehicleStatusEnum.Maintenance, inventory.VehicleStatusId);
		}

		[Fact]
		public void UpdateStatus_ToMaintenance_WhenRented_ShouldThrow()
		{
			var inventory = CreateDefaultInventory();
			inventory.UpdateStatus(VehicleStatusEnum.Rented);
			Assert.Throws<InvalidVehicleStateException>(() =>
				inventory.UpdateStatus(VehicleStatusEnum.Maintenance));
		}

		// ============================================
		// VehicleCode Value Object
		// ============================================

		[Fact]
		public void VehicleCode_SameValues_ShouldBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			Assert.Equal(code1, code2);
		}

		[Fact]
		public void VehicleCode_DifferentValues_ShouldNotBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Honda", "Civic", VehicleTypeEnum.Sedan);
			Assert.NotEqual(code1, code2);
		}

		[Fact]
		public void VehicleCode_SameMakeModel_DifferentType_ShouldNotBeEqual()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.SUV);
			Assert.NotEqual(code1, code2);
		}

		// ============================================
		// VehicleCode — IsSameVehicle
		// ============================================

		[Fact]
		public void IsSameVehicle_SameValues_ShouldReturnTrue()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			Assert.True(code1.IsSameVehicle(code2));
		}

		[Fact]
		public void IsSameVehicle_DifferentMake_ShouldReturnFalse()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Honda", "Camry", VehicleTypeEnum.Sedan);
			Assert.False(code1.IsSameVehicle(code2));
		}

		[Fact]
		public void IsSameVehicle_DifferentType_ShouldReturnFalse()
		{
			var code1 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.Sedan);
			var code2 = new VehicleCode("Toyota", "Camry", VehicleTypeEnum.SUV);
			Assert.False(code1.IsSameVehicle(code2));
		}
	}
}

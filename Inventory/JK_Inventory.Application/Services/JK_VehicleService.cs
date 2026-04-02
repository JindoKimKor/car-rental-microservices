using Inventory.Domain.AggregatesModel.InventoryAggregate;
using JK_Inventory.Application.DTOs;
using JK_Inventory.Application.Interfaces;
using InventoryEntity = Inventory.Domain.AggregatesModel.InventoryAggregate.Inventory;

namespace Inventory.Application.Services
{
	public class JK_VehicleService : IVehicleService
	{
		private readonly IInventoryRepository _repository;

		public JK_VehicleService(IInventoryRepository repository)
		{
			_repository = repository;
		}

		public async Task<JK_VehicleDto?> GetVehicleById(int id)
		{
			var inventory = await _repository.FindByIdAsync(id);
			if (inventory == null)
				return null;

			return ToDto(inventory);
		}

		public async Task<IEnumerable<JK_VehicleDto>> GetAllVehicles()
		{
			var inventories = await _repository.FindAllAsync();
			return inventories.Select(ToDto);
		}

		public async Task CreateVehicle(JK_CreateVehicleDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Make))
				throw new ArgumentException("Make is required.");
			if (string.IsNullOrWhiteSpace(dto.Model))
				throw new ArgumentException("Model is required.");
			if (!Enum.IsDefined(typeof(VehicleLocationEnum), dto.LocationId))
				throw new ArgumentException($"Invalid LocationId: {dto.LocationId}");
			if (!Enum.IsDefined(typeof(VehicleTypeEnum), dto.VehicleTypeId))
				throw new ArgumentException($"Invalid VehicleTypeId: {dto.VehicleTypeId}");

			// Step 2: Application no longer creates VehicleCode/Vehicle directly.
			// Aggregate Root (Inventory) controls Child Entity creation internally.
			var vehicleType = (VehicleTypeEnum)dto.VehicleTypeId;
			var inventory = new InventoryEntity(dto.Make, dto.Model, vehicleType, dto.LocationId);

			_repository.Add(inventory);
			await _repository.UnitOfWork.SaveChangesAsync();
		}

		public async Task<JK_VehicleDto> UpdateVehicleStatus(int id, JK_UpdateVehicleStatusDto dto)
		{
			var inventory = await _repository.FindByIdAsync(id)
				?? throw new KeyNotFoundException($"Vehicle with id {id} not found.");

			var newStatus = Enum.Parse<VehicleStatusEnum>(dto.Status, ignoreCase: true);
			inventory.UpdateStatus(newStatus);

			await _repository.UnitOfWork.SaveChangesAsync();
			return ToDto(inventory);
		}

		public async Task DeleteVehicle(int id)
		{
			await _repository.RemoveAsync(id);
			await _repository.UnitOfWork.SaveChangesAsync();
		}

		// Application accesses Aggregate Root's flat properties only.
		// No deep traversal into Child Entities (Vehicle.VehicleCode.Make).
		private JK_VehicleDto ToDto(InventoryEntity inventory)
		{
			return new JK_VehicleDto
			{
				Id = inventory.Id,
				Make = inventory.Make,
				Model = inventory.Model,
				LocationId = inventory.VehicleLocationId,
				VehicleTypeId = (int)inventory.VehicleType,
				Status = ((VehicleStatusEnum)inventory.VehicleStatusId).ToString()
			};
		}
	}
}

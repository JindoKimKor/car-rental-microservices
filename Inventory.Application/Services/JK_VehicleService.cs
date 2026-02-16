using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.ValueObjects;
using JK_Inventory.Application.DTOs;
using JK_Inventory.Application.Interfaces;

namespace Inventory.Application.Services
{
	public class JK_VehicleService : IVehicleService
	{
		private readonly IVehicleRepository _repository;

		public JK_VehicleService(IVehicleRepository repository)
		{
			_repository = repository;
		}

		public async Task<JK_VehicleDto?> GetVehicleById(int id)
		{
			var vehicle = await _repository.FindByIdAsync(id);
			if (vehicle == null)
				return null;

			return ToDto(vehicle);
		}

		public async Task<IEnumerable<JK_VehicleDto>> GetAllVehicles()
		{
			var vehicles = await _repository.FindAllAsync();
			return vehicles.Select(ToDto);
		}

		public async Task CreateVehicle(JK_CreateVehicleDto dto)
		{
			var vehicleCode = new VehicleCode(dto.Make, dto.Model);
			var vehicle = new Vehicle(vehicleCode, dto.LocationId, dto.VehicleTypeId);
			await _repository.SaveAsync(vehicle);
		}

		public async Task<JK_VehicleDto> UpdateVehicleStatus(int id, JK_UpdateVehicleStatusDto dto)
		{
			var vehicle = await _repository.FindByIdAsync(id)
				?? throw new KeyNotFoundException($"Vehicle with id {id} not found.");

			var status = Enum.Parse<VehicleStatus>(dto.Status, ignoreCase: true);

			switch (status)
			{
				case VehicleStatus.Available:
					vehicle.MarkAvailable();
					break;
				case VehicleStatus.Rented:
					vehicle.MarkRented();
					break;
				case VehicleStatus.Reserved:
					vehicle.MarkReserved();
					break;
				case VehicleStatus.Maintenance:
					vehicle.MarkServiced();
					break;
			}

			await _repository.SaveAsync(vehicle);
			return ToDto(vehicle);
		}

		public async Task DeleteVehicle(int id)
		{
			await _repository.RemoveAsync(id);
		}

		private JK_VehicleDto ToDto(Vehicle vehicle)
		{
			return new JK_VehicleDto
			{
				Id = vehicle.Id,
				Make = vehicle.VehicleCode.Make,
				Model = vehicle.VehicleCode.Model,
				LocationId = vehicle.LocationId,
				VehicleTypeId = vehicle.VehicleTypeId,
				Status = vehicle.Status.ToString()
			};
		}
	}
}

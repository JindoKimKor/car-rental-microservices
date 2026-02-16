using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services
{
    internal class VehicleService : IVehicleService
    {
		private readonly IVehicleRepository _repository;

		public VehicleService(IVehicleRepository repository)
		{
			_repository = repository;
		}

		public Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VehicleDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleDto> UpdateStatusAsync(int id, UpdateVehicleStatusDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

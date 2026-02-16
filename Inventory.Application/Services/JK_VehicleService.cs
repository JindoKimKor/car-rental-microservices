using JK_Inventory.Application.DTOs;
using JK_Inventory.Application.Interfaces;

namespace Inventory.Application.Services
{
    internal class JK_VehicleService : IVehicleService
    {
		private readonly IVehicleRepository _repository;

		public JK_VehicleService(IVehicleRepository repository)
		{
			_repository = repository;
		}

		public Task<JK_VehicleDto> CreateAsync(JK_CreateVehicleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JK_VehicleDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<JK_VehicleDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JK_VehicleDto> UpdateStatusAsync(int id, JK_UpdateVehicleStatusDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

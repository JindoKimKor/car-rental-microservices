using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Entities;
using JK_Inventory.Application.Interfaces;
using JK_Inventory.Infrastructure.Persistence;

namespace JK_Inventory.Infrastructure.Repositories
{
	public class JK_VehicleRepository : IVehicleRepository
    {
		private readonly JK_InventoryDbContext _context;

		public JK_VehicleRepository(JK_InventoryDbContext context)
		{
			_context = context;
		}

		public Task AddAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}

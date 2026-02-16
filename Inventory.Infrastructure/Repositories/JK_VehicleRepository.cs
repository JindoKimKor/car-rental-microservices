using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.ValueObjects;
using JK_Inventory.Application.Interfaces;
using JK_Inventory.Infrastructure.Persistence;
using JK_Inventory.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace JK_Inventory.Infrastructure.Repositories
{
	public class JK_VehicleRepository : IVehicleRepository
    {
		private readonly JK_InventoryDbContext _context;

		public JK_VehicleRepository(JK_InventoryDbContext context)
		{
			_context = context;
		}

		public async Task<Vehicle?> FindByIdAsync(int id)
		{
			var inventory = await _context.JkInventories
				.Include(i => i.Vehicle)
				.FirstOrDefaultAsync(i => i.Id == id);

			if (inventory == null)
				return null;

			return ToDomain(inventory);
		}

		public async Task<IEnumerable<Vehicle>> FindAllAsync()
		{
			var inventories = await _context.JkInventories
				.Include(i => i.Vehicle)
				.ToListAsync();

			return inventories.Select(ToDomain);
		}

		public async Task SaveAsync(Vehicle vehicle)
		{
			if (vehicle.Id == 0)
			{
				// Use navigation property so EF Core inserts JkVehicle first,
				// then JkInventory with the correct FK — all in one SaveChanges
				var dbInventory = new JkInventory
				{
					Vehicle = new JkVehicle
					{
						Make = vehicle.VehicleCode.Make,
						Model = vehicle.VehicleCode.Model,
						VehicleTypeId = vehicle.VehicleTypeId
					},
					VehicleLocationId = vehicle.LocationId,
					VehicleStatusId = (int)vehicle.Status,
					LastUpdated = DateTime.UtcNow
				};

				_context.JkInventories.Add(dbInventory);
				await _context.SaveChangesAsync();
			}
			else
			{
				var inventory = await _context.JkInventories
					.FirstOrDefaultAsync(i => i.Id == vehicle.Id);

				if (inventory == null)
					return;

				inventory.VehicleStatusId = (int)vehicle.Status;
				inventory.LastUpdated = DateTime.UtcNow;

				await _context.SaveChangesAsync();
			}
		}

		public async Task RemoveAsync(int id)
		{
			var inventory = await _context.JkInventories
				.FirstOrDefaultAsync(i => i.Id == id);

			if (inventory == null)
				return;

			_context.JkInventories.Remove(inventory);
			await _context.SaveChangesAsync();
		}

		// Maps JkInventory (+ JkVehicle) → Domain Vehicle.
		// Vehicle.Id = JkInventory.Id because the domain entity represents
		// an inventory record (vehicle + location + status), not just a vehicle.
		// The same physical vehicle (JkVehicle) can have multiple inventory entries.
		private Vehicle ToDomain(JkInventory inventory)
		{
			return new Vehicle(
				inventory.Id,
				new VehicleCode(inventory.Vehicle.Make, inventory.Vehicle.Model),
				inventory.VehicleLocationId,
				inventory.Vehicle.VehicleTypeId,
				(VehicleStatus)inventory.VehicleStatusId
			);
		}
	}
}

using Inventory.Domain.AggregatesModel.InventoryAggregate;
using Inventory.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using InventoryEntity = Inventory.Domain.AggregatesModel.InventoryAggregate.Inventory;

namespace JK_Inventory.Infrastructure.Repositories
{
	public class JK_InventoryRepository : IInventoryRepository
	{
		private readonly JK_InventoryContext _context;

		public JK_InventoryRepository(JK_InventoryContext context)
		{
			_context = context;
		}

		public IUnitOfWork UnitOfWork => _context;

		public InventoryEntity Add(InventoryEntity inventory)
		{
			return _context.Inventories.Add(inventory).Entity;
		}

		public async Task<InventoryEntity?> FindByIdAsync(int id)
		{
			return await _context.Inventories
				.Include(i => i.Vehicle)
				.Include(i => i.Location)
				.Include(i => i.Status)
				.FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<IEnumerable<InventoryEntity>> FindAllAsync()
		{
			return await _context.Inventories
				.Include(i => i.Vehicle)
				.Include(i => i.Location)
				.Include(i => i.Status)
				.ToListAsync();
		}

		public async Task RemoveAsync(int id)
		{
			var inventory = await _context.Inventories.FindAsync(id);
			if (inventory != null)
				_context.Inventories.Remove(inventory);
		}
	}
}

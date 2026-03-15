using Inventory.Domain.SeedWork;

namespace Inventory.Domain.AggregatesModel.InventoryAggregate
{
	/// <summary>
	/// Repository contract for Inventory Aggregate Root.
	/// Defines domain-permitted operations only — no direct DB access.
	/// IUnitOfWork is inherited from IRepository for transaction commit.
	/// Implementation lives in Infrastructure layer.
	/// </summary>
	public interface IInventoryRepository : IRepository<Inventory>
	{
		Inventory Add(Inventory inventory);
		Task<Inventory?> FindByIdAsync(int id);
		Task<IEnumerable<Inventory>> FindAllAsync();
		Task RemoveAsync(int id);
	}
}

using Inventory.Domain.Common;

namespace JK_Inventory.Application.Interfaces
{
	public interface IRepository<T> where T : Entity, IAggregateRoot
	{
		Task<T?> FindByIdAsync(int id);
		Task<IEnumerable<T>> FindAllAsync();
		Task SaveAsync(T entity);
		Task RemoveAsync(int id);
	}
}

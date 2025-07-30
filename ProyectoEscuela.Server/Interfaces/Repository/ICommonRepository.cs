using ProyectoEscuela.Server.Pagination;
using System.Linq.Expressions;

namespace ProyectoEscuela.Server.Interfaces.Repository
{
    public interface ICommonRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task DeleteChangesAsync(TEntity entity, CancellationToken cancellationToken);

        Task<PageResult<TEntity>> GetPageResultAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task<bool> ValidateAsync(Expression<Func<TEntity, bool>> predicate);
	
	 Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}

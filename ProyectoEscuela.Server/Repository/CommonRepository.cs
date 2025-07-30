using Microsoft.EntityFrameworkCore;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;
using System.Linq.Expressions;

namespace ProyectoEscuela.Server.Repository
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected readonly AppDBContext _context;

        public CommonRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteChangesAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _context.Set<T>().FindAsync(id, cancellationToken);

        public async Task<PageResult<T>> GetPageResultAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var t = await _context.Set<T>().AsNoTracking().CountAsync(cancellationToken);
            var items = await _context.Set<T>().
                  Skip((pageNumber - 1) * pageSize).
                  Take(pageSize).
                  ToListAsync(cancellationToken);
            return new PageResult<T>(items, pageNumber, t, pageSize);
        }

        public async Task SaveChangesAsync( CancellationToken cancellationToken) =>
            await _context.SaveChangesAsync(cancellationToken);
        

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ValidateAsync(Expression<Func<T, bool>> predicate)
        {
            var exist = _context.Set<T>().AnyAsync(predicate);
            return exist;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

      

    }
}
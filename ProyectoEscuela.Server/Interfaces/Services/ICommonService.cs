using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Interfaces.Services
{
    public interface ICommonService<TInsert, TUpdate, TResponse>
    {
        Task<PageResult<TResponse>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<TResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TResponse> InsertAsync(TInsert entityInsertDto, CancellationToken cancellationToken);
        Task<TResponse> UpdateAsync(Guid id, TUpdate entityUpdateDto, CancellationToken cancellationToken);
        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

using ProyectoEscuela.Server.DTOs.Calificacion;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class CalificacionService : ICalificacionService
    {
        public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CalificacionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<CalificacionDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CalificacionDto> InsertAsync(CalificacionInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CalificacionDto> UpdateAsync(Guid id, CalificacionUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

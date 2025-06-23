using ProyectoEscuela.Server.DTOs.Maestro;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class MaestroService : IMaestroService
    {
        public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MaestroDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<MaestroDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MaestroDto> InsertAsync(MaestroInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MaestroDto> UpdateAsync(Guid id, MaestroUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

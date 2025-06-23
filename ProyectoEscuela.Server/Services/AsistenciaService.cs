
using ProyectoEscuela.Server.DTOs.Asistencia;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AsistenciaDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<AsistenciaDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AsistenciaDto> InsertAsync(AsistenciaInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AsistenciaDto> UpdateAsync(Guid id, AsistenciaUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

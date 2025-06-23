using ProyectoEscuela.Server.DTOs.Materia;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class MateriaService : IMateriaService
    {
        public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MateriaDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<MateriaDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MateriaDto> InsertAsync(MateriaInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MateriaDto> UpdateAsync(Guid id, MateriaUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.Logging;
using ProyectoEscuela.Server.DTOs.Alumno;
using ProyectoEscuela.Server.DTOs.Materia;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;
        private readonly ILogger<MateriaDto> _logger;

        public MateriaService(IMateriaRepository materiaRepository, ILogger<MateriaDto> logger)
        {
            _materiaRepository = materiaRepository;
            _logger = logger;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);
            if (materia == null)
            {
                _logger.LogError("Materia with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Materia with ID {id} not found.");
            }
            await _materiaRepository.DeleteChangesAsync(materia, cancellationToken);
            _logger.LogInformation("Materia with ID {Id} successfully deleted.", id);
            return id;
        }

        public async Task<MateriaDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);

            if (materia == null)
            {
                _logger.LogError($"Materia with ID {id} not found");
                throw new KeyNotFoundException($"Materia with ID {id} not found");
            }

            var materiaDto = new MateriaDto(
                Id: materia.Id,
                NombreMateria: materia.NombreMateria,
                Descripcion: materia.Descripcion,
                MaestroId: materia.MaestroId
            );

            _logger.LogError($"Materia with ID {id} successfully retrieved.", id);
            return materiaDto;
        }

        public async Task<PageResult<MateriaDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogError("Invalid page parameters: pageNumber and pageSize must be greater than 0. ");
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            var entityWithNumber = await _materiaRepository.GetPageResultAsync(pageNumber, pageSize, cancellationToken);
            var dto = entityWithNumber.Items.Select(x => new MateriaDto(
                Id: x.Id,
                NombreMateria: x.NombreMateria,
                Descripcion: x.Descripcion,
                MaestroId: x.MaestroId
                )).ToList();

            if (!dto.Any())
            {
                _logger.LogError("No Register found.");
                throw new KeyNotFoundException("The list empty");
            }

            PageResult<MateriaDto> pageResult = new()
            {
                TotalItems = entityWithNumber.TotalItems,
                CurrentPage = entityWithNumber.CurrentPage,
                TotalPages = entityWithNumber.TotalPages,
                Items = dto
            };

            _logger.LogInformation(
            "Successfully retrieved {Count} alumnos. Page {CurrentPage} of {TotalPage}.",
            dto.Count,
            pageResult.CurrentPage,
            pageResult.TotalPages);

            return pageResult;
        }

        public async Task<MateriaDto> InsertAsync(MateriaInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            if (entityInsertDto == null)
            {
                _logger.LogError("MaestroInsertDto cannot be null.");
                throw new ArgumentNullException(nameof(entityInsertDto), "EntityInsertDto cannot be null.");
            }

            var materia = new Materia
            {
                Id = Guid.NewGuid(),
                NombreMateria = entityInsertDto.NombreMateria,
                Descripcion = entityInsertDto.Descripcion,
                MaestroId = entityInsertDto.MaestroId
            };

            await _materiaRepository.AddAsync(materia, cancellationToken);

            var materiaDto = new MateriaDto(
                Id: materia.Id,
                NombreMateria: materia.NombreMateria,
                Descripcion: materia.Descripcion,
                MaestroId: materia.MaestroId

            );

            _logger.LogInformation("Materia with ID {Id} successfully created.", materiaDto.Id);
            return materiaDto;
        }

        public async Task<MateriaDto> UpdateAsync(Guid id, MateriaUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);
            if (materia == null)
            {
                _logger.LogError("Materia with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Materia with ID {id} not found.");
            }

            if (entityUpdateDto == null)
            {
                _logger.LogError("MateriaUpdateDto cannot be null.");
                throw new ArgumentNullException(nameof(entityUpdateDto), "EntityUpdateDto cannot be null.");
            }
            materia.NombreMateria = entityUpdateDto.NombreMateria;
            materia.Descripcion = entityUpdateDto.Descripcion;
            materia.MaestroId = entityUpdateDto.MaestroId;
            await _materiaRepository.UpdateAsync(materia, cancellationToken);

            var materiaDto = new MateriaDto(
                Id: materia.Id,
                NombreMateria: materia.NombreMateria,
                Descripcion: materia.Descripcion,
                MaestroId: materia.MaestroId
            );

            _logger.LogInformation("Materia with ID {Id} successfully updated.", materiaDto.Id);
            return materiaDto;
        }
    }
}

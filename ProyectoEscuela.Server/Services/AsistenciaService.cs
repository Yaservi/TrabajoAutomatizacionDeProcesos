
using Microsoft.Extensions.Logging;
using ProyectoEscuela.Server.DTOs.Asistencia;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class AsistenciaService : IAsistenciaService
    {

        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly ILogger<AsistenciaDto> _logger;
        public AsistenciaService(IAsistenciaRepository asistenciaRepository, ILogger<AsistenciaDto> logger)
        {
            _asistenciaRepository = asistenciaRepository;
            _logger = logger;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var asistencia = await _asistenciaRepository.GetByIdAsync(id, cancellationToken);
            if (asistencia == null)
            {
                _logger.LogError("Asistencia with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Asistencia with ID {id} not found.");
            }
            await _asistenciaRepository.DeleteChangesAsync(asistencia, cancellationToken);
            _logger.LogInformation("Asistencia with ID {Id} successfully deleted.", id);
            return id;
        }

        public async Task<AsistenciaDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var asistencia = await _asistenciaRepository.GetByIdAsync(id, cancellationToken);
            if (asistencia == null)
            {
                _logger.LogError("Asistencia with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Asistencia with ID {id} not found.");
            }
            var asistenciaDto = new AsistenciaDto(
                Id: asistencia.Id,
                Estado: asistencia.Estado,
                FechaAsistencia: asistencia.FechaAsistencia,
                AlumnoId: asistencia.AlumnoId,
                MateriaId: asistencia.MateriaId
            );
            _logger.LogInformation("Asistencia with ID {Id} successfully retrieved.", id);
            return asistenciaDto;
        }

        public async Task<PageResult<AsistenciaDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogError("Invalid page parameters: pageNumber and pageSize must be greater than 0. ");
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            var entityWithNumber = await _asistenciaRepository.GetPageResultAsync(pageNumber, pageSize, cancellationToken);
            var dto = entityWithNumber.Items.Select(x => new AsistenciaDto(
                Id: x.Id,
                Estado: x.Estado,
                FechaAsistencia: x.FechaAsistencia,
                AlumnoId: x.AlumnoId,
                MateriaId: x.MateriaId
             )).ToList();

            if (!dto.Any())
            {
                _logger.LogError("No Register found.");
                throw new KeyNotFoundException("The list empty");
            }

            PageResult<AsistenciaDto> pageResult = new()
            {
                TotalItems = entityWithNumber.TotalItems,
                CurrentPage = entityWithNumber.CurrentPage,
                TotalPages = entityWithNumber.TotalPages,
                Items = dto
            };

            _logger.LogInformation(
                " Successfully retrieved {Count} asistencia. Page {CurrentPage} of {TotalPage}.",
                dto.Count,
                pageResult.CurrentPage,
                pageResult.TotalPages);

            return pageResult;
        }

        public async Task<AsistenciaDto> InsertAsync(AsistenciaInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            if (entityInsertDto == null)
            {
                _logger.LogError("AsistenciaInsertDto cannot be null.");
                throw new ArgumentNullException(nameof(entityInsertDto), "AsistenciaInsertDto cannot be null.");
            }
            var asistencia = new Asistencias
            {
                Estado = entityInsertDto.Estado,
                FechaAsistencia = entityInsertDto.FechaAsistencia,
                AlumnoId = entityInsertDto.AlumnoId,
                MateriaId = entityInsertDto.MateriaId
            };

            await _asistenciaRepository.AddAsync(asistencia, cancellationToken);

            var asistenciaDto = new AsistenciaDto(
                Id: asistencia.Id,
                Estado: asistencia.Estado,
                FechaAsistencia: asistencia.FechaAsistencia,
                AlumnoId: asistencia.AlumnoId,
                MateriaId: asistencia.MateriaId
            );
            _logger.LogInformation("Asistencia with ID {Id} successfully created.", asistencia.Id);
            return asistenciaDto;
        }

        public async Task<AsistenciaDto> UpdateAsync(Guid id, AsistenciaUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            var asistencia = await _asistenciaRepository.GetByIdAsync(id, cancellationToken);
            if (asistencia == null)
            {
                _logger.LogError("Asistencia with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Asistencia with ID {id} not found.");
            }
            if (entityUpdateDto == null)
            {
                _logger.LogError("AsistenciaUpdateDto cannot be null.");
                throw new ArgumentNullException(nameof(entityUpdateDto), "AsistenciaUpdateDto cannot be null.");
            }
            asistencia.Estado = entityUpdateDto.Estado;
            asistencia.FechaAsistencia = entityUpdateDto.FechaAsistencia;
            asistencia.AlumnoId = entityUpdateDto.AlumnoId;
            asistencia.MateriaId = entityUpdateDto.MateriaId;


            await _asistenciaRepository.UpdateAsync(asistencia, cancellationToken);
            var asistenciaDto = new AsistenciaDto(
                Id: asistencia.Id,
                Estado: asistencia.Estado,
                FechaAsistencia: asistencia.FechaAsistencia,
                AlumnoId: asistencia.AlumnoId,
                MateriaId: asistencia.MateriaId
            );
            _logger.LogInformation("Asistencia with ID {Id} successfully updated.", asistencia.Id);
            return asistenciaDto;
        }
    }
}

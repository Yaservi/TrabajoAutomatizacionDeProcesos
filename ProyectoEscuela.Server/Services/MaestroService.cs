using Microsoft.Extensions.Logging;
using ProyectoEscuela.Server.DTOs.Maestro;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;

namespace ProyectoEscuela.Server.Services
{
    public class MaestroService : IMaestroService
    {
        private readonly IMaestroRepository _maestroRepository;
        private readonly ILogger<MaestroDto> _logger;
        public MaestroService(IMaestroRepository maestroRepository, ILogger<MaestroDto> logger)
        {
            this._maestroRepository = maestroRepository;
            this._logger = logger;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var maestro = await _maestroRepository.GetByIdAsync(id, cancellationToken);
            if (maestro == null)
            {
                _logger.LogError("Maestro with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Maestro with ID {id} not found.");
            }

            await _maestroRepository.DeleteChangesAsync(maestro, cancellationToken);
            _logger.LogInformation("Maestro with ID {Id} successfully deleted.", id);
            return id;
        }

        public async Task<MaestroDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var maestro = await _maestroRepository.GetByIdAsync(id, cancellationToken);

            if (maestro == null)
            {
                _logger.LogError("Maestro with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Maestro with ID {id} not found.");
            }
            var maestroDto = new MaestroDto(
                Id: maestro.Id,
                Nombre: maestro.Nombre,
                Apellido: maestro.Apellido,
                Direccion: maestro.Direccion,
                FechaNacimiento: maestro.FechaNacimiento,
                Telefono: maestro.Telefono,
                Email: maestro.Email
            );
            _logger.LogInformation("Maestro with ID {Id} successfully retrieved.", id);
            return maestroDto;
        }

        public async Task<PageResult<MaestroDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogError("Invalid page parameters: pageNumber and pageSize must be greater than 0. ");
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }
            var entityWithNumber = await _maestroRepository.GetPageResultAsync(pageNumber, pageSize, cancellationToken);
            var dto = entityWithNumber.Items.Select(x => new MaestroDto(
                Id: x.Id,
                Nombre: x.Nombre,
                Apellido: x.Apellido,
                Direccion: x.Direccion,
                FechaNacimiento: x.FechaNacimiento,
                Telefono: x.Telefono,
                Email: x.Email
                )).ToList();

            if (!dto.Any())
            {
                _logger.LogError("No Register found.");
                throw new KeyNotFoundException("The list empty");
            }
            PageResult<MaestroDto> pageResult = new()
            {
                TotalItems = entityWithNumber.TotalItems,
                CurrentPage = entityWithNumber.CurrentPage,
                TotalPages = entityWithNumber.TotalPages,
                Items = dto
            };

            _logger.LogInformation(
                " Successfully retrieved {Count} alumnos. Page {CurrentPage} of {TotalPage}.",
                dto.Count,
                pageResult.CurrentPage,
                pageResult.TotalPages);

            return pageResult;
        }

        public async Task<MaestroDto> InsertAsync(MaestroInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            if (entityInsertDto == null)
            {
                _logger.LogError("AlumnoInsertDto is null.");
                throw new ArgumentNullException(nameof(entityInsertDto), "AlumnoInsertDto cannot be null.");
            }

            var maestro = new Maestro
            {
                Nombre = entityInsertDto.Nombre,
                Apellido = entityInsertDto.Apellido,
                Direccion = entityInsertDto.Direccion,
                FechaNacimiento = entityInsertDto.FechaNacimiento,
                Telefono = entityInsertDto.Telefono,
                Email = entityInsertDto.Email
            };

            await _maestroRepository.AddAsync(maestro, cancellationToken);

            var maestroDto = new MaestroDto(
                Id: maestro.Id,
                Nombre: maestro.Nombre,
                Apellido: maestro.Apellido,
                Direccion: maestro.Direccion,
                FechaNacimiento: maestro.FechaNacimiento,
                Telefono: maestro.Telefono,
                Email: maestro.Email);
            _logger.LogInformation("Maestro with ID {Id} successfully created.", maestro.Id);
            return maestroDto;
        }

        public async Task<MaestroDto> UpdateAsync(Guid id, MaestroUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
            var maestro = await _maestroRepository.GetByIdAsync(id, cancellationToken);

            if (maestro == null)
            {
                _logger.LogError("MestroUpdateDto is Null.");
                throw new KeyNotFoundException($"Maestro with ID {id} not found.");
            }

            if (entityUpdateDto == null)
            {
                _logger.LogError("MaestroUpdateDto is null.");
                throw new ArgumentNullException(nameof(entityUpdateDto), "MaestroUpdateDto cannot be null.");
            }
            maestro.Nombre = entityUpdateDto.Nombre;
            maestro.Apellido = entityUpdateDto.Apellido;
            maestro.Direccion = entityUpdateDto.Direccion;
            maestro.FechaNacimiento = entityUpdateDto.FechaNacimiento;
            maestro.Telefono = entityUpdateDto.Telefono;
            maestro.Email = entityUpdateDto.Email;

            await _maestroRepository.UpdateAsync(maestro, cancellationToken);

            var maestroDto = new MaestroDto(
                Id: maestro.Id,
                Nombre: maestro.Nombre,
                Apellido: maestro.Apellido,
                Direccion: maestro.Direccion,
                FechaNacimiento: maestro.FechaNacimiento,
                Telefono: maestro.Telefono,
                Email: maestro.Email);
            _logger.LogInformation("Maestro with ID {Id} successfully updated.", maestro.Id);
            return maestroDto;
        }
    }
}

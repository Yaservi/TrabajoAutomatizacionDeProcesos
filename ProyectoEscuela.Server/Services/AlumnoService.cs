using ProyectoEscuela.Server.DTOs.Alumno;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoEscuela.Server.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly ILogger<AlumnoDto> _logger;
        public AlumnoService(IAlumnoRepository alumnoRepository, ILogger<AlumnoDto> logger)
        {
            _alumnoRepository = alumnoRepository;
            _logger = logger;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id, cancellationToken);
            if (alumno == null)
            {
                _logger.LogError("Alumno with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Alumno with ID {id} not found.");
            }
            await _alumnoRepository.DeleteChangesAsync(alumno, cancellationToken);

            _logger.LogInformation("Alumno with ID {Id} successfully deleted.", id);
            return id;
        }

        public async Task<AlumnoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id, cancellationToken);

            if (alumno == null)
            {
                _logger.LogError("Alumno with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Alumno with ID {id} not found.");
            }

            var alumnoDto = new AlumnoDto(
                Id: alumno.Id,
                Nombre: alumno.Nombre,
                Apellido: alumno.Apellido,
                Direccion: alumno.Direccion,
                FechaNacimiento: alumno.FechaNacimiento,
                Telefono: alumno.Telefono,
                Email: alumno.Email
            );  

            _logger.LogInformation("Alumno with ID {Id} successfully retrieved.", id);
            return alumnoDto;

        }

        public async Task<PageResult<AlumnoDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogError("Invalid page parameters: pageNumber and pageSize must be greater than 0. ");
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            var entityWithNumber = await _alumnoRepository.GetPageResultAsync(pageNumber, pageSize, cancellationToken);
            var dto = entityWithNumber.Items.Select(x => new AlumnoDto(
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

            PageResult<AlumnoDto> pageResult = new()
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

        public async Task<AlumnoDto> InsertAsync(AlumnoInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            if(entityInsertDto == null)
            {
                _logger.LogError("AlumnoInsertDto is null.");
                throw new ArgumentNullException(nameof(entityInsertDto), "AlumnoInsertDto cannot be null.");
            }

            var alumno = new Alumno
            {
                Id = Guid.NewGuid(),
                Nombre = entityInsertDto.Nombre,
                Apellido = entityInsertDto.Apellido,
                Direccion = entityInsertDto.Direccion,
                FechaNacimiento = entityInsertDto.FechaNacimiento,
                Telefono = entityInsertDto.Telefono,
                Email = entityInsertDto.Email
            };

            await _alumnoRepository.AddAsync(alumno, cancellationToken);

            var alumnoDto = new AlumnoDto(
                Id: alumno.Id,
                Nombre: alumno.Nombre,
                Apellido: alumno.Apellido,
                Direccion: alumno.Direccion,
                FechaNacimiento: alumno.FechaNacimiento,
                Telefono: alumno.Telefono,
                Email: alumno.Email
            );
            _logger.LogInformation("Alumno with ID {Id} successfully created.", alumno.Id);
            return alumnoDto;
        }

        public async Task<AlumnoDto> UpdateAsync(Guid id, AlumnoUpdateDto entityUpdateDto, CancellationToken cancellationToken)
        {
           var alumno = await _alumnoRepository.GetByIdAsync(id, cancellationToken);
            if (alumno == null)
            {
                _logger.LogError("Alumno with ID {Id} not found.", id);
                throw new KeyNotFoundException($"Alumno with ID {id} not found.");
            }
            if (entityUpdateDto == null)
            {
                _logger.LogError("AlumnoUpdateDto is null.");
                throw new ArgumentNullException(nameof(entityUpdateDto), "AlumnoUpdateDto cannot be null.");
            }

            alumno.Nombre = entityUpdateDto.Nombre;
            alumno.Apellido = entityUpdateDto.Apellido;
            alumno.Direccion = entityUpdateDto.Direccion;
            alumno.FechaNacimiento = entityUpdateDto.FechaNacimiento;
            alumno.Telefono = entityUpdateDto.Telefono;
            alumno.Email = entityUpdateDto.Email;

            await _alumnoRepository.UpdateAsync(alumno, cancellationToken);

            var alumnoDto = new AlumnoDto(
                Id: alumno.Id,
                Nombre: alumno.Nombre,
                Apellido: alumno.Apellido,
                Direccion: alumno.Direccion,
                FechaNacimiento: alumno.FechaNacimiento,
                Telefono: alumno.Telefono,
                Email: alumno.Email
            );
            _logger.LogInformation("Alumno with ID {Id} successfully updated.", id);
            return alumnoDto;
        }
    }
}

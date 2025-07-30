//using ProyectoEscuela.Server.DTOs.Calificacion;
//using ProyectoEscuela.Server.Interfaces.Repository;
//using ProyectoEscuela.Server.Interfaces.Services;
//using ProyectoEscuela.Server.Models;
//using ProyectoEscuela.Server.Pagination;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProyectoEscuela.Server.Services
//{
//    public class CalificacionService : ICalificacionService
//    {
//        private readonly ICalificacionRepository _calificacionRepository;

//        public CalificacionService(ICalificacionRepository calificacionRepository)
//        {
//            _calificacionRepository = calificacionRepository;
//        }

//        public async Task<CalificacionDto> InsertAsync(CalificacionInsertDto entityInsertDto, CancellationToken cancellationToken)
//        {
//            var entity = new Calificacion
//            {
//                Id = Guid.NewGuid(),
//                Participacion = entityInsertDto.Participacion,
//                PrimerParcial = entityInsertDto.PrimerParcial,
//                SegundoParcial = entityInsertDto.SegundoParcial,
//                ExamenFinal = entityInsertDto.ExamenFinal,
//                TrabajoInvestigacion = entityInsertDto.TrabajoInvestigacion,
//                TrabajoFinal = entityInsertDto.TrabajoFinal,
//                Nota = entityInsertDto.Nota,
//                IdAlumno = entityInsertDto.IdAlumno,
//                IdMateria = entityInsertDto.IdMateria
//                //fechaCreacion = DateTime.UtcNow
//                // fechaModificacion = null, // no es necesario asignar, es nullable
//            };

//            try
//            {
//                await _calificacionRepository.AddAsync(entity, cancellationToken);
//            }
//            catch (Exception ex)
//            {
//                // Lanzamos con mensaje interno para depurar
//                throw new Exception("Error guardando la calificación: " + ex.GetBaseException().Message);
//            }

//            return new CalificacionDto(
//                entity.Id,
//                entity.Participacion,
//                entity.PrimerParcial,
//                entity.SegundoParcial,
//                entity.ExamenFinal,
//                entity.TrabajoInvestigacion,
//                entity.TrabajoFinal,
//                entity.Nota,
//                entity.IdAlumno,
//                entity.IdMateria,
//                entity.fechaCreacion,
//                entity.fechaModificacion
//            );
//        }

//        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
//        {
//            var entity = await _calificacionRepository.GetByIdAsync(id, cancellationToken);
//            if (entity == null)
//                return Guid.Empty;

//            await _calificacionRepository.DeleteChangesAsync(entity, cancellationToken);
//            return id;
//        }



//        public Task<CalificacionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<PageResult<CalificacionDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<CalificacionDto> UpdateAsync(Guid id, CalificacionUpdateDto dto, CancellationToken cancellationToken)
//        {
//            var entity = await _calificacionRepository.GetByIdAsync(id, cancellationToken);

//            if (entity == null)
//                throw new ArgumentException($"No existe una calificación con ID: {id}");

//            entity.Participacion = dto.Participacion;
//            entity.PrimerParcial = dto.PrimerParcial;
//            entity.SegundoParcial = dto.SegundoParcial;
//            entity.ExamenFinal = dto.ExamenFinal;
//            entity.TrabajoInvestigacion = dto.TrabajoInvestigacion;
//            entity.TrabajoFinal = dto.TrabajoFinal;
//            entity.Nota = dto.Nota;
//            entity.IdAlumno = dto.IdAlumno;
//            entity.IdMateria = dto.IdMateria;
//            entity.fechaModificacion = DateTime.UtcNow;

//            await _calificacionRepository.UpdateAsync(entity, cancellationToken);

//            return new CalificacionDto(
//                entity.Id,
//                entity.Participacion,
//                entity.PrimerParcial,
//                entity.SegundoParcial,
//                entity.ExamenFinal,
//                entity.TrabajoInvestigacion,
//                entity.TrabajoFinal,
//                entity.Nota,
//                entity.IdAlumno,
//                entity.IdMateria,
//                entity.fechaCreacion,
//                entity.fechaModificacion
//            );
//        }

//        public async Task<IEnumerable<CalificacionDto>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            var calificaciones = await _calificacionRepository.GetAllAsync(cancellationToken);

//            return calificaciones.Select(c => new CalificacionDto(
//                c.Id,
//                c.Participacion,
//                c.PrimerParcial,
//                c.SegundoParcial,
//                c.ExamenFinal,
//                c.TrabajoInvestigacion,
//                c.TrabajoFinal,
//                c.Nota,
//                c.IdAlumno,
//                c.IdMateria,
//                c.fechaCreacion,
//                c.fechaModificacion
//            ));
//        }



//    }
//}


using ProyectoEscuela.Server.DTOs.Calificacion;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Interfaces.Services;
using ProyectoEscuela.Server.Models;
using ProyectoEscuela.Server.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoEscuela.Server.Services
{
    public class CalificacionService : ICalificacionService
    {
        private readonly ICalificacionRepository _calificacionRepository;

        public CalificacionService(ICalificacionRepository calificacionRepository)
        {
            _calificacionRepository = calificacionRepository;
        }

        public async Task<CalificacionDto> InsertAsync(CalificacionInsertDto entityInsertDto, CancellationToken cancellationToken)
        {
            var entity = new Calificacion
            {
                Id = Guid.NewGuid(),
                Participacion = entityInsertDto.Participacion,
                PrimerParcial = entityInsertDto.PrimerParcial,
                SegundoParcial = entityInsertDto.SegundoParcial,
                ExamenFinal = entityInsertDto.ExamenFinal,
                TrabajoInvestigacion = entityInsertDto.TrabajoInvestigacion,
                TrabajoFinal = entityInsertDto.TrabajoFinal,
                Nota = entityInsertDto.Nota,
                IdAlumno = entityInsertDto.IdAlumno,
                IdMateria = entityInsertDto.IdMateria
            };

            try
            {
                await _calificacionRepository.AddAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Error guardando la calificación: " + ex.GetBaseException().Message);
            }

            return new CalificacionDto(
                entity.Id,
                entity.Participacion,
                entity.PrimerParcial,
                entity.SegundoParcial,
                entity.ExamenFinal,
                entity.TrabajoInvestigacion,
                entity.TrabajoFinal,
                entity.Nota,
                entity.IdAlumno,
                entity.IdMateria,
                entity.fechaCreacion,
                entity.fechaModificacion
            );
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _calificacionRepository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return Guid.Empty;

            await _calificacionRepository.DeleteChangesAsync(entity, cancellationToken);
            return id;
        }

        public Task<CalificacionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<CalificacionDto>> GetPageResult(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Los valores de paginación deben ser mayores a cero.");

            var totalItems = await _calificacionRepository.CountAsync(cancellationToken);

            var calificaciones = await _calificacionRepository.GetPaginatedAsync(pageNumber, pageSize, cancellationToken);

            var items = calificaciones.Select(c => new CalificacionDto(
                c.Id,
                c.Participacion,
                c.PrimerParcial,
                c.SegundoParcial,
                c.ExamenFinal,
                c.TrabajoInvestigacion,
                c.TrabajoFinal,
                c.Nota,
                c.IdAlumno,
                c.IdMateria,
                c.fechaCreacion,
                c.fechaModificacion
            ));

            return new PageResult<CalificacionDto>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<CalificacionDto> UpdateAsync(Guid id, CalificacionUpdateDto dto, CancellationToken cancellationToken)
        {
            var entity = await _calificacionRepository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                throw new ArgumentException($"No existe una calificación con ID: {id}");

            entity.Participacion = dto.Participacion;
            entity.PrimerParcial = dto.PrimerParcial;
            entity.SegundoParcial = dto.SegundoParcial;
            entity.ExamenFinal = dto.ExamenFinal;
            entity.TrabajoInvestigacion = dto.TrabajoInvestigacion;
            entity.TrabajoFinal = dto.TrabajoFinal;
            entity.Nota = dto.Nota;
            entity.IdAlumno = dto.IdAlumno;
            entity.IdMateria = dto.IdMateria;
            entity.fechaModificacion = DateTime.UtcNow;

            await _calificacionRepository.UpdateAsync(entity, cancellationToken);

            return new CalificacionDto(
                entity.Id,
                entity.Participacion,
                entity.PrimerParcial,
                entity.SegundoParcial,
                entity.ExamenFinal,
                entity.TrabajoInvestigacion,
                entity.TrabajoFinal,
                entity.Nota,
                entity.IdAlumno,
                entity.IdMateria,
                entity.fechaCreacion,
                entity.fechaModificacion
            );
        }

        public async Task<IEnumerable<CalificacionDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var calificaciones = await _calificacionRepository.GetAllAsync(cancellationToken);

            return calificaciones.Select(c => new CalificacionDto(
                c.Id,
                c.Participacion,
                c.PrimerParcial,
                c.SegundoParcial,
                c.ExamenFinal,
                c.TrabajoInvestigacion,
                c.TrabajoFinal,
                c.Nota,
                c.IdAlumno,
                c.IdMateria,
                c.fechaCreacion,
                c.fechaModificacion
            ));
        }
    }
}

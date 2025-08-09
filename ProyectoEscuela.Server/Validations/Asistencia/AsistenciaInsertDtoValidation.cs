using FluentValidation;
using ProyectoEscuela.Server.DTOs.Asistencia;

namespace ProyectoEscuela.Server.Validations.Asistencia
{
    public class AsistenciaInsertDtoValidation : AbstractValidator<AsistenciaInsertDto>
    {
        public AsistenciaInsertDtoValidation()
        {
           
            RuleFor(x => x.FechaAsistencia)
                .NotEmpty().WithMessage("La fecha de asistencia no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de asistencia no puede ser futura.");
            RuleFor(x => x.AlumnoId)
                .NotEmpty().WithMessage("El ID del alumno no puede estar vacío.");
            RuleFor(x => x.MateriaId)
                .NotEmpty().WithMessage("El ID de la materia no puede estar vacío.");
        }
    }
}

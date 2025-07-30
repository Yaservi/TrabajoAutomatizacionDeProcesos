using FluentValidation;
using ProyectoEscuela.Server.DTOs.Calificacion;

namespace ProyectoEscuela.Server.Validations.Calificacion
{
    public class CalificacionInsertDtoValidator : AbstractValidator<CalificacionInsertDto>
    {
        public CalificacionInsertDtoValidator()
        {
            RuleFor(x => x.Participacion)
                .InclusiveBetween(0, 100).WithMessage("Participación debe estar entre 0 y 100");

            RuleFor(x => x.PrimerParcial)
                .InclusiveBetween(0, 100).WithMessage("Primer Parcial debe estar entre 0 y 100");

            RuleFor(x => x.SegundoParcial)
                .InclusiveBetween(0, 100).WithMessage("Segundo Parcial debe estar entre 0 y 100");

            RuleFor(x => x.ExamenFinal)
                .InclusiveBetween(0, 100).WithMessage("Examen Final debe estar entre 0 y 100");

            RuleFor(x => x.TrabajoInvestigacion)
                .InclusiveBetween(0, 100).WithMessage("Trabajo de Investigación debe estar entre 0 y 100");

            RuleFor(x => x.TrabajoFinal)
                .InclusiveBetween(0, 100).WithMessage("Trabajo Final debe estar entre 0 y 100");

            RuleFor(x => x.Nota)
                .InclusiveBetween(0, 100).WithMessage("Nota debe estar entre 0 y 100");

            //RuleFor(x => x.IdAlumno)
            //    .NotEmpty().WithMessage("IdAlumno es obligatorio");

            //RuleFor(x => x.IdMateria)
            //    .NotEmpty().WithMessage("IdMateria es obligatorio");
        }
    }
}
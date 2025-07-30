using FluentValidation;
using ProyectoEscuela.Server.DTOs.Materia;

namespace ProyectoEscuela.Server.Validations.Materia
{
    public class MateriaUpdateDtoValidation : AbstractValidator<MateriaUpdateDto>
    {
        public MateriaUpdateDtoValidation()
        {
            RuleFor(m => m.NombreMateria)
               .NotNull().WithMessage("El nombre no puede ser nulo.")
               .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
               .MaximumLength(150).WithMessage("El nombre no debe exceder los 150 caracteres.")
               .Must(name => !name.StartsWith(" ") && !name.EndsWith(" "))
               .WithMessage("El nombre no puede iniciar ni terminar con espacio.")
               .Must(name => !System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}"))
               .WithMessage("El nombre no puede contener espacios múltiples seguidos.");

            RuleFor(m => m.Descripcion)
               .NotNull().WithMessage("La Descripcion no puede ser nulo.")
               .MinimumLength(3).WithMessage("La Descripcion  debe tener al menos 3 caracteres.")
               .MaximumLength(850).WithMessage("La Descripcion no debe exceder los 850 caracteres.")
               .Must(name => !name.StartsWith(" ") && !name.EndsWith(" "))
               .WithMessage("La Descripcion  no puede iniciar ni terminar con espacio.")
               .Must(name => !System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}"))
               .WithMessage("La Descripcion  puede contener espacios múltiples seguidos.");

            RuleFor(m => m.MaestroId)
                .NotEmpty().WithMessage("El ID del maestro no puede ser nulo o vacío.")
                .Must(id => id != Guid.Empty).WithMessage("El ID del maestro no puede ser un GUID vacío.");
        }
    }
}

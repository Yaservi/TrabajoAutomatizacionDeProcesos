using FluentValidation;
using ProyectoEscuela.Server.DTOs.Alumno;

namespace ProyectoEscuela.Server.Validations.Alumno
{
    public class AlumnoUpdateDtoValidation: AbstractValidator<AlumnoUpdateDto>
    {
        public AlumnoUpdateDtoValidation() 
        {
            RuleFor(a => a.Nombre).
                NotNull().WithMessage("Nose permiten nulo").
                MinimumLength(3).WithMessage("Debe tener al menos 3 letras").
                MaximumLength(50).WithMessage("No debe tener mas de 50 letras").
                Must(name => !name.StartsWith(" ") && !name.EndsWith(" ")).WithMessage("No puede iniciar ni terminar con espacio.").
                Must(name => !System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}")).WithMessage("No puede contener espacios múltiples seguidos.");

            RuleFor(a => a.Apellido).
                  NotNull().WithMessage("Nose permiten nulo").
                MinimumLength(3).WithMessage("Debe tener al menos 3 letras").
                MaximumLength(50).WithMessage("No debe tener mas de 50 letras").
                Must(name => !name.StartsWith(" ") && !name.EndsWith(" ")).WithMessage("No puede iniciar ni terminar con espacio.").
                Must(name => !System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}")).WithMessage("No puede contener espacios múltiples seguidos.");

            RuleFor(a => a.Direccion).
                  NotNull().WithMessage("Nose permiten nulo").
                MinimumLength(3).WithMessage("Debe tener al menos 3 letras").
                MaximumLength(50).WithMessage("No debe tener mas de 50 letras").
                Must(name => !name.StartsWith(" ") && !name.EndsWith(" ")).WithMessage("No puede iniciar ni terminar con espacio.").
                Must(name => !System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}")).WithMessage("No puede contener espacios múltiples seguidos.");

            RuleFor(a => a.Telefono).
                NotEmpty().WithMessage("El teléfono es obligatorio.").
                Matches(@"^\d{10}$").WithMessage("El teléfono debe contener exactamente 10 dígitos.");

            RuleFor(a => a.FechaNacimiento).
                NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.").
                LessThan(DateTime.Today).WithMessage("La fecha de nacimiento debe ser anterior a hoy.");

            RuleFor(a => a.Email).
                NotEmpty().WithMessage("El correo electrónico es obligatorio.").
                EmailAddress().WithMessage("El correo electrónico no es válido.");
        }
    }
}

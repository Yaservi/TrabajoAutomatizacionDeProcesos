namespace ProyectoEscuela.Server.DTOs.Alumno
{
    public sealed record AlumnoDto(
    Guid Id,
    string Nombre,
    string Apellido,
    string Direccion,
    DateTime FechaNacimiento,
    string Telefono,
    string Email);
}

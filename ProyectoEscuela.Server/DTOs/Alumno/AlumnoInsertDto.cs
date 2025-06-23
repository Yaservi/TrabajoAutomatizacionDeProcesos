namespace ProyectoEscuela.Server.DTOs.Alumno
{
    public sealed record AlumnoInsertDto(
        string Nombre,
        string Apellido,
        string Direccion,
        DateTime FechaNacimiento,
        string Telefono,
        string Email);
}

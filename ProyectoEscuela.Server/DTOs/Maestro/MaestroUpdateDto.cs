namespace ProyectoEscuela.Server.DTOs.Maestro
{
    public sealed record MaestroUpdateDto(
        string Nombre,
        string Apellido,
        string Direccion,
        DateTime FechaNacimiento,
        string Telefono,
        string Email
        );
}

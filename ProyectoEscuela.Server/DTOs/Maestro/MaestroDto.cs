namespace ProyectoEscuela.Server.DTOs.Maestro
{
    public sealed record MaestroDto(
        Guid Id,
        string Nombre,
        string Apellido,
        string Direccion,
        DateTime FechaNacimiento,
        string Telefono,
        string Email);
}

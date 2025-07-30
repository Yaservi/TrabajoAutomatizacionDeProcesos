namespace ProyectoEscuela.Server.DTOs.Maestro
{
    public sealed record MaestroInsertDto(
        string Nombre,
        string Apellido,
        string Direccion,
        DateTime FechaNacimiento,
        string Telefono,
        string Email);
}

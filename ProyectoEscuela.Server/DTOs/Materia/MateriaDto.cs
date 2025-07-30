namespace ProyectoEscuela.Server.DTOs.Materia
{
    public sealed record MateriaDto(
        Guid Id,
        string NombreMateria,
        string Descripcion,
        Guid MaestroId
        );
}

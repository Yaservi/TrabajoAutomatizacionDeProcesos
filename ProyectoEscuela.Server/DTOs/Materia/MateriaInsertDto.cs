namespace ProyectoEscuela.Server.DTOs.Materia
{
    public sealed record MateriaInsertDto(
         string NombreMateria,
         string Descripcion,
         Guid MaestroId);
}

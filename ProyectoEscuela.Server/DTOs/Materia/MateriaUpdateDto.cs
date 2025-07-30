namespace ProyectoEscuela.Server.DTOs.Materia
{
    public sealed record MateriaUpdateDto(
         string NombreMateria,
         string Descripcion,
         Guid MaestroId
        );
}

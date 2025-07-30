namespace ProyectoEscuela.Server.DTOs.Asistencia
{
    public sealed record AsistenciaDto(
        Guid Id,
        string Estado,
        DateTime FechaAsistencia,
        Guid AlumnoId,
        Guid MateriaId
        );
}

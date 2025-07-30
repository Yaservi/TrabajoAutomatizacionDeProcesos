namespace ProyectoEscuela.Server.DTOs.Asistencia
{
    public sealed record AsistenciaUpdateDto(
        string Estado,
        DateTime FechaAsistencia,
        Guid AlumnoId,
        Guid MateriaId
        );
}

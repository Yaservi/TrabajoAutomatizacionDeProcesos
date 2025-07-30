namespace ProyectoEscuela.Server.DTOs.Asistencia
{
    public sealed record AsistenciaInsertDto
    (
        string Estado,
        DateTime FechaAsistencia,
        Guid AlumnoId,
        Guid MateriaId
    );
}

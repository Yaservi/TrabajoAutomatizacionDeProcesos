namespace ProyectoEscuela.Server.DTOs.Calificacion
{
    public sealed record CalificacionUpdateDto(
       Guid Id,
       double Participacion,
       double PrimerParcial,
       double SegundoParcial,
       double ExamenFinal,
       double TrabajoInvestigacion,
       double TrabajoFinal,
       double Nota,
       Guid IdAlumno,
       Guid IdMateria
   );
}

namespace ProyectoEscuela.Server.DTOs.Calificacion
{
    public sealed record CalificacionInsertDto(
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

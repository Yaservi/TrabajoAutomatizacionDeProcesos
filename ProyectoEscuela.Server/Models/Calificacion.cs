namespace ProyectoEscuela.Server.Models
{
    public class Calificacion : BaseEntity
    {
        public Guid Id { get; set; }
        public double Participacion { get; set; }
        public double PrimerParcial { get; set; }
        public double SegundoParcial { get; set; }
        public double ExamenFinal { get; set; }
        public double TrabajoInvestigacion { get; set; }
        public double TrabajoFinal { get; set; }
        public double Nota { get; set; }

        public Guid IdAlumno { get; set; }
        public Alumno Alumno { get; set; }

        public Guid IdMateria { get; set; }
        public Materia Materia { get; set; }
    }
}

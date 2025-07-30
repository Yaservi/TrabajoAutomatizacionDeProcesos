namespace ProyectoEscuela.Server.Models
{
    public class Asistencias : BaseEntity
    {
        public Guid Id { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAsistencia { get; set; }

        public Guid AlumnoId { get; set; }
        public virtual Alumno Alumno { get; set; }

        public Guid MateriaId { get; set; }
        public virtual Materia Materia { get; set; }
    }
}

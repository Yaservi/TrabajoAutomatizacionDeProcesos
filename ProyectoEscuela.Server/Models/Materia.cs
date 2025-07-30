namespace ProyectoEscuela.Server.Models
{
    public class Materia : BaseEntity
    {
        public Guid Id { get; set; }
        public string NombreMateria { get; set; }
        public string Descripcion { get; set; }

        public Guid MaestroId { get; set; }
        public virtual Maestro Maestro { get; set; }

        public virtual IEnumerable<Alumno> Alumno { get; set; }
        public virtual IEnumerable<Calificacion> Calificacion { get; set; }
        public virtual IEnumerable<Asistencias> Asistencias { get; set; }

    }
}

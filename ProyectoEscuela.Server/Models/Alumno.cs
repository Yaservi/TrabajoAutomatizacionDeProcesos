namespace ProyectoEscuela.Server.Models
{
    public class Alumno: BaseEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<Materia> Materia { get; set; }
        public virtual IEnumerable<Asistencias> Asistencias { get; set; }
        public virtual IEnumerable<Calificacion> Calificacion { get; set; }

    }
}

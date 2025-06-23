namespace ProyectoEscuela.Server.Models
{
    public class Maestro : BaseEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Materia> Materia { get; set; }
    }
}

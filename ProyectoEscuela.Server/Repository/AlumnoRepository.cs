using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Repository
{
    public class AlumnoRepository:CommonRepository<Alumno>, IAlumnoRepository
    {
        public AlumnoRepository(AppDBContext context): base (context) { }
    }
}

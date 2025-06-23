
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Repository
{
    public class CalificacionRepository: CommonRepository<Calificacion>, ICalificacionRepository
    {
        public CalificacionRepository(AppDBContext context) : base(context) { }
    }
}

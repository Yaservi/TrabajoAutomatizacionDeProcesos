using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Repository
{
    public class AsistenciaRepository:CommonRepository<Asistencias>, IAsistenciaRepository
    {
        public AsistenciaRepository(AppDBContext context) : base(context)
        {
        }
    }
}

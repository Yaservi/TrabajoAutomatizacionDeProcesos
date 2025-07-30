using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Repository
{
    public class MaestroRepository: CommonRepository<Maestro>, IMaestroRepository
    {
        public MaestroRepository(AppDBContext context) : base(context)
        {
        }
    }

}

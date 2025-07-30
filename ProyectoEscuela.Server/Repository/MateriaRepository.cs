

using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Repository
{
    public class MateriaRepository: CommonRepository<Materia>, IMateriaRepository
    {
        public MateriaRepository(AppDBContext context): base (context) { }
    }
}


//using ProyectoEscuela.Server.Interfaces.Repository;
//using ProyectoEscuela.Server.Models;

//namespace ProyectoEscuela.Server.Repository
//{
//    public class CalificacionRepository: CommonRepository<Calificacion>, ICalificacionRepository
//    {
//        public CalificacionRepository(AppDBContext context) : base(context) { }
//    }
//}


using Microsoft.EntityFrameworkCore;
using ProyectoEscuela.Server.Interfaces.Repository;
using ProyectoEscuela.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoEscuela.Server.Repository
{
    public class CalificacionRepository : CommonRepository<Calificacion>, ICalificacionRepository
    {
        private readonly AppDBContext _context;

        public CalificacionRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Calificaciones.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Calificacion>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Calificaciones
                .AsNoTracking()
                .OrderBy(c => c.fechaCreacion) // O usa c.Id si no tienes fecha
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}

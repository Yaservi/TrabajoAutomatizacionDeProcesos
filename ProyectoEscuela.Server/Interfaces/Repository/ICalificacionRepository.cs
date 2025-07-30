using ProyectoEscuela.Server.Models;

namespace ProyectoEscuela.Server.Interfaces.Repository
{
    public interface ICalificacionRepository: ICommonRepository<Calificacion>
    {

        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Calificacion>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    }
}

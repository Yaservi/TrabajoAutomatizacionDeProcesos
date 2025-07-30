using Azure;
using ProyectoEscuela.Server.DTOs.Calificacion;

namespace ProyectoEscuela.Server.Interfaces.Services
{
    public interface ICalificacionService: ICommonService<CalificacionInsertDto, CalificacionUpdateDto, CalificacionDto>
    {
        Task<IEnumerable<CalificacionDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}

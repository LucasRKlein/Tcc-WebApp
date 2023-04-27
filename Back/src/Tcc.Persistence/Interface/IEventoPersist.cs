using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Models;

namespace Tcc.Persistence.Interface
{
    public interface IEventoPersist
    {
        Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}
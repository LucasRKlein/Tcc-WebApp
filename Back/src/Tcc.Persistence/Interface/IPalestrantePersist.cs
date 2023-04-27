using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Models;

namespace Tcc.Persistence.Interface
{
    public interface IPalestrantePersist : IGeralPersist
    {
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}
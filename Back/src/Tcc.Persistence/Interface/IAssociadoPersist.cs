using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Models;

namespace Tcc.Persistence.Interface
{
    public interface IAssociadoPersist : IGeralPersist
    {
        Task<PageList<Associado>> GetAllAssociadosAsync(PageParams pageParams);
        Task<Associado> GetAssociadoByUserIdAsync(int userId);

        //Task<PageList<Associado>> GetAllAssociadosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        //Task<Associado> GetAssociadoByIdAsync(int userId, int associadoId, bool includePalestrantes = false);
    }
}

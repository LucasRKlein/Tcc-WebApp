using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Models;

namespace Tcc.Persistence.Interface
{
    public interface IAssociadoPersist : IGeralPersist
    {
        Task<PageList<Associado>> GetAllAssociadosAsync(PageParams pageParams);

        Task<Associado> GetAssociadoByIdAsync(int associadoId);
    }
}

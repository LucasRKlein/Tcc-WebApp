using System.Threading.Tasks;
using Tcc.Application.Dtos;
using Tcc.Persistence.Models;

namespace Tcc.Application.Interfaces
{
    public interface IAssociadoService
    {
        Task<AssociadoDto> CreateAssociado(AssociadoDto model);
        Task<AssociadoDto> UpdateAssociado(int associadoId, AssociadoDto model);
        Task<bool> DeleteAssociado(int associadoId);

        Task<PageList<AssociadoDto>> GetAllAssociadosAsync(PageParams pageParams);
        Task<AssociadoDto> GetAssociadoByIdAsync(int associadoId);
    }
}

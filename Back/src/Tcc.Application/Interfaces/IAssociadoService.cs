using System;
using System.Threading.Tasks;
using Tcc.Application.Dtos;
using Tcc.Persistence.Models;

namespace Tcc.Application.Interfaces
{
    public interface IAssociadoService
    {
        Task<AssociadoDto> CreateAssociado(AssociadoDto model);
        Task<AssociadoDto> UpdateAssociado(Guid associadoId, AssociadoDto model);
        Task<bool> DeleteAssociado(Guid associadoId);

        Task<PageList<AssociadoDto>> GetAllAssociadosAsync(PageParams pageParams);
        Task<AssociadoDto> GetAssociadoByIdAsync(Guid associadoId);
    }
}

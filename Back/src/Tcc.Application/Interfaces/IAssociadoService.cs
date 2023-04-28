using System.Threading.Tasks;
using Tcc.Application.Dtos;
using Tcc.Persistence.Models;

namespace Tcc.Application.Interfaces
{
    public interface IAssociadoService
    {
        Task<AssociadoDto> AddAssociados(int userId, AssociadoDto model);
        Task<AssociadoDto> UpdateAssociado(int userId, int eventoId, AssociadoDto model);
        Task<bool> DeleteAssociado(int userId, int eventoId);


        Task<PageList<AssociadoDto>> GetAllAssociadosAsync(int userId, PageParams pageParams);
        Task<AssociadoDto> GetAssociadoByIdAsync(int userId, int eventoId);


        //Trocar palestrantes por Vistorias ou Veiculos

        //Task<PageList<AssociadoDto>> GetAllAssociadosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        //Task<AssociadoDto> GetAssociadoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}

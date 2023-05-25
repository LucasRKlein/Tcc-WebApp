using TccApp.Domain.Dtos;
using TccApp.Domain.Models;
using TccApp.Models;

namespace TccApp.Services
{
    public interface IRestService
    {
        Task<BackResponseDto<TokenResponse>> LoginAsync(LoginDto login);
        void SetCredentials(string token);
        Task LogoutAsync();
        Task<UsuarioAppModel> GetPerfilAppAsync();
        Task<List<AssociadoModel>> GetAssociadosAsync();
        //Task<List<CategoriaVistoriaModel>> GetCategoriasAsync();

        Task<BackResponseDto<AssociadoModel>> PostAssociadoAsync(AssociadoModel model);
        //Task<BackResponseDto<VistoriaAcessorioModel>> PostVistoriaAcessorioAsync(VistoriaAcessorioModel model);        
    }
}

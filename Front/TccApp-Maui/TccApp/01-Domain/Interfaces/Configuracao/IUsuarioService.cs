using TccApp.Domain.Dtos;
using TccApp.Domain.Models;

namespace TccApp.Domain.Interfaces
{
    public interface IUsuarioService: IRepository<UsuarioAppModel>
    {
        Task<BackResponseDto<TokenResponse>> LoginAsync(string usuario, string senha);
        Task LogoutAsync();
        Task<UsuarioAppModel> GetPerfilAppAsync();
        UsuarioAppModel GetPerfilAppLocal();
        void ClearPerfilLocal();
        void SavePerfilLocal(UsuarioAppModel model);
    }
}

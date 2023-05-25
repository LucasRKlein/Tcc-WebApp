using TccApp.Domain.Dtos;
using TccApp.Domain.Interfaces;
using TccApp.Domain.Models;
using TccApp.Infraestructure.Criptografia;
using TccApp.Infraestructure.Helpers;

namespace TccApp.Services
{
    public class UsuarioService : Repository<UsuarioAppModel>, IUsuarioService
    {
        private readonly IRestService _restService;
        private readonly string _chaveCripto;

        public UsuarioService(IRestService service)
        {
            _restService = service;
            _chaveCripto = "#Kleincode_@TccApp#0702";
        }

        public async Task<BackResponseDto<TokenResponse>> LoginAsync(string usuario, string senha)
        {
            var loginDto = new LoginDto()
            {
                UserName = usuario,
                Password = senha,
            };

            var result = await _restService.LoginAsync(loginDto);
            if (result.Data.Token != string.Empty)
            {
                _restService.SetCredentials(result.Data.Token);
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _restService.LogoutAsync();
        }

        public async Task<UsuarioAppModel> GetPerfilAppAsync()
        {
            var perfil = await _restService.GetPerfilAppAsync();
            
            return perfil;
        }

        public UsuarioAppModel GetPerfilAppLocal()
        {
            var encriptador = new Encriptador(_chaveCripto);

            var perfil = Get(1.GetGuidSequencial());
            if (perfil != null)
            {
                perfil.Password = encriptador.Decriptar(perfil.Password);
            }

            return perfil;
        }

        public void ClearPerfilLocal()
        {
            var perfil = Get(1.GetGuidSequencial());

            if (perfil == null) return;

            Delete(perfil);
        }

        public void SavePerfilLocal(UsuarioAppModel model)
        {
            ClearPerfilLocal();

            //Sempre será GUID = 1
            model.Id = 1.GetGuidSequencial();

            var encriptador = new Encriptador(_chaveCripto);
            model.Password = encriptador.Encriptar(model.Password);

            Create(model);
        }
    }
}

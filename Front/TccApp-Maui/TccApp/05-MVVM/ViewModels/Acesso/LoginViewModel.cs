using TccApp.Domain.Interfaces;
using TccApp.Infraestructure.Helpers;

namespace TccApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConnectivity _connectivity;

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string password;

        [RelayCommand]
        public async Task Login()
        {
            await LoginAsync();
        }
        
        public LoginViewModel(IConnectivity connectivity,
            IUsuarioService usuarioService)
        {
            _connectivity = connectivity;
            _usuarioService = usuarioService;

            Title = "Login";
        }
       
        private async Task LoginAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Sem Conexão!","Favor verificar a internet e tentar novamente.", "OK");
                return;
            }

            IsBusy = true;

            //await Task.Delay(3000);

            var dataResponse = await _usuarioService.LoginAsync(userName, password);

            if (dataResponse == null)
            {
                await Shell.Current.DisplayAlert("Erro!", "Erro de comunicação.", "OK");
                return;
            }

            if (!dataResponse.Success)
            {
                await Shell.Current.DisplayAlert("Erro!", dataResponse.Errors, "OK");
                return;
            }

            var perfil = await _usuarioService.GetPerfilAppAsync();
            if (perfil == null)
            {
                await Shell.Current.DisplayAlert("Erro!", "Erro de comunicação.", "OK");
                return;
            }

            await _usuarioService.LogoutAsync();

            //retorno de perfil da plataforma não traz password
            perfil.Password = password;

            _usuarioService.SavePerfilLocal(perfil);

            IsBusy = false;

            await Shell.Current.GoToAsync($"..?loginId={1.GetGuidSequencial()}");
        }
    }
}

using TccApp.Domain.Interfaces;
using TccApp.Domain.Models;
using TccApp.Infraestructure.Helpers;
using TccApp.Views;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamId), "id")]
    public partial class PerfilViewModel : BaseItemViewModel<UsuarioAppModel>, IQueryAttributable
    {
        [ObservableProperty]
        string userName;
        
        [ObservableProperty]
        string nome;

        [ObservableProperty]
        bool enabledLogin;

        [RelayCommand]
        public async Task Login()
        {
            await LoginAsync();
        }

        private readonly IUsuarioService service;
        public PerfilViewModel(IUsuarioService service) : base(service)
        {
            this.service = service;

            Title = "Perfil do Usuário";
            EnabledLogin = false;
            ParamId = 1.GetGuidSequencial().ToString();
        }

        protected override void SetModelFromView()
        {
            //Não fazer nada neste caso
        }

        protected override void SetViewFromModel()
        {
            this.UserName = Model.UserName;
            this.Nome = Model.Nome;
        }

        private async Task LoginAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        
        protected override async void SetModel(string id)
        {
            if (!GetPerfil())
            {
                await Shell.Current.DisplayAlert("Info", "Não há perfil registrado. É necessário fazer o login.", "OK");

                await LoginAsync();
            }
        }

        private bool GetPerfil()
        {
            //Sempre fixo
            Model = service.Get(1.GetGuidSequencial());

            if (Model != null)
            {
                SetViewFromModel();

                EnabledLogin = false;
                return true;
            }

            EnabledLogin = true;
            return false;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("loginId"))
            {
                //Não é preciso utilizar o parâmetro pois é o Guid = "1" fixo
                GetPerfil();
            }
        }
    }
}
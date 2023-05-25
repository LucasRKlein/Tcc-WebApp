
using TccApp.Domain.Interfaces;
using TccApp.Views;

namespace TccApp.ViewModels
{
    public partial class LogoutViewModel : BaseViewModel
    {
        [RelayCommand]
        public async Task Logout()
        {
            await LogoutAppAsync();
        }

        private readonly IUsuarioService usuarioService;
        private readonly IAssociadoService associadoService;
        //private readonly IAcessorioVeiculoService acessorioService;
        
        public LogoutViewModel(IUsuarioService usuarioService,
            IAssociadoService associadoService)
        {
            this.usuarioService = usuarioService;
            this.associadoService = associadoService;

            Title = "Sair do App";
        }

        private async Task LogoutAppAsync()
        {
            int qtdAssociados = associadoService.Count();
            var mensagem = "";
            
            if (qtdAssociados > 0)
            {
                mensagem += $"Há {qtdAssociados} associado(s) não sincronizado(s) com a base.{"\n\n"}";
            }
            mensagem += $">>> Todos dados serão excluídos <<<";
            mensagem += $"{"\n\n"}Confirmar saída do App?";


            var result = await Shell.Current.DisplayAlert("Atenção!", mensagem, "Confirmar", "Cancelar");

            if (result)
            {
                IsBusy = true;

                DeleteAssociados();
                DeleteTabelas();
                DeleteUsuario();

                IsBusy = false;

                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }        

        private void DeleteAssociados()
        {
            var listaVistorias = associadoService.GetAll();
            foreach (var item in listaVistorias)
            {
                associadoService.DeleteAssociado(item);
            }
        }

        private void DeleteTabelas()
        {
            //acessorioService.DeleteAll();
            //categoriaService.DeleteAll();
            //avariaService.DeleteAll();
            //remocaoItemService.DeleteAll();
            //remocaoItemTipoService.DeleteAll();
        }

        private void DeleteUsuario()
        {
            usuarioService.DeleteAll();
        }
    }
}
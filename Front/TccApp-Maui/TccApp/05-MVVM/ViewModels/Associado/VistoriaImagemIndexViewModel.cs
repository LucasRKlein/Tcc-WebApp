using TccApp.Domain.Dtos;
using TccApp.Domain.Interfaces;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamDto), "veiculoDto")]
    public partial class VistoriaImagemIndexViewModel : BaseViewModel
    {
        public VeiculoDto ParamDto
        {
            set
            {
                veiculoDto = value;
                GetLista();
            }
        }

        private VeiculoDto veiculoDto;


        #region Controles de Tela

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task DeleteItem(VistoriaImagemModel model)
        {
            await DeleteItemAsync(model);
        }

        [RelayCommand]
        public async Task CreateNewItem()
        {
            await CreateNewItemAsync();
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await GoBackAsync();
        }

        [RelayCommand]
        public Task GetListaRefresh()
        {
            GetLista();
            return Task.CompletedTask;
        }

        [ObservableProperty]
        ObservableCollection<VistoriaImagemModel> lista = new ObservableCollection<VistoriaImagemModel>();

        #endregion

        private readonly IVistoriaImagemService service;
        private Guid associadoId;

        public VistoriaImagemIndexViewModel(IVistoriaImagemService service) : base()
        {
            Title = "Veículo";

            this.service = service;
        }

        private void GetLista()
        {
            //IsRefreshing = true;

            Lista.Clear();

            var listaItemDb = service.GetAll(x => x.VeiculoId == veiculoDto.VeiculoId);

            foreach (var item in listaItemDb)
            {
                Lista.Add(item);
            }

            //IsRefreshing = false;
        }

        private async Task DeleteItemAsync(VistoriaImagemModel model)
        {
            bool result = await Shell.Current.DisplayAlert("Atenção", "Confirmar a exclusão do item?", "Confirmar", "Cancelar");

            if (!result)
                return;

            service.Delete(model);
            Lista.Remove(model);
        }

        protected async Task CreateNewItemAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(VistoriaImagemPage)}", true, new Dictionary<string, object>
            {
                ["veiculoDto"] = veiculoDto
            });
        }

        private async Task GoBackAsync()
        {
            //await Shell.Current.GoToAsync($"..?id={veiculoDto.AssociadoId}");

            await Shell.Current.GoToAsync($"{nameof(VeiculoPage)}", true, new Dictionary<string, object>
            {
                ["veiculoDto"] = veiculoDto
            });
        }
    }
}

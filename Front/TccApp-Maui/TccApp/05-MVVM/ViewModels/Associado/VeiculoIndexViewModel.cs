using TccApp.Domain.Dtos;
using TccApp.Domain.Interfaces;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamId), "id")]
    public partial class VeiculoIndexViewModel : BaseViewModel
    {
        public string ParamId
        {
            set
            {
                SetParamId(value);
            }
        }

        private VeiculoDto veiculoDto { get; set; }


        #region Controles de Tela
        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task DeleteItem(VeiculoModel model)
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
        
        [RelayCommand]
        public async Task GoToItem(VeiculoModel model)
        {
            await GoToPageItemAsync(model);
        }

        [ObservableProperty]
        ObservableCollection<VeiculoModel> lista = new ObservableCollection<VeiculoModel>();

        #endregion

        private readonly IVeiculoService service;
        private Guid associadoId;

        public VeiculoIndexViewModel(IVeiculoService service) : base()
        {
            Title = "Veículo";

            this.service = service;
        }

        private void SetParamId(string id)
        {
            associadoId = Guid.Parse(id);


            GetLista();
        }

        private void GetLista()
        {
            //IsRefreshing = true;

            Lista.Clear();

            var listaItemDb = service.GetAll(x => x.AssociadoId == associadoId);

            foreach (var item in listaItemDb)
            {
                Lista.Add(item);
            }

            //IsRefreshing = false;
        }

        private async Task GoToPageItemAsync(VeiculoModel model)
        {
            if (model == null)
                return;

            var strPage = nameof(VeiculoPage);

            if (string.IsNullOrEmpty(strPage))
            {
                await Shell.Current.DisplayAlert("Info", "Não há tela de detalhamento para este item.", "OK");
                return;
            }

            // preencho o DTO para mandar para a tela de veiculo
            veiculoDto = new VeiculoDto();
            veiculoDto.VeiculoId = model.Id;
            veiculoDto.AssociadoId = associadoId;

            //await Shell.Current.GoToAsync($"{strPage}?id={model.Id}");
            await Shell.Current.GoToAsync($"{nameof(VeiculoPage)}", true, new Dictionary<string, object>
            {
                ["veiculoDto"] = veiculoDto
            });
        }

        private async Task DeleteItemAsync(VeiculoModel model)
        {
            bool result = await Shell.Current.DisplayAlert("Atenção", "Confirmar a exclusão do item?", "Confirmar", "Cancelar");

            if (!result)
                return;

            service.Delete(model);
            Lista.Remove(model);
        }

        private async Task CreateNewItemAsync()
        {
            // preencho o DTO para mandar para a tela de veiculo
            veiculoDto = new VeiculoDto();
            veiculoDto.VeiculoId = Guid.Empty;
            veiculoDto.AssociadoId = associadoId;

            await Shell.Current.GoToAsync($"{nameof(VeiculoPage)}", true, new Dictionary<string, object>
            {
                ["veiculoDto"] = veiculoDto
            });
        }

        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AssociadoPage)}?id={associadoId}");
        }
    }
}

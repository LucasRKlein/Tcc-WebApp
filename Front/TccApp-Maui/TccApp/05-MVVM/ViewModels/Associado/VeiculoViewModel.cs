using AutoPatioApp.Domain.Dtos;
using System.Reflection;
using TccApp.Domain.Dtos;
using TccApp.Domain.Interfaces;
using TccApp.Enums;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamDto), "veiculoDto")]

    public partial class VeiculoViewModel : BaseViewModel
    {
        public VeiculoDto ParamDto
        {
            set
            {
                SetModel(value);
                OnPropertyChanged();
            }
        }

        //Controles de tela
        private VeiculoModel Model { get; set; }

        [ObservableProperty]
        bool isNewItem;

        [ObservableProperty]
        bool enabledDelete;
        //

        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        DateTime dataInclusao;

        [ObservableProperty]
        string placa;

        [ObservableProperty]
        string marcaModelo;

        [ObservableProperty]
        string anoModelo;

        [ObservableProperty]
        string valorFipe;

        [ObservableProperty]
        StatusCadastroType statusCadastro;

        [ObservableProperty]
        OrigemCadastroType origemCadastro;
        
        [ObservableProperty]
        OrigemCadastroType statusRegistro;


        [RelayCommand]
        public async Task ImagemVeiculo()
        {
            await AddImagemAsync();
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await GoToBackAsync();
        }

        [RelayCommand]
        public async Task Save()
        {
            await SaveAsync();
        }

        [RelayCommand]
        public async Task Delete()
        {
            await DeleteAsync();
        }

        private VeiculoDto veiculoDto;

        private readonly IVeiculoService service;

        public VeiculoViewModel(IVeiculoService service) : base()
        {
            Title = "Veículo";

            this.service = service;
        }

        protected virtual void SetModel(VeiculoDto dto)
        {
            veiculoDto = dto;

            if (dto.VeiculoId.ToString() == string.Empty || dto.VeiculoId == Guid.Empty)
            {
                CreateNewModel();
            }
            else
            {
                EditModel(dto.VeiculoId);
            }
            SetViewFromModel();
        }

        private void CreateNewModel()
        {
            IsNewItem = true;
            Model = new VeiculoModel();
            Model.StatusCadastro = StatusCadastroType.PreCadastro;
            Model.OrigemCadastro = OrigemCadastroType.App;
            Model.StatusRegistro = StatusRegistroType.Pendente;

        }

        private void EditModel(Guid id)
        {
            EnabledDelete = true;

            Model = service.Get(id);
        }

        private void SetViewFromModel()
        {
            Id = Model.Id;
            DataInclusao = Model.DataInclusao;
            Placa = Model.Placa;
            MarcaModelo = Model.MarcaModelo;
            AnoModelo = Model.AnoModelo;
            ValorFipe = Model.ValorFipe;
        }

        private void SetModelFromView()
        {
            Model.Id = Id;
            Model.DataInclusao = DataInclusao;
            Model.Placa = Placa;
            Model.MarcaModelo = MarcaModelo;
            Model.AnoModelo = AnoModelo;
            Model.ValorFipe = ValorFipe;
        }

        protected async Task SaveAsync()
        {
            //if (string.IsNullOrEmpty(ImagemUrl))
            //{
            //    return;
            //}

            var model = new VeiculoModel();
            model.AssociadoId = veiculoDto.AssociadoId;
            model.Placa = placa;
            model.MarcaModelo = marcaModelo;
            model.AnoModelo = AnoModelo;
            model.ValorFipe = ValorFipe;
            model.DataInclusao = new DateTime();
            model.StatusCadastro = StatusCadastroType.PreCadastro;
            model.OrigemCadastro = OrigemCadastroType.App;
            model.StatusRegistro = StatusRegistroType.Pendente;

            service.Create(model);

            await Shell.Current.DisplayAlert("Info", "Veiculo adicionado!", "Ok");

            await GoToBackAsync();
        }

        private async Task DeleteAsync()
        {
            bool result = await Shell.Current.DisplayAlert("Atenção", "Confirmar a exclusão do item?", "Confirmar", "Cancelar");

            if (!result)
                return;

            DeleteData();

            //await ShowMessage(_service.Sucess, "Registro excluído.");

            await Shell.Current.GoToAsync($"..?deletado={Model.Id}");
        }

        protected virtual void DeleteData()
        {
            service.Delete(Model);
        }

        private async Task GoToBackAsync()
        {
            //await Shell.Current.GoToAsync($"..?id={veiculoDto.AssociadoId}");
            await Shell.Current.GoToAsync($"{nameof(VeiculoIndexPage)}?id={veiculoDto.AssociadoId}");
        }

        private async Task AddImagemAsync()
        {
            SetModelFromView();
            SaveData();

            try
            {
                //await Shell.Current.GoToAsync($"{nameof(VistoriaImagemIndexPage)}?id={Model.Id}");
                await Shell.Current.GoToAsync($"{nameof(VistoriaImagemIndexPage)}", true, new Dictionary<string, object>
                {
                    ["veiculoDto"] = veiculoDto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected virtual void SaveData()
        {
            if (IsNewItem)
            {
                service.Create(Model);
            }
            else
            {
                service.Update(Model);
            }
        }
    }
}

using TccApp.Data;
using TccApp.Domain.Models;

namespace TccApp.ViewModels
{
    public abstract partial class BaseItemViewModel<TModel> : BaseViewModel where TModel : BaseModel, new()
    {
        public string ParamId
        {
            get => Model?.Id.ToString();
            set
            {
                SetModel(value);
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        bool isNewItem;

        [ObservableProperty]
        bool enabledDelete;

        //protected bool ActiveDelete;

        protected TModel Model { get; set; }

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

        protected IRepository<TModel> Repo;

        public BaseItemViewModel(IRepository<TModel> repo)
        {
            Repo = repo;
            IsNewItem = false;
            EnabledDelete = false;
            //ActiveDelete = false;
        }
        
        
        protected abstract void SetViewFromModel();
        protected abstract void SetModelFromView();

        protected virtual void SetModel(string id)
        {
            if (id == string.Empty)
            {
                IsNewItem = true;
                Model = new TModel();
            }
            else
            {
                EnabledDelete = true;

                Model = Repo.Get(Guid.Parse(id));
            }
            SetViewFromModel();
        }

        protected virtual async Task GoToBackAsync()
        {
            await Shell.Current.GoToAsync($"..?goback_detail={string.Empty}");
            //await Shell.Current.GoToAsync("..");
        }

        protected virtual async Task<bool> ValidateModel()
        {
            return true;
        }

        protected virtual async Task SaveAsync()
        {
            SetModelFromView();

            if (!await ValidateModel())
                return;

            if (isNewItem)
            {
                Repo.Create(Model);
            }
            else
            {
                Repo.Update(Model);
            }

            await ShowMessage(Repo.Sucess, Repo.StatusMessage);

            await Shell.Current.GoToAsync($"..?salvo={Model.Id}");
        }

        protected async Task DeleteAsync()
        {
            bool result = await Shell.Current.DisplayAlert("Atenção", "Confirmar a exclusão do item?", "Yes", "No");

            if (!result) 
                return;

            Repo.Delete(Model);

            await ShowMessage(Repo.Sucess, Repo.StatusMessage);

            await Shell.Current.GoToAsync($"..?deletado={Model.Id}");
        }

        protected virtual async Task ShowMessage(bool sucesso, string mensagem) 
        {
            var titulo = sucesso ? "Info" : "Erro";

            await Shell.Current.DisplayAlert(titulo, mensagem, "Ok");
        }
    }
}
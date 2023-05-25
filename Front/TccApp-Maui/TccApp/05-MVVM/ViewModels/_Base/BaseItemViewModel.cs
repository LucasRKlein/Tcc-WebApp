using TccApp.Domain.Interfaces;
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
        
        protected List<string> ValidateErrors;

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
            ValidateErrors = new List<string>();
        }

        protected abstract void SetViewFromModel();
        protected abstract void SetModelFromView();

        protected virtual void SetModel(string id)
        {
            if (id == string.Empty)
            {
                CreateNewModel();
            }
            else
            {
                EditModel(id);
            }
            SetViewFromModel();
        }

        protected virtual void CreateNewModel()
        {
            IsNewItem = true;
            Model = new TModel();
        }

        protected virtual void EditModel(string id)
        {
            EnabledDelete = true;

            Model = Repo.Get(Guid.Parse(id));
        }

        protected virtual async Task GoToBackAsync()
        {
            await Shell.Current.GoToAsync($"..?goback_detail={string.Empty}");
            //await Shell.Current.GoToAsync("..");
        }

        protected virtual bool ValidateToSave()
        {
            ValidateErrors.Clear();
            return true;
        }

        protected virtual void SaveData()
        {
            if (isNewItem)
            {
                Repo.Create(Model);
            }
            else
            {
                Repo.Update(Model);
            }
        }

        protected virtual async Task SaveAsync()
        {
            SetModelFromView();

            if (!ValidateToSave())
            {
                ShowMessageErrors();

                return;
            }

            SaveData();
            await ShowMessage(Repo.Sucess, Repo.StatusMessage);

            await Shell.Current.GoToAsync($"..?salvo={Model.Id}");
        }

        protected virtual async void ShowMessageErrors()
        {
            var str = "";
            for (int i = 0; i < ValidateErrors.Count; i++)
            {
                str += (i == 0) ? ValidateErrors[i] : $"\\n{ValidateErrors[i]}";
            }

            await ShowMessage(false, str);
        }

        protected virtual void DeleteData()
        {
            Repo.Delete(Model);
        }

        protected async Task DeleteAsync()
        {
            bool result = await Shell.Current.DisplayAlert("Atenção", "Confirmar a exclusão do item?", "Confirmar", "Cancelar");

            if (!result)
                return;

            DeleteData();

            //await ShowMessage(Repo.Sucess, "Registro excluído.");

            await Shell.Current.GoToAsync($"..?deletado={Model.Id}");
        }

        protected virtual async Task ShowMessage(bool sucesso, string mensagem)
        {
            var titulo = sucesso ? "Info" : "Erro";

            await Shell.Current.DisplayAlert(titulo, mensagem, "Ok");
        }
    }
}
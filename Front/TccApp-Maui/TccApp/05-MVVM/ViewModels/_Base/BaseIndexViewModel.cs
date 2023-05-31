using TccApp.Domain.Interfaces;
using TccApp.Domain.Models;

namespace TccApp.ViewModels
{
    public abstract partial class BaseIndexViewModel<TModel> : BaseViewModel, IQueryAttributable
        where TModel : BaseModel
    {
        public ObservableCollection<TModel> Lista { get; set; }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task GoToItem(TModel model)
        {
            await GoToPageItemAsync(model);
        }

        [RelayCommand]
        public async Task CreateNewItem()
        {
            await CreateNewItemAsync();
        }

        [RelayCommand]
        public async Task GetListaRefresh()
        {
            await GetListaAsync();
        }

        private readonly IRepository<TModel> _service;

        public BaseIndexViewModel(IRepository<TModel> service)
        {
            _service = service;

            GetListaAsync();
        }

        protected virtual async Task GoToPageItemAsync(TModel model)
        {
            if (model == null)
                return;

            var strPage = GetPageItemName();

            if (string.IsNullOrEmpty(strPage))
            {
                await Shell.Current.DisplayAlert("Info", "Não há tela de detalhamento para este item.", "OK");
                return;
            }


            await Shell.Current.GoToAsync($"{strPage}?id={model.Id}");

            //await Shell.Current.GoToAsync(GetPageItemName(), true, new Dictionary<string, object>
            //{
            //    {"Model", model}
            //});
        }

        protected virtual async Task CreateNewItemAsync()
        {
            var strPage = GetPageItemName();

            if (string.IsNullOrEmpty(strPage))
            {
                await Shell.Current.DisplayAlert("Info", "Não há opção de criação para este item.", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"{strPage}?id={string.Empty}");
        }

        protected abstract string GetPageItemName();

        protected virtual async Task GetListaAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var listaRepo = _service.GetAll();

                if (Lista == null)
                {
                    Lista = new ObservableCollection<TModel>();
                }
                else
                {
                    Lista.Clear();
                }

                foreach (var item in listaRepo)
                {
                    Lista.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Não foi possível retornar lista de dados: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count() == 0)
            {
                //Refresh da lista
                await GetListaAsync();
            }
            else
            {
                if (query.ContainsKey("deletado"))
                {
                    var id = Guid.Parse(query["deletado"].ToString());

                    var model = Lista.Where(x => x.Id == id).FirstOrDefault();

                    if (model != null)
                        Lista.Remove(model);
                }
                else if (query.ContainsKey("salvo"))
                {
                    var id = Guid.Parse(query["salvo"].ToString());

                    var model = Lista.Where(x => x.Id == id).FirstOrDefault();

                    if (model != null)
                    {
                        //remover model desatualizado
                        Lista.Remove(model);
                    }

                    model = _service.Get(id);
                    Lista.Insert(0, model);
                }

                query.Clear();
            }
        }
    }
}
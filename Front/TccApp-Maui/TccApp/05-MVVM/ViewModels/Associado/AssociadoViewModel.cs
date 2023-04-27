using TccApp.Data;
using TccApp.Models;  

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamId), "id")]
    public partial class AssociadoViewModel : BaseItemViewModel<AssociadoModel>
    {
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        DateTime dataInclusao;

        [ObservableProperty]
        int codigo;

        [ObservableProperty]
        string nome;

        public AssociadoViewModel(IRepository<AssociadoModel> repo) : base(repo)
        {
            Title = "Associado";
            //ActiveDelete = true;
        }

        protected override void SetModelFromView()
        {
            Model.Id = Id;
            Model.DataInclusao = dataInclusao;

            Model.Nome = nome;
            Model.Codigo = codigo;
        }

        protected override void SetViewFromModel()
        {
            Id = Model.Id;
            DataInclusao = Model.DataInclusao;
            Codigo = Model.Codigo;
            Nome = Model.Nome;
        }

        protected override async Task<bool> ValidateModel()
        {
            if (Model.Nome == string.Empty)
            {
                await Shell.Current.DisplayAlert("Erro!", "Campo Nome não pode ser nulo.", "OK");
                return false;
            }

            return true;
        }
    }
}

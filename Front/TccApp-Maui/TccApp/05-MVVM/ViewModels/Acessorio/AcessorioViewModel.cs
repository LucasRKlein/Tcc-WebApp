using TccApp.Domain.Interfaces;
using TccApp.Models;  

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamId), "id")]
    public partial class AcessorioViewModel : BaseItemViewModel<AcessorioModel>
    {
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        DateTime dataInclusao;

        [ObservableProperty]
        int codigo;

        [ObservableProperty]
        string nome;

        public AcessorioViewModel(IRepository<AcessorioModel> repo) : base(repo)
        {
            Title = "Acessorio";
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

        protected override bool ValidateToSave()
        {
            if (string.IsNullOrEmpty(Model.Nome))
            {
                ValidateErrors.Add("Campo Nome não pode ser nulo.");
                return false;
            }

            return true;
        }
    }
}

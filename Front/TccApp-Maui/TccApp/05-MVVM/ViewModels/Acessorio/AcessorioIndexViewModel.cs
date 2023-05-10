using TccApp.Data;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    public partial class AcessorioIndexViewModel : BaseIndexViewModel<AcessorioModel>
    {

        public AcessorioIndexViewModel(IRepository<AcessorioModel> repo) : base(repo)
        {
            Title = "Acessorios";
        }

        protected override string GetPageItemName()
        {
            return nameof(AcessorioPage);
        }
    }
}

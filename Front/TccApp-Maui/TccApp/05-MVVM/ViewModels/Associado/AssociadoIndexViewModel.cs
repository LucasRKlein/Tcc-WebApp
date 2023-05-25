using TccApp.Domain.Interfaces;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    public partial class AssociadoIndexViewModel : BaseIndexViewModel<AssociadoModel>
    {

        public AssociadoIndexViewModel(IRepository<AssociadoModel> repo) : base(repo)
        {
            Title = "Associados";
        }

        protected override string GetPageItemName()
        {
            return nameof(AssociadoPage);
        }
    }
}

using TccApp.Domain.Interfaces;
using TccApp.Models;
using TccApp.Views;

namespace TccApp.ViewModels
{
    public partial class AssociadoIndexViewModel : BaseIndexViewModel<AssociadoModel>
    {
        public AssociadoIndexViewModel(IAssociadoService service) : base(service)
        {
            Title = "Associados";
        }

        protected override string GetPageItemName()
        {
            return nameof(AssociadoPage);
        }
    }
}

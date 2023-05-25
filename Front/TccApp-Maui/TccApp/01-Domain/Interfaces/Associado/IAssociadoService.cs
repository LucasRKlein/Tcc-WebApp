using TccApp.Models;

namespace TccApp.Domain.Interfaces
{
    public interface IAssociadoService : IRepository<AssociadoModel>
    {
        bool DeleteAssociado(AssociadoModel model);
        List<string> ValidateToSave(AssociadoModel model);
        List<string> ValidateToFinalize(AssociadoModel model);
    }
}

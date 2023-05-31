using TccApp.Models;

namespace TccApp.Domain.Interfaces
{
    public interface IVistoriaImagemService : IRepository<VistoriaImagemModel>
    {
        List<VistoriaImagemModel> GetByVeiculoId(Guid veiculoId);
    }
}

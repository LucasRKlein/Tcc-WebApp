using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Domain.Interfaces
{
    public interface IVeiculoService : IRepository<VeiculoModel>
    {
        List<VeiculoModel> GetByAssociadoId(Guid associadoId);
    }
}
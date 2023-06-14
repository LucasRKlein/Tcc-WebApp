using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Services
{
    public class VeiculoService : Repository<VeiculoModel>, IVeiculoService
    {
        public VeiculoService()
        {
        }

        public List<VeiculoModel> GetByAssociadoId(Guid associadoId)
        {
            return GetAll(x => x.AssociadoId == associadoId);
        }
    }
}

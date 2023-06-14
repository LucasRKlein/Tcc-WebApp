using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Services
{
    public class VistoriaImagemService : Repository<VistoriaImagemModel>, IVistoriaImagemService
    {
        public VistoriaImagemService()
        {
        }

        public List<VistoriaImagemModel> GetByVeiculoId(Guid veiculoId)
        {
            return GetAll(x => x.VeiculoId == veiculoId);
        }
    }
}

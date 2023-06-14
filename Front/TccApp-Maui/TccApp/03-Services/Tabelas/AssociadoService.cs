using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Services
{
    public class AssociadoService : Repository<AssociadoModel>, IAssociadoService
    {
        public AssociadoService()
        {
        }

    }
}

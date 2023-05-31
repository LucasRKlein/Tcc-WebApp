using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Services
{
    public class AcessorioService : Repository<AcessorioModel>, IAcessorioService
    {
        public AcessorioService()
        {
        }

    }
}

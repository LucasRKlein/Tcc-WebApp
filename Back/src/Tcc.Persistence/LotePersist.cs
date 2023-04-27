using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tcc.Domain;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly TccContext _context;
        public LotePersist(TccContext context)
        {
            _context = context;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId
                                     && lote.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}
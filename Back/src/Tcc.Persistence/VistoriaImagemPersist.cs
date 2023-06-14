using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class VistoriaImagemPersist : IVistoriaImagemPersist
    {
        private readonly TccContext _context;
        public VistoriaImagemPersist(TccContext context)
        {
            _context = context;
        }

        public async Task<VistoriaImagem> GetByIdAsync(Guid imagemId)
        {
            IQueryable<VistoriaImagem> query = _context.VistoriaImagem;
            query = query.AsNoTracking().Where(e => e.Id == imagemId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VistoriaImagem[]> GetVistoriaImagensByVeiculoIdAsync(Guid veiculoId)
        {
            IQueryable<VistoriaImagem> query = _context.VistoriaImagem;

            query = query.AsNoTracking().Where(vistoriaImagem => vistoriaImagem.VeiculoId == veiculoId);

            return await query.ToArrayAsync();
        }
    }
}

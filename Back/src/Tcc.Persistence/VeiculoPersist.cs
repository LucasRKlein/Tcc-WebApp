using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class VeiculoPersist : IVeiculoPersist
    {
        private readonly TccContext _context;
        public VeiculoPersist(TccContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> GetVeiculoByIdsAsync(Guid associadoId, Guid id)
        {
            IQueryable<Veiculo> query = _context.Veiculos;

            query = query.AsNoTracking()
                         .Where(veiculo => veiculo.AssociadoId == associadoId && veiculo.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Veiculo[]> GetVeiculosByAssociadoIdAsync(Guid associadoId)
        {
            IQueryable<Veiculo> query = _context.Veiculos;

            query = query.AsNoTracking()
                         .Where(veiculo => veiculo.AssociadoId == associadoId);

            return await query.ToArrayAsync();
        }
    }
}

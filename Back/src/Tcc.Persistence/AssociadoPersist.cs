using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Domain;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;
using Tcc.Persistence.Models;

namespace Tcc.Persistence
{
    public class AssociadoPersist : GeralPersist, IAssociadoPersist
    {
        private readonly TccContext _context;
        public AssociadoPersist(TccContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<Associado>> GetAllAssociadosAsync(PageParams pageParams)
        {
            IQueryable<Associado> query = _context.Associados
                .Include(x => x.Veiculos);

            query = query.AsNoTracking().OrderBy(p => p.Nome);

            return await PageList<Associado>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Associado> GetAssociadoByIdAsync(Guid associadoId)
        {
            IQueryable<Associado> query = _context.Associados
                .Include(x => x.Veiculos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == associadoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}

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
                .Include(p => p.User);            

            query = query.AsNoTracking()
                         .Where(p => (p.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
                                      p.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())) &&
                                      p.User.Funcao == Domain.Enum.FuncaoType.Associado)
                         .OrderBy(p => p.Id);

            return await PageList<Associado>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Associado> GetAssociadoByUserIdAsync(int userId)
        {
            IQueryable<Associado> query = _context.Associados
                .Include(p => p.User);

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}

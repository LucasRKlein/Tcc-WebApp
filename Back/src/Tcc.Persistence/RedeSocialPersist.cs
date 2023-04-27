using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tcc.Domain;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class RedeSocialPersist : GeralPersist, IRedeSocialPersist
    {
        private readonly TccContext _context;

        public RedeSocialPersist(TccContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id) 
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.EventoId == eventoId &&
                                      rs.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id) 
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.PalestranteId == palestranteId &&
                                      rs.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId) 
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
        public async Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId) 
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.PalestranteId == palestranteId);

            return await query.ToArrayAsync();
        }
    }
}
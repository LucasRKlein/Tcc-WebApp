using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Domain;

namespace Tcc.Persistence.Interface
{
    public interface IRedeSocialPersist : IGeralPersist
    {
        Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id);
        Task<RedeSocial> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId);
        Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId);
    }
}
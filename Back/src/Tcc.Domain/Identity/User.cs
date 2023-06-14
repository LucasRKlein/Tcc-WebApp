using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tcc.Domain.Enum;

namespace Tcc.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public TituloType Titulo { get; set; }
        public string Descricao { get; set; }
        public FuncaoType Funcao { get; set; }
        public string ImagemURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Domain.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            RegistroAtivo = true;
            DataInclusao = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid? UsuarioIdInclusao { get; set; }
        public Guid? UsuarioIdAlteracao { get; set; }
        public bool RegistroAtivo { get; set; }
    }
}

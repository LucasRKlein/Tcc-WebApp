using System;

namespace Tcc.Application.Dtos.Base
{
    public class BaseDto
    {
        public BaseDto()
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

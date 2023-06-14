using System;
using Tcc.Application.Dtos;
using Tcc.Application.Dtos.Base;
using Tcc.Domain.Base;
using Tcc.Domain.Enum;

namespace Tcc.Application
{
    public class VeiculoDto : BaseDto
    {
        public string Placa { get; set; }
        public string MarcaModelo { get; set; }
        public string AnoModelo { get; set; }
        public string ValorFipe { get; set; }

        public Guid AssociadoId { get; set; }
        public AssociadoDto AssociadoDto { get; set; }
        
        /// <summary>
        /// pre cadastro, aprovado
        /// </summary>
        public StatusCadastroType StatusCadastro { get; set; }

        /// <summary>
        /// App, Sistema
        /// </summary>
        public OrigemCadastroType OrigemCadastro { get; set; }
    }
}

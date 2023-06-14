using System;
using System.Collections.Generic;
using Tcc.Domain.Base;
using Tcc.Domain.Enum;

namespace Tcc.Domain
{
    public class Veiculo : BaseModel
    {
        public string Placa { get; set; }
        public string MarcaModelo { get; set; }
        public string AnoModelo { get; set; }
        public string ValorFipe { get; set; }

        public Guid AssociadoId { get; set; }
        public Associado Associado { get; set; }

        /// <summary>
        /// pre cadastro, aprovado
        /// </summary>
        public StatusCadastroType StatusCadastro { get; set; }

        /// <summary>
        /// App, Sistema
        /// </summary>
        public OrigemCadastroType OrigemCadastro { get; set; }

        public IEnumerable<VistoriaImagem> ListaImagens { get; set; }
    }
}

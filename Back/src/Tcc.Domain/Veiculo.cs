using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Domain.Enum;

namespace Tcc.Domain
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string MarcaModelo { get; set; }
        public string AnoModelo { get; set; }
        public string ValorFipe { get; set; }

        public int AssociadoId { get; set; }
        public Associado Associado { get; set; }

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

using SQLite;
using TccApp.Domain.Models;
using TccApp.Enums;

namespace TccApp.Models
{
    public class VeiculoModel : BaseModel
    {

        [Indexed, NotNull]
        public Guid AssociadoId { get; set; }
        
        [MaxLength(8)]
        public string Placa { get; set; }

        [MaxLength(50)]
        public string MarcaModelo { get; set; }
        
        [MaxLength(4)]
        public string AnoModelo { get; set; }

        [MaxLength(9)]
        public string ValorFipe { get; set; }

        /// <summary>
        /// pre cadastro, aprovado
        /// </summary>
        public StatusCadastroType StatusCadastro { get; set; }

        /// <summary>
        /// App, Sistema
        /// </summary>
        public OrigemCadastroType OrigemCadastro { get; set; }

        // para controle no APP
        public StatusRegistroType StatusRegistro { get; set; }
    }
}

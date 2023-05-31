using SQLite;
using TccApp.Domain.Models;
using TccApp.Enums;

namespace TccApp.Models
{
    [Table("Associado")]
    public class AssociadoModel : BaseModel
    {   
        //public int Id { get; set; }

        [MaxLength(60), Indexed, NotNull]
        public string Nome { get; set; }

        #region Dados de pessoa
        public string ImagemURL { get; set; }
        public string Cpf { get; set; }
        public SexoType Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }

        [MaxLength(15)]
        public string Celular { get; set; }
        
        [MaxLength(60)]
        public string Email { get; set; }
        #endregion

        #region Dados de endereço
        public string RuaAvenida { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string EstadoNome { get; set; }
        public string CidadeNome { get; set; }
        #endregion
        
        //[Ignore]
        //public List<Veiculo> Veiculos { get; set; }

        public StatusCadastroType StatusCadastro { get; set; }
        public OrigemCadastroType OrigemCadastro { get; set; }


        // para controle no APP
        public StatusRegistroType StatusRegistro { get; set; }

        [Ignore]
        public List<VeiculoModel> Avarias { get; set; }
    }
}

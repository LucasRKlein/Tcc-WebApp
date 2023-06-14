using SQLite;
using TccApp.Domain.Models;
using TccApp.Enums;

namespace TccApp.Models
{
    public class VistoriaImagemModel : BaseModel
    {
        [Indexed, NotNull]
        public Guid VeiculoId { get; set; }        

        [MaxLength(300)]
        public string ImagemUrl { get; set; }

        [Ignore]
        public string ImagemBase64 { get; set; }

        /// <summary>
        /// Define que a imagem foi excluída ou trocada por outra.
        /// Este controle serve para resolver o bug com exclusão ou trocas de imagens
        /// do componente "Image" do Maui na versão dotNet 7.
        /// </summary>
        [DefaultValue(false)]
        public bool Excluida { get; set; }


        ///// <summary>
        ///// pre cadastro, aprovado
        ///// </summary>
        //public StatusCadastroType StatusCadastro { get; set; }

        ///// <summary>
        ///// App, Sistema
        ///// </summary>
        //public OrigemCadastroType OrigemCadastro { get; set; }

        // para controle no APP
        public StatusRegistroType StatusRegistro { get; set; }
    }
}

using SQLite;
using TccApp.Domain.Models;

namespace TccApp.Models
{
    [Table("Acessorio")]
    public class AcessorioModel : BaseModel
    {
        public int Codigo { get; set; }

        [MaxLength(60), Indexed, NotNull]
        public string Nome { get; set; }
    }
}

using SQLite;
using TccApp.Domain.Models;

namespace TccApp.Models
{
    [Table("Associado")]
    public class AssociadoModel : BaseModel
    {
        public int Codigo { get; set; }

        [MaxLength(60), Indexed, NotNull]
        public string Nome { get; set; }
    }
}

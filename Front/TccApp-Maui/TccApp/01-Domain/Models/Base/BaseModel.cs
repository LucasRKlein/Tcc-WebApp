using SQLite;

namespace TccApp.Domain.Models
{
    /// <summary>
    ///  Classe base para todos as classes de entidades persistentes.
    /// </summary>
    public class BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid();
            DataInclusao = DateTime.Now;
        }
    }
}

using SQLite;

namespace TccApp.Domain.Models
{
    /// <summary>
    /// Usuário do App.
    /// Serve para montar perfil e automatizar operações de 
    /// sincronização com API
    /// </summary>

    [Table("UsuarioApp")]
    public class UsuarioAppModel: BaseModel 
    {
        [MaxLength(50)]
        public string Nome { get; set; }

        /// <summary>
        /// Equivalente ao UserNameTenant do usuário backend
        /// </summary>
        [MaxLength(256)]
        public string UserName { get; set; }

        [MaxLength(256)]
        public string Password { get; set; }
    }
}

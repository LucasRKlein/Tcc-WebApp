using TccApp.Domain.Consts;
using SQLite;

namespace TccApp.Services
{
    /// <summary>
    /// Conexão com banco.
    /// Desta forma podemos ter conexão singleton com o banco.
    /// </summary>
    public class DatabaseConnection: IDisposable
    {
        public readonly SQLiteConnection Database;
        //private readonly SQLiteAsyncConnection Database;

        public DatabaseConnection()
        {
            Database = new SQLiteConnection(ConfigApp.DatabasePath, ConfigApp.Flags);
            //Database = new SQLiteAsyncConnection(ConfigApp.DatabasePath, ConfigApp.Flags);
        }

        public void Dispose()
        {
            Database.Close();
        }
    }
}

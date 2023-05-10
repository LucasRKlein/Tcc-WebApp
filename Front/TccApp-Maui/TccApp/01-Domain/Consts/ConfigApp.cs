using SQLite;
using System.Globalization;

namespace TccApp.Domain.Consts
{
    public static class ConfigApp
    {
        public const string ProjectName = "TccApp";
        public static readonly CultureInfo CurrentCultureInfo = new CultureInfo("pt-BR");

#if DEBUG
        // URL of REST service (Android não usa localhost)
        public static string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost";
        public static string Scheme = "https"; // or http
        public static string Port = "5001";
        public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}";
#else
        public static string RestUrl = "https://admin.autopatio.com.br";
#endif

        public static string ApiUrl = $"{RestUrl}/api/v1";

        public static string ApiUrlTabelas = $"{ApiUrl}/TabelasApp";
        public static string ApiUrlAccount = $"{ApiUrl}/AccountApp";
        public static string ApiUrlVistoria = $"{ApiUrl}/VistoriaApp";

        public const string CdnUrl = "https://kleincode.blob.core.windows.net/cdn";

        public const string DBFileName = "sqlite.db";

        public const SQLiteOpenFlags Flags =
             // open the database in read/write mode
             SQLiteOpenFlags.ReadWrite |
             // create the database if it doesn't exist
             SQLiteOpenFlags.Create |
             // enable multi-threaded database access
             SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                //Para produção ou teste com emulador
                //return Path.Combine(FileSystem.AppDataDirectory, DBFileName);

                //Para teste windows (poder ter acesso ao DB por fora para validação)
                return Path.Combine(@"C:\source\Estudos\TCC\Tcc-WebApp\Front\TccApp-Maui", DBFileName);
            }
        }

    }
}

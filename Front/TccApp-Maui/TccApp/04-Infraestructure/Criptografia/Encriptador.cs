using System.Security.Cryptography;
using System.Text;

namespace TccApp.Infraestructure.Criptografia
{
    /// <summary>
    /// Classe para encriptação e decriptação de texto.
    /// Utiliza chave privada e modelo de algorítimo simétrico "Aes".
    /// Atenção: utilizar a mesma chave e vetor de inicialização na ecriptação e decriptação.
    /// </summary>
    public class Encriptador
    {
        /// <summary>
        /// Chave string
        /// </summary>
        protected string Key { get; set; }

        /// <summary>
        /// Vetor inicial necessário para o procedimento.
        /// Se não passado no construtor então gerará um automático.
        /// Atenção: Dever ser com tamanho múltiplo de 8 posições.
        /// </summary>
        protected byte[] IniVetor { get; set; }
        
        /// <summary>
        /// Modelo de algorítimo utilizado.
        /// </summary>
        protected Aes Algoritimo { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="key">Chave privada para encriptar e decriptar</param>
        public Encriptador(string key)
        {
            Key = key;
            
            Algoritimo = Aes.Create();
            
            IniVetor = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            //Gerar um vetor
            //IniVetor = Algoritimo.IV;
        }

        /// <summary>
        /// </summary>
        /// <param name="key">Chave privada para encriptar e decriptar</param>
        /// <param name="iniVetor">Vetor inicial usado no processo.</param>
        public Encriptador(string key, byte[] iniVetor)
        {
            Key = key;
            IniVetor = iniVetor;
            Algoritimo = Aes.Create();
        }

        /// <summary>
        /// Encripta o texto informado.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Texto encriptado.</returns>
        public string Encriptar(string texto)
        {
            byte[] resultadoArr;

            var encriptarArr = Encoding.UTF8.GetBytes(texto);

            var encriptador = Algoritimo.CreateEncryptor(GetKey(), IniVetor);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write);

            cryptoStream.Write(encriptarArr, 0, encriptarArr.Length);
            cryptoStream.FlushFinalBlock();
            resultadoArr = memoryStream.ToArray();

            Algoritimo.Dispose();

            return Convert.ToBase64String(resultadoArr);
        }

        /// <summary>
        /// Decripta o texto informado.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Texto decriptado.</returns>
        public string Decriptar(string texto)
        {
            byte[] resultadoArr;

            var encriptadoArr = Convert.FromBase64String(texto);

            var decriptador = Algoritimo.CreateDecryptor(GetKey(), IniVetor);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, decriptador, CryptoStreamMode.Write);
            
            cryptoStream.Write(encriptadoArr, 0, encriptadoArr.Length);
            cryptoStream.FlushFinalBlock();
            resultadoArr = memoryStream.ToArray();

            Algoritimo.Dispose();
            return Encoding.Default.GetString(resultadoArr);
        }

        /// <summary>
        /// Gera a chave de criptografia válida dentro do array.
        /// E ajusta a mesma caso esteja fora dos limites do algorítimo.
        /// </summary>
        /// <returns>Chave com array de bytes.</returns>
        public virtual byte[] GetKey()
        {
            string salt = string.Empty;

            // Ajusta o tamanho da chave se necessário e retorna uma chave válida
            if (Algoritimo.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = Key.Length * 8;
                int minSize = Algoritimo.LegalKeySizes[0].MinSize;
                int maxSize = Algoritimo.LegalKeySizes[0].MaxSize;
                int skipSize = Algoritimo.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    Key = Key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        Key = Key.PadRight(validSize / 8, '*');
                    }
                }
            }

            PasswordDeriveBytes keyPassw = new PasswordDeriveBytes(Key, ASCIIEncoding.UTF8.GetBytes(salt));

            return keyPassw.GetBytes(Key.Length);
        }
    }
}

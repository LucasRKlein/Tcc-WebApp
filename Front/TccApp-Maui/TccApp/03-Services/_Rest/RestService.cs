using System.Net.Http.Headers;
using System.Net.Http.Json;
using TccApp.Domain.Consts;
using TccApp.Domain.Dtos;
using TccApp.Domain.Models;
using TccApp.Models;

namespace TccApp.Services
{
    public class RestService : IRestService
    {
        public static string ApiUrlTabelas = $"{ConfigApp.ApiUrl}/TabelasApp";
        public static string ApiUrlAccount = $"{ConfigApp.ApiUrl}/Account";

        private HttpClient httpClient;
        private JsonSerializerOptions serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;

        private string _tenant;
        private string _token;

        public RestService(IHttpsClientHandlerService service)
        {
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                httpClient = new HttpClient(handler);
            else
                httpClient = new HttpClient();
#else
            httpClient = new HttpClient();
# endif

            serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<BackResponseDto<TokenResponse>> LoginAsync(LoginDto login)
        {
            var url = $"{ApiUrlAccount}/Login";

            httpClient.DefaultRequestHeaders.Clear();

            BackResponseDto<TokenResponse> dataResponse;
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, login);

                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        dataResponse = await JsonSerializer.DeserializeAsync<BackResponseDto<TokenResponse>>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Utilizado para setar as credenciais.
        /// Após recebê-las pelo login e antes de iniciar outras chamadas no servidor.
        /// Serve para evitar envio de parâmetros repetitos nas chamadas.
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="token"></param>
        public void SetCredentials(string tenant, string token)
        {
            _tenant = tenant;
            _token = token;
        }

        public async Task LogoutAsync()
        {
            var url = $"{ApiUrlAccount}/LogoutApp";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            try
            {
                var response = await httpClient.PostAsync(url, null);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
            }
        }

        public async Task<UsuarioAppModel> GetPerfilAppAsync()
        {
            var url = $"{ApiUrlAccount}/GetUserApp";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");


            UsuarioAppModel usuarioApp;
            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var dataResponse = await JsonSerializer
                                                .DeserializeAsync<BackResponseDto<UsuarioAppModel>>(responseStream, serializerOptions);
                        usuarioApp = dataResponse.Data;
                    }

                    return usuarioApp;
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        public async Task<List<AssociadoModel>> GetAssociadosAsync()
        {
            return await GetTabelaModelAsync<AssociadoModel>("GetAssociados");
        }

        //public async Task<BackResponseDto<AssociadoModel>> PostAssociadoAsync(AssociadoModel model)
        public async Task<AssociadoModel> PostAssociadoAsync(AssociadoModel model)
        {
            //return await PostCreateModelAsync("Associado", model);

            var url = $"{ConfigApp.ApiUrl}/Associado";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            //BackResponseDto<AssociadoModel> dataResponse;
            AssociadoModel dataResponse;
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        // dataResponse = await JsonSerializer.DeserializeAsync<BackResponseDto<AssociadoModel>>(responseStream, serializerOptions);
                        dataResponse = await JsonSerializer.DeserializeAsync<AssociadoModel>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }

                Console.WriteLine($"StatusCode: {response.StatusCode}, Mensagem : {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        public async Task<VeiculoModel> PostVeiculoAsync(VeiculoModel model)
        {
            //return await PostCreateModelAsync("Veiculo", model);

            var url = $"{ConfigApp.ApiUrl}/Veiculo";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            //BackResponseDto<VeiculoModel> dataResponse;
            VeiculoModel dataResponse;
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        //dataResponse = await JsonSerializer.DeserializeAsync<BackResponseDto<VeiculoModel>>(responseStream, serializerOptions);
                        dataResponse = await JsonSerializer.DeserializeAsync<VeiculoModel>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }

                Console.WriteLine($"StatusCode: {response.StatusCode}, Mensagem : {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        public async Task<VistoriaImagemModel> PostVistoriaImagemAsync(VistoriaImagemModel model)
        {
            //return await PostCreateModelAsync("VistoriaImagem", model);

            var url = $"{ConfigApp.ApiUrl}/VistoriaImagem";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            //BackResponseDto<VistoriaImagemModel> dataResponse;
            VistoriaImagemModel dataResponse;
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        dataResponse = await JsonSerializer.DeserializeAsync<VistoriaImagemModel>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }

                Console.WriteLine($"StatusCode: {response.StatusCode}, Mensagem : {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        public async Task<VistoriaImagemModel> UploadImagemByVistoriaImagemIdAsync(VistoriaImagemModel model)
        {
            var url = $"{ConfigApp.ApiUrl}/VistoriaImagem/upload-image/{model.Id}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            VistoriaImagemModel dataResponse;
            try
            {
                // Crie um objeto MultipartFormDataContent para enviar a imagem como parte da solicitação
                var content = new MultipartFormDataContent();

                //Pega a imagem local e transforma em array de bytes
                string localFilePath = Path.Combine(FileSystem.AppDataDirectory, "Imagens", model.ImagemUrl);
                FileStream stream = File.OpenRead(localFilePath);

                byte[] byteArr;

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byteArr = memoryStream.ToArray();
                }

                // Crie um ByteArrayContent para representar o arquivo/imagem
                var imagemContent = new ByteArrayContent(byteArr);
                imagemContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // Ou outro tipo de mídia correspondente ao formato da imagem

                // Adicione o conteúdo da imagem ao objeto MultipartFormDataContent
                content.Add(imagemContent, "arquivo", model.ImagemUrl+ ".jpg"); // Especifique o nome do arquivo que será usado no servidor

                // Faça a chamada HTTP POST para o endpoint "upload-image/{vistoriaImagemId}"
                var response = await httpClient.PostAsync(url, content);

                // Verifique a resposta do servidor e faça o tratamento necessário
                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        dataResponse = await JsonSerializer.DeserializeAsync<VistoriaImagemModel>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }
                else
                {
                }
                Console.WriteLine($"StatusCode: {response.StatusCode}, Mensagem : {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        public async Task<BackResponseDto<TModel>> PostCreateModelAsync<TModel>(string controller, TModel model)
        {
            var url = $"{ConfigApp.ApiUrl}/{controller}/Create";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            BackResponseDto<TModel> dataResponse;
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        dataResponse = await JsonSerializer.DeserializeAsync<BackResponseDto<TModel>>(responseStream, serializerOptions);
                    }

                    return dataResponse;
                }

                Console.WriteLine($"StatusCode: {response.StatusCode}, Mensagem : {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        private async Task<List<TModel>> GetTabelaModelAsync<TModel>(string endPointRest)
        {
            var response = await GetTabelaResponse(endPointRest);

            if (response.IsSuccessStatusCode)
            {
                return await GetTabelaDeserialized<TModel>(response);
            }

            return null;
        }

        private async Task<List<TModel>> GetTabelaDeserialized<TModel>(HttpResponseMessage response)
        {
            List<TModel> lista;

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var dataResponse = await JsonSerializer
                                        .DeserializeAsync<BackResponseListDto<TModel>>(responseStream, serializerOptions);
                lista = dataResponse.Data;
            }

            return lista;
        }

        private async Task<HttpResponseMessage> GetTabelaResponse(string endPoint)
        {
            var url = $"{ApiUrlTabelas}/{endPoint}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            //var builder = new UriBuilder(url);
            //builder.Query = "tenant=teste";
            //url = builder.ToString();

            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync(url);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode}, Mensagem:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Utilizado para setar as credenciais.
        /// Após recebê-las pelo login e antes de iniciar outras chamadas no servidor.
        /// Serve para evitar envio de parâmetros repetitos nas chamadas.
        /// </summary>
        /// <param name="token"></param>
        public void SetCredentials(string token)
        {
            _token = token;
        }
    }
}
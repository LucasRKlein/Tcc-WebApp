using System.Net.Http.Headers;
using TccApp.Domain.Interfaces;
using TccApp.Domain.Models;
using TccApp.Enums;
using TccApp.Services;
using TccApp.Views;

namespace TccApp.ViewModels
{
    public partial class SincronizarViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !IsBusy;

        [RelayCommand]
        public async Task Sincronizar()
        {
            await SincronizarAsync();
        }

        private UsuarioAppModel modelUsuarioApp;

        private readonly IRestService restService;
        private readonly IUsuarioService usuarioService;
        private readonly IAssociadoService associadoService;
        private readonly IVeiculoService veiculoService;
        private readonly IVistoriaImagemService vistoriaImagemService;

        private readonly IConnectivity connectivity;
        private List<string> listaErros;

        public SincronizarViewModel(IConnectivity connectivity,
            IRestService restService,
            IUsuarioService usuarioService,
            IAssociadoService associadoService,
            IVeiculoService veiculoService,
            IVistoriaImagemService vistoriaImagemService)
        {
            this.connectivity = connectivity;

            this.restService = restService;
            this.usuarioService = usuarioService;
            this.associadoService = associadoService;
            this.veiculoService = veiculoService;
            this.vistoriaImagemService = vistoriaImagemService;

            listaErros = new List<string>();
        }

        private async Task SincronizarAsync()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Sem Conexão!", "Favor verificar a internet e tentar novamente.", "OK");
                return;
            }

            IsBusy = true;
            listaErros.Clear();

            //await Task.Delay(10000);

            modelUsuarioApp = await GetUsuarioAppAsync();

            if (modelUsuarioApp == null)
            {
                IsBusy = false;
                return;
            }

            //Login
            var token = await LoginAutoAsync(modelUsuarioApp);

            if (token != string.Empty)
            {
                restService.SetCredentials(token);

                //Sincronizar - Upload
                await AssociadoAsync();
                await VeiculoAsync();
                await VistoriaImagemAsync();

                //Sincronizar - Download
                //await AcessoriosAsync();

                //Logout
                await usuarioService.LogoutAsync();
            }

            IsBusy = false;

            if (listaErros.Count == 0)
            {
                await Shell.Current.DisplayAlert("Info", "Sincronização efetuado com sucesso.", "Ok");
            }
            else
            {
                var mensagem = "";
                foreach (var item in listaErros)
                {
                    mensagem += $"{"\n"}item";
                }
                await Shell.Current.DisplayAlert("Erros de sincronização", mensagem, "Ok");
            }
        }

        private async Task<string> LoginAutoAsync(UsuarioAppModel model)
        {
            var dataResponse = await usuarioService.LoginAsync(model.UserName, model.Password);

            if (dataResponse == null || !dataResponse.Success)
            {
                listaErros.Add($"<<Login Erro>>");
                if (dataResponse != null)
                {
                    listaErros.Add($"<<Login Erro>>[Detalhe] Retorno backend: {dataResponse.Errors}");
                }
                return "";
            }

            return dataResponse.Data.Token;
        }

        private async Task<UsuarioAppModel> GetUsuarioAppAsync()
        {
            var model = usuarioService.GetPerfilAppLocal();

            if (model == null)
            {
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                return null;
            }

            return model;
        }

        private async Task AssociadoAsync()
        {
            var lista = associadoService.GetAll(x => x.StatusRegistro == StatusRegistroType.Pendente);
            foreach (var item in lista)
            {
                var dataResponse = await restService.PostAssociadoAsync(item);

                if (dataResponse == null)
                {
                    listaErros.Add($"<<Associado Erro>> Id:{item.Id}, Nome:{dataResponse.Nome}");
                }

                //if (dataResponse == null || !dataResponse.Success)
                //{
                //    listaErros.Add($"<<Associado Erro>> Id:{item.Id}, Nome:{item.Nome}");
                //    if (dataResponse != null)
                //    {
                //        listaErros.Add($"<<Associado Erro>>[Detalhe] Retorno backend: {dataResponse.Errors}");
                //    }
                //}

                item.StatusRegistro = StatusRegistroType.Enviado;
                associadoService.Update(item);
            }
        }

        private async Task VeiculoAsync()
        {
            var lista = veiculoService.GetAll(x => x.StatusRegistro == StatusRegistroType.Pendente);
            foreach (var item in lista)
            {
                var dataResponse = await restService.PostVeiculoAsync(item);

                if (dataResponse == null)
                {
                    listaErros.Add($"<<Veiculo Erro>> Id:{item.Id}, Nome:{item.MarcaModelo}");
                }

                //if (dataResponse == null || !dataResponse.Success)
                //{
                //    listaErros.Add($"<<Veiculo Erro>> Id:{item.Id}, Marca/Modelo:{item.MarcaModelo}");
                //    if (dataResponse != null)
                //    {
                //        listaErros.Add($"<<Veiculo Erro>>[Detalhe] Retorno backend: {dataResponse.Errors}");
                //    }
                //}

                item.StatusRegistro = StatusRegistroType.Enviado;
                veiculoService.Update(item);
            }
        }

        private async Task VistoriaImagemAsync()
        {
            var lista = vistoriaImagemService.GetAll(x => x.StatusRegistro == StatusRegistroType.Pendente);
            foreach (var item in lista)
            {
                var dataResponse = await restService.PostVistoriaImagemAsync(item);
                var dataResponseImagem = await restService.UploadImagemByVistoriaImagemIdAsync(item);

                if (dataResponse == null)
                {
                    listaErros.Add($"<<Vistoria Imagem Erro>> Id:{item.Id}, VeiculoId:{item.VeiculoId}");
                }

                if (dataResponseImagem == null)
                {
                    listaErros.Add($"<<Vistoria Imagem Erro>> Id:{item.Id}, VeiculoId:{item.VeiculoId}");
                }

                item.StatusRegistro = StatusRegistroType.Enviado;
                vistoriaImagemService.Update(item);

            }
        }

        private string ImageToBase64(string imagemUrl)
        {
            string localFilePath = Path.Combine(FileSystem.AppDataDirectory, "Imagens", imagemUrl);
            FileStream stream = File.OpenRead(localFilePath);

            byte[] bytes;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                    return Convert.ToBase64String(bytes);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }        
    }
}
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TccApp.Domain.Interfaces;
using TccApp.Services;
using TccApp.ViewModels;
using TccApp.Views;

namespace TccApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterServices()
            .RegisterViewModels()
            .RegisterPages();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        //Serviços da biblioteca Maui
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        //Comunicação Api
        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
        builder.Services.AddSingleton<IRestService, RestService>();

        //Conexão banco
        builder.Services.AddSingleton<DatabaseConnection>();

        //Configuração
        builder.Services.AddSingleton<IUsuarioService, UsuarioService>();

        //Associado
        builder.Services.AddTransient<IAssociadoService, AssociadoService>();
        builder.Services.AddTransient<IVeiculoService, VeiculoService>();
        builder.Services.AddTransient<IVistoriaImagemService, VistoriaImagemService>();

        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        //Tabelas
        builder.Services.AddTransient<AcessorioIndexViewModel>();
        builder.Services.AddTransient<AcessorioViewModel>();

        //Associado
        builder.Services.AddTransient<AssociadoIndexViewModel>();
        builder.Services.AddTransient<AssociadoViewModel>();


        //Veiculo
        builder.Services.AddTransient<VeiculoIndexViewModel>();
        builder.Services.AddTransient<VeiculoViewModel>();

        //Imagem
        builder.Services.AddTransient<VistoriaImagemIndexViewModel>();
        builder.Services.AddTransient<VistoriaImagemViewModel>();

        //Sincronizar
        builder.Services.AddSingleton<SincronizarViewModel>();

        //Acesso
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LogoutViewModel>();
        builder.Services.AddTransient<PerfilViewModel>();

        return builder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        //Tabelas
        builder.Services.AddTransient<AcessorioIndexPage>();
        builder.Services.AddTransient<AcessorioPage>();

        //Associado
        builder.Services.AddTransient<AssociadoIndexPage>();
        builder.Services.AddTransient<AssociadoPage>();

        //Veiculo
        builder.Services.AddTransient<VeiculoIndexPage>();
        builder.Services.AddTransient<VeiculoPage>();

        //Imagem
        builder.Services.AddTransient<VistoriaImagemIndexPage>();
        builder.Services.AddTransient<VistoriaImagemPage>();

        //Sincronizar
        builder.Services.AddSingleton<SincronizarPage>();

        //Acesso
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LogoutPage>();
        builder.Services.AddTransient<PerfilPage>();

        //Sobre
        builder.Services.AddTransient<SobrePage>();

        return builder;
    }

}

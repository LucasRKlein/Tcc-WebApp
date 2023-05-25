using Microsoft.Extensions.Logging;
using TccApp.Domain.Interfaces;
using TccApp.Models;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
        .RegisterRepositories()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterPages();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
    {
        //Usuário
        //builder.Services.AddTransient<IRepository<UsuarioAppModel>, Repository<UsuarioAppModel>>();

        //Comunicação Api
        
        //Tabelas
        builder.Services.AddSingleton<IRepository<AcessorioModel>, Repository<AcessorioModel>>();
        builder.Services.AddSingleton<IRepository<AssociadoModel>, Repository<AssociadoModel>>();

        //Vistoria
        //builder.Services.AddTransient<IRepository<VistoriaModel>, Repository<VistoriaModel>>();

        return builder;
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
        //builder.Services.AddTransient<IVistoriaImagemService, VistoriaImagemService>();

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

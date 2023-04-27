using Microsoft.Extensions.Logging;
using TccApp.Data;
using TccApp.Models;
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

        //Tabelas
        builder.Services.AddSingleton<IRepository<AssociadoModel>, Repository<AssociadoModel>>();

        //Vistoria
        //builder.Services.AddTransient<IRepository<VistoriaModel>, Repository<VistoriaModel>>();

        return builder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        //Serviços do App
        //builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
        //builder.Services.AddSingleton<IRestService, RestService>();
        //builder.Services.AddSingleton<IUsuarioService, UsuarioService>();

        //Serviços da biblioteca Maui
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        //builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        //builder.Services.AddSingleton<IMap>(Map.Default);

        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        //Tabelas
        builder.Services.AddTransient<AssociadoIndexViewModel>();
        builder.Services.AddTransient<AssociadoViewModel>();
        
        //Sincronizar
        //builder.Services.AddSingleton<SincronizarViewModel>();

        //Acesso
        //builder.Services.AddTransient<LoginViewModel>();
        //builder.Services.AddTransient<LogoutViewModel>();
        //builder.Services.AddTransient<PerfilViewModel>();

        return builder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        //Tabelas
        builder.Services.AddTransient<AssociadoIndexPage>();
        builder.Services.AddTransient<AssociadoPage>();

        //Vistoria
        //builder.Services.AddTransient<VistoriaIndexPage>();

        //Sincronizar
        //builder.Services.AddSingleton<SincronizarPage>();

        //Acesso
        //builder.Services.AddTransient<LoginPage>();
        //builder.Services.AddTransient<LogoutPage>();
        //builder.Services.AddTransient<PerfilPage>();

        //
        //builder.Services.AddTransient<SobrePage>();

        return builder;
    }

}

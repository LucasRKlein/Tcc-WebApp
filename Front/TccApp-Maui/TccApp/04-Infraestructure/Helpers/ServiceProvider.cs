namespace TccApp.Infraestructure.Helpers;

public static class ServiceHelper
{
    /// <summary>
    /// Service Helper utilizado para Dependency-Injection quando a instanciação 
    /// será sem passar a classe por parâmentro.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetService<TService>()
        => Current.GetService<TService>();

    public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			MauiUIApplicationDelegate.Current.Services;
#else
			null;
#endif
}
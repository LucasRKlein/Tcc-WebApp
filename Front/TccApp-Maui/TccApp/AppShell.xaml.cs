using TccApp.Views;

namespace TccApp;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    public AppShell()
	{
        InitializeComponent();
        RegisterRoutes();

        CurrentItem = sincronizar;
    }

    void RegisterRoutes()
    {
        Routes.Add(nameof(AcessorioPage), typeof(AcessorioPage));
        Routes.Add(nameof(AssociadoPage), typeof(AssociadoPage));
        Routes.Add(nameof(VeiculoIndexPage), typeof(VeiculoIndexPage));
        Routes.Add(nameof(VeiculoPage), typeof(VeiculoPage));
        Routes.Add(nameof(VistoriaImagemIndexPage), typeof(VistoriaImagemIndexPage));
        Routes.Add(nameof(VistoriaImagemPage), typeof(VistoriaImagemPage));
        Routes.Add(nameof(LoginPage), typeof(LoginPage));
        Routes.Add(nameof(AssociadoIndexPage), typeof(AssociadoIndexPage));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}

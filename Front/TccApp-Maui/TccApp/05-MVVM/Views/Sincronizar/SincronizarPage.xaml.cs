using TccApp.ViewModels;

namespace TccApp.Views;

public partial class SincronizarPage : ContentPage
{
    public SincronizarPage(SincronizarViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
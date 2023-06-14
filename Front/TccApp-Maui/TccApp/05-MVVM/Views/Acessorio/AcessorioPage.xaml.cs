using TccApp.ViewModels;

namespace TccApp.Views;

public partial class AcessorioPage : ContentPage
{
    public AcessorioPage(AcessorioViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
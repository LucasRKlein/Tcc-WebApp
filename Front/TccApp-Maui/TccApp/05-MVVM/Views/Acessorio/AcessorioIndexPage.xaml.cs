using TccApp.ViewModels;

namespace TccApp.Views;

public partial class AcessorioIndexPage : ContentPage
{
    public AcessorioIndexPage(AcessorioIndexViewModel viewmodel)
    {
        InitializeComponent();

        BindingContext = viewmodel;
    }
}
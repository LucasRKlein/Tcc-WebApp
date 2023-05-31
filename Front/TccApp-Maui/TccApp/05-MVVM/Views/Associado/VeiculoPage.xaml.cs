using TccApp.ViewModels;

namespace TccApp.Views;

public partial class VeiculoPage : ContentPage
{
    public VeiculoPage(VeiculoViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
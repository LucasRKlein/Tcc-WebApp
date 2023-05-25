
using TccApp.ViewModels;

namespace TccApp.Views;

public partial class PerfilPage : ContentPage
{
    public PerfilPage(PerfilViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}
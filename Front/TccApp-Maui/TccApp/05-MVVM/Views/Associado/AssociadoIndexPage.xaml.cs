using TccApp.ViewModels;

namespace TccApp.Views;

public partial class AssociadoIndexPage : ContentPage
{
    public AssociadoIndexPage(AssociadoIndexViewModel viewmodel)
    {
        InitializeComponent();

        BindingContext = viewmodel;
    }
}
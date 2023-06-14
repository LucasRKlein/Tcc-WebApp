using TccApp.ViewModels;

namespace TccApp.Views;

public partial class AssociadoPage : ContentPage
{
	public AssociadoPage(AssociadoViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
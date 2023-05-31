using TccApp.ViewModels;

namespace TccApp.Views;

public partial class VeiculoIndexPage : ContentPage
{
	public VeiculoIndexPage(VeiculoIndexViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
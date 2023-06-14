using TccApp.ViewModels;

namespace TccApp.Views;

public partial class VistoriaImagemIndexPage : ContentPage
{
	public VistoriaImagemIndexPage(VistoriaImagemIndexViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
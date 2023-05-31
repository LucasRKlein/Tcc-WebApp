using TccApp.ViewModels;

namespace TccApp.Views;

public partial class VistoriaImagemPage : ContentPage
{
	public VistoriaImagemPage(VistoriaImagemViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
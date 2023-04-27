using System.Windows.Input;

namespace TccApp.Views
{
    public partial class SobrePage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public SobrePage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}

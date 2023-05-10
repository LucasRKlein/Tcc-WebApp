namespace TccApp.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        protected void PopulateListFromEnum<TEnum>(List<string> lista)
        {
            lista.Clear();

            foreach (var enumItem in typeof(TEnum).GetFields())
            {
                if (enumItem.IsLiteral)
                {
                    lista.Add(enumItem.Name);
                }
            }
        }
    }
}
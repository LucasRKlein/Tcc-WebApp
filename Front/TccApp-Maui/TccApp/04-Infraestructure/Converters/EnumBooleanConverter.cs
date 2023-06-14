namespace TccApp.Converters
{
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return Binding.DoNothing;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return Binding.DoNothing;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);            

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return Binding.DoNothing;

            return Enum.Parse(targetType, parameterString);
        }
    }
}

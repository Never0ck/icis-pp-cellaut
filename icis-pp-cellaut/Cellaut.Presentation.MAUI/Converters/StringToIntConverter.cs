using System.Globalization;

namespace Cellaut.Presentation.MAUI.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // int -> string
            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // string -> int
            int result = 0;
            int.TryParse(((string)value), out result);
            return result;
        }
    }
}

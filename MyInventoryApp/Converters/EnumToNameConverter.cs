using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyInventoryApp.Converters
{
    public class EnumToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Enum enumValue)
            {
                // Todo: add code to handle camel casing and resource extraction
                return enumValue.ToString();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

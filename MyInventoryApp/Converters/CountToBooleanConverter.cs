using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyInventoryApp.Converters
{
    public class CountToBooleanConverter : IValueConverter
    {
        public CountToBooleanConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var count = System.Convert.ToInt32(value);
                var result = count > 0;
                if (parameter == null)
                    parameter = false;
                if (bool.Parse(parameter.ToString()))
                {
                    result = !result;
                }

                                return result;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

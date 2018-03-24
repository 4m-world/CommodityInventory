using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace MyInventoryApp.Converters
{
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is string imageStr)
            {
                var bytes = System.Convert.FromBase64String(imageStr);
                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

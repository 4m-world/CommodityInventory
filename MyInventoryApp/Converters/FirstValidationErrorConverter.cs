using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using System.Linq;

namespace MyInventoryApp.Converters
{
    public class FirstValidationErrorConverter :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ICollection<string> errors && errors?.Count > 0){
                return errors.ElementAt(0);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

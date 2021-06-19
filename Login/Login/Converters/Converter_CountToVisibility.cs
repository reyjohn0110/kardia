using System;
using System.Globalization;
using Xamarin.Forms;

namespace Login.Converters
{
    public class Converter_CountToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = false;
            if(value is int)
            {
                var i = (int)value;
                ret = i == 0 ? false : true;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

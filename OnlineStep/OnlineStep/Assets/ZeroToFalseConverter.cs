using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnlineStep.Assets
{
    public class ZeroToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return !(bool)value;
            return (double)value != 0.0 ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}

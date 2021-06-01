using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RoboCommerc
{
    public sealed class MethodInfoFilter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var test = (Type)value;
            return test?.GetMethods().Where(x => x.IsPublic || x.IsFamily);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
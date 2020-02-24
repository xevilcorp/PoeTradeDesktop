using PoeTradeDesktop.Schemes.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PoeTradeDesktop.UI.Converters
{
    class PropertyTypeToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.IsDefined(typeof(PropertyType), (int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

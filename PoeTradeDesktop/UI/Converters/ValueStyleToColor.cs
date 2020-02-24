using PoeTradeDesktop.Schemes.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PoeTradeDesktop.UI.Converters
{
    class ValueStyleToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "#ffffff";
            if(value != null)
            {
                ValueStyle style = (ValueStyle)Enum.Parse(typeof(ValueStyle), value.ToString());
                switch (style)
                {
                    case ValueStyle.Augmented: color = "#8888ff"; break;
                    case ValueStyle.Unmet: color = "#d20000"; break;
                    case ValueStyle.PhysicalDamage: color = "#ffffff"; break;
                    case ValueStyle.FireDamage: color = "#960000"; break;
                    case ValueStyle.ColdDamage: color = "#366492"; break;
                    case ValueStyle.LightningDamage: color = "#FFD700"; break;
                    case ValueStyle.ChaosDamage: color = "#d02090"; break;
                    case ValueStyle.MagicItem: color = "#8888FF"; break;
                    case ValueStyle.RareItem: color = "#FFFF77"; break;
                    case ValueStyle.UniqueItem: color = "#af6025"; break;
                }
            }
          
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
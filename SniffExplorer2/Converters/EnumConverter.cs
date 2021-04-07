using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace SniffExplorer.UI.Converters
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Enum @enum)
                return GetDescription(@enum);

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public static string GetDescription(Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes<DescriptionAttribute>().ToArray();
                if (attrs.Length > 0)
                    return attrs[0].Description;
            }

            return en.ToString();
        }
    }
}

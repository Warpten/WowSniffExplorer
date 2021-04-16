using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SniffExplorer.Converters
{
    public sealed class ValueToVisibility : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == default)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

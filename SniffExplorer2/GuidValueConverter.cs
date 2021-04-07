﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.UI
{
    public class GuidValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (!(value is IObjectGUID objectGUID))
                    return string.Empty;

                return objectGUID.ToString();
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

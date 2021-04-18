using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SniffExplorer.Extensions
{
    public static class VisualTreeExtensions
    {
        public static T? FindChild<T>(this DependencyObject depObj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? FindChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static T? FindChild<T>(this DependencyObject depObj, int recursionLevel) where T : DependencyObject
        {
            if (recursionLevel == 0)
                return null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? FindChild<T>(child, recursionLevel - 1);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}

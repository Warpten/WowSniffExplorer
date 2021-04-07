using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SniffExplorer.UI.Misc
{
    public static class MiscExtensions
    {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject parent, bool recursive = false) where T : DependencyObject
        {
            if (parent != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);

                    // If the available child is not null and is of required Type<T> then return with this child else continue this loop
                    if (child is T dependencyObject)
                        yield return dependencyObject;

                    if (recursive)
                        foreach (var childOfChild in FindVisualChildren<T>(child))
                            yield return childOfChild;
                }
            }
        }
    }
}

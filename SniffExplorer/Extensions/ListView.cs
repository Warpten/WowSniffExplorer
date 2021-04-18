using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SniffExplorer.Extensions
{
    public class ListView : DependencyObject
    {
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.RegisterAttached("Stretch",
                typeof(bool),
                typeof(ListView),
                new UIPropertyMetadata(true, null, OnCoerceStretch));

        public static readonly DependencyProperty StretchDataCacheProperty =
            DependencyProperty.RegisterAttached("StretchDataCache",
                typeof(List<GridViewColumn>),
                typeof(ListView),
                new UIPropertyMetadata(null, null, null));
        
        public static bool GetStretch(DependencyObject obj) => (bool) obj.GetValue(StretchProperty);
        public static void SetStretch(DependencyObject obj, bool value) => obj.SetValue(StretchProperty, value);
        
        public static List<GridViewColumn>? GetStretchDataCache(DependencyObject obj) => (List<GridViewColumn>?) obj.GetValue(StretchDataCacheProperty);
        public static void SetStretchDataCache(DependencyObject obj, List<GridViewColumn> value) => obj.SetValue(StretchDataCacheProperty, value);

        public static object OnCoerceStretch(DependencyObject source, object value)
        {
            if (source is not System.Windows.Controls.ListView listView)
                throw new ArgumentException("This property may only be used on ListViews");

            //Setup our event handlers for this list view.
            listView.Loaded += new RoutedEventHandler(lv_Loaded);
            listView.SizeChanged += new SizeChangedEventHandler(lv_SizeChanged);
            return value;
        }
        
        ///
        /// Handles the SizeChanged event of the lv control.
        ///
        /// The source of the event.
        /// The instance containing the event data.
        private static void lv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var lv = (sender as System.Windows.Controls.ListView);
            if (lv.IsLoaded)
                SetColumnWidths(lv);
        }

        ///
        /// Handles the Loaded event of the lv control.
        ///
        /// The source of the event.
        /// The instance containing the event data.
        private static void lv_Loaded(object sender, RoutedEventArgs e)
        {
            var lv = (sender as System.Windows.Controls.ListView);
            SetColumnWidths(lv);
        }

        ///
        /// Sets the column widths.
        ///
        private static void SetColumnWidths(System.Windows.Controls.ListView listView)
        {
            if (listView.View is not GridView gridView)
                return;
            
            double specifiedWidth = 0;

            var columns = GetStretchDataCache(listView);
            if (columns == null)
            {
                //Instance if its our first run.
                columns = new List<GridViewColumn>();
                // Get all columns with no width having been set.
                foreach (var column in gridView.Columns)
                    if (!(column.Width >= 0))
                        columns.Add(column);
                    else
                        specifiedWidth += column.ActualWidth;
            }
            else
            {
                // Get all columns with no width having been set.
                foreach (var column in gridView.Columns)
                    if (!columns.Contains(column))
                        specifiedWidth += column.ActualWidth;
            }

            // Allocate remaining space equally.
            GridViewColumn? lastColumn = default;
            foreach (var column in columns)
            {
                var newWidth = (listView.ActualWidth - specifiedWidth) / columns.Count;
                if (newWidth >= 10)
                    column.Width = newWidth - 10;

                lastColumn = column;
            }

            // Reclaim space for the vertical scroll bar.
            if (lastColumn != null)
            {
                var scrollView = listView.FindChild<ScrollViewer>();
                if (scrollView?.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                {
                    if (lastColumn.Width > SystemParameters.VerticalScrollBarWidth)
                        lastColumn.Width -= SystemParameters.VerticalScrollBarWidth;
                }
            }

            //Store the columns in the Tag property for later use.
            SetStretchDataCache(listView, columns);
        }
    }
}

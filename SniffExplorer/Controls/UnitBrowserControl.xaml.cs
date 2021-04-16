using System.Windows;
using System.Windows.Controls;
using SniffExplorer.Controls.Models;
using SniffExplorer.Windows;

namespace SniffExplorer.Controls
{
    /// <summary>
    /// Interaction logic for UnitBrowserControl.xaml
    /// </summary>
    public partial class UnitBrowserControl : UserControl
    {
        public UnitModel Model => (UnitModel) DataContext;

        public UnitBrowserControl()
        {
            InitializeComponent();
        }

        private void HandleMovementDisplayRequest(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.DataContext is not UnitEntityDisplay unitEntity)
                return;

            var movementBrowser = new MovementBrowser(unitEntity.Object);
            movementBrowser.Show();
        }
    }
}

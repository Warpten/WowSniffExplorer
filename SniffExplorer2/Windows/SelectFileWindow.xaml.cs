using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.UI.Windows
{
    /// <summary>
    /// Interaction logic for SelectFileWindow.xaml
    /// </summary>
    public partial class SelectFileWindow : Window
    {
        [NotifyingProperty(PropertyName = "FilePath")]
        private string _filePath;

        [NotifyingProperty(PropertyName = "IgnoreDescriptors")]
        private bool _ignoreDescriptors = false;

        [NotifyingProperty(PropertyName = "IgnorePlayers")]
        private bool _ignorePlayers = false;

        public SelectFileWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void HandleBrowserButton(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {
                Filter = ".PKT Files (*.pkt)|*.pkt|.BIN Files (*.bin)|*.bin"
            };

            var result = dialog.ShowDialog();
            if (result.GetValueOrDefault(false))
                FilePath = dialog.FileName;
        }

        private void HandleLoadButton(object sender, RoutedEventArgs e)
        {
            var options = new ParsingOptions {
                DiscardUpdateFields = _ignoreDescriptors
            };

            var mainWindow = new MainWindow(_filePath, options);
            mainWindow.Show();

            Close();
        }
    }
}

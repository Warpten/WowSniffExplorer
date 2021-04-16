using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SniffExplorer.Controls.Models;
using SniffExplorer.Parsing.Engine;

namespace SniffExplorer.Controls
{
    /// <summary>
    /// Interaction logic for FileSelectionControl.xaml
    /// </summary>
    public partial class FileSelectionControl : UserControl
    {
        private FileSelectionModel Model => (FileSelectionModel) DataContext;

        public event Action<string, ParsingOptions>? FileSelected;

        public FileSelectionControl()
        {
            InitializeComponent();

            var model = new FileSelectionModel();
            DataContext = model;
        }

        private void HandleBrowserButton(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = ".PKT Files (*.pkt)|*.pkt|.BIN Files (*.bin)|*.bin"
            };

            var result = dialog.ShowDialog();
            if (result.GetValueOrDefault(false))
                Model.FilePath = dialog.FileName;
        }

        private void HandleLoadButton(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Model.FilePath))
                return;

            var options = new ParsingOptions {
                DiscardUpdateFields = Model.DiscardUpdateFields
            };

            FileSelected?.Invoke(Model.FilePath, options);
        }

        #region Drag'n'drop handling
        private void HandleDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void HandleDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] fileNames)
            {
                e.Handled = fileNames[0].EndsWith(".pkt") || fileNames[0].EndsWith(".bin");
                if (e.Handled)
                    Model.FilePath = fileNames[0];
            }
        }
        #endregion
    }
}

using System.Windows;
using SniffExplorer.Controls;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Windows.Models;

namespace SniffExplorer.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel Model => (MainWindowModel) DataContext;

        public MainWindow()
        {
            InitializeComponent();

            Model.Stage = Stage.FileSelection;
        }

        private void HandleFileSelected(string filePath, ParsingOptions options)
        {
            Model.Stage = Stage.FileAnalysis;

            if (_contentControl.Content is FileAnalysisControl fileAnalysisControl)
                fileAnalysisControl.ProcessFile(filePath, options);
        }
    }
}

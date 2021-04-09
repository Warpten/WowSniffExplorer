using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Loading;
using SniffExplorer.UI.Reactive;

namespace SniffExplorer.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherScheduler _uiScheduler;

        public MainWindow()
        {
            InitializeComponent();

            _uiScheduler = new DispatcherScheduler(Application.Current.Dispatcher);
        }

        public MainWindow(string filePath, ParsingOptions options)
            : this()
        {
            try
            {
                var sniffFile = new SniffFile(filePath);
                _parseStatusText.Text = $"Detected client build {sniffFile!.ClientBuild}.";

                Observable.Start(() =>
                {
                    ProcessFileAsync(sniffFile, options)
                        .ObserveOn(_uiScheduler)
                        .Subscribe(data => { OnParseCompleted(data.Context, data.Statistics); });
                }, _uiScheduler);
            }
            catch (InvalidOperationException _)
            {

            }
        }

        private void OnParseCompleted(ParsingContext context, ParsingStatistics statistics)
        {
            var percentage = 100.0f * statistics.ParsedPacketCount / statistics.PacketCount;
            _parseStatusText.Text = $"Processed packets: {statistics.ParsedPacketCount:N0} / {statistics.PacketCount:N0} ({percentage:F2}%). Elapsed time: {statistics.ExecutionTime}.";

            var entities = context.ObjectManager.AsCollection();
            
            _playerDisplayControl.ViewModel.Entities = entities.OfType<Player>();
            _unitDisplayControl.ViewModel.Entities = entities.OfType<Creature>();
        }

        private IObservable<(ParsingContext Context, ParsingStatistics Statistics)> ProcessFileAsync(SniffFile sniffFile, ParsingOptions options)
        {
            var parser = Parser.Of(sniffFile);

            parser.PacketParsed.Sample(TimeSpan.FromMilliseconds(5), _uiScheduler)
                .Subscribe(opcode => {
                    _parseStatusText.Text = $"(#{opcode.Index:N0}) Parsed {opcode.Opcode}.";
                }, () => {
                    parser.Dispose();
                });

            return parser.Run(options);
        }
        
        #region Tablet PC bullshit
        private bool _systemMenuAlignment;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SystemParametersInfo(27 /* SPI_GETMENUDROPALIGNMENT */, 0, ref _systemMenuAlignment, 0);
            if (_systemMenuAlignment)
            {
                var newValue = false;
                SystemParametersInfo(28 /* SPI_SETMENUDROPALIGNMENT */, 0, ref newValue, 0);
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            SystemParametersInfo(28 /* SPI_SETMENUDROPALIGNMENT */, 0, ref _systemMenuAlignment, 0);
        }
        #endregion

        #region Native methods
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        private static extern bool SystemParametersInfo(
            int nAction,
            int nParam,
            ref bool value,
            int ignore);
        #endregion

        private void HideOverFlowButton(object sender, RoutedEventArgs e)
        {
            if (!(sender is ToolBar toolBar))
                return;

            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
                overflowGrid.Visibility = Visibility.Collapsed;

            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
                mainPanelBorder.Margin = new Thickness(0);

            if (toolBar.Template.FindName("OverflowButton", toolBar) is FrameworkElement overflowButton)
                overflowButton.Visibility = Visibility.Collapsed;
        }
    }
}

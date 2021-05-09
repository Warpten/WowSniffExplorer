using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Loading;
using SniffExplorer.Reactive;

namespace SniffExplorer.Controls
{
    /// <summary>
    /// Interaction logic for FileAnalysisControl.xaml
    /// </summary>
    public partial class FileAnalysisControl : UserControl
    {
        private readonly DispatcherScheduler _uiScheduler;

        public FileAnalysisControl()
        {
            InitializeComponent();

            _uiScheduler = new(Application.Current.Dispatcher);
        }
        
        public void ProcessFile(string filePath, ParsingOptions options)
        {
            try
            {
                var sniffFile = new SniffFile(filePath);
                _parseStatusText.Text = $"Detected client build {sniffFile!.ClientBuild}. Collecting packets...";

                Observable.Start(() =>
                {
                    ProcessFileAsync(sniffFile, options)
                        .ObserveOn(_uiScheduler)
                        .Subscribe(data => { OnParseCompleted(data.Context, data.Statistics); });
                }, TaskPoolScheduler.Default);
            }
            catch (InvalidOperationException)
            {

            }
        }

        private void OnParseCompleted(ParsingContext context, ParsingStatistics statistics)
        {
            var percentage = 100.0f * statistics.ParsedPacketCount / statistics.PacketCount;
            _parseStatusText.Text = $"Processed packets: {statistics.ParsedPacketCount:N0} / {statistics.PacketCount:N0} ({percentage:F2}%). Elapsed time: {statistics.ExecutionTime}.";

            var entities = context.ObjectManager.AsCollection();

            _unitDisplayControl.Model.Objects = entities.OfType<Creature>();
            _playerDisplayControl.Model.Objects = entities.OfType<Player>();
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
    }
}

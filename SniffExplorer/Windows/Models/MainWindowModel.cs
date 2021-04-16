using System.ComponentModel;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.Windows.Models
{
    public partial class MainWindowModel : INotifyPropertyChanged
    {
        [NotifyingProperty(PropertyName = "Staeg")]
        private Stage? _stage;

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public enum Stage
    {
        FileSelection,
        FileAnalysis
    }
}

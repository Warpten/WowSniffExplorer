using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.UI.Controls
{
    /// <summary>
    /// Interaction logic for RaceCheckBox.xaml
    /// </summary>
    public partial class RaceCheckBox : CheckBox, INotifyPropertyChanged
    {
        public RaceCheckBox()
        {
            InitializeComponent();
        }

        public static DependencyProperty RaceMaskProperty = DependencyProperty.Register("RaceMask", typeof(RaceMask), typeof(ClassCheckBox));

        [NotifyingProperty(PropertyName = "Text")]
        private string _text;

        private ImageSource _icon;

        public event PropertyChangedEventHandler PropertyChanged;

        public RaceMask RaceMask
        {
            get => (RaceMask) GetValue(RaceMaskProperty);
            set => SetValue(RaceMaskProperty, value);
        }

        public ImageSource Icon
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon))
                    return;

                _icon = value;
                icon.Visibility = value != null ? Visibility.Visible : Visibility.Collapsed;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
            }
        }
    }
}

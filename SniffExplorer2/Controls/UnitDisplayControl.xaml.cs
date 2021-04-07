using System.Windows.Controls;
using SniffExplorer.UI.Controls.Models;

namespace SniffExplorer.UI.Controls
{
    /// <summary>
    /// Interaction logic for UnitDisplayControl.xaml
    /// </summary>
    public partial class UnitDisplayControl : UserControl, IViewModelControl<UnitDisplayViewModel>
    {
        public UnitDisplayViewModel ViewModel { get; }= new();

        public UnitDisplayControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }
}

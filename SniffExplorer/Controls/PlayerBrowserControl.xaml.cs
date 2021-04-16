using System.Windows.Controls;
using SniffExplorer.Controls.Models;

namespace SniffExplorer.Controls
{
    /// <summary>
    /// Interaction logic for PlayerBrowserControl.xaml
    /// </summary>
    public partial class PlayerBrowserControl : UserControl
    {
        public PlayerModel Model => (PlayerModel) DataContext;

        public PlayerBrowserControl()
        {
            InitializeComponent();
        }
    }
}

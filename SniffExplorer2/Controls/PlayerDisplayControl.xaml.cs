using System.ComponentModel;
using System.Windows.Controls;

using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Shared.Attributes.UI;
using SniffExplorer.UI.Controls.Models;

namespace SniffExplorer.UI.Controls
{
    /// <summary>
    /// Interaction logic for PlayerDisplayControl.xaml
    /// </summary>
    public partial class PlayerDisplayControl : UserControl, IViewModelControl<PlayerDisplayViewModel>
    {
        public PlayerDisplayViewModel ViewModel { get; } = new();

        public PlayerDisplayControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }

    public partial class ClassFilters : INotifyPropertyChanged
    {
        [EnumFlagProperty]
        public ClassMask Value { get; set; } = ClassMask.Warrior |
                                               ClassMask.Paladin |
                                               ClassMask.Hunter |
                                               ClassMask.Rogue |
                                               ClassMask.Priest |
                                               ClassMask.DeathKnight |
                                               ClassMask.Shaman |
                                               ClassMask.Mage |
                                               ClassMask.Warlock |
                                               ClassMask.Monk |
                                               ClassMask.Druid |
                                               ClassMask.DemonHunter;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

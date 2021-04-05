using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JetBrains.Annotations;
using SniffExplorer.Parsing.Types.Enums;

namespace SniffExplorer.UI.Controls
{
    /// <summary>
    /// Interaction logic for ClassCheckBox.xaml
    /// </summary>
    public partial class ClassCheckBox : CheckBox, INotifyPropertyChanged
    {
        public ClassCheckBox()
        {
            InitializeComponent();

        }

        public static DependencyProperty ClassMaskProperty = DependencyProperty.Register("ClassMask", typeof(ClassMask), typeof(ClassCheckBox));
        private string _text;
        private ImageSource _icon;

        public ClassMask ClassMask
        {
            get => (ClassMask) GetValue(ClassMaskProperty);
            set => SetValue(ClassMaskProperty, value);
        }

        public string Text
        {
            get => _text;
            set
            {
                if (value == _text)
                    return;
                
                _text = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Icon
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon))
                    return;
                
                _icon = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

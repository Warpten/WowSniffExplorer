using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SniffExplorer.Controls
{
    /// <summary>
    /// Interaction logic for ImageCheckBox.xaml
    /// </summary>
    public partial class ImageCheckBox : UserControl
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(ImageCheckBox));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ImageCheckBox));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ImageCheckBox));
        
        public ImageSource Icon
        {
            get => (ImageSource) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public ImageCheckBox()
        {
            InitializeComponent();
        }
    }
}

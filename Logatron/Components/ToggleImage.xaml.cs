using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Logatron.Components
{
    public partial class ToggleImage : UserControl
    {
        public static readonly DependencyProperty OnOffProperty = DependencyProperty.Register(
            nameof(OnOff), typeof(bool), typeof(ToggleImage), new PropertyMetadata(null));

        public bool OnOff
        {
            get { return (bool)GetValue(OnOffProperty); }
            set { SetValue(OnOffProperty, value); }
        }

        public static readonly DependencyProperty OnImageProperty = DependencyProperty.Register(
            nameof(OnImage), typeof(ImageSource), typeof(ToggleImage), new PropertyMetadata(null));

        public ImageSource OnImage
        {
            get { return (ImageSource)GetValue(OnImageProperty); }
            set { SetValue(OnImageProperty, value); }
        }

        public static readonly DependencyProperty OffImageProperty = DependencyProperty.Register(
            nameof(OffImage), typeof(ImageSource), typeof(ToggleImage), new PropertyMetadata(null));

        public ImageSource OffImage
        {
            get { return (ImageSource)GetValue(OffImageProperty); }
            set { SetValue(OffImageProperty, value); }
        }

        public ToggleImage()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}

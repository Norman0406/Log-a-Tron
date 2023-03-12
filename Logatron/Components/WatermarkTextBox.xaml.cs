using System.Windows;
using System.Windows.Controls;

namespace Logatron.Components
{
    public partial class WatermarkTextBox : UserControl
    {
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
            nameof(Hint), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(null));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public WatermarkTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}

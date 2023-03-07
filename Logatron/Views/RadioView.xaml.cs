using System.Windows;
using System.Windows.Controls;

namespace Logatron.Views
{
    public partial class RadioView : UserControl
    {
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            nameof(StatusProperty), typeof(string), typeof(RadioView), new PropertyMetadata(null));

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty TxProperty = DependencyProperty.Register(
            nameof(Tx), typeof(bool), typeof(RadioView), new PropertyMetadata(null));

        public bool Tx
        {
            get { return (bool)GetValue(TxProperty); }
            set { SetValue(TxProperty, value); }
        }

        public static readonly DependencyProperty FrequencyProperty = DependencyProperty.Register(
            nameof(Frequency), typeof(string), typeof(RadioView), new PropertyMetadata(null));

        public string Frequency
        {
            get { return (string)GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            nameof(Mode), typeof(string), typeof(RadioView), new PropertyMetadata(null));

        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public RadioView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}

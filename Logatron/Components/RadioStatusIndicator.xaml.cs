using System.Windows;
using System.Windows.Controls;

namespace Logatron.Components
{
    public partial class RadioStatusIndicator : UserControl
    {
        public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
            nameof(Disabled), typeof(bool), typeof(RadioStatusIndicator), new PropertyMetadata(null));

        public bool Disabled
        {
            get { return (bool)GetValue(DisabledProperty); }
            set { SetValue(DisabledProperty, value); }
        }

        public static readonly DependencyProperty TxProperty = DependencyProperty.Register(
            nameof(Tx), typeof(bool), typeof(RadioStatusIndicator), new PropertyMetadata(null));

        public bool Tx
        {
            get { return (bool)GetValue(TxProperty); }
            set { SetValue(TxProperty, value); }
        }

        public RadioStatusIndicator()
        {
            InitializeComponent();

            // NOTE: should this also have a view model?
        }
    }
}

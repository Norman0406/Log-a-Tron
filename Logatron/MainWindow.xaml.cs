using System;
using System.Windows;
using Logatron.Handlers.RadioControllers;

namespace Logatron
{
    public partial class MainWindow : Window
    {
        private readonly RadioController _radioController;

        private readonly HamRadioLib.Jt.Client _jtClient;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _radioController = new RadioControllerOmniRig2(RadioView);

            _jtClient = new HamRadioLib.Jt.Client("239.255.255.0", 2237);

            _jtClient.Alerts.Subscribe((alert) =>
            {
            });
        }
    }
}

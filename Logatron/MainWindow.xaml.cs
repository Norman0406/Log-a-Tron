using Logatron.Handlers.RadioControllers;
using System;
using System.Windows;

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

            // initialize radio controller
            _radioController = new RadioControllerOmniRig1(RadioView);

            // initialize jt client
            _jtClient = new HamRadioLib.Jt.Client("239.255.255.0", 2237);
            _jtClient.Alerts.Subscribe((alert) =>
            {
            });

            // initialize lotw client
            var credential = Util.CredentialManager.LoadCredential("LotW");
            if (credential != null)
            {
                HamRadioLib.LotW.Client lotwClient = new(credential.UserName, credential.Password);
                var adifFile = lotwClient.Query().Result;
            }
        }
    }
}

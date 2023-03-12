using Logatron.Views;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Logatron.Handlers.RadioControllers
{
    internal class RadioControllerOmniRig2 : RadioController
    {
        private readonly HamRadioLib.OmniRig2.OmniRig OmniRig = new();
        private readonly CompositeDisposable _subscriptions = new();

        public RadioControllerOmniRig2(RadioView radioView)
            : base(radioView)
        {
            _subscriptions.Add(OmniRig.Status
                .ObserveOnDispatcher()
                .Subscribe(status =>
                {
                    RadioView.Disabled = status != HamRadioLib.OmniRig2.Types.Status.Online;
                }));

            _subscriptions.Add(OmniRig.Frequency
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    RadioView.Frequency = FrequencyStringFromInteger(value / 1000.0);
                }));

            _subscriptions.Add(OmniRig.Mode
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    RadioView.Mode = ModeToString(value);
                }));

            _subscriptions.Add(OmniRig.Transmitting
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    RadioView.Tx = value;
                }));
        }

        private static string ModeToString(HamRadioLib.OmniRig2.Types.Mode mode)
        {
            return mode switch
            {
                HamRadioLib.OmniRig2.Types.Mode.CwUsb => "CW-R",
                HamRadioLib.OmniRig2.Types.Mode.CwLsb => "CW",
                HamRadioLib.OmniRig2.Types.Mode.SsbUsb => "USB",
                HamRadioLib.OmniRig2.Types.Mode.SsbLsb => "LSB",
                HamRadioLib.OmniRig2.Types.Mode.DigitalUsb => "USB-D",
                HamRadioLib.OmniRig2.Types.Mode.DigitalLsb => "LSB-D",
                HamRadioLib.OmniRig2.Types.Mode.AM => "AM",
                HamRadioLib.OmniRig2.Types.Mode.FM => "FM",
                _ => "Unknown",
            }; ;
        }

        protected override void Dispose(bool disposing)
        {
            _subscriptions.Dispose();
        }
    }
}

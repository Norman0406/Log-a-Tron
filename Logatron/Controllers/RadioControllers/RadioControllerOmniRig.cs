using Logatron.Views;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Logatron.Handlers.RadioControllers
{
    internal class RadioControllerOmniRig : RadioController
    {
        private readonly HamRadioLib.OmniRig.OmniRig OmniRig = new();
        private readonly CompositeDisposable _subscriptions = new();

        public RadioControllerOmniRig(RadioView radioView)
            : base(radioView)
        {
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

        private static string ModeToString(HamRadioLib.OmniRig.Mode mode)
        {
            return mode switch
            {
                HamRadioLib.OmniRig.Mode.CwUsb => "CW-R",
                HamRadioLib.OmniRig.Mode.CwLsb => "CW",
                HamRadioLib.OmniRig.Mode.SsbUsb => "USB",
                HamRadioLib.OmniRig.Mode.SsbLsb => "LSB",
                HamRadioLib.OmniRig.Mode.DigitalUsb => "USB-D",
                HamRadioLib.OmniRig.Mode.DigitalLsb => "LSB-D",
                HamRadioLib.OmniRig.Mode.AM => "AM",
                HamRadioLib.OmniRig.Mode.FM => "FM",
                _ => "Unknown",
            }; ;
        }

        protected override void Dispose(bool disposing)
        {
            _subscriptions.Dispose();
        }
    }
}

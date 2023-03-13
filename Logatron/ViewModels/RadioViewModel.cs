using System;
using System.Globalization;

namespace Logatron.ViewModels
{
    public class RadioViewModel : ViewModelBase, IDisposable
    {
        private bool _disabled = true;
        public bool Disabled
        {
            get { return _disabled; }
            set { SetProperty(ref _disabled, value); }
        }

        private bool _tx = false;
        public bool Tx
        {
            get { return _tx; }
            set { SetProperty(ref _tx, value); }
        }

        private string _frequency = FrequencyStringFromInteger(0);
        public string Frequency
        {
            get { return _frequency; }
            set { SetProperty(ref _frequency, value); }
        }

        private string _mode = string.Empty;

        public string Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        public RadioViewModel()
        {
        }

        protected static string FrequencyStringFromInteger(double frequency)
        {
            // Technically this is incorrect. The frequency is passed as 1000 * khz, and is displayed as if it was megahertz.
            // It should be displayed as 14.070.200 instead of 14,070.200.
            return frequency.ToString("0,000.000", CultureInfo.InvariantCulture);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Globalization;

namespace Logatron.MVVM.ViewModels
{
    public partial class RadioViewModel : ViewModelBase, IDisposable
    {
        [ObservableProperty]
        private bool _disabled = true;

        [ObservableProperty]
        private bool _tx = false;

        [ObservableProperty]
        private string _frequency = FrequencyStringFromInteger(0);

        [ObservableProperty]
        private string _mode = string.Empty;

        protected static string FrequencyStringFromInteger(double frequency)
        {
            // Technically this is incorrect. The frequency is passed as 1000 * khz, and is displayed as if it was megahertz.
            // It should be displayed as 14.070.200 instead of 14,070.200.
            return frequency.ToString("0,000.000", CultureInfo.InvariantCulture);
        }

        protected virtual void Dispose(bool disposing)
        {
            // NOP
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

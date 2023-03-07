using System;
using System.Globalization;
using Logatron.Views;

namespace Logatron.Handlers.RadioControllers
{
    internal abstract class RadioController : IDisposable
    {
        protected RadioView RadioView;

        public RadioController(RadioView radioView)
        {
            RadioView = radioView;

            radioView.Status = "Unknown";
            radioView.Tx = false;
            radioView.Frequency = FrequencyStringFromInteger(0);
            radioView.Mode = "Unknown";
        }

        protected static string FrequencyStringFromInteger(double frequency)
        {
            // Technically this is incorrect. The frequency is passed as 1000 * khz, and is displayed as if it was megahertz.
            // It should be displayed as 14.070.200 instead of 14,070.200.
            return frequency.ToString("0,000.000", CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

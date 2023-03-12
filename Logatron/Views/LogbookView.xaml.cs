using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Logatron.Views
{
    public class LogbookEntry
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Callsign { get; set; }

        public string Comments { get; set; }

        public string Name { get; set; }

        public string Mode { get; set; }

        public string Frequency { get; set; }

        public string Band { get; set; }

        public int DXCC { get; set; }

        public string Country { get; set; }

        public int CqZone { get; set; }

        public int ItuZone { get; set; }

        public string RstSent { get; set; }
        
        public string RstRcvd { get; set; }

        public string Gridsquare { get; set; }

        public double Distance { get; set; }
    }

    public partial class LogbookView : UserControl
    {
        public LogbookView()
        {
            InitializeComponent();

            List<LogbookEntry> logbook = new()
            {
                new LogbookEntry() { StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow, Callsign = "HB9HTX", Comments="Nice signal", Name="Norman", Mode="FT8", Frequency="10.136.000", Band="30m" },
                new LogbookEntry() { StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow, Callsign = "HB9HTX", Comments="Nice signal", Name="Norman", Mode="FT8", Frequency="10.136.000", Band="30m" },
                new LogbookEntry() { StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow, Callsign = "HB9HTX", Comments="Nice signal", Name="Norman", Mode="FT8", Frequency="10.136.000", Band="30m" },
            };

            logbookData.ItemsSource = logbook;
        }
    }
}

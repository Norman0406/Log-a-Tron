using System;
using System.ComponentModel.DataAnnotations;

namespace Logatron.Database.DTOs
{
    public class LogbookEntryDTO
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Callsign { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        //public string Mode { get; set; }

        //public string Frequency { get; set; }

        //public string Band { get; set; }

        //public int DXCC { get; set; }

        //public string Country { get; set; }

        //public int CqZone { get; set; }

        //public int ItuZone { get; set; }

        //public string RstSent { get; set; }

        //public string RstRcvd { get; set; }

        //public string Gridsquare { get; set; }

        //public double Distance { get; set; }
    }
}

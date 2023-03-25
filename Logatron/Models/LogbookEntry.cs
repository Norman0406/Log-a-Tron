using System;

namespace Logatron.Models
{
    public class LogbookEntry
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        public DateTime EndTime { get; set; } = DateTime.UtcNow;

        public string Callsign { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;
    }
}

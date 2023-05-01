namespace Logatron.Core.Models;

public class LogbookEntry
{
    public int Id
    {
        get; set;
    }

    /// <summary>
    /// Start time in local time.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    /// End time in local time.
    /// </summary>
    public DateTime EndTime { get; set; } = DateTime.Now;

    public string Callsign { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Comments { get; set; } = string.Empty;
}

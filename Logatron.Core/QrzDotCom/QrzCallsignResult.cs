namespace Logatron.QrzDotCom
{
    public class QrzCallsignResult
    {
        /// <summary>
        /// An informational message for the user
        /// </summary>
        public string? Message { get; internal set; }

        /// <summary>
        /// Callsign data
        /// </summary>
        public Callsign Callsign { get; }

        public QrzCallsignResult(Callsign callsign)
        {
            Callsign = callsign;
        }
    }
}

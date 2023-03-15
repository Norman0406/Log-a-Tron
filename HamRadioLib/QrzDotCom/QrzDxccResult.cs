namespace HamRadioLib.QrzDotCom
{
    public class QrzDxccResult
    {
        /// <summary>
        /// An informational message for the user
        /// </summary>
        public string? Message { get; internal set; }

        /// <summary>
        /// DXCC data
        /// </summary>
        public Dxcc Dxcc { get; }

        public QrzDxccResult(Dxcc dxcc)
        {
            Dxcc = dxcc;
        }
    }
}

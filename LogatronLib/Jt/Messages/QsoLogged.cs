namespace Logatron.Jt.Messages
{
    public class QsoLogged : JtMessage
    {
        public const uint ID = 5;
        public DateTime TimeUtcOff { get; private set; }
        public string? DxCall { get; private set; }
        public string? DxGrid { get; private set; }
        public ulong TxFrequency { get; private set; }
        public string? Mode { get; private set; }
        public string? ReportSent { get; private set; }
        public string? ReportRecv { get; private set; }
        public string? TxPower { get; private set; }
        public string? Comments { get; private set; }
        public string? Name { get; private set; }
        public DateTime TimeUtcOn { get; private set; }
        public string? OperatorCall { get; private set; }
        public string? MyCall { get; private set; }
        public string? MyGrid { get; private set; }

        // TODO: WSJT-X only

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);

            // TODO

            //TimeUtcOff = bytes.UnpackUInt32(ref offset);
            DxCall = bytes.UnpackString(ref offset);
            DxGrid = bytes.UnpackString(ref offset);
            TxFrequency = bytes.UnpackUInt32(ref offset);
            Mode = bytes.UnpackString(ref offset);
            ReportSent = bytes.UnpackString(ref offset);
            ReportRecv = bytes.UnpackString(ref offset);
            TxPower = bytes.UnpackString(ref offset);
            Comments = bytes.UnpackString(ref offset);
            Name = bytes.UnpackString(ref offset);
            //TimeUtcOn = bytes.UnpackUInt32(ref offset);
            OperatorCall = bytes.UnpackString(ref offset);
            MyCall = bytes.UnpackString(ref offset);
            MyGrid = bytes.UnpackString(ref offset);
        }
    }
}

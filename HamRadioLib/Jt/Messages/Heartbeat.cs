namespace HamRadioLib.Jt.Messages
{
    public class Heartbeat : JtMessage
    {
        public const uint ID = 0;
        public uint MaxSchema { get; private set; }
        public string? Version { get; private set; }
        public string? Revision { get; private set; }

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);

            MaxSchema = bytes.UnpackUInt32(ref offset);
            Version = bytes.UnpackString(ref offset);
            Revision = bytes.UnpackString(ref offset);
        }
    }
}

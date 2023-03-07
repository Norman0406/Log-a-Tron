namespace HamRadioLib.Jt
{
    public abstract class JtMessage
    {
        public uint MagicNumber { get; private set; }
        public uint Schema { get; private set; }
        public uint MessageType { get; private set; }
        public string? ClientId { get; private set; }

        public abstract void FromBytes(byte[] bytes);

        protected void ParseHeader(byte[] bytes, ref int offset)
        {
            MagicNumber = bytes.UnpackUInt32(ref offset);
            Schema = bytes.UnpackUInt32(ref offset);
            MessageType = bytes.UnpackUInt32(ref offset);
            ClientId = bytes.UnpackString(ref offset);
        }
    }
}

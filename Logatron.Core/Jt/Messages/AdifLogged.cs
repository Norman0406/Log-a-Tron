namespace Logatron.Jt.Messages
{
    public class AdifLogged : JtMessage
    {
        public const uint ID = 12;
        public string? Adif { get; private set; }

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);

            Adif = bytes.UnpackString(ref offset);
        }
    }
}

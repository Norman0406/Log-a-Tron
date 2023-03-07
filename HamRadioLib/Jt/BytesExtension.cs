using System.Text;

namespace HamRadioLib.Jt
{
    internal static class BytesExtension
    {
        private static ReadOnlySpan<byte> NormalizeEndianness(byte[] bytes, int offset, int size)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return bytes.AsSpan(offset, size);
            }

            byte[] outBytes = new byte[size];
            Buffer.BlockCopy(bytes, offset, outBytes, 0, size);
            Array.Reverse(outBytes, 0, size);
            return outBytes.AsSpan(0, size);
        }

        public static uint UnpackUInt32(this byte[] bytes, ref int offset)
        {
            int size = sizeof(uint);
            ReadOnlySpan<byte> bytesSpan = NormalizeEndianness(bytes, offset, size);
            uint value = BitConverter.ToUInt32(bytesSpan);
            offset += size;
            return value;
        }

        public static ulong UnpackUInt64(this byte[] bytes, ref int offset)
        {
            int size = sizeof(ulong);
            ReadOnlySpan<byte> bytesSpan = NormalizeEndianness(bytes, offset, size);
            ulong value = BitConverter.ToUInt64(bytesSpan);
            offset += size;
            return value;
        }

        public static bool UnpackBool(this byte[] bytes, ref int offset)
        {
            int size = sizeof(bool);
            ReadOnlySpan<byte> bytesSpan = NormalizeEndianness(bytes, offset, size);
            bool value = BitConverter.ToBoolean(bytesSpan);
            offset += size;
            return value;
        }

        public static int UnpackInt32(this byte[] bytes, ref int offset)
        {
            int size = sizeof(int);
            ReadOnlySpan<byte> bytesSpan = NormalizeEndianness(bytes, offset, size);
            int value = BitConverter.ToInt32(bytesSpan);
            offset += size;
            return value;
        }

        public static double UnpackDouble(this byte[] bytes, ref int offset)
        {
            int size = sizeof(double);
            ReadOnlySpan<byte> bytesSpan = NormalizeEndianness(bytes, offset, size);
            double value = BitConverter.ToDouble(bytesSpan);
            offset += size;
            return value;
        }

        public static uint UnpackUInt8(this byte[] bytes, ref int offset)
        {
            byte value = bytes[offset];
            offset += sizeof(byte);
            return value;
        }

        public static string UnpackString(this byte[] bytes, ref int offset)
        {
            int length = (int)bytes.UnpackUInt32(ref offset);

            if (length < 0)
            {
                return string.Empty;
            }

            string value = Encoding.Default.GetString(bytes, offset, length);
            offset += length;
            return value;
        }
    }
}

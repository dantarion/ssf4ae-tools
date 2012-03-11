using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChallengeModeEditor
{
    public static class Utils
    {
        public static string ReadCString(this BinaryReader reader)
        {
            var sb = new StringBuilder();
            char c = reader.ReadChar();
            while (c != 0)
            {
                sb.Append(c);
                c = reader.ReadChar();
            }
            return sb.ToString();
        }

        public static void ReadConst32(this BinaryReader reader, int constant)
        {
            var actual = reader.ReadInt32();
            if (actual != constant)
            {
                throw new InvalidOperationException("Expecting " + constant + ", actual is " + actual);
            }
        }

        public static void ReadZeroBytes(this BinaryReader reader, int count)
        {
            var bytes = reader.ReadBytes(count);
            for (int i = 0; i < count; i++)
            {
                if (bytes[i] != 0)
                {
                    throw new InvalidOperationException("Non-zero byte at offset " + i + ", value is " + bytes[i]);
                }
            }
        }

        public static void Seek(this BinaryReader reader, long offset, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            reader.BaseStream.Seek(offset, seekOrigin);
        }

        public static string ReadCharArrayString(this BinaryReader reader, int count)
        {
            return new string(reader.ReadChars(count)).TrimEnd('\0');
        }

        public static void WriteCharArrayString(this BinaryWriter writer, string str, int length)
        {
            if (str == null) { str = string.Empty; }
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = i < str.Length ? str[i] : (char)0;
            }

            writer.Write(chars);
        }

        public static void WriteZeroBytes(this BinaryWriter writer, int count)
        {
            writer.Write(new byte[count]);
        }

        public static void WriteInt32(this BinaryWriter writer, int value)
        {
            writer.Write(value);
        }
    }
}

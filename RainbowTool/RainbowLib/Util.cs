using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
namespace RainbowLib
{
    [Flags]
    public enum Input : ushort
    {
        NEUTRAL = 0x1,
        UP = 0x2,
        DOWN = 0x4,
        BACK = 0x8,
        FORWARD = 0x10,
        LP = 0x40,
        MP = 0x80,
        HP = 0x100,
        LK = 0x200,
        MK = 0x400,
        HK = 0x800
    }
    public static class Cloner
    {
        public static T Clone<T>(T source)
        {
            var clonable = source as ICloneable;
            if (clonable != null)
            {
                return (T)clonable.Clone();
            }
            else
            {
                return ShallowCopy(source);
            }
        }

        public static T ShallowCopy<T>(T source)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Seek(0);

                T clone = (T)formatter.Deserialize(stream);
                foreach (PropertyInfo pinfo in source.GetType().GetProperties())
                {
                    if (!pinfo.PropertyType.IsClass || !pinfo.CanWrite ||
                        pinfo.IsDefined(typeof(SerializableAttribute), true))
                        continue;
                    var val = pinfo.GetValue(source, null);
                    pinfo.SetValue(clone, val, null);
                }

                return clone;
            }
        }
    }
    public static class Util
    {
        public static void writeStringTable(BinaryWriter outFile, int OffsetBase, List<String> strings)
        {
            for (int i = 0; i < strings.Count; i++)
            {
                outFile.Seek(0, SeekOrigin.End);
                int off = 0;
                if (strings[i] != null)
                {
                    off = (int)outFile.BaseStream.Position;
                    outFile.Write(strings[i].ToCharArray());
                    outFile.Write(false);
                }
                outFile.Seek(OffsetBase + i * 4, SeekOrigin.Begin);
                outFile.Write(off);
            }
            outFile.Seek(0, SeekOrigin.End);
        }
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
        public static void Seek(this Stream str, long off)
        {
            str.Seek(off, SeekOrigin.Begin);
        }

        public static void LogUnkEnumFlags(Enum en, string parent, string Name, int Index = -1)
        {
            if (Char.IsDigit(en.ToString()[0])
                        && en.ToString()[0] != '0')
            {
                AELogger.Log("undefined " + en.GetType().Name + " enum flag value: "
                    + en + " in " + parent + " named " + Name +
                    (Index == -1 ? "" : (" at index " + Index)),
                    false
                    );
            }
        }

        public static void LogUnkEnum(Enum en, string parent, string Name, int Index = -1)
        {
            if (!Enum.IsDefined(en.GetType(), en))
            {
                AELogger.Log("undefined " + en.GetType().Name + " enum value: "
                    + en + " in  "+ parent +" named " + Name +
                    (Index == -1 ? "" : (" at index " + Index)),
                    false
                    );
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
namespace RainbowLib.BAC
{
    [Serializable]
    public class BaseCommand : INotifyPropertyChanged, ICloneable
    {
        public BaseCommand()
        {

        }
        private ushort _StartFrame;
        public ushort StartFrame
        {
            get { return _StartFrame; }
            set
            {
                _StartFrame = value;
                OnPropertyChanged("StartFrame");
            }
        }
        private ushort _EndFrame;
        public ushort EndFrame
        {
            get { return _EndFrame; }
            set
            {
                _EndFrame = value;
                OnPropertyChanged("EndFrame");
            }
        }
        public String RawString
        {
            get
            {
                if (Raw == null)
                    return null;
                var sb = new StringBuilder();
                int i = 0;
                foreach (byte b in Raw)
                {
                    i++;
                    sb.AppendFormat("{0:X02}", b);
                    if (i % 8 == 0)
                        sb.Append("\n");
                }
                return sb.ToString();
            }
            set
            {
                if (value == null)
                    return;
                value = value.Replace("\n", "");
                var arr = new byte[value.Length / 2];
                try
                {

                    for (int i = 0; i < value.Length; i += 2)
                    {
                        arr[i/2] = byte.Parse(value.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (Exception)
                {
                    throw new FormatException();
                }
                Raw = arr;
            }
        }
        private byte[] _Raw;
        public byte[] Raw
        {
            get { return _Raw; }
            set
            {
                _Raw = value;
                OnPropertyChanged("Raw");
                OnPropertyChanged("RawString");
            }
        }

        public virtual object Clone()
        {
            return Cloner.ShallowCopy(this);
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
         
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
    }
}

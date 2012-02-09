using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class EtcCommand : BaseCommand
    {
        public enum EtcCommandType : ushort
        {
            CONTROL = 0,
            GFX = 5,
            GFX2 = 6,
        }
        [Flags]
        public enum EtcControlType : short
        {
            UNKNOWN = 0,
            VFX_SCRIPT = 2,
            SUPERFLASH = 5
        }
        private EtcCommandType _Type;
        public EtcCommandType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        private ushort _ShortParam;
        public ushort ShortParam
        {
            get { return _ShortParam; }
            set
            {
                _ShortParam = value;
                OnPropertyChanged("ShortParam");
            }
        }
        #region propgen
        public int Unknown00
        {
            get { return _RawParams[00]; }
            set
            {
                _RawParams[00] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown01
        {
            get { return _RawParams[01]; }
            set
            {
                _RawParams[01] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown02
        {
            get { return _RawParams[02]; }
            set
            {
                _RawParams[02] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown03
        {
            get { return _RawParams[03]; }
            set
            {
                _RawParams[03] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown04
        {
            get { return _RawParams[04]; }
            set
            {
                _RawParams[04] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown05
        {
            get { return _RawParams[05]; }
            set
            {
                _RawParams[05] = value;
                OnPropertyChanged("RawParams");
            }
        }
        public int Unknown06
        {
            get { return _RawParams[06]; }
            set
            {
                _RawParams[06] = value;
                OnPropertyChanged("RawParams");
            }
        }
        #endregion
        private int[] _RawParams;
        public int[] RawParams
        {
            get { return _RawParams; }
            set
            {
                _RawParams = value;
                OnPropertyChanged("RawParams");
            }
        }


    }
}

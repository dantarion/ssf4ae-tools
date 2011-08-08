using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class StateCommand : BaseCommand
    {
        [Flags]
        public enum StateFlags : uint
        {
            CROUCHING =  0x02,
            AIRBOURNE =  0x04,
            COUNTERHIT = 0x10,
        }
        private StateFlags _Flags;
        public StateFlags Flags
        {
            get { return _Flags; }
            set
            {
                _Flags = value;
                OnPropertyChanged("Flags");
            }
        }
        private uint _UnknownFlags2;
        public uint UnknownFlags2
        {
            get { return _UnknownFlags2; }
            set
            {
                _UnknownFlags2 = value;
                OnPropertyChanged("UnknownFlags2");
            }
        }
        
        
    }
}

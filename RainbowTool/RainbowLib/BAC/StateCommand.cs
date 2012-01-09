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
            _1 = 0x01,
            CROUCHING =  0x02,
            AIRBOURNE =  0x04,
            _8 = 0x08,
            COUNTERHIT = 0x10,
            _20 = 0x20,
            _40 = 0x40,
            _80 = 0x80
        }
        private StateFlags _Flags;
        public StateFlags Flags
        {
            get { return _Flags; }
            set
            {
                _Flags = value;
                /*if(!StateFlags.IsDefined(typeof(StateFlags),_Flags))
                {
                    AELogger.Log("undefinied stateflags enum value: " + _Flags );
                }*/
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

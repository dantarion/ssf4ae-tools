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
            NONE                = 0,
            STAND               = 0x01,
            CROUCH              =  0x02,
            AIR                 =  0x04,
            // DOWN                = 0x08, not used
            COUNTERHIT          = 0x10,
            // BEHIND              = 0x20, not used
            FORCE_TURN          = 0x40,
            CAN_TURN            = 0x80,
            FLIP                = 0x100,
            KEEP_CMD_SIDE       = 0x200,
            HOVERING            = 0x400,
            IMMOVABLE_MYSELF    = 0x800,
            IMMOVABLE_TARGET    = 0x1000,
            INVIS_MYSELF        = 0x2000,
            INVIS_TARGET        = 0x4000,
            //IGNORE_VEL_X        = 0x8000,
            //IGNORE_VEL_Y        = 0x10000,
            //IGNORE_VEL_Z        = 0x20000,
            //IGNORE_ACC_X        = 0x40000,
            //IGNORE_ACC_Y        = 0x80000,
            //IGNORE_ACC_Z        = 0x100000,
            IGNORE_PHYSICS_YZ = 0x1F0000,
            IGNORE_PHYSICS_XYZ = 0x1F8000,
            IGNORE_COUNTER_ATK = 0x200000
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

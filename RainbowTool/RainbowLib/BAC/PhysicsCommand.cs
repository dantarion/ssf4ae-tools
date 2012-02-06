using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class PhysicsCommand : BaseCommand
    {
        private float _XVel;
        public float XVel
        {
            get { return _XVel; }
            set
            {
                _XVel = value;
                OnPropertyChanged("XVel");
            }
        }
        private float _YVel;
        public float YVel
        {
            get { return _YVel; }
            set
            {
                _YVel = value;
                OnPropertyChanged("YVel");
            }
        }

        private uint _Unk01;
        public uint Unk01
        {
            get { return _Unk01; }
            set
            {
                _Unk01 = value;
                OnPropertyChanged("Unk01");
            }
        }
        [Flags]
        public enum PFlags : uint
        {
            XVEL = 0x01,
            YVEL = 0x10,
            XACCEL = 0x10000,
            YACCEL = 0x100000,
        }
        private PFlags _PhysicsFlags;
        public PFlags PhysicsFlags
        {
            get { return _PhysicsFlags; }
            set
            {
                _PhysicsFlags = value;
                OnPropertyChanged("PhysicsFlags");
            }
        }
        private float _XAccel;
        public float XAccel
        {
            get { return _XAccel; }
            set
            {
                _XAccel = value;
                OnPropertyChanged("XAccel");
            }
        }

        private float _YAccel;
        public float YAccel
        {
            get { return _YAccel; }
            set
            {
                _YAccel = value;
                OnPropertyChanged("YAccel");
            }
        }

        private ulong _Unk02;
        public ulong Unk02
        {
            get { return _Unk02; }
            set
            {
                _Unk02 = value;
                OnPropertyChanged("Unk02");
            }
        }
        /*
         * 00000000C3F5283E
            0000000010001000
            000000000AD723BC
            0000000000000000
        */
    }
}

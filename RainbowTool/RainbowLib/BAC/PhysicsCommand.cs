using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class PhysicsCommand : BaseCommand
    {
        private float velX;
        public float VelX
        {
            get { return this.velX; }
            set
            {
                this.velX = value;
                OnPropertyChanged("VelX");
            }
        }
        private float velY;
        public float VelY
        {
            get { return this.velY; }
            set
            {
                this.velY = value;
                OnPropertyChanged("VelY");
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
            //todo: nibble SET NEG Z = 7, SET NEG Y = 6 , SET NEG X= 5 SET SIGN = 4, ADD = 3, SCALE = 2, SET = 1
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
        private float accX;
        public float AccX
        {
            get { return this.accX; }
            set
            {
                this.accX = value;
                OnPropertyChanged("AccX");
            }
        }

        private float accY;
        public float AccY
        {
            get { return this.accY; }
            set
            {
                this.accY = value;
                OnPropertyChanged("AccY");
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

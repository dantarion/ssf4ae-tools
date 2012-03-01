using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class InvincCommand : BaseCommand
    {
        [Flags]
        public enum InvincFlags : uint
        {
            NONE = 0,
            PUSH = 0x01,
            HIT = 0x02,
            THROW = 0x04,
            PROJECTILE = 0x08,
            ABSORB_HIT = 0x10,
            ABSORB_THROW = 0x20,
            ABSORB_PROJECTILE = 0x40,
            COUNTER_HIT = 0x80,
            COUNTER_SP_MOVE = 0x200
        }

        private InvincFlags _Flags;
        public InvincFlags Flags
        {
            get { return this._Flags; }
            set
            {
                this._Flags = value;
                OnPropertyChanged("Flags");
            }
        }

        [Flags]
        public enum BodyParts : uint
        {
            NONE = 0,
            WAIST = 0x1,
            STOMACH = 0x2,
            CHEST = 0x4,
            HEAD = 0x8,
            L_SHOULDER = 0x10,
            L_ELBOW = 0x20,
            L_WRIST = 0x40,
            L_HAND = 0x80,
            L_HIP = 0x100,
            L_KNEE = 0x200,
            L_ANKLE = 0x400,
            L_FOOT = 0x800,
            R_SHOULDER = 0x1000,
            R_ELBOW = 0x2000,
            R_WRIST = 0x4000,
            R_HAND = 0x8000,
            R_HIP = 0x10000,
            R_KNEE = 0x20000,
            R_ANKLE = 0x40000,
            R_FOOT = 0x80000,
            L_ARM = 0x100000,
            L_LEG = 0x200000,
            R_ARM = 0x400000,
            R_LEG = 0x800000,
            L_ARM_ALL = 0x1000F0,
            L_LEG_ALL = 0x200F00,
            R_ARM_ALL = 0x40F000,
            R_LEG_ALL = 0x8F0000,
        }

        private BodyParts _Location;
        public BodyParts Location
        {
            get { return this._Location; }
            set
            {
                this._Location = value;
                OnPropertyChanged("Location");
            }
        }
        
        private UInt16 _Unk02;
        public UInt16 Unk02
        {
            get { return _Unk02; }
            set
            {
                _Unk02 = value;
                OnPropertyChanged("Unk02");
            }
        }

        private ushort _Unk03;
        public ushort Unk03
        {
            get { return _Unk03; }
            set
            {
                _Unk03 = value;
                OnPropertyChanged("Unk03");
            }
        }
        private ushort _Unk04;
        public ushort Unk04
        {
            get { return _Unk04; }
            set
            {
                _Unk04 = value;
                OnPropertyChanged("Unk04");
            }
        }
        private ushort _Unk05;
        public ushort Unk05
        {
            get { return _Unk05; }
            set
            {
                _Unk05 = value;
                OnPropertyChanged("Unk05");
            }
        }
    }
}

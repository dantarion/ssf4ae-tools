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

        private UInt16 _Unk01;
        public UInt16 Unk01
        {
            get { return _Unk01; }
            set
            {
                _Unk01 = value;
                OnPropertyChanged("Unk01");
            }
        }
        
        private UInt32 _Unk02;
        public UInt32 Unk02
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

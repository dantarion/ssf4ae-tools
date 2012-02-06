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
        public enum InFlags : ushort
        {
            PUSH = 0x01,
            HIT = 0x02,
            THROW = 0x04,
            PROJECTILE = 0x08,
            ARMOR1 = 0x10,
            ARMOR2 = 0x40
        }
        private InFlags _InvincFlags;
        public InFlags InvincFlags
        {
            get { return _InvincFlags; }
            set
            {
                _InvincFlags = value;
                OnPropertyChanged("InvincFlags");
            }
        }
        private ushort _Unk01;
        public ushort Unk01
        {
            get { return _Unk01; }
            set
            {
                _Unk01 = value;
                OnPropertyChanged("Unk01");
            }
        }
        private ushort _Location;
        public ushort Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("Location");
            }
        }
        private ushort _Unk02;
        public ushort Unk02
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
        private ushort _Unk06;
        public ushort Unk06
        {
            get { return _Unk06; }
            set
            {
                _Unk06 = value;
                OnPropertyChanged("Unk06");
            }
        }
    }
}

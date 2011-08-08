using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace RainbowLib.BAC
{
    [Serializable]
    public class HitBoxData : INotifyPropertyChanged
    {
        static string[] names = {"HIT_STANDING","HIT_CROUCHING","HIT_AIR",
        "BLOCK_STANDING","BLOCK_CROUCHING","BLOCK_AIR?",
        "COUNTER_STANDING","COUNTER_CROUCHING","COUNTER_AIR",
        "UNKNOWN_STANDING","UNKNOWN_CROUCHING","UNKNOWN_AIR"};
        public HitBoxData(int index)
        {
            Name = names[index];
        }
        public string Name { get; private set; }
        private short _Damage;
        public short Damage
        {
            get { return _Damage; }
            set
            {
                _Damage = value;
                OnPropertyChanged("Damage");
            }
        }
        private short _Stun;
        public short Stun
        {
            get { return _Stun; }
            set
            {
                _Stun = value;
                OnPropertyChanged("Stun");
            }
        }
        [Flags]
        public enum HitBoxEffect : ushort
        {
            NORMAL = 0,
            RUN_SCRIPT = 1,
            NO_REACTION = 2,
            JUGGLE = 4,

        }
        private HitBoxEffect _Effect;
        public HitBoxEffect Effect
        {
            get { return _Effect; }
            set
            {
                _Effect = value;
                OnPropertyChanged("Effect");
            }
        }
        [NonSerialized]
        private Reference<Script> _OnHit;
        public Script OnHit
        {
            get { return _OnHit; }
            set
            {
                _OnHit = value;
                OnPropertyChanged("OnHit");
            }
        }
        private ushort _AttackerHitstop;
        public ushort AttackerHitstop
        {
            get { return _AttackerHitstop; }
            set
            {
                _AttackerHitstop = value;
                OnPropertyChanged("AttackerHitstop");
            }
        }
        private ushort _AttackerShaking;
        public ushort AttackerShaking
        {
            get { return _AttackerShaking; }
            set
            {
                _AttackerShaking = value;
                OnPropertyChanged("AttackerShaking");
            }
        }
        private ushort _VictimHitstop;
        public ushort VictimHitstop
        {
            get { return _VictimHitstop; }
            set
            {
                _VictimHitstop = value;
                OnPropertyChanged("VictimHitstop");
            }
        }
        private ushort _VictimShaking;
        public ushort VictimShaking
        {
            get { return _VictimShaking; }
            set
            {
                _VictimShaking = value;
                OnPropertyChanged("VictimShaking");
            }
        }
        private uint _HitGFX;
        public uint HitGFX
        {
            get { return _HitGFX; }
            set
            {
                _HitGFX = value;
                OnPropertyChanged("HitGFX");
            }
        }
        private short _Unknown1;
        public short Unknown1
        {
            get { return _Unknown1; }
            set
            {
                _Unknown1 = value;
                OnPropertyChanged("Unknown1");
            }
        }
        private short _Unknown2_1;
        public short Unknown2_1
        {
            get { return _Unknown2_1; }
            set
            {
                _Unknown2_1 = value;
                OnPropertyChanged("Unknown2_1");
            }
        }
        private short _Unknown2_2;
        public short Unknown2_2
        {
            get { return _Unknown2_2; }
            set
            {
                _Unknown2_2 = value;
                OnPropertyChanged("Unknown2_2");
            }
        }
        private ushort _HitSFX;
        public ushort HitSFX
        {
            get { return _HitSFX; }
            set
            {
                _HitSFX = value;
                OnPropertyChanged("HitSFX");
            }
        }
        private ushort _HitSFX2;
        public ushort HitSFX2
        {
            get { return _HitSFX2; }
            set
            {
                _HitSFX2 = value;
                OnPropertyChanged("HitSFX2");
            }
        }
        private ushort _VictimSFX;
        public ushort VictimSFX
        {
            get { return _VictimSFX; }
            set
            {
                _VictimSFX = value;
                OnPropertyChanged("VictimSFX");
            }
        }
        private ushort _Unknown3;
        public ushort Unknown3
        {
            get { return _Unknown3; }
            set
            {
                _Unknown3 = value;
                OnPropertyChanged("Unknown3");
            }
        }
        private short _Unknown4;
        public short Unknown4
        {
            get { return _Unknown4; }
            set
            {
                _Unknown4 = value;
                OnPropertyChanged("Unknown4");
            }
        }
        private short _Unknown5;
        public short Unknown5
        {
            get { return _Unknown5; }
            set
            {
                _Unknown5 = value;
                OnPropertyChanged("Unknown5");
            }
        }
        private ushort _Unknown6;
        public ushort Unknown6
        {
            get { return _Unknown6; }
            set
            {
                _Unknown6 = value;
                OnPropertyChanged("Unknown6");
            }
        }
        private ushort _Unknown7;
        public ushort Unknown7
        {
            get { return _Unknown7; }
            set
            {
                _Unknown7 = value;
                OnPropertyChanged("Unknown7");
            }
        }
        private ushort _Unknown8;
        public ushort Unknown8
        {
            get { return _Unknown8; }
            set
            {
                _Unknown8 = value;
                OnPropertyChanged("Unknown8");
            }
        }
        private short _Unknown9;
        public short Unknown9
        {
            get { return _Unknown9; }
            set
            {
                _Unknown9 = value;
                OnPropertyChanged("Unknown9");
            }
        }
        private short _Unknown10;
        public short Unknown10
        {
            get { return _Unknown10; }
            set
            {
                _Unknown10 = value;
                OnPropertyChanged("Unknown10");
            }
        }
        private float _ForceUnknown2;
        public float ForceUnknown2
        {
            get { return _ForceUnknown2; }
            set
            {
                _ForceUnknown2 = value;
                OnPropertyChanged("ForceUnknown2");
            }
        }
        private float _ForceX;
        public float ForceX
        {
            get { return _ForceX; }
            set
            {
                _ForceX = value;
                OnPropertyChanged("ForceX");
            }
        }
        private float _ForceY;
        public float ForceY
        {
            get { return _ForceY; }
            set
            {
                _ForceY = value;
                OnPropertyChanged("ForceY");
            }
        }

        private float _ForceUnknown3;
        public float ForceUnknown3
        {
            get { return _ForceUnknown3; }
            set
            {
                _ForceUnknown3 = value;
                OnPropertyChanged("ForceUnknown3");
            }
        }
        private float _ForceXAcceleration;
        public float ForceXAcceleration
        {
            get { return _ForceXAcceleration; }
            set
            {
                _ForceXAcceleration = value;
                OnPropertyChanged("ForceXAcceleration");
            }
        }
        private float _ForceYAcceleration;
        public float ForceYAcceleration
        {
            get { return _ForceYAcceleration; }
            set
            {
                _ForceYAcceleration = value;
                OnPropertyChanged("ForceYAcceleration");
            }
        }
        private float _ForceUnknown4;
        public float ForceUnknown4
        {
            get { return _ForceUnknown4; }
            set
            {
                _ForceUnknown4 = value;
                OnPropertyChanged("ForceUnknown4");
            }
        }
        private float _ForceUnknown5;
        public float ForceUnknown5
        {
            get { return _ForceUnknown5; }
            set
            {
                _ForceUnknown5 = value;
                OnPropertyChanged("ForceUnknown5");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

    }
    public class HitBoxDataset : INotifyPropertyChanged
    {
        public int Index { get; private set; }
        public override string ToString()
        {
            if (Usage.Count > 0)
                //return "#" + Index.ToString() + " - (" + Usage.Count + ")";
                return "#" + Index.ToString() + " - "+UsageString;
            return "#" + Index.ToString();
        }
        public HitBoxDataset(int index)
        {
            Index = index;
        }
        public string UsageString
        {
            get
            {
                var sb = new StringBuilder();
                foreach (Script script in Usage)
                    sb.Append(script.Name + ", ");
                return sb.ToString();
            }
        }
        [NonSerialized]
        private ObservableCollection<Reference<Script>> _Usage = new ObservableCollection<Reference<Script>>();
        public ObservableCollection<Reference<Script>> Usage
        {
            get { return _Usage; }
        }

        private ObservableCollection<HitBoxData> _Data = new ObservableCollection<HitBoxData>();
        public ObservableCollection<HitBoxData> Data
        {
            get { return _Data; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
    }
}

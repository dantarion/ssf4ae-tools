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
        public enum HitBoxEffect : ushort
        {
            HIT = 0,
            SCRIPT = 1,
            BLOCK = 2,
            BLOCK2 = 3,
            BLOW = 4,
            BLOW2 = 5,
            BOUND = 6,
            BOUND2 = 7
        }
        public static int getIndexOffset(HitBoxEffect effect)
        {
            if (effect == HitBoxEffect.HIT)
                return 0x83;
            if (effect == HitBoxEffect.BLOCK)
                return 0x44;
            if (effect == HitBoxEffect.BLOW)
                return 0xC0;
            if (effect == HitBoxEffect.BOUND)
                return 0x1A;
            return 0;
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
            get { return this._AttackerHitstop; }
            set
            {
                this._AttackerHitstop = value;
                OnPropertyChanged("AttackerHitstop");
            }
        }
        private ushort _AttackerShaking;
        public ushort AttackerShaking
        {
            get { return this._AttackerShaking; }
            set
            {
                this._AttackerShaking = value;
                OnPropertyChanged("AttackerShaking");
            }
        }
        private ushort _VictimHitstop;
        public ushort VictimHitstop
        {
            get { return this._VictimHitstop; }
            set
            {
                this._VictimHitstop = value;
                OnPropertyChanged("VictimHitstop");
            }
        }
        private ushort _VictimShaking;
        public ushort VictimShaking
        {
            get { return this._VictimShaking; }
            set
            {
                this._VictimShaking = value;
                OnPropertyChanged("VictimShaking");
            }
        }
        private short _HitGFX;
        public short HitGFX
        {
            get { return _HitGFX; }
            set
            {
                _HitGFX = value;
                OnPropertyChanged("HitGFX");
            }
        }
        private int _Unknown1;
        public int Unknown1
        {
            get { return _Unknown1; }
            set
            {
                _Unknown1 = value;
                OnPropertyChanged("Unknown1");
            }
        }
        private Unused16 _Unused;
        public Unused16 Unused
        {
            get { return _Unused; }
            set
            {
                _Unused = value;
                OnPropertyChanged("Unused");
            }
        }
        private short _HitGFX2;
        public short HitGFX2
        {
            get { return this._HitGFX2; }
            set
            {
                this._HitGFX2 = value;
                OnPropertyChanged("HitGFX2");
            }
        }
        private Unused32 _Unused2;
        public Unused32 Unused2
        {
            get { return _Unused2; }
            set
            {
                _Unused2 = value;
                OnPropertyChanged("Unused2");
            }
        }
        private Unused16 _Unused3;
        public Unused16 Unused3
        {
            get { return _Unused3; }
            set
            {
                _Unused3 = value;
                OnPropertyChanged("Unused2");
            }
        }
        private short _HitSFX;
        public short HitSFX
        {
            get { return _HitSFX; }
            set
            {
                _HitSFX = value;
                OnPropertyChanged("HitSFX");
            }
        }
        private short _HitSFX2;
        public short HitSFX2
        {
            get { return _HitSFX2; }
            set
            {
                _HitSFX2 = value;
                OnPropertyChanged("HitSFX2");
            }
        }
        private short _VictimSFX;
        public short VictimSFX
        {
            get { return _VictimSFX; }
            set
            {
                _VictimSFX = value;
                OnPropertyChanged("VictimSFX");
            }
        }

        private ushort _ArcadeScore;
        public ushort ArcadeScore
        {
            get { return _ArcadeScore; }
            set
            {
                _ArcadeScore = value;
                OnPropertyChanged("ArcadeScore");
            }
        }
        private short _AtkMeter;
        public short AtkMeter
        {
            get { return _AtkMeter; }
            set
            {
                _AtkMeter = value;
                OnPropertyChanged("AtkMeter");
            }
        }
        private short _VctmMeter;
        public short VctmMeter
        {
            get { return _VctmMeter; }
            set
            {
                _VctmMeter = value;
                OnPropertyChanged("VctmMeter");
            }
        }
        private short _JuggleStart;
        public short JuggleStart
        {
            get { return _JuggleStart; }
            set
            {
                _JuggleStart = value;
                OnPropertyChanged("JuggleStart");
            }
        }
        private short _AnimTime;
        public short AnimTime
        {
            get { return _AnimTime; }
            set
            {
                _AnimTime = value;
                OnPropertyChanged("AnimTime");
            }
        }

        [Flags]
        public enum MiscFlags : int
        {
            NONE = 0,
            DONT_KO = 1,
            ARMOR_DMG = 2,
            TECHABLE = 4,
            UNTECHABLE = 8
        }

        private MiscFlags _MiscFlag;
        public MiscFlags MiscFlag
        {
            get { return _MiscFlag; }
            set
            {
                _MiscFlag = value;
                OnPropertyChanged("MiscFlag");
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
         [field: NonSerialized]
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
                var dict = new Dictionary<Script, int>();
                foreach (var script in Usage)
                {
                    if (!dict.ContainsKey(script))
                    {
                        dict.Add(script, 0);
                    }

                    dict[script] = dict[script] + 1;
                }

                var strings = from kvp in dict
                              select kvp.Value > 1 ? kvp.Key.Name + "*" + kvp.Value : kvp.Key.Name;

                return string.Join(", ", strings);
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
         [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
    }
}

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
            switch (effect)
            {
                case HitBoxEffect.HIT:
                    return 0x83;

                case HitBoxEffect.BLOCK:
                    return 0x44;

                case HitBoxEffect.BLOW:
                    return 0xC0;

                case HitBoxEffect.BOUND:
                    return 0x1A;

                default:
                    return 0;
            }
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
        private ushort selfHitstop;
        public ushort SelfHitstop
        {
            get { return this.selfHitstop; }
            set
            {
                this.selfHitstop = value;
                OnPropertyChanged("SelfHitstop");
            }
        }
        private ushort selfShaking;
        public ushort SelfShaking
        {
            get { return this.selfShaking; }
            set
            {
                this.selfShaking = value;
                OnPropertyChanged("SelfShaking");
            }
        }
        private ushort tgtHitstop;
        public ushort TgtHitstop
        {
            get { return this.tgtHitstop; }
            set
            {
                this.tgtHitstop = value;
                OnPropertyChanged("TgtHitstop");
            }
        }
        private ushort tgtShaking;
        public ushort TgtShaking
        {
            get { return this.tgtShaking; }
            set
            {
                this.tgtShaking = value;
                OnPropertyChanged("TgtShaking");
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
        private short tgtSfx;
        public short TgtSFX
        {
            get { return this.tgtSfx; }
            set
            {
                this.tgtSfx = value;
                OnPropertyChanged("TgtSFX");
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
        private short selfMeter;
        public short SelfMeter
        {
            get { return this.selfMeter; }
            set
            {
                this.selfMeter = value;
                OnPropertyChanged("SelfMeter");
            }
        }
        private short tgtMeter;
        public short TgtMeter
        {
            get { return this.tgtMeter; }
            set
            {
                this.tgtMeter = value;
                OnPropertyChanged("TgtMeter");
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
        private short tgtAnimTime;
        public short TgtAnimTime
        {
            get { return this.tgtAnimTime; }
            set
            {
                this.tgtAnimTime = value;
                OnPropertyChanged("TgtAnimTime");
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

        private float velZ;
        public float VelZ
        {
            get { return this.velZ; }
            set
            {
                this.velZ = value;
                OnPropertyChanged("VelZ");
            }
        }
        private float pushbackDist;
        public float PushbackDist
        {
            get { return this.pushbackDist; }
            set
            {
                this.pushbackDist = value;
                OnPropertyChanged("PushbackDist");
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
        private float accZ;
        public float AccZ
        {
            get { return this.accZ; }
            set
            {
                this.accZ = value;
                OnPropertyChanged("AccZ");
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
                return Index.ToString() + " - "+UsageString;
            return Index.ToString();
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

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
            HIT2 = 3,
            KNOCKDOWN = 4,
            KNOCKDOWN2 = 5,
            KNOCKDOWN3 = 6
        }
        public static int getIndexOffset(HitBoxEffect effect)
        {
            if (effect == HitBoxEffect.HIT)
                return 0x83;
            if (effect == HitBoxEffect.BLOCK)
                return 0x44;
            if (effect == HitBoxEffect.KNOCKDOWN)
                return 0xC0;
            if (effect == HitBoxEffect.KNOCKDOWN3)
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
        private ushort _HitSound;
        public ushort HitSound
        {
            get { return _HitSound; }
            set
            {
                _HitSound = value;
                OnPropertyChanged("HitSound");
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
        private short _PainSound;
        public short PainSound
        {
            get { return _PainSound; }
            set
            {
                _PainSound = value;
                OnPropertyChanged("PainSound");
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
        private ushort _AtkMeter;
        public ushort AtkMeter
        {
            get { return _AtkMeter; }
            set
            {
                _AtkMeter = value;
                OnPropertyChanged("AtkMeter");
            }
        }
        private ushort _VctmMeter;
        public ushort VctmMeter
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

        private int _ForceUnknown2;
        public int ForceUnknown2
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

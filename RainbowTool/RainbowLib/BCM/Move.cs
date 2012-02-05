using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace RainbowLib.BCM
{
    public enum PositionRestriction : ushort
    {
        NONE = 0,
        MUST_BE_FAR = 1,
        MUST_BE_CLOSE = 2,
        MUST_BE_HIGH = 3,
        MUST_BE_LOW = 4
    }
    [Flags]
    public enum MoveRestriction : ushort
    {
        NONE = 0,
        PROJECTILE = 0x1,
        KNIFE = 0x04,
        NO_KNIFE = 0x08,
        SPECIAL = 0x40,//This is for Juri's U1, Guile Shades, Gen Stance
        ULTRA = 0x80,
    }
    [Flags]
    public enum MoveFlags : ushort
    {
        LAZY_STICK = 0x1,
        STRICT_STICK = 0x2,
        BUTTONS = 0x10,
        ALL_BUTTONS = 0x20,
        ANY_TWO = 0x40,
        STICK = 0x400,
        ON_PRESS = 0x1000,
        ON_RELEASE = 0x2000
    }
    [Flags]
    public enum StateRestriction : uint
    {
        NONE = 0,
        AIR = 4
    }

    [Flags]
    public enum MoveAttributeFlags : uint
    {
        NONE = 0x0,
        LP = 0x1,
        MP = 0x2,
        HP = 0x4,
        LK = 0x8,
        MK = 0x10,
        HK = 0x20,
        THROW = 0x40,
        SAVING = 0x80,
        APPEAL = 0x100,
        SPECIAL = 0x200,
        SUPER = 0x400,
        ULTRA = 0x800,
        EX = 0x1000,
        TARGET_COMBO = 0x2000,
        UNUSED1 = 0x4000,
        UNUSED2 = 0x8000,
        HIGH_JUMP = 0x10000,
        UNUSED3 = 0x20000,
        UNUSED4 = 0x40000,
        DASH = 0x80000,
        BACK_DASH = 0x100000,
    }

    [Serializable]
    public class Move : INotifyPropertyChanged
    {
        public static Move NULL = new Move("NULL");
        public Move()
            : this("NEW")
        {
        }
        public Move(string name = null)
        {
            Name = name;
            InputMotion = InputMotion.NONE;
            Script = BAC.Script.NullScript;
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private Input _input;

        public Input Input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged("Input");
            }
        }
        private MoveFlags _moveflags;

        public MoveFlags MoveFlags
        {
            get { return _moveflags; }
            set
            {
                _moveflags = value;
                OnPropertyChanged("MoveFlags");
            }
        }
        private MoveRestriction _Restriction;
        public MoveRestriction Restriction
        {
            get { return _Restriction; }
            set
            {
                _Restriction = value;
                OnPropertyChanged("Restriction");
            }
        }

        private PositionRestriction _PositionRestriction;
        public PositionRestriction PositionRestriction
        {
            get { return _PositionRestriction; }
            set
            {
                _PositionRestriction = value;
                OnPropertyChanged("PositionRestriction");
            }
        }
        private float _PositionRestrictionDistance;
        public float PositionRestrictionDistance
        {
            get { return _PositionRestrictionDistance; }
            set
            {
                _PositionRestrictionDistance = value;
                OnPropertyChanged("PositionRestrictionDistance");
            }
        }
        private short _EXRequirement;
        public short EXRequirement
        {
            get { return _EXRequirement; }
            set
            {
                _EXRequirement = value;
                OnPropertyChanged("EXRequirement");
            }
        }
        private short _EXCost;
        public short EXCost
        {
            get { return _EXCost; }
            set
            {
                _EXCost = value;
                OnPropertyChanged("EXCost");
            }
        }
        private short _UltraRequirement;
        public short UltraRequirement
        {
            get { return _UltraRequirement; }
            set
            {
                _UltraRequirement = value;
                OnPropertyChanged("UltraRequirement");
            }
        }
        private short _UltraCost;
        public short UltraCost
        {
            get { return _UltraCost; }
            set
            {
                _UltraCost = value;
                OnPropertyChanged("UltraCost");
            }
        }
        private Reference<InputMotion> _InputMotion;
        public InputMotion InputMotion
        {
            get { return _InputMotion; }
            set
            {
                _InputMotion = value;
                OnPropertyChanged("InputMotion");
            }
        }
        public enum MoveStateRestriction : uint
        {
            THROW = 0x10000,
            GROUND_NORMAL = 0x30000,
            AIR_NORMAL = 0x40000,
            SPECIAL = 0x70000,
        }
        private MoveStateRestriction _StateRestriction;
        public MoveStateRestriction StateRestriction
        {
            get { return _StateRestriction; }
            set
            {
                _StateRestriction = value;
                OnPropertyChanged("StateRestriction");
            }
        }
        public enum MoveUltraRestriction : ulong
        {
            NONE = 0,
            ULTRA2 =        0x0000000100000000,
            ANGRY_SCAR =    0x0000000001000000
        }
        private MoveUltraRestriction _UltraRestriction;
        public MoveUltraRestriction UltraRestriction
        {
            get { return _UltraRestriction; }
            set
            {
                _UltraRestriction = value;
                OnPropertyChanged("UltraRestriction");
            }
        }
        [NonSerialized]
        private int _ScriptIndex;
        public int ScriptIndex
        {
            get { return _ScriptIndex; }
            set
            {
                _ScriptIndex = value;
                OnPropertyChanged("ScriptIndex");
            }
        }
        private Reference<BAC.Script> _Script;
        public BAC.Script Script
        {
            get { return _Script; }
            set
            {
                _Script = value;
                OnPropertyChanged("Script");
            }
        }

        private MoveAttributeFlags _Attributes;
        public MoveAttributeFlags Attributes
        {
            get { return this._Attributes; }
            set
            {
                this._Attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        private float _CpuMinRange;
        public float CpuMinRange
        {
            get { return this._CpuMinRange; }
            set
            {
                this._CpuMinRange = value;
                OnPropertyChanged("CpuMinRange");
            }
        }

        private float _CpuMaxRange;
        public float CpuMaxRange
        {
            get { return this._CpuMaxRange; }
            set
            {
                this._CpuMaxRange = value;
                OnPropertyChanged("CpuMaxRange");
            }
        }

        private uint _Unk2;
        public uint Unk2
        {
            get { return this._Unk2; }
            set
            {
                this._Unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }

        private ushort _Unk3; 
        public ushort Unk3
        {
            get { return this._Unk3; }
            set
            {
                this._Unk3 = value;
                OnPropertyChanged("Unk3");
            }
        }

        private UInt16 _CpuPassiveMove; 
        public UInt16 CpuPassiveMove
        {
            get { return this._CpuPassiveMove; }
            set
            {
                this._CpuPassiveMove = value;
                OnPropertyChanged("CpuPassiveMove");
            }
        }

        private UInt16 _CpuCounterMove;
        public UInt16 CpuCounterMove
        {
            get { return this._CpuCounterMove; }
            set
            {
                this._CpuCounterMove = value;
                OnPropertyChanged("CpuCounterMove");
            }
        }

        private UInt16 _CpuVsStand; 
        public UInt16 CpuVsStand
        {
            get { return this._CpuVsStand; }
            set
            {
                this._CpuVsStand = value;
                OnPropertyChanged("CpuVsStand");
            }
        }

        private UInt16 _CpuVsCrouch; 
        public UInt16 CpuVsCrouch
        {
            get { return this._CpuVsCrouch; }
            set
            {
                this._CpuVsCrouch = value;
                OnPropertyChanged("CpuVsCrouch");
            }
        }

        private UInt16 _CpuVsAir; 
        public UInt16 CpuVsAir
        {
            get { return this._CpuVsAir; }
            set
            {
                this._CpuVsAir = value;
                OnPropertyChanged("CpuVsAir");
            }
        }

        private UInt16 _CpuVsDown; 
        public UInt16 CpuVsDown
        {
            get { return this._CpuVsDown; }
            set
            {
                this._CpuVsDown = value;
                OnPropertyChanged("CpuVsDown");
            }
        }

        private UInt16 _CpuVsStunned;
        public UInt16 CpuVsStunned
        {
            get { return this._CpuVsStunned; }
            set
            {
                this._CpuVsStunned = value;
                OnPropertyChanged("CpuVsStunned");
            }
        }

        private UInt16 _CpuProbeMove; 
        public UInt16 CpuProbeMove
        {
            get { return this._CpuProbeMove; }
            set
            {
                this._CpuProbeMove = value;
                OnPropertyChanged("CpuProbeMove");
            }
        }

        private UInt16 _CpuVsVeryClose;
        public UInt16 CpuVsVeryClose
        {
            get { return this._CpuVsVeryClose; }
            set
            {
                this._CpuVsVeryClose = value;
                OnPropertyChanged("CpuVsVeryClose");
            }
        }

        private UInt16 _CpuVsClose;
        public UInt16 CpuVsClose
        {
            get { return this._CpuVsClose; }
            set
            {
                this._CpuVsClose = value;
                OnPropertyChanged("CpuVsClose");
            }
        }

        private UInt16 _CpuVsMidRange;
        public UInt16 CpuVsMidRange
        {
            get { return this._CpuVsMidRange; }
            set
            {
                this._CpuVsMidRange = value;
                OnPropertyChanged("CpuVsMidRange");
            }
        }

        private UInt16 _CpuVsFar; 
        public UInt16 CpuVsFar
        {
            get { return this._CpuVsFar; }
            set
            {
                this._CpuVsFar = value;
                OnPropertyChanged("CpuVsFar");
            }
        }

        private UInt16 _CpuVsVeryFar; 
        public UInt16 CpuVsVeryFar
        {
            get { return this._CpuVsVeryFar; }
            set
            {
                this._CpuVsVeryFar = value;
                OnPropertyChanged("CpuVsVeryFar");
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

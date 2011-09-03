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
            ULTRA2 = 0x0000000100000000,
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

        private byte[] _AIData;
        public byte[] AIData
        {
            get { return _AIData; }
            set
            {
                _AIData = value;
                OnPropertyChanged("AIData");
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
}

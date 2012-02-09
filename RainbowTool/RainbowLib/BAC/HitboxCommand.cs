using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class HitboxCommand : BaseCommand, ICloneable
    {
        private float _X;
        public float X
        {
            get { return _X; }
            set
            {
                _X = value;
                OnPropertyChanged("X");
            }
        }
        private float _Y;
        public float Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                OnPropertyChanged("Y");
            }
        }
        private float _Rotation;
        public float Rotation
        {
            get { return _Rotation; }
            set
            {
                _Rotation = value;
                OnPropertyChanged("Rotation");
            }
        }
        private float _Width;
        public float Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
        }
        private float _Height;
        public float Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
        }
        private float _FloatUnknown;
        public float FloatUnknown
        {
            get { return _FloatUnknown; }
            set
            {
                _FloatUnknown = value;
                OnPropertyChanged("FloatUnknown");
            }
        }
        private sbyte _ID;
        public sbyte ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");
            }
        }
        private sbyte _Juggle;
        public sbyte Juggle
        {
            get { return _Juggle; }
            set
            {
                _Juggle = value;
                OnPropertyChanged("Juggle");
            }
        }
        public enum HitboxType : byte
        {
            PROXIMITY = 0,
            NORMAL = 1,
            GRAB = 2,
            PROJECTILE = 3,
            REFLECT = 4
        }
        private HitboxType _Type;
        public HitboxType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        public enum HitLevelType : byte
        {
            MID = 0,
            OVERHEAD = 1,
            LOW = 2,
            UNBLOCKABLE = 3,
            AIR_ONLY = 4
        }
        private HitLevelType _HitLevel;
        public HitLevelType HitLevel
        {
            get { return _HitLevel; }
            set
            {
                _HitLevel = value;
                OnPropertyChanged("HitLevel");
            }
        }
        [Flags]
        public enum Flags : ushort
        {
            NONE                = 0,
            LOWER               = 0x01,
            UPPER               = 0x02,
            UNBLOCKABLE         = 0x04,
            BREAK_ARMOR         = 0x08,
            BREAK_COUNTER       = 0x10,
            CROSSUP             = 0x20,
            DONT_HIT_STANDING   = 0x40,
            DONT_HIT_CROUCHING  = 0x80,
            DONT_HIT_AIR        = 0x100,
            DONT_HIT_FRONT      = 0x200,
            DONT_HIT_JUMP_START = 0x400,
            UNKNOWN800          = 0x800,
            UNKNOWN1000         = 0x1000,
            GET_TARGET_SIDE     = 0x2000,
            IGNORE_FINISH       = 0x4000,
            IGNORE_AIMAGE       = 0x8000
        }
        private Flags _HitFlags;
        public Flags HitFlags
        {
            get { return _HitFlags; }
            set
            {
                _HitFlags = value;
                OnPropertyChanged("HitFlags");
            }
        }
        
        private byte _Unknown1;
        public byte Unknown1
        {
            get { return _Unknown1; }
            set
            {
                _Unknown1 = value;
                OnPropertyChanged("Unknown1");
            }
        }
        private sbyte _UnknownByte1;
        public sbyte UnknownByte1
        {
            get { return _UnknownByte1; }
            set
            {
                _UnknownByte1 = value;
                OnPropertyChanged("UnknownByte1");
            }
        }
        private sbyte _Hits;
        public sbyte Hits
        {
            get { return this._Hits; }
            set
            {
                this._Hits = value;
                OnPropertyChanged("Hits");
            }
        }
        private sbyte _JugglePotential;
        public sbyte JugglePotential
        {
            get { return _JugglePotential; }
            set
            {
                _JugglePotential = value;
                OnPropertyChanged("JugglePotential");
            }
        }
        private sbyte _JuggleIncrement;
        public sbyte JuggleIncrement
        {
            get { return _JuggleIncrement; }
            set
            {
                _JuggleIncrement = value;
                OnPropertyChanged("JuggleIncrement");
            }
        }
        private sbyte _JuggleIncrementLimit;
        public sbyte JuggleIncrementLimit
        {
            get { return this._JuggleIncrementLimit; }
            set
            {
                this._JuggleIncrementLimit = value;
                OnPropertyChanged("JuggleIncrementLimit");
            }
        }
        private int _HitboxEffect;
        public int HitboxEffect
        {
            get { return _HitboxEffect; }
            set
            {
                _HitboxEffect = value;
                OnPropertyChanged("HitboxEffect");
            }
        }
        [NonSerialized]
        private HitBoxDataset _HitboxDataSet;
        public HitBoxDataset HitboxDataSet
        {
            get { return _HitboxDataSet; }
            set
            {
                _HitboxDataSet = value;
                OnPropertyChanged("HitboxDataSet");
            }
        }

        public override object Clone()
        {
            HitboxCommand hitbox = (HitboxCommand)base.Clone();
            hitbox.HitboxDataSet = this.HitboxDataSet;
            return hitbox;
        }
    }
}

using System;

namespace RainbowLib.BAC
{
    [Serializable]
    public class TargetLockCommand : BaseCommand
    {
        public enum TargetLockType : int
        {
            MESH_UNLOCK = 0,
            MESH_LOCK = 1,
            PART_UNLOCK = 2,
            PART_LOCK = 3,
            SPECIAL_EFFECT = 4,
        }

        private TargetLockType _Type;
        public TargetLockType Type
        {
            get { return this._Type; }
            set
            {
                this._Type = value;
                OnPropertyChanged("Type");
            }
        }

        private Int32 _AnimId;
        public Int32 AnimId
        {
            get { return this._AnimId; }
            set
            {
                this._AnimId = value;
                OnPropertyChanged("AnimId");
            }
        }

        private Int32 _Unknown2;
        public Int32 Unknown2
        {
            get { return this._Unknown2; }
            set
            {
                this._Unknown2 = value;
                OnPropertyChanged("Unknown2");
            }
        }

        private Int32 _Unknown3;
        public Int32 Unknown3
        {
            get { return this._Unknown3; }
            set
            {
                this._Unknown3 = value;
                OnPropertyChanged("Unknown3");
            }
        }
    }
}

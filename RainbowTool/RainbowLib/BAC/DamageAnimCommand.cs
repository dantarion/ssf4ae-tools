using System;

namespace RainbowLib.BAC
{
    [Serializable]
    public class DamageAnimCommand : BaseCommand
    {
        private Int32 _Type;
        public Int32 Type
        {
            get { return this._Type; }
            set
            {
                this._Type = value;
                OnPropertyChanged("Type");
            }
        }

        private Int32 _Anim;
        public Int32 Anim
        {
            get { return this._Anim; }
            set
            {
                this._Anim = value;
                OnPropertyChanged("Anim");
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

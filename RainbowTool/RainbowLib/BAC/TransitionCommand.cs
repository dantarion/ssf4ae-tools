using System;

namespace RainbowLib.BAC
{
    [Serializable]
    public class TransitionCommand : BaseCommand
    {
        private UInt16 _Flag1;
        public UInt16 Flag1
        {
            get { return this._Flag1; }
            set
            {
                this._Flag1 = value;
                OnPropertyChanged("Flag1");
            }
        }

        private UInt16 _Flag2;
        public UInt16 Flag2
        {
            get { return this._Flag2; }
            set
            {
                this._Flag2 = value;
                OnPropertyChanged("Flag2");
            }
        }

        private float _Float1;
        public float Float1
        {
            get { return this._Float1; }
            set
            {
                this._Float1 = value;
                OnPropertyChanged("Float1");
            }
        }

        private float _Float2;
        public float Float2
        {
            get { return this._Float2; }
            set
            {
                this._Float2 = value;
                OnPropertyChanged("Float2");
            }
        }

        private float _Float3;
        public float Float3
        {
            get { return this._Float3; }
            set
            {
                this._Float3 = value;
                OnPropertyChanged("Float3");
            }
        }

        private float _Float4;
        public float Float4
        {
            get { return this._Float4; }
            set
            {
                this._Float4 = value;
                OnPropertyChanged("Float4");
            }
        }

        private float _Float5;
        public float Float5
        {
            get { return this._Float5; }
            set
            {
                this._Float5 = value;
                OnPropertyChanged("Float5");
            }
        }

        private float _Float6;
        public float Float6
        {
            get { return this._Float6; }
            set
            {
                this._Float6 = value;
                OnPropertyChanged("Float6");
            }
        }
    }
}

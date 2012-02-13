using System;

namespace RainbowLib.BAC
{
    public enum SfxType : ushort
    {
        NORMAL = 0,
        EFFECT = 1,
        VOICE = 2,
        STOMP = 5
    }

    [Serializable]
    public class SfxCommand : BaseCommand
    {
        private SfxType _Type;
        public SfxType Type
        {
            get { return this._Type; }
            set
            {
                this._Type = value;
                OnPropertyChanged("Type");
            }
        }

        private Int16 _Sound;
        public Int16 Sound
        {
            get { return this._Sound; }
            set
            {
                this._Sound = value;
                OnPropertyChanged("Sound");
            }
        }

        private UInt32 _Unknown1;
        public UInt32 Unknown1
        {
            get { return this._Unknown1; }
            set
            {
                this._Unknown1 = value;
                OnPropertyChanged("Unknown1");
            }
        }

        private UInt32 _Unknown2;
        public UInt32 Unknown2
        {
            get { return this._Unknown2; }
            set
            {
                this._Unknown2 = value;
                OnPropertyChanged("Unknown2");
            }
        }

        private UInt32 _Unknown3;
        public UInt32 Unknown3
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class HurtboxCommand : BaseCommand
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
        private uint _Unknown1;
        public uint Unknown1
        {
            get { return _Unknown1; }
            set
            {
                _Unknown1 = value;
                OnPropertyChanged("Unknown1");
            }
        }
        private byte _Unknown2;
        public byte Unknown2
        {
            get { return _Unknown2; }
            set
            {
                _Unknown2 = value;
                OnPropertyChanged("Unknown2");
            }
        }
        private byte _Unknown3;
        public byte Unknown3
        {
            get { return _Unknown3; }
            set
            {
                _Unknown3 = value;
                OnPropertyChanged("Unknown3");
            }
        }
        private byte _Unknown4;
        public byte Unknown4
        {
            get { return _Unknown4; }
            set
            {
                _Unknown4 = value;
                OnPropertyChanged("Unknown4");
            }
        }
        private sbyte _Unknown5;
        public sbyte Unknown5
        {
            get { return _Unknown5; }
            set
            {
                _Unknown5 = value;
                OnPropertyChanged("Unknown5");
            }
        }
        
        
        
        
        
        
        
        
        
    }
}

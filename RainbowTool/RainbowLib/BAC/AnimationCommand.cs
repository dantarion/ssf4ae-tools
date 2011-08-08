using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class AnimationCommand : BaseCommand
    {
        public enum AnimationType : byte
        {
            NORMAL = 0,
            FACE = 1,
            CAMERA = 2,
            RIVAL1 = 3,
            RIVAL2 = 4,
            UC1 = 5,
            UC2 = 6

        }
        public enum AnimationFlags : byte
        {
            NORMAL = 0,
            PHYSICS = 7,
            PHYSICS2=5,
        }
        private AnimationType _Type;
        public AnimationType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
              
        private AnimationFlags _Flags;
        public AnimationFlags Flags
        {
            get { return _Flags; }
            set
            {
                _Flags = value;
                OnPropertyChanged("Flags");
            }
        }
        private short _Animation;
        public short Animation
        {
            get { return _Animation; }
            set
            {
                _Animation = value;
                OnPropertyChanged("Animation");
            }
        }
        private short _StartFrame;
        public short FromFrame
        {
            get { return _StartFrame; }
            set
            {
                _StartFrame = value;
                OnPropertyChanged("FromFrame");
            }
        }
        private short _ToFrame;
        public short ToFrame
        {
            get { return _ToFrame; }
            set
            {
                _ToFrame = value;
                OnPropertyChanged("ToFrame");
            }
        }
    }

}


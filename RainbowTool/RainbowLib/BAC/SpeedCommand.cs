using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class SpeedCommand : BaseCommand
    {
        private float _Multiplier;
        public float Multiplier
        {
            get { return _Multiplier; }
            set
            {
                _Multiplier = value;
                OnPropertyChanged("Multiplier");
            }
        }
        
    }
}

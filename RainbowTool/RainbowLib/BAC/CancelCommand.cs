using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class CancelCommand : BaseCommand
    {
        [Flags]
        public enum CancelType
        {
            STOP = 1,
            START = 0,
            ON_HIT = 6
        }
        private CancelType _Type;
        public CancelType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        [NonSerialized]
        private Reference<BCM.CancelList> _CancelList;
        public BCM.CancelList CancelList
        {
            get { return _CancelList; }
            set
            {
                _CancelList = value;
                OnPropertyChanged("CancelList");
            }
        }

        public override object Clone()
        {
            var cc = (CancelCommand)base.Clone();
            cc.CancelList = this.CancelList;
            return cc;
        }

    }
}

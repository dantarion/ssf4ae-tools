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
        public enum CancelConditions
        {
            START = 0,
            STOP = 1,
            ON_HIT = 2,
            ON_BLOCK = 4,
            ON_WHIFF = 8
        }

        private CancelConditions _Condition;
        public CancelConditions Condition
        {
            get { return this._Condition; }
            set
            {
                this._Condition = value;
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

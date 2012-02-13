using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib.BAC
{
    [Serializable]
    public class FlowCommand : BaseCommand
    {
        //[Flags]
        public enum FlowType : short
        {
            ALWAYS = 0,
            ON_HIT = 1,
            ON_BLOCK = 2,
            SWING = 3,
            GROUP_HIT = 4,
            GROUP_GUARD = 5,
            GROUP_SWING = 6,
            GROUND = 7, 
            ON_LAND = 8,
            ON_WALL = 9,
            ON_COUNTER = 0xA,
            ON_RELEASE = 0xB,
            ON_INPUT = 0xC
        }
        private FlowType _Type;
        public FlowType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        private Input _Input;
        public Input Input
        {
            get { return _Input; }
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
        }
        [NonSerialized]
        private Reference<Script> _TargetScript;
        public Script TargetScript
        {
            get { return _TargetScript; }
            set
            {
                _TargetScript = value;
                OnPropertyChanged("TargetScript");
            }
        }
        private short _Unknown;
        public short TargetFrame
        {
            get { return _Unknown; }
            set
            {
                _Unknown = value;
                OnPropertyChanged("Unknown");
            }
        }
        public override object Clone()
        {
            var cmd = (FlowCommand)base.Clone();
            cmd.TargetScript = this.TargetScript;
            return cmd;
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace RainbowLib.BCM
{
    public class Charge
    {
        public String Name{get;set;}
        public Input Input{get;set;}
        public ushort Buffer{get;set;}
        public ushort Frames{get;set;}
        public ushort Unknown1{get;set;}
        public ushort Unknown2{get;set;}
        public ushort Unknown3{get;set;}
        public ushort Unknown4{get;set;}
        public override string ToString()
        {
            return string.Format("{0,15} - {1,20} - {2,3} frame buf, {3} frame charge, {4:X04} {5:X04} {6:X04} {7:X04}",
                Name, Input, Buffer, Frames, Unknown1, Unknown2, Unknown3, Unknown4);
        }
    }
}

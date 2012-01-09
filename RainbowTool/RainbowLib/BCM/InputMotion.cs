using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace RainbowLib.BCM
{
    [Serializable]
    public class InputMotion : INotifyPropertyChanged
    {
        public static InputMotion NONE = new InputMotion("NONE");
        private string _name="";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public InputMotion()
            : this("NEW")
        {
        }
        public InputMotion(string name)
        {
            Name = name;
        }
        private ObservableCollection<InputMotionEntry> _Entries = new ObservableCollection<InputMotionEntry>();
        public ObservableCollection<InputMotionEntry> Entries{get{return _Entries;}}
         [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
        public override string ToString()
        {/*
            var sb = new StringBuilder();
            sb.Append(Name + " - ");
            foreach(InputMotionEntry entry in Entries)
            {
                sb.Append("{"+entry.DirectionName+"}");
                if(Entries.Last() != entry)
                    sb.Append(", ");
            }
            return sb.ToString();
          */
            return Name;
        }
    }
    public enum InputType
    {
        NORMAL = 0,
        CHARGE = 1,
        JOY_360 = 2,
        MASH = 3
    }
    public enum InputReqType
    {
        NORMAL = 0,
        LENIENT = 1,
        STRICT = 2,
        MASH = 16
    }
    [Serializable]
    public class InputMotionEntry : INotifyPropertyChanged
    {
        private InputType _type;
        public InputType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if (this.Type == InputType.CHARGE)
                    Input = 0;
                else if (this.Type != InputType.CHARGE)
                    Charge = null;
                OnPropertyChanged("Type");
            }
        }
        public bool IsCharge { get { return this.Type == InputType.CHARGE; } }
        public bool IsNormal { get { return this.Type == InputType.NORMAL; } }
        public ushort Buffer{get;set;}
        private Input _input;
        public Input Input
        {
            get { return _input; }
            set { 
                _input = value;
                OnPropertyChanged("Input");
            }
        }
        private Reference<Charge> _charge;
        public Charge Charge
        {
            get{return _charge;}
            set { _charge = value; OnPropertyChanged("Charge"); }
        }
        public string DirectionName
        {
            get
            {
                if (IsCharge)
                    return "CHARGE["+Charge.ToString()+"]";
                return Input.ToString();
            }
        }
        public MoveFlags MoveFlags{get;set;}
        public InputReqType Flags { get; set; }
        public ushort Requirement{get;set;}
         [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
    }
}

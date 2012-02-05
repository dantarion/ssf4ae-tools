using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace RainbowLib.BCM
{
    [Serializable]
    public class CancelList : INotifyPropertyChanged, ICloneable
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        public override string ToString()
        {
            return Name;
        }
        private ObservableCollection<Reference<Move>> _Moves = new ObservableCollection<Reference<Move>>();
        public ObservableCollection<Reference<Move>> Moves
        {
            get {
                return _Moves; }
        }

         [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        public object Clone()
        {
            var clone = Cloner.ShallowCopy(this);
            for (int i = 0; i < this.Moves.Count; i++)
            {
                clone.Moves[i].Target = this.Moves[i].Target;
            }

            clone.Name = this.Name + "_COPY";
            return clone;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace RainbowLib.BAC
{
    
   
    public enum CommandListType : ushort
    {
        FLOW = 0,
        ANIMATION = 1,
        UNK02 = 2,
        UNK03 = 3,
        SPEED = 4,
        PHYSICS = 5,
        CANCELS = 6,
        HITBOX = 7,
        UNK08 = 8,
        HURTBOX = 9,
        ETC = 10,
        UNK11 = 11,
        SFX = 12,
    }
    public static class CommandListFactory
    {
        public static dynamic ByType(CommandListType type)
        {
            if (type == CommandListType.FLOW)
                return new CommandList<FlowCommand>();
            if (type == CommandListType.ANIMATION)
                return new CommandList<AnimationCommand>();
            if (type == CommandListType.UNK03)
                return new CommandList<StateCommand>();
            if (type == CommandListType.SPEED)
                return new CommandList<SpeedCommand>();
            if (type == CommandListType.CANCELS)
                return new CommandList<CancelCommand>();
            if (type == CommandListType.HURTBOX)
                return new CommandList<HurtboxCommand>();
            if (type == CommandListType.ETC)
                return new CommandList<EtcCommand>();
            if (type == CommandListType.HITBOX)
                return new CommandList<HitboxCommand>();
            return new CommandList<BaseCommand>();
        }
    }
    public class CommandList<T> where T:BaseCommand, INotifyPropertyChanged
    {
        internal CommandList()
        {
        }
        public override string ToString()
        {
            if (Commands.Count == 0)
                return Type.ToString() + "*";
            else
                return Type.ToString();

        }
        public BaseCommand GenerateCommand()
        {
            return (BaseCommand)typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);
        }
        private CommandListType _Type;
        public CommandListType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
        }
        private ObservableCollection<T> _Commands = new ObservableCollection<T>();
        public ObservableCollection<T> Commands
        {
            get { return _Commands; }
        }

         [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

    }
}

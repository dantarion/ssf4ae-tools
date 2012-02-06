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
        STATE = 3,
        SPEED = 4,
        PHYSICS = 5,
        CANCELS = 6,
        HITBOX = 7,
        INVINC = 8,
        HURTBOX = 9,
        ETC = 10,
        DAMAGEANIM = 11,
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
            if (type == CommandListType.STATE)
                return new CommandList<StateCommand>();
            if (type == CommandListType.SPEED)
                return new CommandList<SpeedCommand>();
            if (type == CommandListType.PHYSICS)
                return new CommandList<PhysicsCommand>();
            if (type == CommandListType.CANCELS)
                return new CommandList<CancelCommand>();
            if (type == CommandListType.HURTBOX)
                return new CommandList<HurtboxCommand>();
            if (type == CommandListType.ETC)
                return new CommandList<EtcCommand>();
            if (type == CommandListType.HITBOX)
                return new CommandList<HitboxCommand>();
            if (type == CommandListType.INVINC)
                return new CommandList<InvincCommand>();
            return new CommandList<BaseCommand>();
        }
    }
    [Serializable]
    public class CommandList<T> : ObservableCollection<T>, ICloneable where T:BaseCommand, INotifyPropertyChanged 
    {
        internal CommandList()
        {
        }
        public override string ToString()
        {
            if (Count == 0)
                return Type.ToString() + "*";
            else
                return Type.ToString();

        }

        public object Clone()
        {
            var clone = new CommandList<T>();
            clone.Type = this.Type;
            foreach (var element in this)
            {
                clone.Add(Cloner.Clone(element));
            }

            return clone;
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
         [field: NonSerialized]
        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

    }
}

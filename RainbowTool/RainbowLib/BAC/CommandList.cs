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
        TRANSITION = 2,
        STATE = 3,
        SPEED = 4,
        PHYSICS = 5,
        CANCELS = 6,
        HITBOX = 7,
        INVINC = 8,
        HURTBOX = 9,
        ETC = 10,
        TARGETLOCK = 11,
        SFX = 12,
    }
    public static class CommandListFactory
    {
        public static dynamic ByType(CommandListType type)
        {
            switch (type)
            {
                case CommandListType.FLOW:
                    return new CommandList<FlowCommand>();
                case CommandListType.ANIMATION:
                    return new CommandList<AnimationCommand>();
                case CommandListType.TRANSITION:
                    return new CommandList<TransitionCommand>();
                case CommandListType.STATE:
                    return new CommandList<StateCommand>();
                case CommandListType.SPEED:
                    return new CommandList<SpeedCommand>();
                case CommandListType.PHYSICS:
                    return new CommandList<PhysicsCommand>();
                case CommandListType.CANCELS:
                    return new CommandList<CancelCommand>();
                case CommandListType.HURTBOX:
                    return new CommandList<HurtboxCommand>();
                case CommandListType.ETC:
                    return new CommandList<EtcCommand>();
                case CommandListType.HITBOX:
                    return new CommandList<HitboxCommand>();
                case CommandListType.INVINC:
                    return new CommandList<HurtNodeCommand>();
                case CommandListType.TARGETLOCK:
                    return new CommandList<TargetLockCommand>();
                case CommandListType.SFX:
                    return new CommandList<SfxCommand>();
                default:
                    return new CommandList<BaseCommand>();
            }
        }
    }
    [Serializable]
    public class CommandList<T> : ObservableCollection<T>, ICloneable where T:BaseCommand, INotifyPropertyChanged 
    {
        public CommandList()
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

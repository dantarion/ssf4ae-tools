using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using RainbowLib.BAC;
namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for ScriptWindow.xaml
    /// </summary>
    public partial class ScriptWindow : Window
    {
        void myFilter(object sender, FilterEventArgs e)
        {
            RainbowLib.BAC.Script script = e.Item as RainbowLib.BAC.Script;
            e.Accepted = script != RainbowLib.BAC.Script.NullScript;

        }
        public ScriptWindow(String path)
        {
            var binding = new Binding(path);
            binding.Source = App.OpenedFiles.BACFile;
            this.SetBinding(Window.DataContextProperty, binding);
            InitializeComponent();

            this.ListBox.WorkingType = path;

            if (!UserSettings.CurrentSettings.WindowCollection.ContainsKey(Name))
                UserSettings.CurrentSettings.WindowCollection.Add(Name, new TypeSettings());
            else
            {
                Left = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.X;
                Top = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.Y;

                Width = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Width;
                Height = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Height;
            }
            this.Title = path + " - " + MainWindow.Opened;
        }
        private int nextIndex = 0;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
                nextIndex = (int)((dynamic)(e.RemovedItems[0])).Type;
        }

        private void ScriptSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && ComboBox != null)
                ComboBox.SelectedIndex = nextIndex;
        }

        private void timelineDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "FrameIndex")
            {
                e.Column.Header = "#";
            }
        }

        private void WindowLocationChanged(object sender, EventArgs e)
        {            
            UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation = new Point(Left, Top);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].ThisSize = e.NewSize;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].IsMaximized = WindowState.HasFlag(System.Windows.WindowState.Maximized);

            if (!UserSettings.CurrentSettings.WindowCollection[Name].IsMaximized) return;
            UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation = RestoreBounds.Location;
            UserSettings.CurrentSettings.WindowCollection[Name].ThisSize = RestoreBounds.Size;
        }

    }

    public class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return string.Format("{0:X}", value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            Int32 val = Int32.Parse((string)value, System.Globalization.NumberStyles.HexNumber);

            return val;
        }
    }
    public class EnumHexConverter<T> : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return string.Format("{0:X}", value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            Int32 val = Int32.Parse((string)value, System.Globalization.NumberStyles.HexNumber);
            if (typeof(T).IsEnum)
            {
                return Enum.Parse(typeof(T), val.ToString());
            }
            else
            {
                return val;
            }
        }
    }
    public class HexConverterUInt32<T> : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return string.Format("{0:X}", value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            uint val = UInt32.Parse((string)value, System.Globalization.NumberStyles.HexNumber);
            //RainbowLib.AELogger.Log(val + " " + value.ToString());
            if (typeof(T).IsEnum)
            {
                return Enum.Parse(typeof(T), val.ToString());
            }
            else
            {
                return val;
            }
        }
    }

    public class RealFrameConverter : DependencyObject, IValueConverter
    {


        public RainbowLib.BAC.Script Target
        {
            get { return (RainbowLib.BAC.Script)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(RainbowLib.BAC.Script), typeof(RealFrameConverter), new UIPropertyMetadata(null));



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BaseCommand cmd = value as BaseCommand;
            if (value == null||parameter == null)
                return "";
            float curFrame = 0;
            float speed = 0;
            for (int i = 0; i < cmd.StartFrame; i++)
            {
                foreach (SpeedCommand speedCommand in Target.CommandLists[(int)CommandListType.SPEED])
                {
                    if (i > speedCommand.StartFrame)
                        if (speedCommand.Multiplier == 0)
                            speed = 0;
                        else
                            speed = 1.0f / speedCommand.Multiplier;
                    curFrame += speed;
                }
            }
            return String.Format("Actual StartFrame: {0}", curFrame);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimelineFrame
    {
        public enum EventType
        {
            Start,
            End,
            Just
        }

        public int FrameIndex { get; set; }
        public string Flow { get; set; }
        public string Animation { get; set; }
        public string Transition { get; set; }
        public string State { get; set; }
        public string Speed { get; set; }
        public string Physics { get; set; }
        public string Cancels { get; set; }
        public string Hitbox { get; set; }
        public string Invinc { get; set; }
        public string Hurtbox { get; set; }
        public string Etc { get; set; }
        public string TgtLock { get; set; }
        public string Sfx { get; set; }

        private static string AppendString(string original, string addition, EventType et)
        {
            if (et != EventType.Just)
            {
                addition = string.Format("[{0}]{1}", addition, et == EventType.Start ? "+" : "-");
            }
            if (string.IsNullOrEmpty(original)) 
                return addition;
            else 
                return original + "\r\n" + addition;
        }

        public void AddInfo(BaseCommand cmd, CommandListType type, EventType et)
        {
            switch (type)
            {
                case CommandListType.FLOW:
                    var flow = (FlowCommand)cmd;
                    this.Flow = AppendString(this.Flow, flow.Type + ":" + flow.TargetScript.Name, et);
                    return;

                case CommandListType.ANIMATION:
                    var anim = (AnimationCommand)cmd;
                    var animName = new IndexReferenceTypeConverter().Convert(
                        new object[] { anim.Type, anim.Animation }, null, null, null);
                    this.Animation = AppendString(this.Animation, anim.Type + ":" + animName, et);
                    return;

                case CommandListType.TRANSITION:
                    var transition = (TransitionCommand)cmd;
                    this.Transition = AppendString(this.Transition, string.Join("/", transition.Flag1, transition.Flag2), et);
                    return;

                case CommandListType.STATE:
                    var state = (StateCommand)cmd;
                    this.State = AppendString(this.State, state.Flags.ToString(), et);
                    return;

                case CommandListType.SPEED:
                    var speed = (SpeedCommand)cmd;
                    this.Speed = AppendString(this.Speed, speed.Multiplier + "x", et);
                    return;

                case CommandListType.PHYSICS:
                    var physics = (PhysicsCommand)cmd;
                    this.Physics = AppendString(this.Physics, physics.PhysicsFlags.ToString(), et);
                    return;

                case CommandListType.CANCELS:
                    var cancel = (CancelCommand)cmd;
                    this.Cancels = AppendString(this.Cancels, cancel.Condition + ":" + cancel.CancelList, et);
                    return;

                case CommandListType.HITBOX:
                    var hitbox = (HitboxCommand)cmd;
                    string str = hitbox.Type == HitboxCommand.HitboxType.PROXIMITY ? "PROX" : "#" + hitbox.HitboxDataSet.Index;
                    this.Hitbox = AppendString(this.Hitbox, str, et);
                    return;

                case CommandListType.INVINC:
                    var invinc = (HurtNodeCommand)cmd;
                    this.Invinc = AppendString(this.Invinc, invinc.Flags.ToString(), et);
                    return;

                case CommandListType.HURTBOX:
                    var hurtbox = (HurtboxCommand)cmd;
                    this.Hurtbox = AppendString(this.Hurtbox, string.Format("{0},{1}/{2},{3}", hurtbox.X, hurtbox.Y, hurtbox.Width, hurtbox.Height), et);
                    return;

                case CommandListType.ETC:
                    var etc = (EtcCommand)cmd;
                    this.Etc = AppendString(this.Etc, string.Join("/", etc.Type, etc.ShortParam, etc.Unknown00, etc.Unknown01), et);
                    return;

                case CommandListType.TARGETLOCK:
                    var dmgAnim = (TargetLockCommand)cmd;
                    this.TgtLock= AppendString(this.TgtLock, dmgAnim.Type + ":" + dmgAnim.DmgScript, et);
                    return;

                case CommandListType.SFX:
                    var sfx = (SfxCommand)cmd;
                    this.Sfx = AppendString(this.Sfx, sfx.Type + ":" + sfx.Sound, et);
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class TimelineGenerator : IValueConverter
    {
        public static TimelineGenerator Instance { get { return instance; }}

        private static readonly TimelineGenerator instance = new TimelineGenerator();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var superlist = value as ObservableCollection<dynamic>;
            if (superlist == null)
            {
                return null;
            }

            var result = new Dictionary<int, TimelineFrame>();
            foreach (dynamic list in superlist)
            {
                foreach (BaseCommand cmd in list)
                {
                    int duration = cmd.EndFrame - cmd.StartFrame;
                    int index = cmd.StartFrame;
                    if (!result.ContainsKey(index))
                    {
                        result.Add(index, new TimelineFrame { FrameIndex = index });
                    }
                    if (duration == 1)
                    {
                        result[index].AddInfo(cmd, list.Type, TimelineFrame.EventType.Just);
                    }
                    else
                    {
                        result[index].AddInfo(cmd, list.Type, TimelineFrame.EventType.Start);
                        index = cmd.EndFrame;
                        if (!result.ContainsKey(index))
                        {
                            result.Add(index, new TimelineFrame { FrameIndex = index });
                        }
                        result[index].AddInfo(cmd, list.Type, TimelineFrame.EventType.End);
                    }
                }
            }

            return result.Values.OrderBy(f => f.FrameIndex).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

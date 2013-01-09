using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
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
            var script = e.Item as RainbowLib.BAC.Script;
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
            
            UserSettings.OnSettingsChanged += UserSettings_OnSettingsChanged;
            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += CurrentPropertyChanged;
            if (UserSettings.CurrentSettings.UseAeroScheme)
                if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                {
                    Style = (Style) FindResource("AeroStyle");
                    ListBox.Margin = new Thickness(1, 35, 1, 1);
                }

        }

        void CurrentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("IsGlassEnabled")) return;

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled && UserSettings.CurrentSettings.UseAeroScheme)
                Style = (Style)FindResource("AeroStyle");
            else
                Style = null;
        }

        void UserSettings_OnSettingsChanged(object sender, object oVar, Type pType)
        {
            CurrentPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsGlassEnabled"));
        }

        private int _nextIndex = 0;
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
                _nextIndex = (int)((dynamic)(e.RemovedItems[0])).Type;
        }

        private void ScriptSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && ComboBox != null)
                ComboBox.SelectedIndex = _nextIndex;

            tbscrcnt.Text = String.Format("{0} Scripts", ListBox.ItemCount);

            try
            {
                var scr = ListBox.SelectedValue as Script;
                var converter = new RealFrameCounter { Target=scr };

                statlbl.Content = converter.GetHumanFrames;

            }
            catch(Exception er)
            {
                Console.WriteLine(er);
            }

        }

        private void TimelineDataGridAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            Int32 val = Int32.Parse((string)value, NumberStyles.HexNumber);

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

    public class RealFrameCounter
    {

        public Script Target { get; set; }

        public object GetHumanFrames
        {
            get
            {
                var hitboxcalc = GetHitboxFrameCalculations;
                var peram = new Object[] {(int)hitboxcalc[0],(int)hitboxcalc[1],(int)hitboxcalc[2],(int)GetCalculatedFrames}; //round everything

                return String.Format("Frames* -> Approx [ Start Up {0}, Active {1}, Recovery {2}, Total {3} ]",peram);
            }
        }

        public float GetCalculatedFrames
        {
            get
            {
                ushort realFrames = Target.TotalFrames;
                var speed = (ObservableCollection<SpeedCommand>) Target.CommandLists[4];

                if (speed == null || speed.Count <= 0) return realFrames;

                var totalframesskipped = 0f;

                for (var i = 0; i < speed.Count; i++)
                {
                    var appliedframes = 0;

                    if (i < speed.Count - 1)
                    {
                        appliedframes = speed[i + 1].StartFrame - speed[i].StartFrame;
                    }
                    else
                    {
                        appliedframes = realFrames - speed[i].StartFrame;
                    }

                    if (speed[i].Multiplier == 0)
                        totalframesskipped += appliedframes;
                    else
                        totalframesskipped += appliedframes/speed[i].Multiplier;
                }

                return totalframesskipped;

            }
        }

        public float[] GetHitboxFrameCalculations
        {
            get
            {
                var hitboxs = (ObservableCollection<HitboxCommand>)Target.CommandLists[7];

                float startup = 0f, active = 0f, recovery = 0f;

                foreach (var itm in hitboxs)
                {
                    if (itm.Type == HitboxCommand.HitboxType.PROXIMITY) continue;
                    active += (itm.EndFrame - itm.StartFrame)/GetFrameSpeedMulty(itm.StartFrame);

                    if (startup == 0)
                        startup = GetSpeedModifiedFramesUpTo(itm.StartFrame);
                }


                recovery = GetCalculatedFrames - active - startup;
                
                return new[]{startup,active,recovery};
            }
        }

        public float GetSpeedModifiedFramesUpTo(int frame)
        {
            var speed = (ObservableCollection<SpeedCommand>)Target.CommandLists[4];

            if (speed == null || speed.Count <= 0) return frame;

            var totalframesskipped = 0f;

            for (var i = 0; i < speed.Count; i++)
            {
                var appliedframes = 0;

                if (i < speed.Count - 1)
                {
                    if (speed[i].EndFrame >= frame) continue;
                    appliedframes = speed[i + 1].StartFrame - speed[i].StartFrame;
                }
                else
                {
                    if (speed[speed.Count - 1].EndFrame >= frame) continue;
                    appliedframes = frame - speed[i].StartFrame;
                }              

                if (speed[i].Multiplier == 0)
                    totalframesskipped += appliedframes;
                else
                    totalframesskipped += appliedframes / speed[i].Multiplier;
            }

            return totalframesskipped;          
        }

        public float GetFrameSpeedMulty(int frame)
        {
            var speed = (ObservableCollection<SpeedCommand>) Target.CommandLists[4];

            if (speed == null || speed.Count <= 0) return 1f;

            for (int i = 0; i < speed.Count; i++)
            {

                if (i < speed.Count - 1)
                {
                    if (frame >= speed[i].EndFrame && frame <= speed[i + 1].StartFrame)
                        return speed[i].Multiplier;
                }
                else
                {
                    var lastmult = speed[speed.Count - 1].Multiplier;
                    return lastmult == 0 ? 1f : lastmult;
                }
            }

            return 1f;
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

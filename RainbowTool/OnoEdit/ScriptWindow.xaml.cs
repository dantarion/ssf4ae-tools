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
}

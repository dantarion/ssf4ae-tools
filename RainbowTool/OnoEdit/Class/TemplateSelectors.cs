using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using RainbowLib.BAC;
using RainbowLib;
using System.Globalization;
namespace OnoEdit
{
    public class AnimationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<short, RainbowLib.IndexReference> dict = new Dictionary<short, RainbowLib.IndexReference>();
            var type = (AnimationCommand.AnimationType)value;
            switch (type)
            {
                case AnimationCommand.AnimationType.NORMAL:
                    dict = ResourceManager.Load("OBJ");
                    break;
                case AnimationCommand.AnimationType.FACE:
                    dict = ResourceManager.Load("FCE");
                    break;
                case AnimationCommand.AnimationType.CAMERA:
                    dict = ResourceManager.Load("CAM");
                    break;
                case AnimationCommand.AnimationType.UC1:
                    dict = ResourceManager.Load("UC1");
                    break;
                case AnimationCommand.AnimationType.UC2:
                    dict = ResourceManager.Load("UC2");
                    break;
            }
            if(culture == null)
                return dict;
            return dict.Values;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class IndexReferenceTypeConverter : IMultiValueConverter, IValueConverter
    {
 
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            var index = (short)values[1];
            var dict = new AnimationTypeConverter().Convert(values[0], targetType, parameter, null);
            return this.Convert(index, targetType, dict, culture);
        }
        public object[] ConvertBack(object value, Type[] targetType,
    object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var obj = new object[2];
            obj[0] = Binding.DoNothing;
            var val = (RainbowLib.IndexReference)value;
            obj[1] = val.Value;
            return obj;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dict = parameter as Dictionary<short, RainbowLib.IndexReference>;
            var index = (short)value;
            if (dict == null)
                return null;
            if (dict.ContainsKey(index))
                return dict[index];
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return ((RainbowLib.IndexReference)value).Value;
        }
    }
    public class EtcTemplateSelector : DataTemplateSelector, IMultiValueConverter, IValueConverter
    {
        private bool _edit = false;
        public EtcTemplateSelector(bool edit)
        {
            _edit = edit;
        }
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var etc = item as EtcCommand;
            if (etc == null)
                return null;
            var dt = new DataTemplate();
            if (!_edit)
            {
                dt.VisualTree = new FrameworkElementFactory(typeof(Label));
                dt.VisualTree.SetValue(Label.PaddingProperty, new Thickness(2));
                var mb = new MultiBinding();
                dt.VisualTree.SetValue(Label.ContentProperty, mb);

                var b1 = new Binding("Type");
                b1.Mode = BindingMode.OneWay;
                mb.Converter = this;
                mb.Bindings.Add(b1);

                var b2 = new Binding("ShortParam");
                mb.Bindings.Add(b2);
            }
            else if (etc.Type == EtcCommand.EtcCommandType.CONTROL ||
                     etc.Type == EtcCommand.EtcCommandType.GFX ||
                     etc.Type == EtcCommand.EtcCommandType.GFX2)
            {
                dt.VisualTree = new FrameworkElementFactory(typeof(ComboBox));
                var mb = new MultiBinding();
                dt.VisualTree.SetValue(ComboBox.SelectedItemProperty, mb);

                var b1 = new Binding("Type");
                b1.Mode = BindingMode.OneWay;
                mb.Converter = this;
                mb.Bindings.Add(b1);
                mb.ConverterParameter = etc.Type;
                var b2 = new Binding("ShortParam");
                mb.Bindings.Add(b2);

                var b3 = new Binding("Type");
                b3.Converter = this;
                b3.Mode = BindingMode.OneWay;
                dt.VisualTree.SetBinding(ComboBox.ItemsSourceProperty, b3);
            }
            else
            {
                return App.Current.Resources["DumbEditTemplate"] as DataTemplate;
            }
            return dt;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var ct = (EtcCommand.EtcCommandType)values[0];
            Int16 val;
            if (values[1] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            val = System.Convert.ToInt16(values[1]);
            switch (ct)
            {
                case EtcCommand.EtcCommandType.CONTROL:
                    return (EtcCommand.EtcControlType)val;
                case EtcCommand.EtcCommandType.GFX:
                case EtcCommand.EtcCommandType.GFX2:
                    var dict = ResourceManager.Load("VFX");
                    if (dict.ContainsKey(val))
                        return dict[val];
                    break;
            }

            return val;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var rt = new object[2];
            rt[0] = Binding.DoNothing;
            if (value != null && value.GetType() == typeof(IndexReference))
            {
                rt[1] = System.Convert.ToUInt32(((IndexReference)value).Value);
            }
            else
            {
                rt[1] = value;
            }
            return rt;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (EtcCommand.EtcCommandType)value;
            switch (t)
            {
                case EtcCommand.EtcCommandType.CONTROL:
                    return Enum.GetValues(typeof(EtcCommand.EtcControlType));
                case EtcCommand.EtcCommandType.GFX:
                case EtcCommand.EtcCommandType.GFX2:
                    return ResourceManager.Load("VFX").Values;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

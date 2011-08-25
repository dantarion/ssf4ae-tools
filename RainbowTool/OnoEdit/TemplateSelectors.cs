using System;
using System.Collections.Generic;
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
            obj[0] = parameter;
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
            throw new NotImplementedException();
        }
    }
    public class AnimationComboBoxTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var ac = item as AnimationCommand;
            if (ac == null)
                return null;
            /*


            var dt = new DataTemplate();
            var factory = new FrameworkElementFactory(typeof(ComboBox));
            dt.VisualTree = factory;
            //Binding
            //Setup Multibinding
            var mb = new MultiBinding();
            mb.Converter = new IndexReferenceTypeConverter();
            var bind1 = new Binding("Type");
            bind1.Mode = BindingMode.OneWay;
            mb.Bindings.Add(bind1);
            mb.Bindings.Add(new Binding("Animation"));
            factory.SetValue(ComboBox.SelectedItemProperty, mb);

            var b = new Binding("Type");
            b.Mode = BindingMode.OneTime;
            b.Converter = new DictConverter();
            factory.SetValue(ComboBox.ItemsSourceProperty, b);
            return dt;
             * */
            return null;
        }
    }
    public class AnimationLabelTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var ac = item as AnimationCommand;
            if (ac == null)
                return null;
            var dt = new DataTemplate();
            var factory = new FrameworkElementFactory(typeof(Label));
            dt.VisualTree = factory;

            //Setup Multibinding
            var mb = new MultiBinding();
            mb.Converter = new IndexReferenceTypeConverter();
            var bind1 = new Binding("Type");
            bind1.Mode = BindingMode.OneWay;
            mb.Bindings.Add(bind1);
            mb.Bindings.Add(new Binding("Animation"));
            
            factory.SetValue(Label.ContentProperty, mb);
            
            return dt;
        }
    }
}

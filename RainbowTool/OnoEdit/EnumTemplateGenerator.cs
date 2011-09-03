using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Reflection;
using System.Globalization;

namespace OnoEdit
{
    public class EnumTemplateGenerator : DataTemplateSelector
    {
        private Type enumType;
        private string property;
        private bool _edit;
        public EnumTemplateGenerator(Type type, String prop, bool edit)
        {
            enumType = type;
            property = prop;
            _edit = edit;
        }
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var datatemplate = new DataTemplate();
            if (item == null)
                return datatemplate;
            if (_edit)
            {
                datatemplate.VisualTree = new FrameworkElementFactory(typeof(StackPanel));
                var converter = new EnumValueConverter(item,property);
                foreach (object value in Enum.GetValues(enumType))
                {
                    var cbox = new FrameworkElementFactory(typeof(CheckBox));
                    cbox.SetValue(CheckBox.ContentProperty, value.ToString());
                    Delegate add = (RoutedEventHandler)delegate(object obj, RoutedEventArgs e) { };
                    var b = new Binding(property);
                    b.Converter = converter;
                    b.ConverterParameter = value;
                    b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    cbox.SetValue(CheckBox.IsCheckedProperty, b);
                    cbox.AddHandler(CheckBox.CheckedEvent, add);
                    cbox.AddHandler(CheckBox.UncheckedEvent, add);
                    datatemplate.VisualTree.AppendChild(cbox);
                }
            }
            else
            {
                datatemplate.VisualTree = new FrameworkElementFactory(typeof(Label));
                datatemplate.VisualTree.SetValue(Label.ContentProperty, new Binding(property));
            }

            return datatemplate;
        }
        public class EnumValueConverter : IValueConverter
        {
            private object obj;
            private string propname;
            public EnumValueConverter(object obj, string propname)
            {
                this.obj = obj;
                this.propname = propname;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                long mask = System.Convert.ToInt64(parameter);
                var val = obj.GetType().GetProperty(propname).GetValue(obj, null);
                var cur =  System.Convert.ToInt64(val);

                return ((mask & cur) != 0);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var val = System.Convert.ToInt64(obj.GetType().GetProperty(propname).GetValue(obj, null));
                val ^= System.Convert.ToInt64(parameter);
                return Enum.Parse(targetType, val.ToString());
            }
        }
    }

}

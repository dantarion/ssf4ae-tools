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
            InitializeComponent(); ;
        }
        private int nextIndex = 0;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
                nextIndex = (int)((dynamic)(e.RemovedItems[0])).Type;
        }

        private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && ComboBox != null)
                ComboBox.SelectedIndex = nextIndex;
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            processColumns(e,DataGrid,RawEnumCheckbox);
        }

        public static void processColumns(DataGridAutoGeneratingColumnEventArgs e, DataGrid DataGrid, CheckBox raw)
        {
            DataGrid.FontFamily = new FontFamily("Consolas");
            if (e.PropertyType == typeof(RainbowLib.BCM.CancelList))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BCMFile.CancelLists;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.DisplayMemberPath = "Name";
                col.Header = "Cancel";
                e.Column = col;
            }
            if (e.PropertyType == typeof(RainbowLib.BAC.Script))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BACFile.Scripts;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.DisplayMemberPath = "Name";
                col.Header = "Script";
                e.Column = col;
            }
            if (e.PropertyName.Contains("Unknown") && e.PropertyType != typeof(float))
            {
                var col = new DataGridTextColumn();
                //col.ItemsSource = Enum.GetValues(e.PropertyType);
                var b = new Binding(e.PropertyName);
                col.Binding = b;
                b.Converter = new HexConverter();
                //col.DisplayMemberPath = "Name";
                col.Header = e.PropertyName.Replace("Unknown","Unk");
                e.Column = col;
            }
            if (e.PropertyType.BaseType == typeof(Enum) && raw.IsChecked.Value)
            {
                var col = new DataGridTextColumn();
                //col.ItemsSource = Enum.GetValues(e.PropertyType);
                var b = new Binding(e.PropertyName);
                col.Binding = b;
                b.StringFormat = "X";
                //b.Converter = new HexConverter();
                //col.DisplayMemberPath = "Name";
                col.Header = e.PropertyName;
                e.Column = col;
            }
            else
                if (e.PropertyType.BaseType == typeof(Enum) && !Attribute.IsDefined(e.PropertyType, typeof(FlagsAttribute)))
                {
                    var col = new DataGridComboBoxColumn();
                    col.ItemsSource = Enum.GetValues(e.PropertyType);
                    col.SelectedItemBinding = new Binding(e.PropertyName);
                    col.Header = e.PropertyName;
                    e.Column = col;
                }
                else if (e.PropertyType.BaseType == typeof(Enum))
                {
                    var col = new DataGridTextColumn();
                    //col.ItemsSource = Enum.GetValues(e.PropertyType);
                    col.Binding = new Binding(e.PropertyName);
                    //col.DisplayMemberPath = "Name";
                    col.Header = e.PropertyName;
                    e.Column = col;
                }
            if (e.PropertyName == "Raw")
                e.Cancel = true;
            if (e.PropertyName == "StartFrame")
            {
                e.Column.Header = "S";
                e.Column.DisplayIndex = 0;
            }
            if (e.PropertyName == "EndFrame")
            {
                e.Column.Header = "E";
                e.Column.DisplayIndex = 1;
            }
            if (e.PropertyName == "Animation" && !raw.IsChecked.Value)
            {
                var column = new DataGridTemplateColumn();
                column.CellTemplate = App.Current.Resources["AnimationViewTemplate"] as DataTemplate;
                column.CellEditingTemplate = App.Current.Resources["AnimationEditTemplate"] as DataTemplate;
                column.Header = e.PropertyName;
                e.Column = column;

            }
            if (e.PropertyName == "HitGFX" && !raw.IsChecked.Value)
            {
                var c = new DataGridComboBoxColumn();
                var b = new Binding(e.PropertyName);
                
                b.Converter = new IndexReferenceTypeConverter();
                var data = RainbowLib.ResourceManager.Load("VFX2");
                b.ConverterParameter = data;
                c.ItemsSource = data.Values;
                c.Header = e.PropertyName;
                //c.SelectedValuePath = "Value";
                c.DisplayMemberPath = "Name";
                c.SelectedValueBinding = b;
                e.Column = c;
            }
            if (e.PropertyName == "ShortParam" && !raw.IsChecked.Value)
            {
                var column = new DataGridTemplateColumn();
                column.CellTemplateSelector = new EtcTemplateSelector(false);
                column.CellEditingTemplateSelector = new EtcTemplateSelector(true);
                column.Header = e.PropertyName;
                e.Column = column;
            }
            // start anotak
            if (e.PropertyName == "RawString")
            {
                // this hurt my braaaaaaiinnnsss
                var col = new DataGridTextColumn();
                var b = new Binding(e.PropertyName);
                col.Binding = b;
                col.EditingElementStyle = (Style)Application.Current.Resources["RawStringBox"];
                if (col.EditingElementStyle == null)
                    throw new Exception("rawstring style problem");
                col.Header = "RawString";
                e.Column = col;
            }
            // end anotak
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
           dynamic list = ComboBox.SelectedValue;
           list.Commands.Add(list.GenerateCommand());
           // DataGrid.ItemsSource as 
        }

        private void RemoveCommand(object sender, RoutedEventArgs e)
        {
            List<object> tmpList = new List<object>();
            foreach (object obj in DataGrid.SelectedItems)
                tmpList.Add(obj);
            foreach (object obj in tmpList)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                list.Commands.Remove(item);
            }
        }
        private void MoveToTopCommand(object sender, RoutedEventArgs e)
        {
            List<object> tmpList = new List<object>();
            foreach (object obj in DataGrid.SelectedItems)
                tmpList.Add(obj);
            foreach (object obj in tmpList)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                list.Commands.Remove(item);
                list.Commands.Insert(0, item);
            }
        }
        private void MoveToBottomCommand(object sender, RoutedEventArgs e)
        {
            List<object> tmpList = new List<object>();
            foreach (object obj in DataGrid.SelectedItems)
                tmpList.Add(obj);
            foreach (object obj in tmpList)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                list.Commands.Remove(item);
                list.Commands.Add(item);
            }
        }
        private void MoveUpCommand(object sender, RoutedEventArgs e)
        {
            List<object> tmpList = new List<object>();
            foreach (object obj in DataGrid.SelectedItems)
                tmpList.Add(obj);
            foreach (object obj in tmpList)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                var index = Math.Max(0, list.Commands.IndexOf(item) - 1);
                list.Commands.Remove(item);
                list.Commands.Insert(index, item);
            }
        }
        private void MoveDownCommand(object sender, RoutedEventArgs e)
        {
            List<object> tmpList = new List<object>();
            foreach (object obj in DataGrid.SelectedItems)
                tmpList.Add(obj);
            foreach (object obj in tmpList)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                var index = Math.Min(list.Commands.Count - 1, list.Commands.IndexOf(item) + 1);
                list.Commands.Remove(item);
                list.Commands.Insert(index, item);
            }
        }
        private void DuplicateCommand(object sender, RoutedEventArgs e)
        {
            int startindex = DataGrid.Items.IndexOf(DataGrid.SelectedItems[DataGrid.SelectedItems.Count - 1]);
            foreach (object obj in DataGrid.SelectedItems)
            {
                dynamic list = ComboBox.SelectedValue;
                dynamic item = obj;
                var index = startindex+DataGrid.SelectedItems.IndexOf(obj);
                dynamic clone = item.Clone();
                list.Commands.Insert(index, clone);
            }
        }
        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataGrid.SelectedValue == null)
                e.Handled = true;

        }
    }

    public class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return string.Format("{0:X}",value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var val = Int32.Parse((string)value, System.Globalization.NumberStyles.HexNumber);
            return val;
        }
    }
}

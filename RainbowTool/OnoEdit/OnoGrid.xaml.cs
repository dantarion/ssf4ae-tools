using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Globalization;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for OnoGrid.xaml
    /// </summary>
    public partial class OnoGrid : OnoEditControl
    {
        public OnoGrid()
        {
            InitializeComponent();
        }       

        private void ColumnGeneration(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Change to a fixed width font
            myDataGrid.FontFamily = new FontFamily("Consolas");

            /*
             * Comboboxes 
             */
            //Charges
            if (e.PropertyType == typeof(RainbowLib.BCM.Charge))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BCMFile.Charges;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.DisplayMemberPath = "Name";
                col.Header = "Charge";
                e.Column = col;
            }
            //CancelLists
            if (e.PropertyType == typeof(RainbowLib.BCM.CancelList))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BCMFile.CancelLists;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.DisplayMemberPath = "Name";
                col.Header = "Cancel";
                e.Column = col;
            }
            //InputMotions
            if (e.PropertyType == typeof(RainbowLib.BCM.InputMotion))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BCMFile.InputMotions;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.DisplayMemberPath = "Name";
                col.Header = "InputMotion";
                e.Column = col;
            }
            //HitBoxDataset
            if (e.PropertyType == typeof(RainbowLib.BAC.HitBoxDataset))
            {
                var col = new DataGridComboBoxColumn();
                col.ItemsSource = App.OpenedFiles.BACFile.HitboxTable;
                col.SelectedItemBinding = new Binding(e.PropertyName);
                col.Header = "HitboxData";
                e.Column = col;
            }
            //Scripts
            if (e.PropertyType == typeof(RainbowLib.BAC.Script))
            {
                if (RawCheckbox.IsChecked.Value)
                {
                    var col = new DataGridTextColumn();
                    //col.ItemsSource = Enum.GetValues(e.PropertyType);
                    var b = new Binding(e.PropertyName); // + ".Index"
                    col.Binding = b;
                    //b.StringFormat = "X";
                    b.Converter = new ScriptConverter();
                    //col.DisplayMemberPath = "Name";
                    col.Header = e.PropertyName;
                    col.Header = "Script";
                    e.Column = col;
                }
                else
                {
                    var col = new DataGridComboBoxColumn();
                    col.ItemsSource = App.OpenedFiles.BACFile.Scripts;
                    col.SelectedItemBinding = new Binding(e.PropertyName);
                    col.DisplayMemberPath = "Name";
                    col.Header = "Script";
                    e.Column = col;
                }
            }
            //Hex display and entry of Unknown params
            if (e.PropertyName.Contains("Unknown") && e.PropertyType != typeof(float))
            {
                var col = new DataGridTextColumn();
                //col.ItemsSource = Enum.GetValues(e.PropertyType);
                var b = new Binding(e.PropertyName);
                col.Binding = b;
                b.Converter = new HexConverter();
                //col.DisplayMemberPath = "Name";
                col.Header = e.PropertyName.Replace("Unknown", "Unk");
                e.Column = col;
            }
            //Raw Enum Display
            if (e.PropertyType.BaseType == typeof(Enum) && RawCheckbox.IsChecked.Value)
            {
                var col = new DataGridTextColumn();
                //col.ItemsSource = Enum.GetValues(e.PropertyType);
                var b = new Binding(e.PropertyName);
                col.Binding = b;

                Type type = typeof(EnumHexConverter<>).MakeGenericType(e.PropertyType);
                b.Converter = Activator.CreateInstance(type) as IValueConverter;
                
                //col.DisplayMemberPath = "Name";
                col.Header = e.PropertyName;
                e.Column = col;
            }
            //Enum Dispaly (ComboBox)
            else
                if (e.PropertyType.BaseType == typeof(Enum) && !Attribute.IsDefined(e.PropertyType, typeof(FlagsAttribute)))
                {
                    var col = new DataGridComboBoxColumn();
                    col.ItemsSource = Enum.GetValues(e.PropertyType);
                    col.SelectedItemBinding = new Binding(e.PropertyName);
                    col.Header = e.PropertyName;
                    e.Column = col;
                }// Enum Display (Checkboxes) for flags
                else if (e.PropertyType.BaseType == typeof(Enum))
                {
                    var col = new DataGridTemplateColumn();
                    col.CellTemplateSelector = new EnumTemplateGenerator(e.PropertyType, e.PropertyName, false);
                    col.CellEditingTemplateSelector = new EnumTemplateGenerator(e.PropertyType, e.PropertyName, true);
                    col.Header = e.PropertyName;
                    e.Column = col;
                }
            if (e.PropertyName == "Raw" || e.PropertyName == "ScriptIndex"||(e.Column.IsReadOnly && e.PropertyName != "Name"))
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
            if (e.PropertyName == "Animation" && !RawCheckbox.IsChecked.Value)
            {
                var column = new DataGridTemplateColumn();
                column.CellTemplate = App.Current.Resources["AnimationViewTemplate"] as DataTemplate;
                column.CellEditingTemplate = App.Current.Resources["AnimationEditTemplate"] as DataTemplate;
                column.Header = e.PropertyName;
                e.Column = column;

            }
            if (e.PropertyName == "HitGFX" && !RawCheckbox.IsChecked.Value)
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
            if (e.PropertyName == "ShortParam" && !RawCheckbox.IsChecked.Value)
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

        public override ListCollectionView ListCollectionView
        {
            get { return (ListCollectionView)this.myDataGrid.ItemsSource; }
        }

        public override object[] SelectedItems
        {
            get 
            {
                return this.myDataGrid.SelectedItems.Cast<object>().ToArray();
            }
        } 

        public override void ScrollCurrent()
        {
            myDataGrid.ScrollIntoView(ListCollectionView.CurrentItem);
        }
        private void RawCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            myDataGrid.AutoGenerateColumns = false;
            myDataGrid.AutoGenerateColumns = true;
        }
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
  "SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(OnoGrid));

        public event SelectionChangedEventHandler SelectionChanged
        {
            add { myDataGrid.SelectionChanged += value; }
            remove { myDataGrid.SelectionChanged -= value; }
        }
        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(OnoGrid), new UIPropertyMetadata(null));

        private void myDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var lc = this.ListCollectionView;
            if(lc != null)
            {
                if (lc.IsAddingNew)
                {
                    lc.CommitNew();
                }

                if (lc.IsEditingItem)
                {
                    lc.CommitEdit();
                }
            }
        }
    }

    public class ScriptConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return -1;
            }
            return string.Format("{0:X}", (value as RainbowLib.BAC.Script).Index);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            Int32 index = Int32.Parse((string)value, System.Globalization.NumberStyles.HexNumber);
            RainbowLib.AELogger.Log(index.ToString() + " and " + App.OpenedFiles.BACFile.Scripts.Count);
            RainbowLib.BAC.Script scr = App.OpenedFiles.BACFile.Scripts.Where(x => x.Index == index).First();
            return scr;
        }
    }
}

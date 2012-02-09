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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for OnoList.xaml
    /// </summary>
    public partial class OnoList : OnoEditControl
    {
        public OnoList()
        {
            InitializeComponent();
           // ListCollectionView().Filter = myFilter;
            CopyICommand vCopyCommand = new CopyICommand(this);
            KeyBinding CopyCmdKeyBinding = new KeyBinding(
                vCopyCommand,
                Key.C,
                ModifierKeys.Control);
            myDataGrid.InputBindings.Add(CopyCmdKeyBinding);

            PasteICommand vPasteCommand = new PasteICommand(this);
            KeyBinding PasteCmdKeyBinding = new KeyBinding(
                vPasteCommand,
                Key.V,
                ModifierKeys.Control);
            myDataGrid.InputBindings.Add(PasteCmdKeyBinding);
        }

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        "SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(OnoList));

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
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(OnoList), new UIPropertyMetadata(null));

        public override ListCollectionView ListCollectionView
        {
            get { return this.myDataGrid.ItemsSource as ListCollectionView; }
        }

        public override object[] SelectedItems
        {
            get { return this.myDataGrid.SelectedItems.Cast<object>().ToArray(); }
        }

        public override void ScrollCurrent()
        {
            myDataGrid.ScrollIntoView(ListCollectionView.CurrentItem);
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.RoutedEvent = SelectionChangedEvent;
            RaiseEvent(e);

        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted =  !e.Item.GetType().GetProperty("Name").GetValue(e.Item, null).Equals("NONE");
        }
    }
}

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections;

namespace OnoEdit
{
    public class OnoEditControl : UserControl
    {
        public OnoEditControl()
        {
        }


        public object[] Copy
        {
            get { return (object[])GetValue(CopyProperty); }
            set { SetValue(CopyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Copy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CopyProperty =
            DependencyProperty.Register("Copy", typeof(object[]), typeof(OnoEditControl), new UIPropertyMetadata(null));

        public int FrozenColumns
        {
            get { return (int)GetValue(FrozenColumnsProperty); }
            set { SetValue(FrozenColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FrozenColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrozenColumnsProperty =
            DependencyProperty.Register("FrozenColumns", typeof(int), typeof(OnoGrid), new UIPropertyMetadata(0));

        public bool EditEnabled
        {
            get { return (bool)GetValue(EditEnabledProperty); }
            set { SetValue(EditEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditEnabledProperty =
            DependencyProperty.Register("EditEnabled", typeof(bool), typeof(OnoGrid), new UIPropertyMetadata(true));

        public virtual ListCollectionView ListCollectionView
        {
            get { return null; }
        }

        public virtual object[] SelectedItems
        {
            get { return null; }
        }

        public virtual void ScrollCurrent() { }

        protected virtual void RemoveCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;
            
            //if (lc.IsAddingNew)
            //    lc.CancelNew();
            //else
            //{
                
            lc.CommitEdit();
            foreach (var item in this.SelectedItems.ToArray())
            {
                lc.Remove(item);
            }
        }

        protected virtual void MoveToTopCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;
            var current = lc.CurrentItem;
            var list = lc.SourceCollection as System.Collections.IList;
            list.Remove(current);
            list.Insert(0, current);
            lc.MoveCurrentTo(current);
            ScrollCurrent();
        }

        protected virtual void MoveDownCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;
            var current = lc.CurrentItem;
            var list = lc.SourceCollection as System.Collections.IList;
            var index = list.IndexOf(current);
            list.Remove(current);
            list.Insert(index + 1, current);
            lc.MoveCurrentTo(current);
            ScrollCurrent();
        }

        protected virtual void MoveUpCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;
            var current = lc.CurrentItem;
            var list = lc.SourceCollection as System.Collections.IList;
            var index = list.IndexOf(current);
            list.Remove(current);
            list.Insert(Math.Max(0, index - 1), current);
            lc.MoveCurrentTo(current);
            ScrollCurrent();
        }

        protected virtual void MoveToBottomCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;
            var current = lc.CurrentItem;
            var list = lc.SourceCollection as System.Collections.IList;
            var index = list.IndexOf(current);
            list.Remove(current);
            list.Add(current);
            lc.MoveCurrentTo(current);
            ScrollCurrent();
        }

        protected virtual void AddCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView;

            var newObj = lc.AddNew();
            checkScriptIndex(newObj);
            ScrollCurrent();
        }

        private void checkScriptIndex(object newObj)
        {
            var script = newObj as RainbowLib.BAC.Script;
            if (script != null)
            {
                var existingScripts = this.ListCollectionView.SourceCollection.Cast<RainbowLib.BAC.Script>();
                script.Index = existingScripts.Any() ? existingScripts.Max(s => s.Index) + 1 : 1;
            }
        }

        protected virtual void CopyCommand(object sender, RoutedEventArgs e)
        {
            this.Copy = this.SelectedItems;
        }

        protected virtual void PasteCommand(object sender, RoutedEventArgs e)
        {
            this.DoPaste(this.Copy);
        }

        protected virtual void DuplicateCommand(object sender, RoutedEventArgs e)
        {
            this.DoPaste(this.SelectedItems);
        }

        private void DoPaste(object[] items)
        {
            if (items == null || items.Length == 0)
            {
                return;
            }

            var view = this.ListCollectionView;
            var source = (IList)view.SourceCollection;

            var index = this.SelectedItems.Max(item => source.IndexOf(item));
            try
            {
                foreach (var item in items)
                {
                    var clone = RainbowLib.Cloner.Clone(item);
                    checkScriptIndex(clone);
                    source.Insert(++index, clone);
                }

                view.MoveCurrentTo(source[index]);
                ScrollCurrent();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot paste that here!");
            }
        }

        protected new virtual void ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (!EditEnabled)
                e.Handled = true;
        }
    }

    public class ClipboardToStringConverter : IValueConverter
    {
        public static readonly ClipboardToStringConverter Instance = new ClipboardToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as object[];
            if (items == null || items.Length == 0)
            {
                return null;
            }

            return string.Format(
                "{0}[{1}] {{ {2} }}",
                items[0].GetType().Name,
                items.Length,
                string.Join(", ", items));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

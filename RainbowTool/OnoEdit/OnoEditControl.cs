using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace OnoEdit
{
    public class OnoEditControl : UserControl
    {
        public OnoEditControl()
        {
        }


        public object Copy
        {
            get { return (object)GetValue(CopyProperty); }
            set { SetValue(CopyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Copy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CopyProperty =
            DependencyProperty.Register("Copy", typeof(object), typeof(OnoEditControl), new UIPropertyMetadata(null));

        
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

        public virtual ListCollectionView ListCollectionView()
        {
            return null;
        }
        public virtual void ScrollCurrent()
        {
           
        }
        protected virtual void DuplicateCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView();
            var current = lc.CurrentItem;
            var clone = RainbowLib.Cloner.Clone(current);
             var list = lc.SourceCollection as System.Collections.IList;
             checkScriptIndex(clone);
           
            var index = list.IndexOf(current);
            list.Insert(index + 1, clone);
            lc.MoveCurrentTo(clone);
            ScrollCurrent();

        }

        protected virtual void RemoveCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView();
            if (lc.IsAddingNew)
                lc.CancelNew();
            else
            {
                lc.CommitEdit();
                lc.Remove(lc.CurrentItem);
            }
        }

        protected virtual void MoveToTopCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView();
            var current = lc.CurrentItem;
            var list = lc.SourceCollection as System.Collections.IList;
            list.Remove(current);
            list.Insert(0, current);
            lc.MoveCurrentTo(current);
            ScrollCurrent();
        }

        protected virtual void MoveDownCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView();
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
            ListCollectionView lc = ListCollectionView();
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
            ListCollectionView lc = ListCollectionView();
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
            ListCollectionView lc = ListCollectionView();

            var newObj = lc.AddNew();
            checkScriptIndex(newObj);
            ScrollCurrent();
        }

        private void checkScriptIndex(object newObj)
        {
            if (newObj.GetType() == typeof(RainbowLib.BAC.Script))
            {
                int newIndex = 0;
                foreach (RainbowLib.BAC.Script script in ListCollectionView().SourceCollection as System.Collections.IList)
                {
                    newIndex = Math.Max(newIndex, script.Index);
                }
                (newObj as RainbowLib.BAC.Script).Index = newIndex + 1;
            }
        }
        protected virtual void PasteCommand(object sender, RoutedEventArgs e)
        {
            if (Copy == null)
                return;
            ListCollectionView lc = ListCollectionView();
            var current = lc.CurrentItem;
            var clone = RainbowLib.Cloner.Clone(Copy);
            var list = lc.SourceCollection as System.Collections.IList;
            var index = list.IndexOf(current);
            try
            {
                checkScriptIndex(clone);
                list.Insert(index + 1, clone);
                lc.MoveCurrentTo(clone);
                ScrollCurrent();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot paste that here!");
            }

        }

        protected virtual void CopyCommand(object sender, RoutedEventArgs e)
        {
            ListCollectionView lc = ListCollectionView();
            var current = lc.CurrentItem;
            Copy = current;
        }

        protected new virtual void ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (!EditEnabled)
                e.Handled = true;
        }
    }
}

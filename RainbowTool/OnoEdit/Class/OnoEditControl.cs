using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections;
using System.Windows.Input;
using RainbowLib.BAC;

namespace OnoEdit
{
    public class OnoEditControl : UserControl
    {
        public OnoEditControl()
        {
            this.PreviewKeyDown += this.Base_PreviewKeyDown;
        }

        private void Base_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift)) // && IsKeyboardFocused
            {
                if (e.Key == Key.C) 
                    this.CopyCommand(sender, e);
                else if (e.Key == Key.V) 
                    this.PasteCommand(sender, e);
            }
        }

        public object[] Copy
        {
            get
            {
                //Just deep copy the ECT

                var obj = GetValue(CopyProperty) as Object[];

                if (obj[0].GetType().ToString() == "RainbowLib.BAC.EtcCommand")
                    return ObjectExtensions.Clone((object[])GetValue(CopyProperty));
                else
                    return (GetValue(CopyProperty) as Object[]);

            }
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

        //Used for new script
        public String WorkingType { get; set; }

        protected virtual void AddCommand(object sender, RoutedEventArgs e)
        {

            if(WorkingType != null)
            {
                //New script

                Console.WriteLine(WorkingType);

                var newscript = new Script(App.OpenedFiles.BACFile.VFXScripts.Count, "New Script")
                                    {
                                        FirstHitboxFrame = 0,
                                        LastHitboxFrame = 0,
                                        IASAFrame = 0,
                                        TotalFrames = 100,
                                        UnknownFlags1 = 0,
                                        UnknownFlags2 = 0,
                                        UnknownFlags3 = 0
                                    };


                foreach (var type in Enum.GetValues(typeof(CommandListType)))
                {
                    var cmd = CommandListFactory.ByType((CommandListType) type);
                    cmd.Type = (CommandListType)type;
                    newscript.CommandLists.Add(cmd);
                }
                switch(WorkingType)
                {
                    case "VFXScripts":
                        App.OpenedFiles.BACFile.VFXScripts.Add(newscript);
                        break;
                    case "Scripts":
                        App.OpenedFiles.BACFile.Scripts.Add(newscript);
                        break;
                    default:
                        Console.WriteLine("No valid ID"); break;
                }
                return;
            }

            try
            {                              
                ListCollectionView lc = ListCollectionView;
                            
                if(!lc.CanAddNew) return;

                var newObj = lc.AddNew();
                CheckScriptIndex(newObj);
                ScrollCurrent();
            }
            catch
            {
                MessageBox.Show("This feature is unavailable at this time.");
            }
        }

        private void CheckScriptIndex(object newObj)
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

            int index = source.Count > 0 ? this.SelectedItems.Max(item => source.IndexOf(item)) : 0;

            try
            {
                foreach (var item in items)
                {
                    var clone = RainbowLib.Cloner.Clone(item);
                    CheckScriptIndex(clone);
                    if (source.Count == 0)
                    {
                        source.Add(clone);
                    }
                    else
                    {
                        source.Insert(++index, clone);
                    }
                }

                view.MoveCurrentTo(source[index]);
                ScrollCurrent();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot paste that here!");
                RainbowLib.AELogger.Log("Paste exception: " + e.Message);
                RainbowLib.AELogger.Log("Paste index: " + index);
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

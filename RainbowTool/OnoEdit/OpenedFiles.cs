using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using RainbowLib;
namespace OnoEdit
{
    public class OpenedFiles : DependencyObject
    {
        public bool FilesOpened
        {
            get { return (bool)GetValue(FilesOpenedProperty); }
            set { SetValue(FilesOpenedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BCMFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilesOpenedProperty =
            DependencyProperty.Register("FilesOpened", typeof(bool), typeof(OpenedFiles), new UIPropertyMetadata(false));  
        public BACFile BACFile
        {
            get { return (BACFile)GetValue(BACFileProperty); }
            set { SetValue(BACFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BCMFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BACFileProperty =
            DependencyProperty.Register("BACFile", typeof(BACFile), typeof(OpenedFiles), new UIPropertyMetadata(null));   
        public BCMFile BCMFile
        {
            get { return (BCMFile)GetValue(BCMFileProperty); }
            set { SetValue(BCMFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BCMFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BCMFileProperty =
            DependencyProperty.Register("BCMFile", typeof(BCMFile), typeof(OpenedFiles), new UIPropertyMetadata(null));

        public AELogger Log
        {
            get { return (AELogger)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        }
        public static readonly DependencyProperty LogProperty =
            DependencyProperty.Register("Log", typeof(AELogger), typeof(OpenedFiles), new UIPropertyMetadata(null));
    }
}

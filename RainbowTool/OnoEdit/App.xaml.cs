using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using RainbowLib;
namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OpenedFiles OpenedFiles
        {
            get
            {
                return Application.Current.Resources["OpenedFiles"] as OpenedFiles;
            }

        }
    }
}

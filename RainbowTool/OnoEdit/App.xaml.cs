using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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

        static bool IsValidAsPath(String cPath)
        {
            try
            {
                var fi = new FileInfo(cPath); //Let path do the hard work, why re-invent the wheel?

                if (!fi.Extension.Equals(".bcm"))
                {
                    fi = null;
                    GC.Collect();
                    return false;
                }

                fi = null;
                GC.Collect();
                return true;
            }
            catch(Exception er)
            {
                Console.WriteLine(er);
                return false;
            }

        }

        private static String ArgumentPath { get; set; }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            UserSettings.Load();

            if (e.Args.Length > 0)
            {
                foreach (var str in e.Args.Where(IsValidAsPath))
                {
                    ArgumentPath = str;

                    break;
                }

            }

            if (ArgumentPath != null)
                new MainWindow(ArgumentPath).Show();
            else
                new MainWindow().Show();

        }
    }
}

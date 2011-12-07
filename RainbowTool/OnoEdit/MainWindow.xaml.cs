using System.Windows;
using Microsoft.Win32;
using System.Security.Principal;
using RainbowLib;
using System.Windows.Input;
using System.Reflection;
using System;
using System.Threading;
namespace OnoEdit
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // start anotak
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ApplicationThreadException);
            // end anotak
            InitializeComponent();
            //CommandBindings
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, ClickOpen));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, ClickSave, FilesOpened));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, ClickSaveAs, FilesOpened));

            this.Title = "Ono! " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            //Build Timestamp
            var version = Assembly.GetEntryAssembly().GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(
            TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
            TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
                        //CommandManager.AddExecutedHandler(ApplicationCommands.Open, ClickOpen);
            BuildTime.Content = "Build Date: "+buildDateTime.ToShortDateString();

            AELogger.Log("Build Date: " + buildDateTime.ToShortDateString(), false);
        }

        private void FilesOpened(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = App.OpenedFiles.FilesOpened;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // App.OpenedFiles.FileOpened = false;   
        }

        private void ClickCharges(object sender, RoutedEventArgs e)
        {
            new ChargeWindow().Show();
        }
        private void ClickInputs(object sender, RoutedEventArgs e)
        {
            new InputWindow().Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit Ono!?", "Quitting?", MessageBoxButton.YesNo);

            // start anotak edit
            if (result == MessageBoxResult.Yes)
            {
                AELogger.WriteLog();
                App.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
            // end anotak edit
        }

        private void ClickCancels(object sender, RoutedEventArgs e)
        {
            new CancelListWindow().Show();
        }

        private void ClickMoves(object sender, RoutedEventArgs e)
        {
            new MoveWindow().Show();
        }
        private void ClickScripts(object sender, RoutedEventArgs e)
        {
            new ScriptWindow("Scripts").Show();
        }
        private void ClickVFXScripts(object sender, RoutedEventArgs e)
        {
            new ScriptWindow("VFXScripts").Show();
        }
        private void ClickHitboxTable(object sender, RoutedEventArgs e)
        {
            new HitboxDataWindow().Show();
        }
        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            var sb = new OpenFileDialog();
            sb.Filter = "SF4AE BCM Files|*.bcm";
            var result = sb.ShowDialog(this);
            if ((bool)result.Value)
            {
                App.OpenedFiles.BCMFile = BCMFile.FromFilename(sb.FileName);
                string bacfile = sb.FileName.Replace(".bcm", ".bac");
                App.OpenedFiles.BACFile = BACFile.FromFilename(bacfile, App.OpenedFiles.BCMFile);
                opened = sb.FileName;
                RainbowLib.ResourceManager.LoadCharacterData(System.IO.Path.GetFileNameWithoutExtension(opened));
                App.OpenedFiles.FilesOpened = true;
            }

        }
        private string opened = null;
        private void ClickSave(object sender, RoutedEventArgs e)
        {
            /*
            var sb = new SaveFileDialog();
            sb.InitialDirectory = System.IO.Path.GetDirectoryName(opened);
            bool result = sb.ShowDialog(this);
            if(result)*/
            var result = MessageBox.Show("Are you sure you want to overwrite \""+opened+"\" ?", "Overwrite File", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                BCMFile.ToFilename(opened, App.OpenedFiles.BCMFile);
                BACFile.ToFilename(opened.Replace(".bcm",".bac"), App.OpenedFiles.BACFile, App.OpenedFiles.BCMFile);
            }
        }
        private void ClickSaveAs(object sender, RoutedEventArgs e)
        {
            
            var sb = new SaveFileDialog();
            sb.InitialDirectory = System.IO.Path.GetDirectoryName(opened);
            sb.Filter = "SF4AE BCM Files|*.bcm";
            sb.AddExtension = true;
            sb.OverwritePrompt = true;
            bool result = (bool)sb.ShowDialog(this);
            if (result)
            {
                opened = sb.FileName;
                BCMFile.ToFilename(sb.FileName,App.OpenedFiles.BCMFile);
                BACFile.ToFilename(sb.FileName.Replace(".bcm", ".bac"), App.OpenedFiles.BACFile, App.OpenedFiles.BCMFile);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("#sf4-modding@irc.synirc.net\n code.google.com/p/ssf4ae-tools/");
        }

        // start anotak
        public void ApplicationThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            if (((Exception)e.ExceptionObject) != null)
            {
                AELogger.Log("Exception: " + ((Exception)e.ExceptionObject).Message);

                AELogger.Log("Exception: " + ((Exception)e.ExceptionObject).StackTrace);

                if (((Exception)e.ExceptionObject).InnerException != null)
                {
                    AELogger.Log("InnerException: " + ((Exception)e.ExceptionObject).InnerException.ToString());
                }
                MessageBox.Show(((Exception)e.ExceptionObject).Message + "\n" + ((Exception)e.ExceptionObject).StackTrace, "Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AELogger.Log("ERROR THROWING EXCEPTION EVERYTHING IS BROKEN");
                MessageBox.Show("error properly displaying the error, something really bad happened.", "Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            AELogger.WriteLog();
            #if (DEBUG)
            #else
            Application.Current.Shutdown();
            #endif
        }

        private void IRC_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://widget.mibbit.com/?server=irc.synirc.net&nick=ono_user%3F%3F%3F&channel=%23sf4-modding");
        }

        private void GC_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/ssf4ae-tools/");
        }
        // end anotak
    }
}

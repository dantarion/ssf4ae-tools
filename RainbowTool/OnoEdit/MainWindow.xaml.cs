using System.Windows;
using Microsoft.Win32;
using System.Security.Principal;
using RainbowLib;
using System.Windows.Input;
using System.Reflection;
using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq;

namespace OnoEdit
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _opened = null;
        public static string Opened
        {
            get
            {
                return System.IO.Path.GetFileName(_opened);
            }
        }

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
            App.OpenedFiles.Log = AELogger.Logger;
            this.PreviewKeyDown += this.Base_PreviewKeyDown;
        }

        private void Base_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt)) // && IsKeyboardFocused
            {
                if (e.Key == Key.P && App.OpenedFiles.FilesOpened)
                {
                    MessageBoxResult result = MessageBox.Show("do you want to possibly break the char! check the log after",
                        "do you want to possibly break the char!", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        AELogger.Log("ok lets do this!");
                        int hitbox_count = App.OpenedFiles.BACFile.HitboxTable.Count;

                        for (int i = 0; i < hitbox_count; i++)
                        {
                            string cur = App.OpenedFiles.BACFile.HitboxTable[i].UsageString;
                            Regex reg = new Regex("^R?\\d+[LMH][PK]"); // normals
                            //if(reg.IsMatch(cur))
                            if (cur.Length != 0)
                            {
                                if(App.OpenedFiles.BACFile.HitboxTable[i].Data[8].Effect
                                    == RainbowLib.BAC.HitBoxData.HitBoxEffect.HIT &&
                                    App.OpenedFiles.BACFile.HitboxTable[i].Data[8].Damage > 0)
                                {
                                    int index = App.OpenedFiles.BACFile.HitboxTable[i].Data[8].OnHit.Index + 61;
                                    index = index < 192 ? 192 : index;
                                    AELogger.Log("match! " + cur + ". lets change HIT " + App.OpenedFiles.BACFile.HitboxTable[i].Data[8].OnHit.Name + " (index " + App.OpenedFiles.BACFile.HitboxTable[i].Data[8].OnHit.Index + " to " + index);
                                    RainbowLib.BAC.Script newHit = App.OpenedFiles.BACFile.Scripts.Where(x => x.Index == index).First();
                                    AELogger.Log("\tto BLOW " + newHit.Name);
                                    App.OpenedFiles.BACFile.HitboxTable[i].Data[8].Effect = RainbowLib.BAC.HitBoxData.HitBoxEffect.BLOW;
                                    App.OpenedFiles.BACFile.HitboxTable[i].Data[8].OnHit = newHit;
                                }

                                Regex jreg = new Regex("^[789][LM]");
                                if (jreg.IsMatch(cur))
                                {
                                    short curstun = App.OpenedFiles.BACFile.HitboxTable[i].Data[3].TgtAnimTime;
                                    short newstun = 14;
                                    if (curstun < newstun)
                                    {
                                        AELogger.Log("jumpmatch! " + cur + ". lets change blockstun " + curstun + " to " + newstun);
                                        App.OpenedFiles.BACFile.HitboxTable[i].Data[3].TgtAnimTime = newstun;
                                        App.OpenedFiles.BACFile.HitboxTable[i].Data[4].TgtAnimTime = newstun;
                                    }
                                }
                            }
                        }
                        int moves_count = App.OpenedFiles.BCMFile.Moves.Count;
                        for (int i = 0; i < moves_count; i++)
                        {
                            if(App.OpenedFiles.BCMFile.Moves[i].EXRequirement == 0)
                            {
                                Regex reg = new Regex("^R?\\d+[MH][PK]");
                                string cur = App.OpenedFiles.BCMFile.Moves[i].Name;
                                if (reg.IsMatch(cur))
                                {
                                    short oldmeter = App.OpenedFiles.BCMFile.Moves[i].EXCost;
                                    short newmeter = oldmeter;
                                    if (cur.Contains('M'))
                                    {
                                        newmeter = -4;
                                    }
                                    if (cur.Contains('H'))
                                    {
                                        newmeter = -7;
                                    }
                                    if (cur.Contains('9') && !cur.Contains('2'))
                                    {
                                        newmeter = -9;
                                    }
                                    if (cur.Contains('8'))
                                    {
                                        newmeter = -8;
                                    }
                                    if (cur.Contains("FEINT"))
                                    {
                                        newmeter = 0;
                                    }

                                    AELogger.Log("changing meter gain of " + cur + " from " + oldmeter + " to " + newmeter);

                                    App.OpenedFiles.BCMFile.Moves[i].EXCost = newmeter;
                                }

                                if (cur.StartsWith("APPEAL"))
                                {
                                    AELogger.Log("changing meter gain of " + cur + " from " + App.OpenedFiles.BCMFile.Moves[i].EXCost + " to " + 20);
                                    App.OpenedFiles.BCMFile.Moves[i].EXCost = 20;
                                }
                            }
                        }
                    }
                }
            }
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
                AELogger.Log("quitting");
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
                AELogger.Log("Opening BCM " + System.IO.Path.GetFileName(_opened));
                App.OpenedFiles.BCMFile = BCMFile.FromFilename(sb.FileName);
                string bacfile = sb.FileName.Replace(".bcm", ".bac");
                AELogger.Log("Opened BCM, Opening BAC " + System.IO.Path.GetFileName(_opened));
                App.OpenedFiles.BACFile = BACFile.FromFilename(bacfile, App.OpenedFiles.BCMFile);
                _opened = sb.FileName;
                RainbowLib.ResourceManager.LoadCharacterData(System.IO.Path.GetFileNameWithoutExtension(_opened));
                App.OpenedFiles.FilesOpened = true;
                AELogger.Log("Opened BAC " + System.IO.Path.GetFileName(_opened));
                this.Title = "Ono! " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " - " +  System.IO.Path.GetFileName(_opened);
            }
            
        }
        
        private void ClickSave(object sender, RoutedEventArgs e)
        {
            /*
            var sb = new SaveFileDialog();
            sb.InitialDirectory = System.IO.Path.GetDirectoryName(opened);
            bool result = sb.ShowDialog(this);
            if(result)*/
            var result = MessageBox.Show("Are you sure you want to overwrite \""+_opened+"\" ?", "Overwrite File", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                AELogger.Log("Saving BCM " + System.IO.Path.GetFileName(_opened));
                BCMFile.ToFilename(_opened, App.OpenedFiles.BCMFile);
                AELogger.Log("Saved BCM, Saving BAC " + System.IO.Path.GetFileName(_opened));
                BACFile.ToFilename(_opened.Replace(".bcm",".bac"), App.OpenedFiles.BACFile, App.OpenedFiles.BCMFile);
                AELogger.Log("Saved BAC " + System.IO.Path.GetFileName(_opened));
            }
        }
        private void ClickSaveAs(object sender, RoutedEventArgs e)
        {
            
            var sb = new SaveFileDialog();
            sb.InitialDirectory = System.IO.Path.GetDirectoryName(_opened);
            sb.Filter = "SF4AE BCM Files|*.bcm";
            sb.AddExtension = true;
            sb.OverwritePrompt = true;
            bool result = (bool)sb.ShowDialog(this);
            if (result)
            {
                _opened = sb.FileName;
                AELogger.Log("Saving BCM " + System.IO.Path.GetFileName(_opened));
                BCMFile.ToFilename(sb.FileName,App.OpenedFiles.BCMFile);
                AELogger.Log("Saved BCM, Saving BAC " + System.IO.Path.GetFileName(_opened));
                BACFile.ToFilename(sb.FileName.Replace(".bcm", ".bac"), App.OpenedFiles.BACFile, App.OpenedFiles.BCMFile);
                AELogger.Log("Saved BAC " + System.IO.Path.GetFileName(_opened));
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
            new AboutWindow().Show();
            //System.Diagnostics.Process.Start("http://code.google.com/p/ssf4ae-tools/");
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            new LogWindow().Show();
        }
        // end anotak
    }
}

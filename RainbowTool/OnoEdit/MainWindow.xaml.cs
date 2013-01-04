using System.Windows;
using System.Windows.Media;
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
            get { return Class.CharNames.GetName(System.IO.Path.GetFileName(_opened).Substring(0, 3)); }
        }

        public static String Aopened
        {
            get { return System.IO.Path.GetFileName(_opened); }
        }

        ~MainWindow()
        {
            UserSettings.Save();
        }

        public MainWindow()
        {
            // start anotak
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ApplicationThreadException);
            // end anotak
            InitializeComponent();
            //CommandBindings

            //Find Shell Library at http://archive.msdn.microsoft.com/WPFShell/Release/ProjectReleases.aspx?ReleaseId=4332

            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CurrentPropertyChanged);
            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Style = (Style)FindResource("AeroStyle");

            Class.CharNames.LoadDictionary();

            if (!UserSettings.CurrentSettings.WindowCollection.ContainsKey(Name))
                UserSettings.CurrentSettings.WindowCollection.Add(Name, new TypeSettings());
            else
            {
                Left = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.X;
                Top = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.Y;

                Width = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Width;
                Height = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Height;
            }

            recentFileList.MenuClick += (s, e) => RecentOpen(e.Filepath);

            exButtonCharge.OnClick += exButtonCharge_OnClick;
            exButtonInput.OnClick += exButtonInput_OnClick;
            exButtonMoves.OnClick += exButtonMoves_OnClick;
            exButtonCancels.OnClick += exButtonCancels_OnClick;
            exButtonScripts.OnClick += exButtonScripts_OnClick;
            exButtonVFX.OnClick += exButtonVFX_OnClick;
            exButtonHitBox.OnClick += exButtonHitBox_OnClick;

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

        public MainWindow(String openPath)
        {
            // start anotak
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ApplicationThreadException);
            // end anotak
            InitializeComponent();
            //CommandBindings

            //Find Shell Library at http://archive.msdn.microsoft.com/WPFShell/Release/ProjectReleases.aspx?ReleaseId=4332

            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CurrentPropertyChanged);
            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Style = (Style)FindResource("AeroStyle");

            Class.CharNames.LoadDictionary();

            if (!UserSettings.CurrentSettings.WindowCollection.ContainsKey(Name))
                UserSettings.CurrentSettings.WindowCollection.Add(Name, new TypeSettings());
            else
            {
                Left = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.X;
                Top = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.Y;

                Width = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Width;
                Height = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Height;
            }

            recentFileList.MenuClick += (s, e) => RecentOpen(e.Filepath);

            exButtonCharge.OnClick += exButtonCharge_OnClick;
            exButtonInput.OnClick += exButtonInput_OnClick;
            exButtonMoves.OnClick += exButtonMoves_OnClick;
            exButtonCancels.OnClick += exButtonCancels_OnClick;
            exButtonScripts.OnClick += exButtonScripts_OnClick;
            exButtonVFX.OnClick += exButtonVFX_OnClick;
            exButtonHitBox.OnClick += exButtonHitBox_OnClick;

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
            BuildTime.Content = "Build Date: " + buildDateTime.ToShortDateString();

            AELogger.Log("Build Date: " + buildDateTime.ToShortDateString(), false);
            App.OpenedFiles.Log = AELogger.Logger;
            this.PreviewKeyDown += this.Base_PreviewKeyDown;

            //Re-Use Already build function
            RecentOpen(openPath);
        }

        void CurrentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("IsGlassEnabled")) return;

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Style = (Style)FindResource("AeroStyle");
            else
                Style = null;
        }

        #region ButtonClicks

        void exButtonHitBox_OnClick(object sender, EventArgs e)
        {
            new HitboxDataWindow().Show();
        }

        void exButtonVFX_OnClick(object sender, EventArgs e)
        {
            new ScriptWindow("VFXScripts").Show();
        }

        void exButtonScripts_OnClick(object sender, EventArgs e)
        {
            new ScriptWindow("Scripts").Show();
        }

        void exButtonCancels_OnClick(object sender, EventArgs e)
        {
            new CancelListWindow().Show();
        }

        void exButtonMoves_OnClick(object sender, EventArgs e)
        {
            new MoveWindow().Show();
        }

        void exButtonInput_OnClick(object sender, EventArgs e)
        {
            new InputWindow().Show();
        }

        void exButtonCharge_OnClick(object sender, EventArgs e)
        {
            new ChargeWindow().Show();
        }

        #endregion

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

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            var sb = new OpenFileDialog {Filter = "SF4AE BCM Files|*.bcm"};
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
                App.OpenedFiles.FilesOpened = true; // yay
                recentFileList.InsertFile(sb.FileName);
                AELogger.Log("Opened BAC " + System.IO.Path.GetFileName(_opened));
                this.Title = "Editing " + Class.CharNames.GetName(System.IO.Path.GetFileName(_opened).Substring(0,3)) + " -Ono!";
            }
            
        }

        private void RecentOpen(String file)
        {
            AELogger.Log("Opening BCM " + System.IO.Path.GetFileName(_opened));
                App.OpenedFiles.BCMFile = BCMFile.FromFilename(file);
                string bacfile = file.Replace(".bcm", ".bac");
                AELogger.Log("Opened BCM, Opening BAC " + System.IO.Path.GetFileName(_opened));
                App.OpenedFiles.BACFile = BACFile.FromFilename(bacfile, App.OpenedFiles.BCMFile);
                _opened = file;
                RainbowLib.ResourceManager.LoadCharacterData(System.IO.Path.GetFileNameWithoutExtension(_opened));
                App.OpenedFiles.FilesOpened = true;
                AELogger.Log("Opened BAC " + System.IO.Path.GetFileName(_opened));
                this.Title = "Editing " + Class.CharNames.GetName(System.IO.Path.GetFileName(_opened).Substring(0, 3)) + " -Ono!";        
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

        [Obsolete]
        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("#sf4-modding@irc.synirc.net\n code.google.com/p/ssf4ae-tools/");
        }

        private void WindowLocationChanged(object sender, EventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation = new Point(Left, Top);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].ThisSize = e.NewSize;
        }

        private void PrefClick(object sender, RoutedEventArgs e)
        {
            //Add new preference window
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

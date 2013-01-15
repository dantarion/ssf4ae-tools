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
using System.Windows.Shapes;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            //Build Timestamp

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
            Style = (Style)FindResource("AeroStyle");

            BVersion.Content = "Version : " + version;

            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(
            TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
            TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
            //CommandManager.AddExecutedHandler(ApplicationCommands.Open, ClickOpen);
            BVersion.Content += "\nBuild Date: " + buildDateTime.ToShortDateString() + " " + buildDateTime.ToShortTimeString();
            OtherLabel.Text =
            "Credits (alphabetical)\n"
            + "Main Programming: Dantarion\nProgramming: Anotak, razor5070, Waterine\n"
            + "File format info:\nACCELERATOR, Anotak, Dantarion, Gojira, Error1, Illitirit, Piecemontee, Polarity,\nProvidenceangle, Sindor, Waterine, yeb, Zeipher\n"
            + "\nBug Reports:\nBebopfan, Comeback Mechanic, Error1, Mnszyk, Polarity, razor5070, yeb, Zeipher\n"
            + "Special thanks:\nahfb, Banana Ken, combovid.com, Dandy J, Eternal(Modding Wiki), hunterk (aemods.pbworks.com),\nsonichurricane.com, SSJ George Bush, xentax.com, Zandwich\n"
            + "IRC: #sf4-modding on irc.synirc.net\n"
            + "If you contributed and Anotak or razor5070 missed your name when making this window,\nplease let them know on IRC or shoryuken.com, thank you.";
            if (App.OpenedFiles.FilesOpened)
            {
                // charges, input, moves, cancels, scripts, vfx scripts, hitbox table
                FileInfo.Text = "Filename: " + MainWindow.Aopened +
                    "\nCharge: " + App.OpenedFiles.BCMFile.Charges.Count +
                    " Motion: " + (App.OpenedFiles.BCMFile.InputMotions.Count - 1) +
                    " Move: " + App.OpenedFiles.BCMFile.Moves.Count +
                    " Cancel: " + App.OpenedFiles.BCMFile.CancelLists.Count +
                    " Script: " + (App.OpenedFiles.BACFile.Scripts.Count - 1) +
                    " VFX: "  + (App.OpenedFiles.BACFile.VFXScripts.Count - 1) +
                    " Hit: "  + App.OpenedFiles.BACFile.HitboxTable.Count;
            }
        }

        private void ClickGoogleCode(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/ssf4ae-tools/");
        }
    }
}

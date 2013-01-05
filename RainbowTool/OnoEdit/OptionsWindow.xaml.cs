using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {

        private bool Changed { get; set; }

        public OptionsWindow()
        {
            InitializeComponent();
            Loaded += OptionsWindowLoaded;
            Closing += OptionsWindowClosing;

            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += CurrentPropertyChanged;
            if (UserSettings.CurrentSettings.UseAeroScheme){
                if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                    Style = (Style)FindResource("AeroStyle");
            } else exButtonsave.Height = 28;

            exButtonsave.RouteLinkClickEvent = true;
            exButtonsave.OnLinkClick += exButtonsave_OnLinkClick;
            exButtonsave.OnClick += exButtonsave_OnClick;
            exButtonsave.LinkOverrideText = "Check for update now";

        }

        void OptionsWindowLoaded(object sender, RoutedEventArgs e)
        {
            cblinkview.IsChecked = UserSettings.CurrentSettings.LearnLinkEnabled;
            cblinkview.Checked += Cbchanged; cblinkview.Unchecked += Cbchanged;
            cbrememberfile.IsChecked = UserSettings.CurrentSettings.RememberLastFile;
            cbrememberfile.Checked += Cbchanged; cbrememberfile.Unchecked += Cbchanged;
            cbupdates.IsChecked = UserSettings.CurrentSettings.CheckForUpdates;
            cbupdates.Checked += Cbchanged; cbupdates.Unchecked += Cbchanged;
            cbfriendlyname.IsChecked = UserSettings.CurrentSettings.ShowFriendlyNames;
            cbfriendlyname.Checked += Cbchanged; cbfriendlyname.Unchecked += Cbchanged;
            cbuseaero.IsChecked = UserSettings.CurrentSettings.UseAeroScheme;
            cbuseaero.Checked += Cbchanged; cbuseaero.Unchecked += Cbchanged;
            tbexe.Text = UserSettings.CurrentSettings.ExecutableLocation;
            tbexe.TextChanged += TbexeTextChanged;
        }

        void OptionsWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Changed) return;

            var result = MessageBox.Show("Save changes?","Ono!", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes) return;
            Save();
        }

        void exButtonsave_OnClick(object sender, EventArgs e)
        {
            Save();
            Changed = false;           
            Close();
        }

        void exButtonsave_OnLinkClick(object sender, EventArgs e)
        {
            //Do update method
        }

        void CurrentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {            
            if (!e.PropertyName.Equals("IsGlassEnabled")) return;

            if (!UserSettings.CurrentSettings.UseAeroScheme) return;

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Style = (Style)FindResource("AeroStyle");
            else
                Style = null;
        }

        private void Cbchanged(object sender, RoutedEventArgs e)
        {
            Changed = true;
        }

        void TbexeTextChanged(object sender, TextChangedEventArgs e)
        {
            Changed = true;
        }

        private void Save()
        {
            Debug.Assert(cbrememberfile.IsChecked != null, "cbrememberfile.IsChecked != null");
            UserSettings.CurrentSettings.RememberLastFile = (bool)cbrememberfile.IsChecked;
            Debug.Assert(cblinkview.IsChecked != null, "cblinkview.IsChecked != null");
            UserSettings.CurrentSettings.LearnLinkEnabled = (bool)cblinkview.IsChecked;
            Debug.Assert(cbupdates.IsChecked != null, "cbupdates.IsChecked != null");
            UserSettings.CurrentSettings.CheckForUpdates = (bool)cbupdates.IsChecked;
            Debug.Assert(cbfriendlyname.IsChecked != null, "cbfriendlyname.IsChecked != null");
            UserSettings.CurrentSettings.ShowFriendlyNames = (bool) cbfriendlyname.IsChecked;
            Debug.Assert(cbuseaero.IsChecked != null, "cbuseaero.IsChecked != null");
            UserSettings.CurrentSettings.UseAeroScheme = (bool) cbuseaero.IsChecked;
            UserSettings.CurrentSettings.ExecutableLocation = tbexe.Text;

            UserSettings.RaiseEvent(this, UserSettings.CurrentSettings.UseAeroScheme, typeof (bool));

        }

        private void BtnofdClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "SSFIV.exe|SSFIV.exe", Title = "Super Street Fighter IV Arcade Edition" };

            var oresult = ofd.ShowDialog();

            if (oresult != true) return;

            UserSettings.CurrentSettings.ExecutableLocation = ofd.FileName;
            tbexe.Text = UserSettings.CurrentSettings.ExecutableLocation;
        }

    }
}

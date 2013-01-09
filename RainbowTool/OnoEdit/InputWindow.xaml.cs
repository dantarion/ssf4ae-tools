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
using OnoEdit.Class;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
            if (!UserSettings.CurrentSettings.WindowCollection.ContainsKey(Name))
                UserSettings.CurrentSettings.WindowCollection.Add(Name, new TypeSettings());
            else
            {
                Left = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.X;
                Top = UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation.Y;

                Width = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Width;
                Height = UserSettings.CurrentSettings.WindowCollection[Name].ThisSize.Height;
            }
            this.Title = Title + " - " + MainWindow.Opened;

            UserSettings.OnSettingsChanged += UserSettings_OnSettingsChanged;
            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += CurrentPropertyChanged;
            if (UserSettings.CurrentSettings.UseAeroScheme)
                if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                {
                    Style = (Style)FindResource("AeroStyle");
                    ListBox.Margin = new Thickness(0,35,0,0);
                }
        }

        void UserSettings_OnSettingsChanged(object sender, object oVar, Type pType)
        {
            CurrentPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsGlassEnabled"));
        }

        void CurrentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("IsGlassEnabled")) return;

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled && UserSettings.CurrentSettings.UseAeroScheme)
            {
                Style = (Style)FindResource("AeroStyle"); ListBox.Margin = new Thickness(0, 35, 0, 0);
            }
            else
            {
                Style = null; ListBox.Margin = new Thickness(0);
            }
        }

        private void AddInputMotion(object sender, RoutedEventArgs e)
        {
            var tmp = new RainbowLib.BCM.InputMotion("NEW");
            App.OpenedFiles.BCMFile.InputMotions.Add(tmp);
        }

        private void RemoveInputMotion(object sender, RoutedEventArgs e)
        {
            App.OpenedFiles.BCMFile.InputMotions.Remove(ListBox.SelectedValue as RainbowLib.BCM.InputMotion);
        }

        private void WindowLocationChanged(object sender, EventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation = new Point(Left, Top);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].ThisSize = e.NewSize;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            UserSettings.CurrentSettings.WindowCollection[Name].IsMaximized = WindowState.HasFlag(System.Windows.WindowState.Maximized);

            if (!UserSettings.CurrentSettings.WindowCollection[Name].IsMaximized) return;
            UserSettings.CurrentSettings.WindowCollection[Name].ThisLocation = RestoreBounds.Location;
            UserSettings.CurrentSettings.WindowCollection[Name].ThisSize = RestoreBounds.Size;
        }
    }
}

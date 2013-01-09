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
    /// Interaction logic for ChargeWindow.xaml
    /// </summary>
    public partial class ChargeWindow : Window
    {
        public ChargeWindow()
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
                    var stringsize = Util.MeasureString(Title, 54);
                    Grid.IndentInterActPanel = stringsize.Width;
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
                Style = (Style)FindResource("AeroStyle");
                var stringsize = Util.MeasureString(Title, 54);
                Grid.IndentInterActPanel = stringsize.Width;
            }
            else
            {
                Style = null; Grid.IndentInterActPanel = 5;
            }
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

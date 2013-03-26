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
using RainbowLib;
using RainbowLib.BAC;

namespace OnoEdit
{
    /// <summary>
    /// Interaction logic for HitboxDataWindow.xaml
    /// </summary>
    public partial class HitboxDataWindow : Window
    {
        public HitboxDataWindow()
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

            odgrid.ShowCommandButtons = false;
            odgrid.Allowdeleterows = false;
            this.Title = "Hitbox Tables - " + MainWindow.Opened;
            UserSettings.OnSettingsChanged += UserSettings_OnSettingsChanged;
            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += CurrentPropertyChanged;
            if (UserSettings.CurrentSettings.UseAeroScheme)
                if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                {
                    Style = (Style)FindResource("AeroStyle");
                    btnnew.Style = (Style)FindResource("BorderlessButton");
                    btnrem.Style = (Style)FindResource("BorderlessButton");
                    btnimp.Style = (Style)FindResource("BorderlessButton");
                    btnexp.Style = (Style)FindResource("BorderlessButton");
                    btnplace.Margin = new Thickness(5, 35, 5, 5);
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
                Style = (Style)FindResource("AeroStyle"); btnplace.Margin = new Thickness(5, 35, 5, 5);
                btnnew.Style = (Style)FindResource("BorderlessButton");
                btnrem.Style = (Style)FindResource("BorderlessButton");
                btnimp.Style = (Style)FindResource("BorderlessButton");
                btnexp.Style = (Style)FindResource("BorderlessButton");
            }
            else
            {
                Style = null; ListBox.Margin = new Thickness(0);
                btnplace.Margin = new Thickness(5);
                btnnew.Style = null;
                btnrem.Style = null;
                btnimp.Style = null;
                btnexp.Style = null;
            }
        }

        private void AddHB(object sender, RoutedEventArgs e)
        {
            //debug stuff
            Console.WriteLine(@"HitBox Count : " + App.OpenedFiles.BACFile.HitboxTable.Count);
            App.OpenedFiles.BACFile.HitboxTable.Add(new HitBoxDataset(BACFile.LoadedHitBoxCount+1));
            Console.WriteLine(@"HitBox Count : " + App.OpenedFiles.BACFile.HitboxTable.Count);
            BACFile.LoadedHitBoxCount++;
            var dataset = App.OpenedFiles.BACFile.HitboxTable[App.OpenedFiles.BACFile.HitboxTable.Count-1];

            for (var j = 0; j < 12; j++)
            {
                var data = new HitBoxData(j);
                dataset.Data.Add(data);
                data.Damage = 0;
                data.Stun = 0;
                data.Effect = HitBoxData.HitBoxEffect.HIT;
                data.OnHit = new Script(0);
                data.SelfHitstop = 0;
                data.SelfShaking = 0;
                data.TgtHitstop = 0;
                data.TgtShaking = 0;
                data.HitGFX = -1;
                data.Unknown1 = 0;
                data.Unused = 0;
                data.HitGFX2 = -1;
                data.Unused2 = 0;
                data.Unused3 = 0;
                data.HitSFX = -1;
                data.HitSFX2 = -1;
                data.TgtSFX = -1;
                data.ArcadeScore = 0;
                data.SelfMeter = 0;
                data.TgtMeter = 0;
                data.JuggleStart = 1;
                data.TgtAnimTime = 10;
                data.MiscFlag = HitBoxData.MiscFlags.NONE;
                data.VelX = 0.05f;
                data.VelY = 0.05f;
                data.VelZ = 0;
                data.PushbackDist = 0.5f;
                data.AccX = 0;
                data.AccY = -0.005f;
                data.AccZ = 0;
            }

        }

        private void AddHB(object[] item)
        {
            //debug stuff
            Console.WriteLine(@"HitBox Count : " + App.OpenedFiles.BACFile.HitboxTable.Count);
            App.OpenedFiles.BACFile.HitboxTable.Add(new HitBoxDataset(BACFile.LoadedHitBoxCount + 1));
            Console.WriteLine(@"HitBox Count : " + App.OpenedFiles.BACFile.HitboxTable.Count);

            BACFile.LoadedHitBoxCount++;

            var dataset = App.OpenedFiles.BACFile.HitboxTable[App.OpenedFiles.BACFile.HitboxTable.Count - 1];

            for (var j = 0; j < 12; j++)
            {
                dataset.Data.Add((HitBoxData)item[j]);
            }

        }

        private void RemoveHB(object sender, RoutedEventArgs e)
        {

        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //ScriptWindow.processColumns(e, myDataGrid, RawDisplayCheckbox);
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


        private Object[] BufferItem;

        private void CopyMenuItem(object sender, RoutedEventArgs e)
        {
            BufferItem = ((HitBoxDataset)ListBox.SelectedItem).Data.ToArray();
        }

        private void DupMenuItem(object sender, RoutedEventArgs e)
        {
            AddHB(((HitBoxDataset) ListBox.SelectedItem).Data.ToArray());
        }

        private void PasteMenuItem(object sender, RoutedEventArgs e)
        {
            AddHB(BufferItem);
        }

        private void btnimp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnexp_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

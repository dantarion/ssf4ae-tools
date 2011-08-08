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
    /// Interaction logic for ChargeWindow.xaml
    /// </summary>
    public partial class ChargeWindow : Window
    {
        public ChargeWindow()
        {
            InitializeComponent();
        }

        private void AddCharge(object sender, RoutedEventArgs e)
        {
            var tmp = new RainbowLib.BCM.Charge();
            tmp.Name = "NEW";
            App.OpenedFiles.BCMFile.Charges.Add(tmp);
        }

        private void RemoveCharge(object sender, RoutedEventArgs e)
        {
            var charge = ChargesGrid.SelectedValue as RainbowLib.BCM.Charge;
            App.OpenedFiles.BCMFile.Charges.Remove(charge);
            foreach (RainbowLib.BCM.InputMotion motion in App.OpenedFiles.BCMFile.InputMotions.Where(x => x.Entries.Where(y => y.Charge == charge).Count() != 0))
            {
                MessageBox.Show(string.Format("You deleted {0} which was still referenced by {1}. This needs to be fixed before saving", charge.Name, motion.Name));
            }
        }
    }
}

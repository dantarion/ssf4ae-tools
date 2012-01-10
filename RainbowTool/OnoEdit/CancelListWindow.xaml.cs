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
    /// Interaction logic for CancelListWindow.xaml
    /// </summary>
    public partial class CancelListWindow : Window
    {
        public CancelListWindow()
        {
            InitializeComponent();
        }
        private void AddCancelList(object sender, RoutedEventArgs e)
        {
            var tmp = new RainbowLib.BCM.CancelList();
            tmp.Name = "NEW";
            App.OpenedFiles.BCMFile.CancelLists.Add(tmp);
        }

        private void RemoveCancelList(object sender, RoutedEventArgs e)
        {
            App.OpenedFiles.BCMFile.CancelLists.Remove(ListBox.SelectedValue as RainbowLib.BCM.CancelList);

        }
        private void AddMove(object sender, RoutedEventArgs e)
        {
            (ListBox.SelectedValue as RainbowLib.BCM.CancelList).Moves.Add(App.OpenedFiles.BCMFile.Moves[0]);
        }

        private void RemoveMove(object sender, RoutedEventArgs e)
        {
            (ListBox.SelectedValue as RainbowLib.BCM.CancelList).Moves.Remove(
                (Grid.SelectedValue as RainbowLib.Reference<RainbowLib.BCM.Move>));
        }
    }
}

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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddInputMotion(object sender, RoutedEventArgs e)
        {
            var tmp = new RainbowLib.BCM.InputMotion();
            tmp.Name = "NEW";
            App.OpenedFiles.BCMFile.InputMotions.Add(tmp);
        }

        private void RemoveInputMotion(object sender, RoutedEventArgs e)
        {
            App.OpenedFiles.BCMFile.InputMotions.Remove(ListBox.SelectedValue as RainbowLib.BCM.InputMotion);
        }

        private void AddInput(object sender, RoutedEventArgs e)
        {
            var tmp = new RainbowLib.BCM.InputMotionEntry();
            (ListBox.SelectedValue as RainbowLib.BCM.InputMotion).Entries.Add(tmp);
        }

        private void RemoveInput(object sender, RoutedEventArgs e)
        {
            (ListBox.SelectedValue as RainbowLib.BCM.InputMotion).Entries.Remove(InputGrid.SelectedValue as RainbowLib.BCM.InputMotionEntry);
        }
    }
}

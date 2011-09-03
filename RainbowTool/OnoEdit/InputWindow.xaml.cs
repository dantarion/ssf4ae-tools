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
            var tmp = new RainbowLib.BCM.InputMotion("NEW");
            App.OpenedFiles.BCMFile.InputMotions.Add(tmp);
        }

        private void RemoveInputMotion(object sender, RoutedEventArgs e)
        {
            App.OpenedFiles.BCMFile.InputMotions.Remove(ListBox.SelectedValue as RainbowLib.BCM.InputMotion);
        }
    }
}

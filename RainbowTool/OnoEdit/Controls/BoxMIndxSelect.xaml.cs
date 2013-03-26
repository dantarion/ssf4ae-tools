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

namespace OnoEdit.Controls
{
    /// <summary>
    /// Interaction logic for BoxMIndxSelect.xaml
    /// </summary>
    public partial class BoxMIndxSelect : Window
    {
        public int NewIndex { get; set; }

        public BoxMIndxSelect(int oldindex)
        {
            InitializeComponent();
            label1.Content = "Old Index : [" + App.OpenedFiles.BACFile.Scripts[oldindex].Index + "," +
                             App.OpenedFiles.BACFile.Scripts[oldindex].Name + "]"; //Hacked together :/ make better later maybe
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewIndex = int.Parse(tbselind.Text);
            DialogResult = true;
            Close();
        }

        private void tbselind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]", tbselind.Text))
            {
                MessageBox.Show("Please enter only numbers.");
                tbselind.Undo();
            }
        }
    }
}

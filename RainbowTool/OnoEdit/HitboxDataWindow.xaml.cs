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
    /// Interaction logic for HitboxDataWindow.xaml
    /// </summary>
    public partial class HitboxDataWindow : Window
    {
        public HitboxDataWindow()
        {
            InitializeComponent();
            this.Title = Title + " - " + MainWindow.Opened;
        }

        private void AddHB(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveHB(object sender, RoutedEventArgs e)
        {

        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //ScriptWindow.processColumns(e, myDataGrid, RawDisplayCheckbox);
        }
    }
}

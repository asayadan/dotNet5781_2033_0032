using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        MainWindow win;
        public AddBusWindow(MainWindow _win)
        {
            win = _win;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.buses.Add(new Bus(int.Parse(license.Text), DateTime.Parse(date.Text)));
            Hide();
            win.addBus(MainWindow.buses.Count - 1);
        }

        private void mouseEnter(object sender, EventArgs e)
        {
           if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text  = string.Empty;
        }

    }
}

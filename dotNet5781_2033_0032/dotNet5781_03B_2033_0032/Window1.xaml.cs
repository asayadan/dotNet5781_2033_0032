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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow win;
        int index;
        Bus thisBus;
        public Window1(MainWindow _win, int _index)
        {
            InitializeComponent();
            win = _win;
            index = _index;
             thisBus = MainWindow.buses[index];
            textsGrid.DataContext = thisBus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            thisBus = new Bus(66666666, new DateTime(2019, 1, 1));
            textsGrid.DataContext = thisBus;
            thisBus = MainWindow.buses[index];
            textsGrid.DataContext = thisBus;
        }

        private void bt_refule_Click(object sender, RoutedEventArgs e)
        {
            if (thisBus.curStatus == Status.ready)
            {
                thisBus.Event(Status.refueling);
            }
            else
            {
                MessageBox.Show("you can't fix the bus because he is " + thisBus.curStatus.ToString());
            }

        }

        private void bt_finished_Click(object sender, RoutedEventArgs e)
        {
            if (thisBus.curStatus == Status.ready)
            {
                thisBus.Event(Status.fixing);
            }
            else
            {
                MessageBox.Show("you can't fix the bus because he is " + thisBus.curStatus.ToString());
            }
        }
    }
}

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
    /// Interaction logic for Ride.xaml
    /// </summary>
    public partial class Ride : Window
    {
        int index;
        public Ride(int _index)
        {
            index = _index;
            InitializeComponent();
        }

        private void tb_km_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tb_km.Text == "Enter the number of kilometers to ride")
            {
                tb_km.Text = "";
            }
        }

        private void tb_km_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tb_km.Text =="")
            {
                tb_km.Text = "Enter the number of kilometers to ride ";
            }
        }



        private void tb_km_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int dist;
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    int.TryParse((sender as TextBox).Text, out dist);
                    MainWindow.buses[index].rideKM(dist);

                    MessageBox.Show("new ride");

                    MainWindow.buses[index].curStatus = Status.working;
                    double help = dist / rnd.Next(20, 50) * 60 * 60;
                    MainWindow.buses[index].whenWillBeReady = help;
                    MainWindow.buses[index].start =help;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
                
            }
            else if (e.Key!=Key.Back&& e.Key != Key.Delete && ((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9))
            {
                if ((int)e.Key < (int)Key.Left || (int)e.Key > (int)Key.Down)
                {
                    int help = tb_km.CaretIndex;
                    tb_km.Text = tb_km.Text.Remove(help - 1, 1);
                    tb_km.CaretIndex = help - 1;
                }
            }
        }

        private void tb_km_KeyDown(object sender, KeyEventArgs e)
        {
            if (tb_km.Text == "Enter the number of kilometers to ride ")
            {
                tb_km.Text = "";
            }
        }
    }
}

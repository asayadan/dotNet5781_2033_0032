using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for Ride.xaml
    /// </summary>
    public partial class Ride : Window
    {
        MainWindow win;
        int index;
        Bus thisBus;
        public Ride(MainWindow _win, int _index)
        {
            InitializeComponent();
            win = _win;
            index = _index;
            thisBus = MainWindow.buses[index];
        }

        private void tb_km_MouseEnter(object sender, MouseEventArgs e) // As at AddBusWindow
        {
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text = string.Empty;
        }

        private void tb_km_MouseLeave(object sender, MouseEventArgs e) // As at AddBusWindow
        {
            if ((sender as TextBox).Text == "")
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
        }



        private void tb_km_KeyUp(object sender, KeyEventArgs e) // Check if enter was pressed to close the window.
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    index = win.FindIndex(thisBus);
                    int dist;
                    int.TryParse((sender as TextBox).Text, out dist);
                    MainWindow.buses[index].rideKM(dist);
                    MainWindow.buses[index].Event(Status.working, dist);
                    MessageBox.Show("new ride");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }

            }
            else if (e.Key != Key.Back && e.Key != Key.Delete && ((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9))
            {
                if ((int)e.Key < (int)Key.Left || (int)e.Key > (int)Key.Down)
                {
                    int help = tb_km.CaretIndex;
                    tb_km.Text = tb_km.Text.Remove(help - 1, 1);
                    tb_km.CaretIndex = help - 1;
                }
            }
        }

        private void tb_km_KeyDown(object sender, KeyEventArgs e) // As at AddBusWindow
        {
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text = string.Empty;
        }
    }
}

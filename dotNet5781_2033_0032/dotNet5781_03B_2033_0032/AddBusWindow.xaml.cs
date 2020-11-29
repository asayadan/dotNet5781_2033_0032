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
            try
            {
                if (cb_data.IsChecked == false)
                {


                    if (dp_date.SelectedDate != null && license.Text != null)
                    {
                        MainWindow.buses.Add(new Bus(int.Parse(license.Text), (DateTime)dp_date.SelectedDate));
                        Close();
                        win.addBus(MainWindow.buses.Count - 1);
                    }
                    else
                        MessageBox.Show("you need to fill all the variabls");
                }
                else if (dp_date.SelectedDate != null && license.Text != null && date_treatment.SelectedDate != null && tb_mileage.Text != null && tb_mileage_since_last_tratment.Text != null && tb_fuel.Text != null)
                {
                    MainWindow.buses.Add(new Bus(int.Parse(license.Text), (DateTime)dp_date.SelectedDate, (DateTime)date_treatment.SelectedDate, int.Parse(tb_mileage.Text), int.Parse(tb_mileage_since_last_tratment.Text), int.Parse(tb_fuel.Text)));
                    Close();
                    win.addBus(MainWindow.buses.Count - 1);
                }
                else { MessageBox.Show("unvalid arguments"); }
            }
            catch (ArgumentException ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void mouseEnter(object sender, EventArgs e)
        {
           if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text  = string.Empty;
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == "")
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
        }




        private void keyDown(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
            {
                (sender as TextBox).Text = "";
            }
        }

        private void cb_data_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(sender as CheckBox).IsChecked)
            {
                this.Height = 310;
            }
            else
            {
                this.Height = 150;
            }
        }
    }
}

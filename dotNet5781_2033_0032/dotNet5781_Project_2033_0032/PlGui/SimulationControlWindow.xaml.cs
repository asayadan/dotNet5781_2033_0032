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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for SimulationControlWindow.xaml
    /// </summary>
    public partial class SimulationControlWindow : Window
    {
        bool isWorking = false;
        TimeSpan startTime = new TimeSpan(0);
        public SimulationControlWindow()
        {
            InitializeComponent();
            tb_time.DataContext = startTime;
            for (int i = 0; i < 60; i++)
            {
                lastMinutesComboBox.Items.Add(i);
                lastSecondsComboBox.Items.Add(i);
                if (i < 24) lastHoursComboBox.Items.Add(i);
            }
            lastSecondsComboBox.SelectedIndex = 0;
            lastMinutesComboBox.SelectedIndex = 0;
            lastHoursComboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int speed = int.Parse(tb_speed.Text);
            }
            catch (FormatException ex)//shouldn't happen
            {
                tb_warnings.Text = "invalid speed";
                return;
            }
            catch (ArgumentNullException ex)
            {
                tb_warnings.Text = "invalid speed";
                return;
            }
            tb_warnings.Text = "";
            if (!isWorking)
            {
                gr_time.Visibility = Visibility.Collapsed;
                tb_time.Visibility = Visibility.Visible;
                tb_speed.IsReadOnly = true;
                bt_activation.Content="Running";
            }
            else
            {
                gr_time.Visibility = Visibility.Visible;
                tb_time.Visibility = Visibility.Collapsed;
                tb_speed.IsReadOnly = false;
                bt_activation.Content = "Start";
            }
            isWorking = !isWorking;
            var start = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                        (int)lastMinutesComboBox.SelectedItem,
                                        (int)lastSecondsComboBox.SelectedItem);

        }
    }
}

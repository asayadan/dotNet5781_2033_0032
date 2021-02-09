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
using System.Threading;
using BLAPI;
using System.ComponentModel;
using System.Diagnostics;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for SimulationControlWindow.xaml
    /// </summary>

    public partial class SimulationControlWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        IBL bl;
        int speed;
        bool isWorking = false;
        TimeSpan startTime = TimeSpan.Zero;
        
        Func<TimeSpan> getTime = null;
        public SimulationControlWindow(IBL bL)
        {
            bl = bL;
            InitializeComponent();  
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += StartOrStopTimer;
            getTime = () => startTime;
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

        private void StartOrStopTimer(object sender, DoWorkEventArgs e)
        {

            bl.StartSimulator(startTime, speed, 
                (timeSpan) => 
                App.Current.Dispatcher.Invoke((Action)delegate { tb_time.DataContext = timeSpan; }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                speed = int.Parse(tb_speed.Text);
            }
            catch (FormatException ex)//shouldn't happen
            {
                tb_warnings.Text = "Invalid speed";
                return;
            }
            catch (ArgumentNullException ex)
            {
                tb_warnings.Text = "Invalid speed";
                return;
            }
            tb_warnings.Text = "";
            startTime = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                                (int)lastMinutesComboBox.SelectedItem,
                                                (int)lastSecondsComboBox.SelectedItem);
            if (!isWorking)
            {
                gr_time.Visibility = Visibility.Collapsed;
                tb_time.Visibility = Visibility.Visible;
                tb_speed.IsReadOnly = true;
                worker.RunWorkerAsync();
                bt_activation.Content = "Stop";
            }
            else
            {
                gr_time.Visibility = Visibility.Visible;
                tb_time.Visibility = Visibility.Collapsed;
                tb_speed.IsReadOnly = false;
                bl.StopSimulator();
                bt_activation.Content = "Start";
            }
            isWorking = !isWorking;

            


           

        }


    }
}

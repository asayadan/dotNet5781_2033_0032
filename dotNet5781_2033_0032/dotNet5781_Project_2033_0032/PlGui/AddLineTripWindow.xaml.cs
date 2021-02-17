using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AddLineTripWindow.xaml
    /// </summary>
    public partial class AddLineTripWindow : Window
    {
        IBL bl;
        List<(TimeSpan, TimeSpan)> listTimes;
        int lineId;
        BackgroundWorker worker = new BackgroundWorker();
        public AddLineTripWindow(IBL _bl, int _lineId, List<(TimeSpan, TimeSpan)> helpList)
        {
            bl = _bl;
            listTimes = helpList;
            lineId = _lineId;
            InitializeComponent();
            worker.DoWork += AddLineTrip;
            for (int i = 0; i < 60; i++)
            {
                StartMinutesComboBox.Items.Add(i); lastMinutesComboBox.Items.Add(i); frequencyMinutesComboBox.Items.Add(i);
                startSecondsComboBox.Items.Add(i); lastSecondsComboBox.Items.Add(i); frequencySecondsComboBox.Items.Add(i);
                if (i < 24) { StartHoursComboBox.Items.Add(i); lastHoursComboBox.Items.Add(i); frequencyHoursComboBox.Items.Add(i); }

                lastSecondsComboBox.SelectedIndex = 0; startSecondsComboBox.SelectedIndex = 0; frequencySecondsComboBox.SelectedIndex = 0;
                lastMinutesComboBox.SelectedIndex = 0; StartMinutesComboBox.SelectedIndex = 0; frequencyMinutesComboBox.SelectedIndex = 0;
                lastHoursComboBox.SelectedIndex = 0; StartHoursComboBox.SelectedIndex = 0; frequencyHoursComboBox.SelectedIndex = 0;
            }
        }

        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan start, end;
            start = new TimeSpan((int)StartHoursComboBox.SelectedItem,
                                    (int)StartMinutesComboBox.SelectedItem,
                                    (int)startSecondsComboBox.SelectedItem);
            end = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                    (int)lastMinutesComboBox.SelectedItem,
                                    (int)lastSecondsComboBox.SelectedItem);
            if (IsAlone(start, end))
            {
                if (start<end)
                {


                    worker.RunWorkerAsync();
                }
                else
                {
                    tb_warnings.Text = "the start have to be before the end";
                }
            }
            else
            {
                tb_warnings.Text = "this LineTrip is overlapping with another LineTrip ";
            }
        }
        private bool IsAlone(TimeSpan start, TimeSpan end)
        {
            bool help = true;
            foreach (var item in listTimes)
            {
                if (!(item.Item1 > end || item.Item2 < start))
                    help = false;
            }
            return help;
        }
        private void AddLineTrip(object sender, DoWorkEventArgs e)
        {
            TimeSpan start = TimeSpan.Zero, end = TimeSpan.Zero, frequency = TimeSpan.Zero;
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                start = new TimeSpan((int)StartHoursComboBox.SelectedItem,
                                        (int)StartMinutesComboBox.SelectedItem,
                                        (int)startSecondsComboBox.SelectedItem);
                end = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                        (int)lastMinutesComboBox.SelectedItem,
                                        (int)lastSecondsComboBox.SelectedItem);
                frequency = new TimeSpan((int)frequencyHoursComboBox.SelectedItem,
                                        (int)frequencyMinutesComboBox.SelectedItem,
                                        (int)frequencySecondsComboBox.SelectedItem);
            });
            try
            {
                bl.CreateLineTrip(lineId, start, frequency, end);
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Close();
                });
            }
            catch (BO.BadLineTripException ex)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    tb_warnings.Text = ex.Message;
                });
            }
        }
    }
}

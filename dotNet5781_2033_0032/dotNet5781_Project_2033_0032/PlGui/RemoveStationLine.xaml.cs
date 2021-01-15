using System.Windows;
using BLAPI;
using System.ComponentModel;
using System;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for RemoveStationLine.xaml
    /// </summary>
    public partial class RemoveStationLine : Window
    {
        BO.Line curLine;
        BO.Station curStation;
        IBL bl;
        BackgroundWorker worker = new BackgroundWorker();

        public RemoveStationLine(IBL bL, BO.Line line, BO.StationInLine station)
        {
            InitializeComponent();
            bl = bL; curLine = line; curStation = new BO.Station {Code=station.Code,Name=station.Name };
            for (int i = 0; i < 60; i++)
            {
                lastMinutesComboBox.Items.Add(i);
                lastSecondsComboBox.Items.Add(i);
                if (i < 24) lastHoursComboBox.Items.Add(i);
            }
        }


        public void Remove(object sender, DoWorkEventArgs e)
        {
            double lastDistance = 0;
            TimeSpan lastTimeSpan = TimeSpan.Zero;
            bool valid = true;

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try
                {
                    lastDistance = double.Parse(lastDistanceTextBox.Text);
                    lastTimeSpan = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                        (int)lastMinutesComboBox.SelectedItem,
                                        (int)lastSecondsComboBox.SelectedItem);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Invalid distance!");
                    valid = false;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid distance!");
                    valid = false;
                }
            });

            try
            {
                if (valid)
                {
                    bl.RemoveStationFromLine(curLine.Id, curStation.Code, lastDistance, lastTimeSpan);
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        Close();
                    });
                }
            }

            catch (BO.InvalidLineIDException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void finishButton_Click(object sender, RoutedEventArgs e)
        {

            if (lastDistanceTextBox != null &&
                lastHoursComboBox.SelectedItem != null &&
                lastMinutesComboBox.SelectedItem != null &&
                lastSecondsComboBox.SelectedItem != null)
            {
                worker.DoWork += Remove;
                worker.RunWorkerAsync();
            }
        }
    }
}
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
using System.ComponentModel;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddRemoveStationLine.xaml
    /// </summary>
    public partial class AddStationLine : Window
    {
        IBL bl;
        BO.Line curLine;
        BackgroundWorker worker = new BackgroundWorker();

        public AddStationLine(IBL bL, BO.Line line)
        {
            InitializeComponent();
            bl = bL; curLine = line;
            worker.DoWork += GetStations;
            worker.RunWorkerAsync();
            for (int i = 0; i < 60; i++)
            {
                nextMinutesComboBox.Items.Add(i); lastMinutesComboBox.Items.Add(i);
                nextSecondsComboBox.Items.Add(i); lastSecondsComboBox.Items.Add(i);
                if (i < 24) { nextHoursComboBox.Items.Add(i); lastHoursComboBox.Items.Add(i); }
            }


        }

        void GetStations(object sender, DoWorkEventArgs e)
        {
            var AllStations = bl.RequestAllStations().ToList();
            var stationsInLine = bl.RequestLineStationsInLine(curLine.Id).ToList();

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                foreach (var station in AllStations)
                {
                    if (stationsInLine.FindIndex(x => x.StationId == station.Code) == -1)
                        newStationComboBox.Items.Add(station);
                }
                for (int i = 0; i < stationsInLine.Count + 1; i++)
                    indexStationComboBox.Items.Add(i);
                
                newStationComboBox.DisplayMemberPath = "Name";
                newStationComboBox.SelectedValuePath = "Code";
            });
        }


        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (newStationComboBox.SelectedItem != null &&
                indexStationComboBox.SelectedItem != null && (
                lastDistanceTextBox != null && nextDistanceTextBox != null &&
                lastHoursComboBox.SelectedItem != null && nextHoursComboBox.SelectedItem != null &&
                lastMinutesComboBox.SelectedItem != null && nextMinutesComboBox.SelectedItem != null &&
                lastSecondsComboBox.SelectedItem != null && nextSecondsComboBox.SelectedItem != null
                ||
                indexStationComboBox.SelectedIndex == indexStationComboBox.Items.Count - 1 &&
                lastDistanceTextBox != null && 
                lastHoursComboBox.SelectedItem != null && 
                lastMinutesComboBox.SelectedItem != null && 
                lastSecondsComboBox.SelectedItem != null
                ||
                indexStationComboBox.SelectedIndex == 0 &&
                nextDistanceTextBox != null &&
                nextHoursComboBox.SelectedItem != null &&
                nextMinutesComboBox.SelectedItem != null &&
                nextSecondsComboBox.SelectedItem != null
                ))
            {
                worker= new BackgroundWorker();
                worker.DoWork += AddStation;
                worker.RunWorkerAsync();
            }
        }

        void AddStation(object sender, DoWorkEventArgs e)
        {
            BO.Station station = null;
            double lastDistance = 0, nextDistance = 0;
            int index = 0;
            TimeSpan lastTimeSpan = TimeSpan.Zero, nextTimeSpan = TimeSpan.Zero;
            bool valid = true;

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try
                {
                    station = newStationComboBox.SelectedItem as BO.Station;
                    lastDistance = double.Parse(lastDistanceTextBox.Text);
                    nextDistance = double.Parse(nextDistanceTextBox.Text);
                    index = (int)indexStationComboBox.SelectedItem;
                    lastTimeSpan = new TimeSpan((int)lastHoursComboBox.SelectedItem,
                                        (int)lastMinutesComboBox.SelectedItem,
                                        (int)lastSecondsComboBox.SelectedItem);
                    nextTimeSpan = new TimeSpan((int)nextHoursComboBox.SelectedItem,
                                        (int)nextMinutesComboBox.SelectedItem,
                                        (int)nextSecondsComboBox.SelectedItem);

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
                    bl.CreateStationToLine(curLine.Id, station.Code, index,
                        lastDistance, lastTimeSpan, nextDistance, nextTimeSpan);
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

        private void indexStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int stationAmount = indexStationComboBox.Items.Count;

            if (indexStationComboBox.SelectedIndex != 0 || indexStationComboBox.SelectedIndex != stationAmount - 1)
                for (int i = 2; i <= 5; i++)
                    MainGrid.RowDefinitions.ElementAt(i).Height = GridLength.Auto;

            if (indexStationComboBox.SelectedIndex == 0) {
                MainGrid.RowDefinitions.ElementAt(2).Height = new GridLength(0);
                MainGrid.RowDefinitions.ElementAt(3).Height = new GridLength(0);
            }

            else if (indexStationComboBox.SelectedIndex == stationAmount - 1)
            {
                MainGrid.RowDefinitions.ElementAt(4).Height = new GridLength(0);
                MainGrid.RowDefinitions.ElementAt(5).Height = new GridLength(0);
            }
        }
    }
}


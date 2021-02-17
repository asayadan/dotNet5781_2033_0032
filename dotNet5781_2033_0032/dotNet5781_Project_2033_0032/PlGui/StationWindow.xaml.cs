using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>

    public partial class StationWindow : Window
    {
        IBL bl;
        private static Mutex lineStationMutex = new Mutex();

        #region workers
        BackgroundWorker getAllStationsWorker = new BackgroundWorker();
        BackgroundWorker getLinesInStationWorker = new BackgroundWorker();
        BackgroundWorker searchWorker = new BackgroundWorker();
        BackgroundWorker getLinesInTwoStationWorker = new BackgroundWorker();
        #endregion

        #region ObservableCollection
        ObservableCollection<BO.Station> stationCollection = new ObservableCollection<BO.Station>();
        ObservableCollection<BO.LineTiming> allLinesInStation = new ObservableCollection<BO.LineTiming>();
        ObservableCollection<BO.LineTiming> timeOfLinesInStation = new ObservableCollection<BO.LineTiming>();
        ObservableCollection<(BO.LineTiming, BO.LineTiming)> linesInBothStations = new ObservableCollection<(BO.LineTiming, BO.LineTiming)>();
        #endregion

        TimeSpan tripEndTime;
        BO.Station curStation;
        BO.Station firstStation;
        BO.Station secondStation;
        bool changed = true;

        public StationWindow(IBL bL, string userName)
        {
            bl = bL;
            InitializeComponent();
            SimulationControlWindow win = new SimulationControlWindow(bl); // Opening the simulation window
            win.Show();
            lbl_username.DataContext = userName;
            getAllStationsWorker.DoWork += GetStations;
            SetYellowSignTab();
            SetTripPlanTab();
        }

        #region Yellow Sign Tab
        void SetYellowSignTab() // Setting the yellow sign up
        {
            getAllStationsWorker.RunWorkerAsync();
            getLinesInStationWorker.DoWork += SetLinesInStation;
            searchWorker.DoWork += Search;
            BO.SimulationClock.valueChanged += (object sender, EventArgs e) => { getLinesInStationWorker.RunWorkerAsync(); };
        }
        /// <summary>
        /// sets the and the arrival times lines in the scurrent stations a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetLinesInStation(object sender, DoWorkEventArgs e)
        {
            if (curStation == null)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    timeOfLinesInStation.Clear();
                    allLinesInStation.Clear();
                });
            }
            else
            {
                try
                {
                    lineStationMutex.WaitOne();
                    var lineTimings = bl.RequestLineTimingFromStation(curStation.Code).ToList();
                    lineStationMutex.ReleaseMutex();
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        var sorted = lineTimings.OrderBy(p => p.LineCode);
                        timeOfLinesInStation.Clear();
                        allLinesInStation.Clear();
                        if (!bl.IsSimulationActivated())
                        {
                            tb_currentState.Text = "Simulation Not activated!";
                            tb_currentState.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            tb_currentState.Text = "Simulation activated!";
                            tb_currentState.Foreground = new SolidColorBrush(Colors.Green);
                        }
                        foreach (var lineTiming in lineTimings)
                            timeOfLinesInStation.Add(lineTiming);

                        foreach (var lineTiming in sorted)
                            allLinesInStation.Add(lineTiming);
                    });

                }
                catch (Exception)
                {
                }

            }

        }
        private void GetStations(object sender, DoWorkEventArgs e)
        {
            try
            {
                lineStationMutex.WaitOne();
                stationCollection = new ObservableCollection<BO.Station>(bl.RequestAllStations().ToList());
                lineStationMutex.ReleaseMutex();

                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    cb_stations.DataContext = cb_firstStation.DataContext = cb_secondStation.DataContext = stationCollection;
                    cb_stations.DisplayMemberPath = cb_firstStation.DisplayMemberPath = cb_secondStation.DisplayMemberPath = "Name";
                    cb_stations.SelectedValuePath = cb_firstStation.SelectedValuePath = cb_secondStation.SelectedValuePath = "Code";
                    cb_stations.SelectedIndex = 0;

                    curStation = stationCollection[0];
                });
                SetLinesInStation(sender, e);
            }


            catch (Exception)

            {
                lineStationMutex.ReleaseMutex();
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                StationsInLineDataGrid.DataContext = allLinesInStation;
                ClosestLinesDataGrid.DataContext = timeOfLinesInStation;
                LinesInBothStationsDataGrid.DataContext = linesInBothStations;
            });
        }
        private void cb_stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_stationName.DataContext = curStation = cb_stations.SelectedItem as BO.Station;
            getLinesInStationWorker.RunWorkerAsync();
        }
        private void bt_search_Click(object sender, RoutedEventArgs e)
        {
            searchWorker.RunWorkerAsync(new SearchData { changed = false, search = tb_search.Text });

        }
        private void Search(object sender, DoWorkEventArgs e)
        {

            //  if ((e.Argument as SearchData).changed)
            lineStationMutex.WaitOne();
            var helpList = bl.RequestStationsBy(x => (x as BO.Station).Name.Contains((e.Argument as SearchData).search)).ToList();
            lineStationMutex.ReleaseMutex();

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                stationCollection.Clear();
                foreach (var station in helpList)
                {
                    stationCollection.Add(station);
                }
                curStation = cb_stations.SelectedItem as BO.Station;
            });

        }

        #endregion
        #region Trip Tab
        public void SetTripPlanTab()
        {
            getLinesInTwoStationWorker.DoWork += GetLinesInBothStation;
            BO.SimulationClock.valueChanged += (object sender, EventArgs e) =>
            {
                if (!getLinesInTwoStationWorker.IsBusy)
                    getLinesInTwoStationWorker.RunWorkerAsync();
            };
            LinesInBothStationsDataGrid.DataContext = linesInBothStations;
        }

        private void GetLinesInBothStation(object sender, DoWorkEventArgs e)
        {
            if (firstStation != null && secondStation != null)
            {
                lineStationMutex.WaitOne();
                var res = bl.LinesInTwoStations(firstStation.Code, secondStation.Code).ToList();
                lineStationMutex.ReleaseMutex();
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    bool a = true;
                    for (int i = 0; linesInBothStations.Count == res.Count() && i < linesInBothStations.Count; i++)
                    {
                        if (changed || linesInBothStations[i].Item1.TripStartTime != res[i].Item1.TripStartTime)
                        {
                            changed = false;
                            a = false;
                            break;
                        }
                    }

                    if (!a || linesInBothStations.Count != res.Count())
                    {
                        linesInBothStations.Clear();
                        foreach (var item in res)
                        {
                            linesInBothStations.Add(item);
                        }
                    }
                });
            }
        }

        private void cb_Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_firstStation.SelectedItem != null && cb_secondStation.SelectedItem != null)
            {
                changed = true;
                firstStation = cb_firstStation.SelectedItem as BO.Station;
                secondStation = cb_secondStation.SelectedItem as BO.Station;
                if (!getLinesInTwoStationWorker.IsBusy) getLinesInTwoStationWorker.RunWorkerAsync();
            }

            //else BO.SimulationClock.valueChanged -= (object _sender, EventArgs _e) => getLinesInTwoStation.RunWorkerAsync();

        }

        private void bt_startTrip_Click(object sender, RoutedEventArgs e)
        {
             var tuple = ((BO.LineTiming, BO.LineTiming))LinesInBothStationsDataGrid.SelectedItem;
            tripEndTime = bl.LineInTwoStations(firstStation.Code, secondStation.Code, tuple.Item1.LineCode).Item2.TimeToStation;
            LinesInBothStationsDataGrid.IsEnabled = false;
            pb_tripProgress.Visibility = Visibility.Visible;
            pb_tripProgress.Minimum = 0- tripEndTime.TotalMilliseconds;
            pb_tripProgress.Maximum = 0;
            tripEndTime += BO.SimulationClock.GetTime;
            pb_tripProgress.Value = pb_tripProgress.Minimum;
            bt_startTrip.IsEnabled = false;
            tb_start.Text = "Trip Progress:";
            BO.SimulationClock.valueChanged += Trip;
        }
        private void Trip(object sender, EventArgs e)
        {
            UpdateProgresssBar(sender, e);
            if (BO.SimulationClock.GetTime.TotalMilliseconds - tripEndTime.TotalMilliseconds >= 0)
            {
                BO.SimulationClock.valueChanged -= Trip;
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    bt_startTrip.IsEnabled = true;
                    cb_firstStation.IsEnabled = cb_secondStation.IsEnabled = true;
                    LinesInBothStationsDataGrid.IsEnabled = true;
                    pb_tripProgress.Visibility = Visibility.Collapsed;
                    tb_start.Text = "Waiting to start...";

                });
            }


        }

        private void UpdateProgresssBar(object sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                pb_tripProgress.Value = BO.SimulationClock.GetTime.TotalMilliseconds - tripEndTime.TotalMilliseconds;
            });
        }

        private void LinesInBothStationsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (LinesInBothStationsDataGrid.SelectedItem != null)
            {
                bt_startTrip.IsEnabled = true;

            }
            else bt_startTrip.IsEnabled = false;
        }
        #endregion
    }

    [ValueConversion(typeof(TimeSpan), typeof(String))]
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            int minutes = (int)ts.TotalMinutes;
            if (minutes == 0)
                return '↓'; //bus is at station yay
            return minutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (int.TryParse(strValue, out int result))
            {
                return TimeSpan.FromMinutes(result);
            }
            return DependencyProperty.UnsetValue;
        }


    }

    [ValueConversion(typeof((BO.LineTiming, BO.LineTiming)), typeof(String))]
    public class TimeOfStartTwoLineTimingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tuple = ((BO.LineTiming, BO.LineTiming))value;
            return tuple.Item1.TimeToStation + BO.SimulationClock.GetTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof((BO.LineTiming, BO.LineTiming)), typeof(String))]
    public class TimeOfEndTwoLineTimingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tuple = ((BO.LineTiming, BO.LineTiming))value;
            return tuple.Item2.TimeToStation + BO.SimulationClock.GetTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof((BO.LineTiming, BO.LineTiming)), typeof(String))]
    public class CodeOfTwoLineTimingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tuple = ((BO.LineTiming, BO.LineTiming))value;
            return tuple.Item1.LineCode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        BackgroundWorker getAllStationsWorker = new BackgroundWorker();
        BackgroundWorker getLinesInStationWorker = new BackgroundWorker();
        BackgroundWorker Searchworker = new BackgroundWorker();
        ObservableCollection<BO.Station> stationCollection = new ObservableCollection<BO.Station>();
        ObservableCollection<BO.LineTiming> allLinesInStation = new ObservableCollection<BO.LineTiming>();
        ObservableCollection<BO.LineTiming> timeOfLinesInStation = new ObservableCollection<BO.LineTiming>();

        BO.Station curStation;

        public StationWindow(IBL bL, string userName)
        {
            bl = bL;
            InitializeComponent();
            SimulationControlWindow win = new SimulationControlWindow(bl);
            win.Show();
            lbl_username.DataContext = userName;
            getAllStationsWorker.DoWork += SetWindow;
            getAllStationsWorker.RunWorkerAsync();
            getLinesInStationWorker.DoWork += SetLinesInStation;
            Searchworker.DoWork += Search;
            BO.SimulationClock.valueChanged += (object sender, EventArgs e) => getLinesInStationWorker.RunWorkerAsync();
        }

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
                var lineTimings = bl.RequestLineTimingFromStation(curStation.Code);
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
        }
        private void SetWindow(object sender, DoWorkEventArgs e)
        {
            stationCollection = new ObservableCollection<BO.Station>(bl.RequestAllStations());

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                cb_stations.DataContext = stationCollection;
                cb_stations.DisplayMemberPath = "Name";
                cb_stations.SelectedIndex = 1;
                curStation = stationCollection[1];


            });

            SetLinesInStation(sender, e);

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                StationsInLineDataGrid.DataContext = allLinesInStation;
                ClosestLinesDataGrid.DataContext = timeOfLinesInStation;
            });



        }

        private void cb_stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            tb_stationName.DataContext = curStation = cb_stations.SelectedItem as BO.Station;
            lineStationMutex.WaitOne();

           getLinesInStationWorker.RunWorkerAsync();
            lineStationMutex.ReleaseMutex();
        }
        private void bt_search_Click(object sender, RoutedEventArgs e)
        {
            Searchworker.RunWorkerAsync(new SearchData { changed = false, search = tb_search.Text });

        }
        private void Search(object sender, DoWorkEventArgs e)
        {

            //  if ((e.Argument as SearchData).changed)
            var helpList = bl.RequestStationsBy(x => (x as BO.Station).Name.Contains((e.Argument as SearchData).search));
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                stationCollection.Clear();
                foreach (var station in helpList)
                {
                    stationCollection.Add(station);
                }
                cb_stations.SelectedIndex = 0;
                curStation = cb_stations.SelectedItem as BO.Station;
            });
        }
    }

    [ValueConversion(typeof(TimeSpan), typeof(String))]
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            return ((int)ts.TotalMinutes).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            int result;
            if (int.TryParse(strValue, out result))
            {
                return TimeSpan.FromMinutes(result);
            }
            return DependencyProperty.UnsetValue;
        }

    }

}

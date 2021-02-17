using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MenagmentWindow.xaml
    /// </summary>
    /// 
    public partial class MenagmentWindow : Window
    {
        #region variables
        IBL bl;
        string username;
        bool hasChanged = true;
        private static Mutex lineStationMutex = new Mutex();//maneges the access to LineStationXML
        #endregion
        #region line workers //
        BackgroundWorker updateLineWorker = new BackgroundWorker();//updates the list of the busseswe show
        BackgroundWorker deleteLineWorker = new BackgroundWorker();//deletes lines from here and the memory
        BackgroundWorker updateLineInDSWorker = new BackgroundWorker();//updates the momery 
        BackgroundWorker stationsInLineWorker = new BackgroundWorker();//gets the stations in a certain line
        BackgroundWorker removeStationFromLineWorker = new BackgroundWorker();//deletes a stations from the current line
        BackgroundWorker updateAdjecntStationsWorker = new BackgroundWorker();//updates a station in the memory (AdjecentStations)
        BackgroundWorker getLineTripWorker = new BackgroundWorker();//gets the Trips of this line
        BackgroundWorker deleteLineTripWorker = new BackgroundWorker();//gets the Trips of this line


        #endregion

        #region bus workers
        BackgroundWorker AllBusesWorker = new BackgroundWorker();
        BackgroundWorker UpdateBusWorker = new BackgroundWorker();
        BackgroundWorker DeleteBusWorker = new BackgroundWorker();
        BackgroundWorker fuelOrFixWorker = new BackgroundWorker();
        #endregion
        #region station workers
        BackgroundWorker linesInStationWorker = new BackgroundWorker();
        BackgroundWorker AllStationWorker = new BackgroundWorker();
        BackgroundWorker Searchworker = new BackgroundWorker();
        BackgroundWorker RemoveStationWorker = new BackgroundWorker();
        BackgroundWorker updateStationInDSWorker = new BackgroundWorker();

        #endregion

        #region observableCollections
        ObservableCollection<BO.Line> lineCollection = new ObservableCollection<BO.Line>();
        ObservableCollection<BO.Station> stationCollection = new ObservableCollection<BO.Station>();
        ObservableCollection<BO.Bus> busCollection = new ObservableCollection<BO.Bus>();
        ObservableCollection<BO.LineTrip> tripsInLineCollection = new ObservableCollection<BO.LineTrip>();
        ObservableCollection<BO.StationInLine> stationsInLineCollection = new ObservableCollection<BO.StationInLine>();
        ObservableCollection<BO.Line> linesInStationCollection = new ObservableCollection<BO.Line>();
        #endregion

        #region binding variables
        BO.Line curLine;
        BO.Station curStation;
        BO.Bus curBus;
        #endregion
        #region constractors //
        /// <summary>
        /// Initializes the window
        /// </summary>
        /// <param name="_bl">the BLImp </param>
        /// <param name="user">the the username of user who logged in</param>
        public MenagmentWindow(IBL _bl, string user)
        {
            username = user;
            bl = _bl;
            InitializeComponent();
            SetLinesTab();//stas the line Tab
            SetStationTab();//sets the stations tab
            SetBusTab();//sets the busses tab
        }
        #endregion
        #region lines setters //
        void SetLinesTab()
        {
            cb_lines.DisplayMemberPath = "Code";//show only specific Property of object
            cb_lines.SelectedValuePath = "Id";//selection return only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
            gridLine.DataContext = curLine;
            lbl_usernameStations.DataContext = username;
            StationsInLineDataGrid.DataContext = stationsInLineCollection;
            TripInLineDataGrid.DataContext = tripsInLineCollection;
            cb_lines.DataContext = lineCollection;
            lbl_username.DataContext = username;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            stationsInLineWorker.DoWork += SetAllStationsInLine;
            updateLineWorker.DoWork += SetAllLines;
            deleteLineWorker.DoWork += removeLine;
            updateLineInDSWorker.DoWork += UpdateLine;
            removeStationFromLineWorker.DoWork += removeStationFromLine;
            updateAdjecntStationsWorker.DoWork += UpdateAdjecentStation;
            getLineTripWorker.DoWork += SetAllTripsInLine;
            deleteLineTripWorker.DoWork += DeleteLineTrip;
            stationsInLineWorker.WorkerSupportsCancellation = true;
            getLineTripWorker.WorkerSupportsCancellation = true;
            getLineTripWorker.WorkerSupportsCancellation = true;
            updateLineWorker.RunWorkerAsync();
        }
        /// <summary>
        /// gets all the lines and sets them in the DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetAllLines(object sender, DoWorkEventArgs e)
        {
            var help = bl.RequestAllLines();
            App.Current.Dispatcher.Invoke((Action)delegate 
            {
                lineCollection.Clear();
                foreach (var item in help)
                {
                    //if (!LineListContains(item))
                    lineCollection.Add(item);
                }
                cb_lines.SelectedIndex = 0;
            });
        }
        /// <summary>
        /// get the stations in the current line and shows them on the DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetAllStationsInLine(object sender, DoWorkEventArgs e)
        {
            try
            {
                lineStationMutex.WaitOne();//waiting until no one is using the xml for the LineStations
                var helpList = new List<BO.StationInLine>();
                foreach (var b in bl.RequestStationsInLineByLine(curLine.Id))
                {
                    helpList.Add(b);//we get the list of the staions in this line
                }
                lineStationMutex.ReleaseMutex();//we won't use LineStationXML so we release the mutex
                App.Current.Dispatcher.Invoke((Action)delegate // we update the DataGrid with the stations in this line
                {
                    stationsInLineCollection.Clear();
                    foreach (var station in helpList)
                    {
                        stationsInLineCollection.Add(station);
                    }
                });
            }
            catch (BO.InvalidStationIDException)
            {
                lineStationMutex.ReleaseMutex();
            }
        }
        /// <summary>
        /// get the LineTrips in the current line and shows them on the DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetAllTripsInLine(object sender, DoWorkEventArgs e)
        {
            try
            {
                var helpList = new List<BO.LineTrip>();
                foreach (var b in bl.RequestLineTripInLine(curLine.Id))
                {
                    helpList.Add(b);//we get the list of the staions in this line
                }
                App.Current.Dispatcher.Invoke((Action)delegate // we update the DataGrid with the stations in this line
                {
                    tripsInLineCollection.Clear();
                    foreach (var trip in helpList)
                    {
                        tripsInLineCollection.Add(trip);
                    }
                });
            }
            catch (BO.BadLineTripException)
            {
            }
        }
        #endregion
        #region Lines functions
        /// <summary>
        /// what happens when the selection in cb_lines is changed 
        /// displays the new selected line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //updatelinesWorker.RunWorkerAsync( = (cb_lines.SelectedItem as BO.Line);
            curLine = cb_lines.SelectedItem as BO.Line;
            gridLine.DataContext = curLine;
            if (cb_lines.SelectedValue != null)
            {
                stationsInLineWorker.RunWorkerAsync((int)cb_lines.SelectedValue);
                getLineTripWorker.RunWorkerAsync((int)cb_lines.SelectedValue);
            }
            else
            {
                gridLine.DataContext = new BO.Line();
                stationsInLineCollection.Clear();
            }
        }
        private void bt_AddLine_Click(object sender, RoutedEventArgs e)
        {
            var lineWindow = new AddLineWindow(bl);
            lineWindow.Closing += addClosed;
            lineWindow.Show();
        }
        private void addClosed(object sender, CancelEventArgs e)
        {
            updateLineWorker.RunWorkerAsync();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                if (lineCollection.Count > 0)
                {
                    bt_AddStationToLine.IsEnabled = true;
                    bt_DeleteLine.IsEnabled = true;
                    bt_UpdateLine.IsEnabled = true;
                }
            });
        }
        private void bt_DeleteLine_Click(object sender, RoutedEventArgs e)
        {

            object toRemove = cb_lines.SelectedValue;
            deleteLineWorker.RunWorkerAsync(toRemove);
        }
        private void removeLine(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null)
            {
                int toRemove = (int)e.Argument;
                bl.DeleteLine(toRemove);
                SetAllLines(sender, e);

            }

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                if (lineCollection.Count == 0)
                {
                    bt_AddStationToLine.IsEnabled = false;
                    bt_DeleteLine.IsEnabled = false;
                    bt_UpdateLine.IsEnabled = false;
                }

            });

        }
        /// <summary>
        /// what happen when you press the btRemoveStationFromLine button-removes the current LineStation from the current line
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">params</param>
        private void btRemoveStationFromLine_Click(object sender, RoutedEventArgs e)
        {
            removeStationFromLineWorker.RunWorkerAsync((sender as Button).DataContext);
        }
        /// <summary>
        /// removes the current LineStation from the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">params including teh current Station(and by extension the lineId</param>
        private void removeStationFromLine(object sender, DoWorkEventArgs e)
        {
            var st = e.Argument as BO.StationInLine;//the station to remove
            var index = stationsInLineCollection.IndexOf(st);//gets teh index of the station in the line
            bool valid = true;

            App.Current.Dispatcher.Invoke((Action)delegate 
            {

                if (stationsInLineCollection.Count == 2)
                {
                    MessageBox.Show("You can't have less then 2 stations in line");
                    valid = false;
                }

            });
            if (!valid)
                return;//we can't  have less then 2 stations in line
            if (index == stationsInLineCollection.Count - 1 || index == 0)//if we delete the first or the last station
            {
                bl.DeleteStationFromLine(curLine.Id, st.Code, 0, TimeSpan.Zero);//we remove the station
                stationsInLineWorker.RunWorkerAsync(curLine.Id);//we updates the stations in the DataGrid
                return;
            }
            App.Current.Dispatcher.Invoke((Action)delegate // if the station is in the middle of the line
            {
                var stationWin = new RemoveStationLine(bl, curLine, st);//we create a new window
                stationWin.Closing += StationLineWin_Closing;//when the new window will open this window will open
                stationWin.Show();
            });
        }

        private void bt_AddStationToLine_Click(object sender, RoutedEventArgs e)
        {
            var stationWin = new AddStationLine(bl, curLine);
            stationWin.Closing += StationLineWin_Closing;
            stationWin.Show();
        }
        private void StationLineWin_Closing(object sender, CancelEventArgs e)
        {

            stationsInLineWorker.CancelAsync();
            getLineTripWorker.CancelAsync();
            getLineTripWorker.CancelAsync();
            stationsInLineWorker.RunWorkerAsync(curLine.Id);
            linesInStationWorker.RunWorkerAsync(curStation.Code);
            getLineTripWorker.RunWorkerAsync();
        }
        public bool LineListContains(BO.Line line)
        {
            bool exist = false;
            foreach (var li in lineCollection)
            {
                if (li.Id == line.Id)
                    exist = true;
            }
            return exist;
        }
        private void bt_UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            if (cb_lines.SelectedItem != null)
            {
                var helpLine = cb_lines.SelectedItem as BO.Line;
                updateLineInDSWorker.RunWorkerAsync(helpLine);
            }
        }
        /// <summary>
        /// push the updates you made in the current line to the momory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLine(object sender, DoWorkEventArgs e)
        {
            var helpLine = e.Argument as BO.Line;
            bl.UpdateLine(helpLine);
        }

       public void DeleteLineTrip(object sender, DoWorkEventArgs e)
        {
            var helpLineTrip = e.Argument as BO.LineTrip;
            try
            {
                bl.DeleteLineTrip(helpLineTrip.Id);
                SetAllTripsInLine(sender,e);
            }
            catch (BO.BadLineTripException ex)//this shouldn't happen
            {

            }
        }
        private void btRemoveTrip_Click(object sender, RoutedEventArgs e)
        {
            deleteLineTripWorker.RunWorkerAsync((sender as Button).DataContext);
        }
        private void AddTrip_Click(object sender, RoutedEventArgs e)
        {
            var tripTiming = (from trip in tripsInLineCollection
                              select (trip.StartAt, trip.FinishAt)).ToList();
            var timingWin = new AddLineTripWindow(bl, curLine.Id, tripTiming);
            timingWin.Closing += StationLineWin_Closing;
            timingWin.Show();
        }
        #endregion
        #region setters

        void SetStationTab()
        {
            cbStations.DisplayMemberPath = "Name";//show only specific Property of object
            cbStations.SelectedValuePath = "Code";//selection return only specific Property of object
            gridStation.DataContext = curStation;
            LinesInStationDataGrid.DataContext = linesInStationCollection;
            cbStations.DataContext = stationCollection;
            Searchworker.DoWork += Search;
            AllStationWorker.DoWork += SetAllStations;
            linesInStationWorker.DoWork += SetAllLinesInStation;
            RemoveStationWorker.DoWork += DeleteStation;
            updateStationInDSWorker.DoWork += UpdateStation;
            linesInStationWorker.WorkerSupportsCancellation = true;
            AllStationWorker.RunWorkerAsync();
        }


        private void SetAllStations(object sender, DoWorkEventArgs e)
        {
            var help = bl.RequestAllStations();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                stationCollection.Clear();
                foreach (var item in help)
                {
                    //if (!LineListContains(item))
                    //stationCollection.Add(item);
                    stationCollection.Add(item);
                }
                cbStations.SelectedIndex = 0;

                if (stationCollection.Count == 0)
                {
                    bt_DeleteStation.IsEnabled = false;
                    bt_UpdateStation.IsEnabled = false;
                }

                if (stationCollection.Count > 0)
                {
                    bt_DeleteStation.IsEnabled = true;
                    bt_UpdateStation.IsEnabled = true;
                }

                tb_search.Text = "";
            });
        }

        private void SetAllLinesInStation(object sender, DoWorkEventArgs e)
        {
            try
            {
                var helpList = new List<BO.Line>();
                lineStationMutex.WaitOne();
                foreach (var b in bl.LinesInStation((int)e.Argument))
                {
                    //helpList.Add(bl.Ge
                    helpList.Add(b);
                }
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    linesInStationCollection.Clear();
                    foreach (var line in helpList)
                    {
                        linesInStationCollection.Add(line);
                    }
                });
            }
            catch (BO.InvalidStationIDException)
            {

            }
            finally { lineStationMutex.ReleaseMutex(); }
        }

        #endregion
        #region functions

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curStation = cbStations.SelectedItem as BO.Station;
            gridStation.DataContext = curStation;
            lineStationMutex.WaitOne();
            if (cbStations.SelectedValue != null)
            {
                linesInStationWorker.RunWorkerAsync((int)cbStations.SelectedValue);
            }
            else
            {
                gridStation.DataContext = new BO.Station();
                linesInStationCollection.Clear();
            }
            lineStationMutex.ReleaseMutex();
        }
        private void bt_search_Click(object sender, RoutedEventArgs e)
        {
            Searchworker.RunWorkerAsync(new SearchData { changed = hasChanged, search = tb_search.Text });

        }
        private void Search(object sender, DoWorkEventArgs e)
        {

            //  if ((e.Argument as SearchData).changed)
            var helpList = bl.RequestStationsBy(x => (x as BO.Station).Name.Contains((e.Argument as SearchData).search));
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if ((e.Argument as SearchData).search == "הוועד הלאומי 21")
                    bt_secret.Visibility = Visibility.Visible;
                else
                    bt_secret.Visibility = Visibility.Collapsed;


            });
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                stationCollection.Clear();
                foreach (var station in helpList)
                {
                    stationCollection.Add(station);
                }
                cbStations.SelectedIndex = 0;

                if (stationCollection.Count == 0)
                {
                    bt_DeleteStation.IsEnabled = false;
                    bt_UpdateStation.IsEnabled = false;
                }

                if (stationCollection.Count > 0)
                {
                    bt_DeleteStation.IsEnabled = true;
                    bt_UpdateStation.IsEnabled = true;
                }
            });
        }
        #endregion
        #region busses Tab
        void SetBusTab()
        {
            cbBuses.DisplayMemberPath = "LicenseNum";//show only specific Property of object
            cbBuses.SelectedValuePath = "LicenseNum";
            cbBuses.SelectedIndex = 0; //index of the object to be selected
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.Status));
            cbBuses.DataContext = busCollection;
            AllBusesWorker.DoWork += SetAllBuses;
            DeleteBusWorker.DoWork += DeleteBus;
            UpdateBusWorker.DoWork += UpdateBus;
            fuelOrFixWorker.DoWork += FuelOrFix;
            AllBusesWorker.RunWorkerAsync();
        }

        void SetAllBuses(object sender, DoWorkEventArgs e)
        {
            var help = bl.RequestAllBuses();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                var index = cbBuses.SelectedIndex;

                busCollection.Clear();
                foreach (var item in help)
                {
                    //if (!LineListContains(item))
                    busCollection.Add(item);
                }
                if (index == -1)
                    cbBuses.SelectedIndex = 0;
                else if (index < busCollection.Count)
                    cbBuses.SelectedIndex = index;
                else cbBuses.SelectedIndex = index - 1;
            });
        }

        private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curBus = cbBuses.SelectedItem as BO.Bus;
            gridBus.DataContext = curBus;
        }

        #endregion

        private void bt_DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            if (linesInStationCollection.Count == 0)
            {
                object toRemove = cbStations.SelectedValue;

                RemoveStationWorker.RunWorkerAsync(toRemove);
            }
            else MessageBox.Show("Station still exist in some lines, cannot delete!");
        }

        private void DeleteStation(object sender, DoWorkEventArgs e)
        {
            bl.DeleteStation((int)e.Argument);
            AllStationWorker.RunWorkerAsync();
            
        }

        private void bt_DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            if (busCollection.Count == 1)
            {
                bt_DeleteBus.IsEnabled = false;
                bt_UpdateBus.IsEnabled = false;
                bt_fuel.IsEnabled = false;
                bt_fix.IsEnabled = false;
            }
            DeleteBusWorker.RunWorkerAsync(cbBuses.SelectedValue);
        }

        private void DeleteBus(object sender, DoWorkEventArgs e)
        {
            bl.DeleteBus((int)e.Argument);
            AllBusesWorker.RunWorkerAsync();
        }
        /// <summary>
        /// shshshshshshsh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_secret_Click(object sender, RoutedEventArgs e)
        {
            var win = new HiddenWindow();
            win.Closing += OpenWindowafterUsage;
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// open back the window after we close the secret window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenWindowafterUsage(object sender, CancelEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            bt_secret.Visibility = Visibility.Collapsed;
        }
        void bt_AddBus_Click(object sender, RoutedEventArgs e)
        {
            var busWin = new AddBusWindow(bl);
            busWin.Closing += BusWin_Closing;
            busWin.Show();
        }
        void BusWin_Closing(object sender, CancelEventArgs e)
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            List<int> res1 = list.FindAll(x => x % 2 == 0);
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                if (lineCollection.Count > 0)
                {
                    bt_UpdateBus.IsEnabled = true;
                    bt_DeleteBus.IsEnabled = true;
                    bt_fuel.IsEnabled = true;
                    bt_fix.IsEnabled = true;
                }
            });
            AllBusesWorker.RunWorkerAsync();
        }

        private void bt_UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            updateStationInDSWorker.RunWorkerAsync();
        }
        private void UpdateStation(object sender, DoWorkEventArgs e)
        {
            bl.UpdateStation(curStation);
            AllStationWorker.RunWorkerAsync();
            if (lineCollection.Count != 0) stationsInLineWorker.RunWorkerAsync(curLine.Id);
        }

        private void bt_UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            UpdateBusWorker.RunWorkerAsync();
        }

        private void UpdateBus(object sender, DoWorkEventArgs e)
        {
            bl.UpdateBus(curBus);
            AllBusesWorker.RunWorkerAsync();
        }

        private void bt_AddStation_Click(object sender, RoutedEventArgs e)
        {
            var stationWin = new AddStationWindow(bl);
            stationWin.Closing += StationWin_Closing;
            stationWin.Show();
        }
        void StationWin_Closing(object sender, CancelEventArgs e)
        {
            AllStationWorker.RunWorkerAsync();
        }

        private void bt_FuelOrFix_Click(object sender, RoutedEventArgs e)
        {
            bool isFuel;
            if ((sender as Button).Name == "bt_fuel") isFuel = true;
            else isFuel = false;
            fuelOrFixWorker.RunWorkerAsync(isFuel);
        }
        private void FuelOrFix(object sender, DoWorkEventArgs e)
        {
            if ((bool)e.Argument) bl.FuelBus(curBus.LicenseNum);
            else bl.FixBus(curBus.LicenseNum);
            AllBusesWorker.RunWorkerAsync();
        }

        /// <summary>
        /// what happens when you click on the update button(updates the time between the stations
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e"></param>
        private void bt_updatStationInLine_Click(object sender, RoutedEventArgs e)
        {
            updateAdjecntStationsWorker.RunWorkerAsync((sender as Button).DataContext);//we update the station in a BackgroundWorker
        }

       /// <summary>
       /// enter the updates we made in the station into the memory
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e">the args including the updated stations</param>
        private void UpdateAdjecentStation(object sender, DoWorkEventArgs e)
        {

            var adjSt = e.Argument as BO.StationInLine;
            bl.UpdateAdjacentStations(adjSt.PrevStation, adjSt.StationId, adjSt.DistFromLastStation, adjSt.TimeSinceLastStation);
            stationsInLineWorker.RunWorkerAsync(curLine);//displays the updated stations

        }


    }

    /// <summary>
    /// A converter to show the last station name, instead of the station code
    /// </summary>
    [ValueConversion(typeof(int), typeof(String))]
    public class IntTotationNameAsString : IValueConverter
    {
        Mutex mutx = new Mutex();
        /// <summary>
        /// converts between the code to the name
        /// </summary>
        /// <param name="value">the value t oconvert</param>
        /// <param name="targetType">the Type to convert into</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>the name of the station</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int code = (int)value;
                return BLAPI.BLFactory.GetBL("").RequestStation(code).Name;
            }
            catch (BO.InvalidStationIDException)
            { return ""; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            try
            {
                 BO.Station station = BLAPI.BLFactory.GetBL("").RequestAllStations().Where(p => p.Name == strValue).FirstOrDefault();
                return station.Code;
            }
            catch (NullReferenceException)
            {
                return DependencyProperty.UnsetValue;
            }
        }

    }
}

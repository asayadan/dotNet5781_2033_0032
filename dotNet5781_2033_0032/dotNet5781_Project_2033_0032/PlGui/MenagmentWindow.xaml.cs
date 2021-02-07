using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
        #endregion
        #region line workers
        BackgroundWorker updateLineWorker = new BackgroundWorker();//updates the list of the busseswe show
        BackgroundWorker deleteLineWorker = new BackgroundWorker();//deletes lines from here and the memory
        BackgroundWorker updateLineInDSWorker = new BackgroundWorker();//
        BackgroundWorker stationsInLineWorker = new BackgroundWorker();
        BackgroundWorker removeStationFromLineWorker = new BackgroundWorker();
        BackgroundWorker updateAdjecntStationsWorker = new BackgroundWorker();
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
        ObservableCollection<BO.StationInLine> stationsInLineCollection = new ObservableCollection<BO.StationInLine>();
        ObservableCollection<BO.Line> linesInStationCollection = new ObservableCollection<BO.Line>();
        #endregion

        #region binding variables
        BO.Line curLine;
        BO.Station curStation;
        BO.Bus curBus;
        #endregion
        #region constractors
        public MenagmentWindow(IBL _bl, string user)
        {
            SimulationControlWindow win = new SimulationControlWindow(_bl);
            win.Show();
            username = user;
            bl = _bl;
            InitializeComponent();
            SetLinesTab();
            SetStationTab();
            SetBusTab();
        }
        #endregion
        #region setters
        void SetLinesTab()
        {
            cb_lines.DisplayMemberPath = "Code";//show only specific Property of object
            cb_lines.SelectedValuePath = "Id";//selection return only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
            gridLine.DataContext = curLine;
            lbl_usernameStations.DataContext = username;
            StationsInLineDataGrid.DataContext = stationsInLineCollection;
            cb_lines.DataContext = lineCollection;
            lbl_username.DataContext = username;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            stationsInLineWorker.DoWork += SetAllStationsInLine;
            updateLineWorker.DoWork += SetAllLines;
            deleteLineWorker.DoWork += removeLine;
            updateLineInDSWorker.DoWork += UpdateLine;
            removeStationFromLineWorker.DoWork += removeStationFromLine;
            updateAdjecntStationsWorker.DoWork += UpdateAdjecentStation;
            stationsInLineWorker.WorkerSupportsCancellation = true;
            updateLineWorker.RunWorkerAsync();
        }



        void SetAllLines(object sender, DoWorkEventArgs e)
        {
            var help = bl.RequestAllLines();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
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
        void SetAllStationsInLine(object sender, DoWorkEventArgs e)
        {
            try
            {
                var helpList = new List<BO.StationInLine>();
                foreach (var b in bl.RequestStationsInLine(curLine.Id))
                {
                    helpList.Add(b);
                }
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    stationsInLineCollection.Clear();
                    foreach (var station in helpList)
                    {
                        stationsInLineCollection.Add(station);
                    }
                    cbStations.SelectedIndex = 0;
                });
            }
            catch (BO.InvalidStationIDException)
            {

            }
        }
        #endregion
        #region  functions
        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //updatelinesWorker.RunWorkerAsync( = (cb_lines.SelectedItem as BO.Line);
            curLine = cb_lines.SelectedItem as BO.Line;
            gridLine.DataContext = curLine;
            if (cb_lines.SelectedValue != null)
            {
                stationsInLineWorker.RunWorkerAsync((int)cb_lines.SelectedValue);
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
                bl.RemoveLine(toRemove);
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
        private void btRemoveStationFromLine_Click(object sender, RoutedEventArgs e)
        {
            removeStationFromLineWorker.RunWorkerAsync((sender as Button).DataContext);
        }

        private void removeStationFromLine(object sender, DoWorkEventArgs e)
        {
            var st = e.Argument as BO.StationInLine;
            var index = stationsInLineCollection.IndexOf(st);
            bool valid = true;

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {

                if (stationsInLineCollection.Count == 2)
                {
                    MessageBox.Show("You can't have less then 2 stations in line");
                    valid = false;
                }

            });
            if (!valid)
                return;
            if (index == stationsInLineCollection.Count - 1 || index == 0)
            {
                bl.RemoveStationFromLine(curLine.Id, st.Code, 0, TimeSpan.Zero);
                stationsInLineWorker.RunWorkerAsync(curLine.Id);
                return;
            }
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                var stationWin = new RemoveStationLine(bl, curLine, st);
                stationWin.Closing += StationLineWin_Closing;
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
            stationsInLineWorker.RunWorkerAsync(curLine.Id);
            linesInStationWorker.RunWorkerAsync(curStation.Code);
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
        private void UpdateLine(object sender, DoWorkEventArgs e)
        {
            var helpLine = e.Argument as BO.Line;
            bl.UpdateLine(helpLine);
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
        }

        #endregion
        #region functions

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curStation = cbStations.SelectedItem as BO.Station;
            gridStation.DataContext = curStation;
            if (cbStations.SelectedValue != null)
            {
                linesInStationWorker.RunWorkerAsync((int)cbStations.SelectedValue);
            }
            else
            {
                gridStation.DataContext = new BO.Station();
                linesInStationCollection.Clear();
            }
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
        private void bt_secret_Click(object sender, RoutedEventArgs e)
        {
            var win = new HiddenWindow();
            win.Closing += OpenWindowafterUsage;
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }
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

        private void bt_updatStationInLine_Click(object sender, RoutedEventArgs e)
        {
            updateAdjecntStationsWorker.RunWorkerAsync((sender as Button).DataContext);
        }

        private void UpdateAdjecentStation(object sender, DoWorkEventArgs e)
        {

            var adjSt = e.Argument as BO.StationInLine;
            bl.UpdateAdjacentStations(adjSt.PrevStation, adjSt.StationId, adjSt.DistFromLastStation, adjSt.TimeSinceLastStation);
            stationsInLineWorker.RunWorkerAsync(curLine);

        }
    }
}

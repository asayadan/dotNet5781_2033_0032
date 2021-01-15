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
        #endregion
        #region line workers
        BackgroundWorker updateLineWorker = new BackgroundWorker();//updates the list of the busseswe show
        BackgroundWorker deleteLineWorker = new BackgroundWorker();//deletes lines from here and the memory
        BackgroundWorker updateLineInDSWorker = new BackgroundWorker();//
        BackgroundWorker stationsInLineWorker = new BackgroundWorker();
        BackgroundWorker removeStationFromLineWorker = new BackgroundWorker();
        #endregion

        #region bus workers
        BackgroundWorker busWorker = new BackgroundWorker();
        #endregion
        #region station workers
        BackgroundWorker linesInStationWorker = new BackgroundWorker();
        BackgroundWorker AllStationWorker = new BackgroundWorker();
        #endregion

        #region observableCollections
        ObservableCollection<BO.Line> lineCollection = new ObservableCollection<BO.Line>();
        ObservableCollection<BO.Station> stationCollection = new ObservableCollection<BO.Station>();
        ObservableCollection<BO.Bus> busCollection;
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
            StationsInLineDataGrid.DataContext = stationsInLineCollection;
            cb_lines.DataContext = lineCollection;
            lbl_username.DataContext = username;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            stationsInLineWorker.DoWork += SetAllStationsInLine;
            updateLineWorker.DoWork += SetAllLines;
            deleteLineWorker.DoWork += removeLine;
            updateLineInDSWorker.DoWork += UpdateLine;
            removeStationFromLineWorker.DoWork += removeStationFromLine;
            updateLineWorker.RunWorkerAsync();
        }



        void SetAllLines(object sender, DoWorkEventArgs e)
        {
            var help = bl.GetAllLines();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                lineCollection.Clear();
                foreach (var item in help)
                {
                    //if (!LineListContains(item))
                    lineCollection.Add(item);
                }
                cb_lines.SelectedIndex = 0;
                cbStations.SelectedIndex = 0;
            });
        }
        void SetAllStationsInLine(object sender, DoWorkEventArgs e)
        {
            try
            {
                var helpList = new List<BO.StationInLine>();
                foreach (var b in bl.GetStationsInLine((int)e.Argument))
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
                    bt_AddStation.IsEnabled = true;
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
                    bt_AddStation.IsEnabled = false;
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
            var st = e.Argument as BO.Station;
            var index = stationsInLineCollection.IndexOf(st);
            if (index == stationsInLineCollection.Count - 1 || index == 0)
            {
                bl.RemoveStationFromLine(curLine.Id, st.Code, 0, TimeSpan.Zero);
                stationsInLineWorker.RunWorkerAsync(curLine.Id);
                return;
            }


            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                var stationWin = new RemoveStationLine(bl, curLine, st);
                stationWin.Closing += StationWin_Closing;
                stationWin.Show();
            });
        }

        private void bt_AddStation_Click(object sender, RoutedEventArgs e)
        {
            var stationWin = new AddStationLine(bl, curLine);
            stationWin.Closing += StationWin_Closing;
            stationWin.Show();
        }
        private void StationWin_Closing(object sender, CancelEventArgs e)
        {
            stationsInLineWorker.RunWorkerAsync(curLine.Id);
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
            AllStationWorker.DoWork += SetAllStations;
            linesInStationWorker.DoWork += SetAllLinesInStation;
            linesInStationWorker.WorkerSupportsCancellation = true;
            AllStationWorker.RunWorkerAsync();
        }

        private void SetAllStations(object sender, DoWorkEventArgs e)
        {
            var help = bl.GetAllStations();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                stationCollection.Clear();
                foreach (var item in help)
                {
                        //if (!LineListContains(item))
                        stationCollection.Add(item);
                }
                cbStations.SelectedIndex = 0;
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
        #endregion
        #region busses Tab
        void SetBusTab()
        {
            cbBuses.DisplayMemberPath = "LicenseNum";//show only specific Property of object
            cbBuses.SelectedIndex = 0; //index of the object to be selected
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.Status));
            //      SetAllBuses();
        }

        //void SetAllBuses()
        //{
        //    cbBuses.DataContext = bl.GetAllBuses();
        //}

        private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curBus = cbBuses.SelectedItem as BO.Bus;
            gridBus.DataContext = curBus;
        }
        #endregion
    }
}

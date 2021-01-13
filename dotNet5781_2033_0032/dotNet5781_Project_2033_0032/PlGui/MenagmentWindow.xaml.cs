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

        IBL bl;
        string username;
        BackgroundWorker busWorker = new BackgroundWorker();
        ObservableCollection<BO.Bus> busCollection;
        BackgroundWorker lineWorker = new BackgroundWorker();
        ObservableCollection<BO.Line> lineCollection = new ObservableCollection<BO.Line>();
        BackgroundWorker stationsInLineWorker = new BackgroundWorker();
        ObservableCollection<BO.Station> stationsInLineCollection = new ObservableCollection<BO.Station>();

        BO.Line curLine;
        BO.Bus curBus;
        public MenagmentWindow(IBL _bl, string user)
        {
            username = user;
            bl = _bl;
            InitializeComponent();
            SetBusTab();
            SetLinesTab();
        }

        #region setters
        void SetLinesTab()
        {
            cbLines.DisplayMemberPath = "Code";//show only specific Property of object
            cbLines.SelectedValuePath = "Id";//selection return only specific Property of object
            cbLines.SelectedIndex = 0; //index of the object to be selected
            gridLine.DataContext = curLine;
            StationsInLineDataGrid.DataContext = stationsInLineCollection;
            cbLines.DataContext = lineCollection;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            lineWorker.DoWork += SetAllLines;
            lineWorker.RunWorkerAsync();
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
                cbLines.SelectedIndex = 0;
            });
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

        void SetAllStations(object sender, DoWorkEventArgs e)
        {
            try
            {

                var helpList = new List<BO.Station>();
                foreach (var b in bl.GetLineStationsInLine((int)e.Argument))
                {
                    helpList.Add(bl.GetStation(b.StationId));
                }
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
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

            }
        }

        void SetBusTab()
        {
            cbBuses.DisplayMemberPath = "LicenseNum";//show only specific Property of object
            cbBuses.SelectedIndex = 0; //index of the object to be selected
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.Status));
            SetAllBuses();
        }

        void SetAllBuses()
        {
            cbBuses.DataContext = bl.GetAllBuses();
        }
        #endregion

        #region Line functions
        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lineWorker.RunWorkerAsync( = (cb_lines.SelectedItem as BO.Line);
            curLine = cbLines.SelectedItem as BO.Line;
            gridLine.DataContext = curLine;
            if (cbLines.SelectedValue != null)
            {
                stationsInLineWorker.DoWork += SetAllStations;
                stationsInLineWorker.RunWorkerAsync((int)cbLines.SelectedValue);
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
            lineWorker = new BackgroundWorker();
            lineWorker.DoWork += SetAllLines;
            lineWorker.RunWorkerAsync();
        }
        private void bt_DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            lineWorker = new BackgroundWorker();
            lineWorker.DoWork += removeLine;
            lineWorker.RunWorkerAsync();
        }

        private void removeLine(object sender, DoWorkEventArgs e)
        {
            int toRemove = -1;
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                if (cbLines.SelectedItem != null)
                    toRemove = (int)cbLines.SelectedValue;
                else toRemove = -1;

            });

            if (toRemove != -1)
            {
                bl.RemoveLine(toRemove);
                SetAllLines(sender, e);
            }


        }
        private void btRemoveStation_Click(object sender, RoutedEventArgs e)
        {

            var st = ((sender as Button).DataContext as BO.Station);
            //bl.RemoveStationFromLine(curLine.Id, st.Code);
            //RefreshAllRegisteredCoursesGrid();
            // RefreshAllNotRegisteredCoursesGrid();
        }
        private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curBus = cbBuses.SelectedItem as BO.Bus;
            gridBus.DataContext = curBus;
        }
        #endregion


    }
}

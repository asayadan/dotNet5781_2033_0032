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
        ObservableCollection<BO.Line> lineCollection= new ObservableCollection<BO.Line>();
        BackgroundWorker stationsInLineWorker = new BackgroundWorker();
        ObservableCollection<BO.Station> stationsInLineCollection=new ObservableCollection<BO.Station>();

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
            cb_lines.DisplayMemberPath = "Code";//show only specific Property of object
            cb_lines.SelectedValuePath = "Id";//selection return only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
            gridLine.DataContext = curLine;
            StationsInLineDataGrid.DataContext = stationsInLineCollection;
            cb_lines.DataContext = lineCollection;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            lineWorker.DoWork+=SetAllLines;
            lineWorker.RunWorkerAsync();
        }

        void SetAllLines(object sender, DoWorkEventArgs e)
        {

            var help = bl.GetAllLines();
            foreach (var item in help)
            {
                lineCollection.Add(item);
            }
        }
        void setAllStations(object sender, DoWorkEventArgs e)
        {
            try
            {
                var a = bl.GetLineStationsInLine((int)e.Argument);

                stationsInLineCollection.Clear();
                foreach (var b in a)
                {
                    stationsInLineCollection.Add(bl.GetStation(b.StationId));
                }
            }
            catch (BO.InvalidStationIDException)
            {

            }
        }

        void SetBusTab()
        {
            cbBuses.DisplayMemberPath = "LicenseNum";//show only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
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
            curLine = cb_lines.SelectedItem as BO.Line;
            if (cb_lines.SelectedValue != null)
            {
                stationsInLineWorker.DoWork += setAllStations;
                stationsInLineWorker.RunWorkerAsync((int)cb_lines.SelectedValue);
                //stationsInLineWorker.DoWork -= setAllStations;
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

using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        ObservableCollection<BO.Line> lineCollection;

        BO.Line curLine;
        public MenagmentWindow(IBL _bl, string user)
        {
            username = user;
            bl = _bl;
            InitializeComponent();
            cb_lines.DisplayMemberPath = "Code";//show only specific Property of object
            cb_lines.SelectedValuePath = "Id";//selection return only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            SetAllLines();
        }


        void SetAllLines()
        {
            cb_lines.DataContext = bl.GetAllLines();
        }

        void setAllStations()
        {
            var a = bl.GetLineStationsInLine((int)cb_lines.SelectedValue);
            var c = new List<BO.Station>();
            foreach (var b in a)
            {
                c.Add(bl.GetStation(b.StationId));
            }
            StationsInLineDataGrid.DataContext = c;
        }

        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lineWorker.RunWorkerAsync( = (cb_lines.SelectedItem as BO.Line);
            curLine = cb_lines.SelectedItem as BO.Line;
            gridLine.DataContext = curLine;

            if (cb_lines.SelectedValue != null)
            {
                setAllStations();
            }
        }

        private void Worker(object sender, DoWorkEventArgs e)
        {

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

        }
    }
}

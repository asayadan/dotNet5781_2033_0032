using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        IBL bl;
        BackgroundWorker worker = new BackgroundWorker();
        public AddLineWindow(IBL bL)
        {
            bl = bL;
            InitializeComponent();
            firstStationComboBox.DisplayMemberPath = "Name";//show only specific Property of object
            firstStationComboBox.SelectedValuePath = "Code";
            lastStationComboBox.DisplayMemberPath = "Name";//show only specific Property of object
            lastStationComboBox.SelectedValuePath = "Code";
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            worker.DoWork += SetAllStations;
            worker.RunWorkerAsync();
            for (int i = 0; i < 60; i++)
            {
                cb_minutes.Items.Add(i); cb_seconds.Items.Add(i);
                if (i < 24) { cb_hours.Items.Add(i); }
            }


        }
        void SetAllStations(object sender, DoWorkEventArgs e)
        {
            var help = bl.RequestAllStations();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                firstStationComboBox.ItemsSource = help;
                lastStationComboBox.ItemsSource = help;
            });
        }
        /// <summary>
        /// sets the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddLine(object sender, DoWorkEventArgs e)
        {

            int a = 0, firstStation = 0, lastStation = 0;
            double distance = 0;
            bool valid = true;
            BO.Areas b = BO.Areas.Center;
            TimeSpan timeSpan = TimeSpan.Zero;
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try
                {//gets all the required parameters
                    a = int.Parse(CodeTextBox.Text);
                    b = (BO.Areas)areaComboBox.SelectedItem;
                    firstStation = (int)firstStationComboBox.SelectedValue;
                    lastStation = (int)lastStationComboBox.SelectedValue;
                    distance = double.Parse(tb_distance.Text);
                    timeSpan = new TimeSpan((int)cb_hours.SelectedItem,
                                        (int)cb_minutes.SelectedItem,
                                        (int)cb_seconds.SelectedItem);
                    if (firstStation == lastStation)
                    {
                        tbl_warnings.Text = "the stations should be different";
                        valid = false;
                    }
                }
                catch (OverflowException)
                {
                    tbl_warnings.Text = "Invalid code!";
                    valid = false;
                }
                catch (FormatException)
                {
                    tbl_warnings.Text = "Invalid code!";
                    valid = false;
                }
            });

            try
            {
                if (valid)
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    bl.CreateLine(a, b, firstStation, lastStation, distance, timeSpan);
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
            if (CodeTextBox.Text != null &&
                areaComboBox.SelectedItem != null &&
                firstStationComboBox.SelectedItem != null &&
                lastStationComboBox.SelectedItem != null)
            {
                worker.DoWork -= SetAllStations;
                worker.DoWork += AddLine;
                worker.RunWorkerAsync();
            }
        }


        //public static readonly AttachedPropertyBrowsableForChildrenAttribute attachedPropertyBrowsableForChildrenAttribute;

    }
}

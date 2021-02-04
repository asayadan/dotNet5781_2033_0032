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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBL bl;
        BackgroundWorker worker = new BackgroundWorker();
        public AddStationWindow(IBL bL)
        {
            bl = bL;
            InitializeComponent();
        }


        void AddStation(object sender, DoWorkEventArgs e)
        {

            int code = 0;
            double longitude = 0, latitude = 0;
            string name = string.Empty;
            bool valid = true;
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try
                {
                    code = int.Parse(stationTextBox.Text);
                    longitude = double.Parse(longitudeTextBox.Text);
                    latitude = double.Parse(latitudeTextBox.Text);
                    name = nameTextBox.Text;
                }
                catch (OverflowException)
                {
                    tbl_warnings.Text = "Invalid input!";
                    valid = false;
                }
                catch (FormatException)
                {
                    tbl_warnings.Text = "Invalid input!";
                    valid = false;
                }
            });

            try
            {
                if (valid)
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    bl.CreateStation(code, name, longitude, latitude);
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        Close();
                    });
                }
            }

            catch (BO.InvalidStationIDException ex)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    tbl_warnings.Text = ex.Message;
                });
            }
        }


        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(stationTextBox.Text) &&
                !string.IsNullOrEmpty(nameTextBox.Text) &&
                !string.IsNullOrEmpty(longitudeTextBox.Text) &&
                !string.IsNullOrEmpty(latitudeTextBox.Text))
            {
                worker.DoWork += AddStation;
                worker.RunWorkerAsync();
            }

            else tbl_warnings.Text = "Please fill all fields!";
        }
    }
}

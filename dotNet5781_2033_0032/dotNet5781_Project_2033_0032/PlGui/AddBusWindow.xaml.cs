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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        IBL bl;
        BackgroundWorker worker = new BackgroundWorker();
        public AddBusWindow(IBL bL)
        {
            bl = bL;
            InitializeComponent();
        }


        void AddBus(object sender, DoWorkEventArgs e)
        {

            int licenseNumber = 0;
            double mileage = 0, fuel = 0;
            bool valid = true;
            BO.Status status = BO.Status.fixing;
            DateTime fromDate = new DateTime();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try
                {
                    licenseNumber = int.Parse(licenseTextBox.Text);
                    fromDate = fromDateDatePicker.DisplayDate;
                    mileage = double.Parse(mileageTextBox.Text);
                    if (fuelTextBox.Text == "Full Gas Tank") fuel = 1200;
                    else fuel = double.Parse(fuelTextBox.Text);
                    
                    if (fuel < 0 || mileage < 0)
                    {
                        tbl_warnings.Text = "Remaining fuel and Total trip cannot be negative!";
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
                    bl.AddBus(licenseNumber, fromDate, fuel, mileage);
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        Close();
                    });
                }
            }

            catch (BO.InvalidBusLicenseNumberException ex)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    tbl_warnings.Text = ex.Message;
                });
            }
        }


        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (licenseTextBox.Text != null &&
                fromDateDatePicker.SelectedDate != null &&
                !string.IsNullOrEmpty(mileageTextBox.Text) &&
                !string.IsNullOrEmpty(fuelTextBox.Text))
            {
                worker.DoWork += AddBus;
                worker.RunWorkerAsync();
            }

            else tbl_warnings.Text = "Please fill all fields!";
        }
    }
}

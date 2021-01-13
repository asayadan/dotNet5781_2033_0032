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
        void SetAllStations(object sender, DoWorkEventArgs e)
        {
            var help = bl.GetAllStations();
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                firstStationComboBox.ItemsSource = help;
                lastStationComboBox.ItemsSource = help;
            });
        }

        void AddLine(object sender, DoWorkEventArgs e)
        {

            int a = 0, c = 0, d = 0;
            bool valid = true;
            BO.Areas b = BO.Areas.Center;

            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                try {
                    a = int.Parse(CodeTextBox.Text);
                    b = (BO.Areas)areaComboBox.SelectedItem;
                    c = (int)firstStationComboBox.SelectedValue;
                    d = (int)lastStationComboBox.SelectedValue;
                }

                catch (OverflowException)
                {
                    MessageBox.Show("Invalid code!");
                    valid = false;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid code!");
                    valid = false;
                }

                

            });

            try
            {
                if (valid)
                {
                    bl.AddLine(a, b, c, d);
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

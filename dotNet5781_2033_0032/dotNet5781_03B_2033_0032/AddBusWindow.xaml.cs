﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        MainWindow win;
        public AddBusWindow(MainWindow _win)
        {
            win = _win;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // "Done" button has been clicked,
        {                                                           // check if valid then insert.
            try
            {
                if (allFields())
                {
                    if (checkValidation(int.Parse(license.Text), (DateTime)dp_date.SelectedDate))
                    {
                        if (!MainWindow.buses.Exists(x => x.licensePlate == int.Parse(license.Text)))
                        {
                            if (cb_data.IsChecked == false)
                            {
                                MainWindow.buses.Add(new Bus(int.Parse(license.Text), (DateTime)dp_date.SelectedDate));
                                Close();
                                win.addNew(MainWindow.buses.Count - 1);
                            }
                            else
                            {
                                MainWindow.buses.Add(new Bus(int.Parse(license.Text), (DateTime)dp_date.SelectedDate, (DateTime)date_treatment.SelectedDate, int.Parse(tb_mileage.Text), int.Parse(tb_mileage_in_last_tratment.Text), int.Parse(tb_fuel.Text)));
                                Close();
                                win.addNew(MainWindow.buses.Count - 1);
                            }
                        }
                        else throw new ArgumentException("License plate number already exists.");
                    }
                    else throw new ArgumentException("Please enter a valid license plate number.");
                }
                else throw new ArgumentException("Please fill all the fields.");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void mouseEnter(object sender, EventArgs e) // If the mouse enters the textbox, remove the
        {                                                   // preview text to make it easier for the user
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text = string.Empty;
        }

        private void mouseLeave(object sender, MouseEventArgs e) // If the mouse leaves the textbox, return the
        {                                                        // preview text to make it more clear what to enter here.
            if ((sender as TextBox).Text == "")
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
        }




        private void keyDown(object sender, KeyEventArgs e) // If we started writing something, remove the preview text
        {                                                   // to make it easier for the user to write
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
            {
                (sender as TextBox).Text = "";
            }
        }

        private void cb_data_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)cb_data.IsChecked)
            {
                this.Height = 310;
            }
            else
            {
                this.Height = 150;
            }


        }

        bool allFields() // Check if all fiels were inserted.
        {
            var count = MainGrid.Children.Cast<UIElement>().Where(x => (x is TextBox) ? (x as TextBox).Text != (x as TextBox).Tag.ToString()
                        : !(x is Button) && !(x is CheckBox) && (x as DatePicker).SelectedDate != null).Count();
            if ((bool)cb_data.IsChecked)
                return (count == 6);
            else return count == 2;

        }

        public static bool checkValidation(int _licensePlateNumber, DateTime _date)
        {
            if (((_date.Year >= 2018) && (_licensePlateNumber > 9999999)) && (_licensePlateNumber <= 99999999) ||//the bus registered after 2018 and has 8 digits
                ((_date.Year < 2018) && (_licensePlateNumber <= 9999999)) && (_licensePlateNumber > 999999))//the bus registered before 2018 and has 7 digits
                return true;//the license plate number is valid
            else
            {
                return false;
            }
        }

        private void dp_date_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if ((sender as DatePicker).SelectedDate != null)
            {
                date_treatment.DisplayDateStart = (sender as DatePicker).SelectedDate;
            }
        }
    }
}

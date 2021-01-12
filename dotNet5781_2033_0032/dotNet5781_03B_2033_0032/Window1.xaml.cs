using System;
using System.Windows;

namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow win;
        int index;
        Bus thisBus;
        public Window1(MainWindow _win, int _index)
        {
            InitializeComponent();
            win = _win;
            index = _index;
            thisBus = MainWindow.buses[index];
            textsGrid.DataContext = thisBus;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Reloading the data screen
        {                                                           // so the information will be updated.
            try
            {
                index = win.FindIndex(thisBus);
                thisBus = new Bus(66666666, new DateTime(2019, 1, 1));
                textsGrid.DataContext = thisBus;
                thisBus = MainWindow.buses[index];
                textsGrid.DataContext = thisBus;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void bt_refule_Click(object sender, RoutedEventArgs e) // pressed on the refuel button
        {
            try
            {
                index = win.FindIndex(thisBus);
                if (thisBus.curStatus == Status.ready)
                {
                    thisBus.Event(Status.refueling);
                }
                else
                {
                    MessageBox.Show("you can't fix the bus because he is " + thisBus.curStatus.ToString());
                }

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

        }

        private void bt_finished_Click(object sender, RoutedEventArgs e) // Pressed the fix button
        {
            try
            {
                index = win.FindIndex(thisBus);
                if (thisBus.curStatus == Status.ready)
                {
                    thisBus.Event(Status.fixing);
                }
                else
                {
                    MessageBox.Show("you can't fix the bus because he is " + thisBus.curStatus.ToString());
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

        }

    }
}

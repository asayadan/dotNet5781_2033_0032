using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;


namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Bus> buses;
        private DateTime a;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                initilize(ref buses);

                
                for (int i = 0; i < buses.Count; i++)
                    addBus(i);

                var timer1 = new Timer(1000);
                timer1.Elapsed += new ElapsedEventHandler(timer1_Tick);
                a = DateTime.Now;
                UpGrid.DataContext = a;
                timer1.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void timer1_Tick(Object sender, ElapsedEventArgs myEventArgs)
        {
           
            this.Dispatcher.Invoke(() =>
            {
                a=a.AddMinutes(10);
                UpGrid.DataContext = a;


                for (int i = 0; i < buses.Count; i++)
                {
                    buses[i].whenWillBeReady-=600;

                    var row = GridData.Children.Cast<UIElement>().
                        Where(k => Grid.GetRow(k) == i).ToList();

                    if (buses[i].whenWillBeReady < 0)
                    {
                        if (buses[i].curStatus != Status.ready)
                        {
                            if ((buses[i].curStatus == Status.refueling))
                                buses[i].refuel();
                            else if ((buses[i].curStatus == Status.fixing))
                                buses[i].treatment(a);
                            (row[2] as TextBox).Background = new SolidColorBrush(Colors.White);
                            buses[i].curStatus = Status.ready;
                            (row[6] as TextBlock).Text = "";
                            foreach (ProgressBar bar in row.Where(k => k is ProgressBar))
                                (bar as ProgressBar).Visibility = Visibility.Collapsed;
                            foreach (var button in row.Where(k => k is Button))
                                (button as Button).Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        SolidColorBrush mycolor;
                        if (buses[i].curStatus == Status.fixing)
                            mycolor = new SolidColorBrush(Colors.Red);
                        else if ((buses[i].curStatus == Status.refueling))
                            mycolor = new SolidColorBrush(Colors.Yellow);
                        else if ((buses[i].curStatus == Status.working))
                            mycolor = new SolidColorBrush(Colors.Green);
                        else
                            mycolor = new SolidColorBrush(Colors.Blue);

                        foreach (var button in row.Where(k => k is Button))
                            (button as Button).Visibility = Visibility.Collapsed;

                        foreach (ProgressBar bar_help in row.Where(k => k is ProgressBar))
                        {
                            bar_help.Visibility = Visibility.Visible;
                            bar_help.Value = ((buses[i].start - buses[i].whenWillBeReady) * 100 / buses[i].start);
                            bar_help.Background = new SolidColorBrush(Colors.White);
                            bar_help.Foreground = mycolor;
                        }
                         (row[2] as TextBox).Background = mycolor;
                        (row[6] as TextBlock).Text = timeLeft(i);
                    }


                }
            });
        

        }

        public void addBus(int index)
        {
            RowDefinition newRow = new RowDefinition();
            //newRow.MinHeight = 30;
            GridData.RowDefinitions.Add(newRow);
            
            TextBox text = new TextBox();
            text.Text = buses[index].t_licensePlateNumber;
            text.FontSize = 15;
            text.TextAlignment = TextAlignment.Center;
            text.MouseDoubleClick += Drive_Text_DoubleClick;
            var number = new TextBlock();
            number.Text = (index + 1).ToString();
            var useButton = new Button();
            useButton.Content = "Use"; useButton.Click += Drive_Button_Click;
            var fuelButton = new Button();
            fuelButton.Content = "Refuel"; fuelButton.Click += refuelButton_Click;
            var fixButton = new Button();
            fixButton.Content = "Fix"; fixButton.Click += fixButton_Click;
            var timer = new TextBlock();
            timer.Text = "";
            timer.FontSize = 20;
            ProgressBar bar = new ProgressBar();
            bar.Visibility = Visibility.Collapsed;
            Grid.SetRow(bar, index);
            Grid.SetColumnSpan(bar, 3);
            Grid.SetColumn(bar, 2);
            GridData.Children.Add(bar);
            Grid.SetRow(number, index); Grid.SetRow(text, index); Grid.SetRow(useButton, index); Grid.SetRow(fuelButton, index); Grid.SetRow(fixButton, index); Grid.SetRow(timer, index);
            Grid.SetColumn(number, 0); Grid.SetColumn(text, 1); Grid.SetColumn(useButton, 2); Grid.SetColumn(fuelButton, 3); Grid.SetColumn(fixButton, 4); Grid.SetColumn(timer, 5);
            GridData.Children.Add(number); GridData.Children.Add(text); GridData.Children.Add(useButton); GridData.Children.Add(fuelButton); GridData.Children.Add(fixButton); GridData.Children.Add(timer);

        }


        static void initilize(ref List<Bus> buses)
        {
            buses = new List<Bus>();
            Random rnd = new Random(DateTime.Now.Millisecond);
            int plateNumber, range, mileage, mileageInLastTreat, fuel;
            DateTime date;
            for (int i = 0; i < 15; i++)
            {
                DateTime start = new DateTime(2016, 1, 1);
                range = (DateTime.Today - start).Days;
                date = start.AddDays(rnd.Next(range));

                if (date.Year < 2018)
                    plateNumber = rnd.Next(1000000, 9999999);
                else plateNumber = rnd.Next(10000000, 99999999);

                //start = date;
                //range = (DateTime.Today - date).Days;
                //lastTreatment = start.AddDays(rnd.Next(range));

                mileage = rnd.Next(5000);
                mileageInLastTreat = rnd.Next(mileage);
                fuel = rnd.Next(1200);

                buses.Add(new Bus(plateNumber, date, date, mileage, mileageInLastTreat, fuel));
            }

            buses.Add(new Bus(1111111, new DateTime(2011, 1, 1), new DateTime(2018, 1, 1), 30000, 29000, 1000)); // Need a treatment (a year has passed)
            buses.Add(new Bus(2222222, new DateTime(2012, 2, 2), new DateTime(2020, 2, 2), 19500, 0, 1000)); // Need a treatment (20K km has passed)
            buses.Add(new Bus(33333333, DateTime.Now, DateTime.Now, 1000, 900, 20)); // Need a refuel (almost ran out of fuel)

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addBus = new AddBusWindow(this);
            addBus.Show();
        }

        private void Drive_Button_Click(object sender, RoutedEventArgs e)
        {
            Ride AddRide = new Ride(Grid.GetRow((sender as Button)));
            AddRide.Show();
        }

        private void fixButton_Click(object sender, RoutedEventArgs e)
        {
            buses[Grid.GetRow((sender as Button))].Event(Status.fixing);
        }

        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {
            buses[Grid.GetRow((sender as Button))].Event(Status.refueling);
        }
        private void Drive_Text_DoubleClick(object sender, RoutedEventArgs e)
        {
            Window1 BusData = new Window1(this, Grid.GetRow((TextBox)sender));
            BusData.Show();
        }
        public DateTime get_time
        { get { return a; } }
        public string timeLeft(int i)
        {
            var seconds = buses[i].whenWillBeReady % 60;
            var minutes = buses[i].whenWillBeReady / 60 % 60;
            var hours = buses[i].whenWillBeReady / 3600 % 60;

            return
                ((hours / 10 == 0) ? "0" : "") + hours.ToString() + ':' +
                ((minutes / 10 == 0) ? "0" : "") + minutes.ToString() + ':' +
                ((seconds / 10 == 0) ? "0" : "") + seconds.ToString();
        }
    }
}

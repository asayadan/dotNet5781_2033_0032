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
        private int page;
        private static int NUM_ROWS=17; 


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                initilize(ref buses);


                
                for (int i = 0; i < buses.Count; i++)
                    addBus(i);
                cb_sort.SelectedIndex = 0;

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
                a = a.AddMinutes(10);
                UpGrid.DataContext = a;


                for (int i = 0; i < buses.Count; i++)
                {
                    buses[i].WhenWillBeReady-=600;


                        if (buses[i].curStatus != Status.ready&& buses[i].WhenWillBeReady <= 0)
                        {
                        if ((buses[i].curStatus == Status.refueling))
                            buses[i].refuel();
                        else if ((buses[i].curStatus == Status.fixing))
                        {
                            buses[i].treatment(a);
                            buses[i].Event(Status.refueling);

                        }
                        enter(i);
                        }
                    Graphics(i);

                }
            });


        }

        public void addBus(int index)
        {
            TextBox text = new TextBox();
            text.Text = buses[index].t_licensePlateNumber;
            text.FontSize = 15;
            text.TextAlignment = TextAlignment.Center;
            text.MouseDoubleClick += Drive_Text_DoubleClick;
            var number = new TextBlock();
            number.Text = (index + 1).ToString();
            number.Background = new SolidColorBrush(Colors.Transparent);
            var useButton = new Button();
            useButton.Content = "Use"; useButton.Click += Drive_Button_Click;
            var fuelButton = new Button();
            fuelButton.Content = "Refuel"; fuelButton.Click += refuelButton_Click;
            var fixButton = new Button();
            fixButton.Content = "Fix"; fixButton.Click += fixButton_Click;
            var timer = new TextBlock();
            timer.Text = ""; timer.FontSize = 20; timer.TextAlignment = TextAlignment.Center; timer.Visibility = Visibility.Collapsed;
            var removeButton = new Button();
            removeButton.Content = "Remove"; removeButton.Click += removeButton_Click; removeButton.Background = new SolidColorBrush(Colors.Red);
            ProgressBar bar = new ProgressBar();
            bar.Visibility = Visibility.Collapsed;
            bar.Background = new SolidColorBrush(Colors.Transparent);
            Grid.SetRow(bar, index);
            Grid.SetColumnSpan(bar, 3);
            Grid.SetColumn(bar, 2);
            GridData.Children.Add(bar);
            Grid.SetRow(number, index); Grid.SetRow(text, index); Grid.SetRow(useButton, index); Grid.SetRow(fuelButton, index); Grid.SetRow(fixButton, index); Grid.SetRow(timer, index); Grid.SetRow(removeButton, index);
            Grid.SetColumn(number, 0); Grid.SetColumn(text, 1); Grid.SetColumn(useButton, 2); Grid.SetColumn(fuelButton, 3); Grid.SetColumn(fixButton, 4); Grid.SetColumn(timer, 5); Grid.SetColumn(removeButton, 5);
            GridData.Children.Add(number); GridData.Children.Add(text); GridData.Children.Add(useButton); GridData.Children.Add(fuelButton); GridData.Children.Add(fixButton); GridData.Children.Add(timer); GridData.Children.Add(removeButton);

            RowDefinition newRow = new RowDefinition();
            newRow.MaxHeight = 30;
            newRow.MinHeight = 30;
            GridData.RowDefinitions.Add(newRow);

        }

        public void addNew(int index)
        {
            addBus(index);
            enter(index);
        }
        static void initilize(ref List<Bus> buses)
        {
            buses = new List<Bus>();
            Random rnd = new Random(DateTime.Now.Millisecond);
            int plateNumber, range, mileage, mileageInLastTreat, fuel;
            DateTime date;
            for (int i = 0; i < 20; i++)
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
            Ride AddRide = new Ride(this,Grid.GetRow((sender as Button)) +page * NUM_ROWS);
            AddRide.Show();
        }

        private void fixButton_Click(object sender, RoutedEventArgs e)
        {
            buses[Grid.GetRow((sender as Button))+ page * NUM_ROWS].Event(Status.fixing);
        }

        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {
            buses[Grid.GetRow((sender as Button))+page * NUM_ROWS].Event(Status.refueling);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = Grid.GetRow(sender as Button);
            buses.RemoveAt(index+page * NUM_ROWS);

            GridData.RowDefinitions.RemoveAt(index); 
            foreach (UIElement element in GridData.Children)
            {
                
                if (Grid.GetRow(element) > index)
                {
                    if (Grid.GetColumn(element) == 0)
                        (element as TextBlock).Text = (Grid.GetRow(element)).ToString();
                    Grid.SetRow(element, Grid.GetRow(element) - 1);
                   
                }

            }
            GridData.Children.RemoveRange(index * 8, 8);
            for (int i = page * NUM_ROWS; i < (page + 1) * NUM_ROWS; i++)
            {   
                if (!(NUM_ROWS > buses.Count))
                    Graphics(i);
            }
            if ((page + 1) * NUM_ROWS >= buses.Count)
            {
                bn_down.IsEnabled = false;
            }
            else
            {
                bn_down.IsEnabled = true;
            }

        }
        private void Drive_Text_DoubleClick(object sender, RoutedEventArgs e)
        {
            Window1 BusData = new Window1(this, Grid.GetRow((TextBox)sender)+page*NUM_ROWS);
            BusData.Show();
        }
        public DateTime get_time
        { get { return a; } }
        public string timeLeft(int i)
        {
            var seconds = buses[i].WhenWillBeReady % 60;
            var minutes = buses[i].WhenWillBeReady / 60 % 60;
            var hours = buses[i].WhenWillBeReady / 3600 % 60;

            return
                ((hours / 10 == 0) ? "0" : "") + hours.ToString() + ':' +
                ((minutes / 10 == 0) ? "0" : "") + minutes.ToString() + ':' +
                ((seconds / 10 == 0) ? "0" : "") + seconds.ToString();
        }

        private void cb_sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            buses.Sort(Conperer_This());

            for (int i = 0; i < buses.Count; i++)
            {
                Graphics(i);
            }
        }
        private void enter(int indx)
        {
            IComparer<Bus> myComperer = Conperer_This();
            while ( indx > 0 &&myComperer.Compare(buses[indx],buses[indx-1])<0)
            {
                swap(indx,indx - 1);
                indx -= 1;
            }
            while ( indx < buses.Count - 1&&myComperer.Compare(buses[indx], buses[indx + 1]) > 0 )
            {
                swap(indx, indx +1);
                indx += 1;
            }
        }
        private IComparer<Bus> Conperer_This()
        {
            if (cb_sort.SelectedItem == cb_sort.Items[0])
                return new comparefuel();
            if (cb_sort.SelectedItem == cb_sort.Items[1])
                return new compareDate();
            if (cb_sort.SelectedItem == cb_sort.Items[2])
                return new CompareMileage();
            if (cb_sort.SelectedItem == cb_sort.Items[3])
                return new CompareTimeTreatment();
            else throw new ArgumentOutOfRangeException("how did you do that");
        }

        private void swap(int index1, int index2)
        {
            Bus help = buses[index1];
            buses[index1]= buses[index2] ;
            buses[index2]=help;

            Graphics(index1);
            Graphics(index2);

        }
        public int FindIndex(Bus thisBus)
        {
            int index = MainWindow.buses.FindIndex(x => (x.licensePlate == thisBus.licensePlate && x._registreationDate == thisBus._registreationDate));
            if (index == -1)
                throw new ArgumentException("the bus has been deleated");
            return index;

        }
        public void Graphics(int i)
        {
            if (i / NUM_ROWS == page)
            {


                var row = GridData.Children.Cast<UIElement>().
                        Where(k => Grid.GetRow(k) == i%NUM_ROWS).ToList();
                if (i >= buses.Count)
                {
                    (row[2] as TextBox).Background = new SolidColorBrush(Colors.Transparent);
                    (row[2] as TextBox).Text = "";
                    (row[1] as TextBlock).Text = "";
                    (row[6] as TextBlock).Visibility = Visibility.Collapsed;
                    foreach (var button in row.Where(k => k is Button))
                        (button as Button).Visibility = Visibility.Collapsed;

                    foreach (ProgressBar bar_help in row.Where(k => k is ProgressBar))
                    {
                        bar_help.Visibility = Visibility.Collapsed;
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
                        mycolor = new SolidColorBrush(Colors.Transparent);



                    (row[2] as TextBox).Background = mycolor;
                    (row[2] as TextBox).Text = buses[i].t_licensePlateNumber;
                    (row[1] as TextBlock).Text = (i + 1).ToString();

                    if (buses[i].WhenWillBeReady <= 0)
                    {

                        buses[i].curStatus = Status.ready;
                        (row[6] as TextBlock).Visibility = Visibility.Collapsed;
                        foreach (ProgressBar bar in row.Where(k => k is ProgressBar))
                            (bar as ProgressBar).Visibility = Visibility.Collapsed;
                        foreach (var button in row.Where(k => k is Button))
                            (button as Button).Visibility = Visibility.Visible;

                    }
                    else
                    {

                        foreach (var button in row.Where(k => k is Button))
                            (button as Button).Visibility = Visibility.Collapsed;

                        foreach (ProgressBar bar_help in row.Where(k => k is ProgressBar))
                        {
                            bar_help.Visibility = Visibility.Visible;
                            double help = ((buses[i].Start - buses[i].WhenWillBeReady) * 100 / buses[i].Start);
                            bar_help.Value = help;
                            bar_help.Foreground = mycolor;
                        }
                         (row[2] as TextBox).Background = mycolor;
                        (row[6] as TextBlock).Visibility = Visibility.Visible;
                        (row[6] as TextBlock).Text = timeLeft(i);
                    }
                }
            }
        }

        private void bn_down_Click(object sender, RoutedEventArgs e)
        {
            if ((page + 1) * NUM_ROWS < buses.Count)
                page += 1;
            if ((page + 1) * NUM_ROWS >= buses.Count)
                bn_down.IsEnabled = false;
            else
                bn_down.IsEnabled = true;
            bn_up.IsEnabled = true;
            for (int i = page*NUM_ROWS;i< (page + 1) * NUM_ROWS; i++)
            {
                Graphics(i);
            }
        }

        private void bn_up_Click(object sender, RoutedEventArgs e)
        {

            if (page > 0)
                page -= 1;
            if (page > 0)
                bn_up.IsEnabled = true;
            else
                bn_up.IsEnabled = false;
            bn_down.IsEnabled = true;
            for (int i = page * NUM_ROWS; i < (page + 1) * NUM_ROWS; i++)
            {
                Graphics(i);
            }
        }
    }
}

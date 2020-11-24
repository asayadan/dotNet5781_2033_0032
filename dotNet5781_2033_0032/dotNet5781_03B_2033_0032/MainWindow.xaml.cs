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

namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Bus> buses;

        public MainWindow()
        {
            InitializeComponent();
            initilize(ref buses);

            for (int i = 0; i < buses.Count; i++)
                addBus(i);
        }

        public void addBus(int index)
        {
            GridData.RowDefinitions.Add(new RowDefinition());
            var text = new TextBlock();
            text.Text = buses[index].licensePlate.ToString();
            text.FontSize = 15;
            text.TextAlignment = TextAlignment.Center;
            var useButton = new Button();
            useButton.Content = "Use";
            useButton.Click += new RoutedEventHandler(useClick);
            var fuelButton = new Button();
            fuelButton.Content = "Refuel";
            var fixButton = new Button();
            fixButton.Content = "Fix";
            Grid.SetRow(text, index); Grid.SetRow(useButton, index); Grid.SetRow(fuelButton, index); Grid.SetRow(fixButton, index);
            Grid.SetColumn(text, 0); Grid.SetColumn(useButton, 1); Grid.SetColumn(fuelButton, 2); Grid.SetColumn(fixButton, 3);
            GridData.Children.Add(text); GridData.Children.Add(useButton); GridData.Children.Add(fuelButton); GridData.Children.Add(fixButton);

        }

        public void useClick(object sender, RoutedEventArgs e)
        {
            int i = Grid.GetRow((sender as Button));
            buses[i].rideKM(15);

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
            buses.Add(new Bus(3333333, new DateTime(2013, 3, 3), new DateTime(2020, 3, 3), 1000, 900, 20)); // Need a refuel (almost ran out of fuel)

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var v = new AddBusWindow(this);
            v.Show();
        }
    }
}

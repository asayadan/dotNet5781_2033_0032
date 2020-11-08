using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2033_0032
{
    class Program
    {
        static void Main(string[] args)
        {

            List<BusStation> stations = new List<BusStation>();
            LineCollection collection = new LineCollection();
            initilize(ref collection, ref stations);
            bool flag = true;
            try
            {

                while (flag == true)
                {
                    Console.WriteLine("\nChoose an option:\n1. Add a line or station to line.\n2. Delete a line or station to line.\n3. Search for lines going through a station or checking what's the fastest choice .\n4. Print all lines or all stations. \n5. Exit.");
                    int help;
                    int lineTemp, stationTemp, indexTemp;
                    float timeHelp, distHelp;
                    int.TryParse(Console.ReadLine(), out help);
                    switch (help)
                    {
                        case 1: //Add
                            Console.WriteLine("1. Add a line\n2. Add a station to line");
                            int.TryParse(Console.ReadLine(), out help);
                            switch (help)
                            {
                                case 1:
                                    Console.WriteLine("Enter the line number.");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    collection.addLine(new BusLine(lineTemp));
                                    break;
                                case 2:
                                    Console.WriteLine("Enter the line number, station number, index in line and distance and time since last station");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    int.TryParse(Console.ReadLine(), out indexTemp);
                                    float.TryParse(Console.ReadLine(), out distHelp);
                                    float.TryParse(Console.ReadLine(), out timeHelp);
                                    collection[lineTemp].addStation(new BusStationLine(stationTemp, distHelp, timeHelp), indexTemp, distHelp, timeHelp);
                                    break;
                            }
                            break;

                        case 2: //Remove
                            Console.WriteLine("1. Remove a line\n2. Remove a station from line");
                            int.TryParse(Console.ReadLine(), out help);
                            switch (help)
                            {
                                case 1:
                                    Console.WriteLine("Enter the line number.");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    collection.removeLine(new BusLine(lineTemp));
                                    break;
                                case 2:
                                    Console.WriteLine("Enter the line number and station number.");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    collection[lineTemp].removeStation(new BusStationLine(lineTemp, 0, 0));
                                    break;
                            }
                            break;
                        case 3:
                            break;
                        case 4:


                            break;
                        case 5:
                            int key2, dist3, time3;

                            int.TryParse(Console.ReadLine(), out key2);

                            int.TryParse(Console.ReadLine(), out dist3);
                            int.TryParse(Console.ReadLine(), out time3);
                            break;
                        default:
                            flag = false;
                            break;
                    }
                }
            }

            catch (ArgumentException exArgument)
            {
                Console.WriteLine(exArgument.Message);

            }

            catch (IndexOutOfRangeException exIndex)
            {
                Console.WriteLine(exIndex.Message);
            }
        }


        public static void initilize(ref LineCollection collection, ref List<BusStation> stations)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int[] stationKeys = new int[] { 160071, 193860, 227778, 249392, 258514, 276786, 322044, 388389, 466106, 481441, 485115, 491827, 497524, 598965, 603645, 606389, 644600, 661660, 682695, 686646, 687693, 691926, 732433, 763550, 766222, 772620, 810898, 844006, 853796, 865922, 880286, 884243, 894569, 895390, 904295, 953591, 961725, 972100, 975522, 999364 };
            int[] lineKeys = new int[] { 5, 97, 267, 315, 456, 527, 690, 755, 870, 978 };
            for (int i = 0; i < 40; i++)
                stations.Add(new BusStation(stationKeys[i]));

            float dist;
            for (int i = 0; i < 10; i++)
            {
                BusLine line = new BusLine(lineKeys[i]);
                for (int j = 0; j < 6; j++)
                {
                    dist = (float)rnd.NextDouble() + rnd.Next(5, 10);
                    line.addStation(new BusStationLine(stations[(i * 6 + j) % 40], dist, dist / 2), j, dist, dist / 2);
                }

                collection.addLine(line);
            }

        }

    }
}

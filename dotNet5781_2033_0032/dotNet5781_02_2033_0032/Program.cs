using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;


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

            int help;
            int lineTemp, stationTemp, stationTemp2, indexTemp;
            float timeHelp, distHelp;
            while (flag == true)
            {
                try
                {
                    Console.WriteLine("\nChoose an option:\n1. Add a line or station to line.\n2. Delete a line or station to line.\n3. Search for lines going through a station or checking what's the fastest choice .\n4. Print all lines or all stations. \n5. Exit.");

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
                                    collection[lineTemp].removeStation(new BusStationLine(lineTemp));
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine("1. Search for lines going through a station.\n2. checking what's the fastest choice");
                            int.TryParse(Console.ReadLine(), out help);
                            switch (help)
                            {
                                case 1:
                                    Console.WriteLine("Enter the station number.");
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    collection.checkStation(stationTemp);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter the first and last stations.");
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp2);
                                    var listA = collection.checkStation(stationTemp);
                                    var listB = collection.checkStation(stationTemp2);
                                    var listRes = new List<BusLine>();

                                    foreach (var lineA in listA)
                                        foreach (var lineB in listB)
                                            if (lineA == lineB)
                                                listRes.Add(lineA.subLine(new BusStationLine(stationTemp), new BusStationLine(stationTemp2)));

                                    break;
                            }
                            break;
                        case 4: // Print all lines or all stations.
                            Console.WriteLine("1. Print all lines \n2. Print all stations");
                            int.TryParse(Console.ReadLine(), out help);
                            switch (help)
                            {
                                case 1:
                                    foreach (var line in collection)
                                        Console.WriteLine(line.ToString());
                                    break;

                                case 2:
                                    foreach (var station in stations)
                                    {

                                        Console.Write("{0} | Lines: ", station.ToString());
                                        foreach (var line in collection.checkStation(station.GetBusStationKey))
                                            Console.Write("{0} ", (line._lineNumber));
                                        Console.WriteLine();
                                    }

                                    break;
                            }
                            break;

                        case 5:
                            flag = false;
                            Console.WriteLine("Good Bye!");
                            break;
                        default:
                            flag = false;
                            break;
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

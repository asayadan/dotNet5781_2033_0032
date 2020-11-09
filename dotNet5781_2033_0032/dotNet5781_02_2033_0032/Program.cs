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
        public const int STATIONS_PER_LINE = 11;

        static void Main(string[] args)
        {

            List<BusStation> stations = new List<BusStation>();
            LineCollection collection = new LineCollection();
            initilize(ref collection, ref stations);
            bool flag = true;

            int help;
            int lineTemp, stationTemp, stationTemp2, indexTemp;
            float timeHelp, distHelp, timeHelp2, distHelp2;
            string addrTemp = null;
            bool ifAddr;
            while (flag == true)
            {
                try
                {
                    addrTemp = null;
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
                                    Console.WriteLine("Enter the line number, station number, index in line, distance and time since last station and 'true' if to input address as well");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    int.TryParse(Console.ReadLine(), out indexTemp);
                                    float.TryParse(Console.ReadLine(), out distHelp);
                                    float.TryParse(Console.ReadLine(), out timeHelp);
                                    bool.TryParse(Console.ReadLine(), out ifAddr);
                                    if (ifAddr)
                                        addrTemp = Console.ReadLine();
                                    Console.WriteLine("enter the distance abd time between the new station and the station after the new station");
                                    float.TryParse(Console.ReadLine(), out distHelp2);
                                    float.TryParse(Console.ReadLine(), out timeHelp2);
                                    int help_indx = stations.FindIndex(x => x.GetBusStationKey == stationTemp); //606389 661660
                                    if (help_indx>=0)
                                    {
                                        collection[lineTemp].addStation(new BusStationLine(stations[help_indx], distHelp, timeHelp, addrTemp), indexTemp, distHelp2, timeHelp2);
                                    }
                                    else
                                    {
                                        BusStationLine new_station = new BusStationLine(stationTemp, distHelp, timeHelp);
                                        stations.Add(new_station);
                                        collection[lineTemp].addStation(new_station, indexTemp, distHelp2, timeHelp2);
                                    }
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
                                    Console.WriteLine("enter the distance abd time between the station before and after the deleted station");
                                    float.TryParse(Console.ReadLine(), out distHelp);
                                    float.TryParse(Console.ReadLine(), out timeHelp);
                                    collection[lineTemp].removeStation(new BusStationLine(stationTemp), distHelp,timeHelp);
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
                                    foreach (var line in collection.checkStation(stationTemp))
                                            Console.Write("{0} ", (line._lineNumber));
                                    break;
                                case 2:
                                    Console.WriteLine("Enter the first and last stations.");
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp2);
                                    var listA = collection.checkStation(stationTemp);
                                    var listB = collection.checkStation(stationTemp2);
                                    var tempCollection = new LineCollection();
                                    foreach (var lineA in listA)
                                        foreach (var lineB in listB)
                                            if (lineA == lineB)
                                                tempCollection.addLine(lineA.subLine(new BusStationLine(stationTemp), new BusStationLine(stationTemp2)));

                                    foreach (var line in tempCollection)
                                        Console.WriteLine(line.ToString());
                                    
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
            int num_stations=40;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int[] stationKeys = new int[] { 160071, 193860, 227778, 249392, 258514, 276786, 322044, 388389, 466106, 481441, 485115, 491827, 497524, 598965, 603645, 606389, 644600, 661660, 682695, 686646, 687693, 691926, 732433, 763550, 766222, 772620, 810898, 844006, 853796, 865922, 880286, 884243, 894569, 895390, 904295, 953591, 961725, 972100, 975522, 999364 };
            int[] lineKeys = new int[] { 5, 97, 267, 315, 456, 527, 690, 755, 870, 978 };
            for (int i = 0; i <  num_stations ; i++)
                stations.Add(new BusStation(stationKeys[i]));

            //for (int i = 0; i < num_stations; i++)
            //{
            //    int help_station_key = rnd.Next(999999);
            //    if (!stations.Exists(x => x.GetBusStationKey == help_station_key))
            //    {
            //        stations.Add(new BusStation(help_station_key));
            //    }
            //    else
            //        i--;
            //}

            float dist;
            for (int i = 0; i < 10; i++)
            {
                BusLine line = new BusLine(lineKeys[i]);
                for (int j = 0; j < STATIONS_PER_LINE; j++)
                {
                    dist = (float)rnd.NextDouble() + rnd.Next(10);
                    line.addStation(new BusStationLine(stations[(i * STATIONS_PER_LINE + j) % num_stations], dist, dist / 2), j, dist, dist / 2);
                }

                collection.addLine(line);
            }

        }
    }
}



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
            bool ifAddr, helpdir;
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
                                    BusLine new_line= new BusLine(lineTemp);
                                    collection.addLine(new_line);
                                    for (int i=0; collection[lineTemp,new_line._direction]._numStations<2;i++)
                                    {
                                        try
                                        {
                                            Console.WriteLine("enter the key of the  station,the distance and time between the first and second stations ");
                                            int.TryParse(Console.ReadLine(), out stationTemp);
                                            float.TryParse(Console.ReadLine(), out distHelp);
                                            float.TryParse(Console.ReadLine(), out timeHelp);
                                            
                                            check_station(ref stations, ref collection, lineTemp, stationTemp, i, distHelp, timeHelp);
                                        }
                                        catch (ArgumentException exArgument)
                                        {
                                            Console.WriteLine(exArgument.Message);
                                            --i;
                                        }

                                        catch (IndexOutOfRangeException exIndex)
                                        {
                                            --i;
                                            Console.WriteLine(exIndex.Message);
                                        }
                                    }


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
                                    check_station(ref stations,ref collection, lineTemp, stationTemp, indexTemp, timeHelp, distHelp, timeHelp2, distHelp2);
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
                                     helpdir = true;
                                    BusLine help_line = new BusLine(lineTemp);
                                    if (collection.num_station(lineTemp) == 2)
                                    {
                                        Console.WriteLine("we have two lines with this line number enter true for the first station and false for the second station");
                                        bool.TryParse(Console.ReadLine(), out helpdir);
                                        help_line._direction = helpdir;
                                        collection.removeLine(help_line);
                                        collection[lineTemp, !helpdir]._direction = true;
                                    }
                                    else
                                    {
                                        collection.removeLine(help_line);

                                    }
                                    break;
                                case 2:
                                    helpdir = true;
                                    Console.WriteLine("Enter the line number and station number.");
                                    int.TryParse(Console.ReadLine(), out lineTemp);
                                    int.TryParse(Console.ReadLine(), out stationTemp);
                                    if (collection.num_station(lineTemp) == 2)
                                    {
                                        Console.WriteLine("we have two lines with this line number enter true for the first station and false for the second station");
                                        bool.TryParse(Console.ReadLine(), out helpdir);
                                    }
                                    if (collection[lineTemp, helpdir]._numStations <= 2)
                                    {
                                        Console.WriteLine("you can't have less than two stations");
                                    }
                                    else
                                    {
                                        Console.WriteLine("enter the distance abd time between the station before and after the deleted station");
                                        float.TryParse(Console.ReadLine(), out distHelp2);
                                        float.TryParse(Console.ReadLine(), out timeHelp2);
                                        collection[lineTemp, helpdir].removeStation(new BusStationLine(stationTemp), distHelp2, timeHelp2);
                                    }
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
                                    tempCollection.sortedLines();
                                    foreach (BusLine line in tempCollection)
                                        Console.WriteLine(line.ToString()+line.totalTime().ToString());
                                    
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
                            Console.WriteLine("error");
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

        private static void check_station( ref List<BusStation> stations,ref LineCollection collection, int lineTemp, int stationTemp, int indexTemp=0, float distHelp = 0, float timeHelp=0, float timeHelp2=0, float distHelp2=0)
        {
            bool helpdir = true;
            if (collection.num_station(lineTemp)==2)
            {
                Console.WriteLine("we have two lines with this line number enter true for the first station and false for the second station");
                bool.TryParse(Console.ReadLine(), out helpdir);
            }
            int help_indx = stations.FindIndex(x => x.GetBusStationKey == stationTemp); //606389 661660
            if (help_indx >= 0)
            {
                collection[lineTemp, helpdir].addStation(new BusStationLine(stations[help_indx], distHelp, timeHelp), indexTemp, distHelp2, timeHelp2);
            }
            else
            {
                BusStation new_station = new BusStation(stationTemp);
                stations.Add(new_station);
                collection[lineTemp, helpdir].addStation(new BusStationLine(new_station, distHelp, timeHelp), indexTemp, distHelp2, timeHelp2);
            }
        }

        public static void initilize(ref LineCollection collection, ref List<BusStation> stations)
        {
            int num_stations = 40;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < num_stations; i++)
            {
                int help_station_key = rnd.Next(100000, 999999);
                if (!stations.Exists(x => x.GetBusStationKey == help_station_key))
                {
                    stations.Add(new BusStation(help_station_key));
                }
                else
                    i--;
            }

            float dist;
            for (int i = 0; i < 10; i++)
            {
                int help_bus_key = rnd.Next(10, 999);
                if (!stations.Exists(x => x.GetBusStationKey == help_bus_key))
                {
                    BusLine line = new BusLine(help_bus_key);
                    for (int j = 0; j < STATIONS_PER_LINE; j++)
                    {
                        dist = (float)rnd.NextDouble() + rnd.Next(20);
                        line.addStation(new BusStationLine(stations[(i * STATIONS_PER_LINE + j) % num_stations], dist, dist / 2), j, dist, dist / 2);
                    }

                    collection.addLine(line);
                }
                else
                    i--;
            }

        }
    }
}



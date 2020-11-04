using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2033_0032
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            BusLine Line = new BusLine();
            while (flag == true)
            {
                int help;
                int.TryParse(Console.ReadLine(), out help);
                switch (help)
                {
                    case 1:
                        try
                        {
                            int key, time, dist, time2, dist2, index;
                            int.TryParse(Console.ReadLine(), out key);
                            int.TryParse(Console.ReadLine(), out dist);
                            int.TryParse(Console.ReadLine(), out time);
                            int.TryParse(Console.ReadLine(), out dist2);
                            int.TryParse(Console.ReadLine(), out time2);
                            int.TryParse(Console.ReadLine(), out index);
                            BusStationLine station = new BusStationLine(key, dist, time);
                            Line.addStation(station, index, dist2, time2);
                        }
                        catch (ArgumentException exArgument)
                        {
                            Console.WriteLine(exArgument.Message);

                        }
                        catch (IndexOutOfRangeException exIndex)
                        {
                            Console.WriteLine(exIndex.Message);
                        }

                        break;
                    case 2:

                        Console.WriteLine(Line.ToString());
                        break;
                    case 3:
                        Console.WriteLine("time:" + Line.totalTime().ToString());
                        Console.WriteLine("dist:" + Line.totalDistance().ToString());
                        break;
                    case 4:
                        int keeee;
                        int.TryParse(Console.ReadLine(),out keeee);
                        Console.WriteLine(Line.exist(keeee).ToString()); 
                        break;
                    case 5:
                        int key2, dist3,time3;

                        int.TryParse(Console.ReadLine(), out key2);
                        int.TryParse(Console.ReadLine(), out dist3);
                        int.TryParse(Console.ReadLine(), out time3);
                        BusStationLine station2 = new BusStationLine(key2,0,0);
                        Line.removeStation(station2,dist3,time3);
                        break;
                    default:
                        flag = false;
                        break;
                }
            }

            Console.WriteLine("hello");
        }
    }
}

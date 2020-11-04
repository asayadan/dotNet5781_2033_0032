using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_2033_0032
{
    class BusLine : IComparable
    {
        #region variables
        private int busLine;
        private BusStationLine firstStation;
        private BusStationLine lastStation;
        private Area area;
        private List<BusStationLine> stations;
        #endregion

        #region methods


        public int _lineNumber
        { get { return busLine; } }
        public static string checkArea(Area area)
        {
            switch (area)
            {
                case Area.General:
                    return "General";
                case Area.North:
                    return "North";
                case Area.South:
                    return "South";
                case Area.Center:
                    return "Center";
                case Area.Jerusalem:
                    return "Jerusalem";
                default:
                    return "ERROR!";
            }

        }
        public override string ToString()
        {
            string a = "Bus line: " + busLine + " Area: " + checkArea(area) + "\n";
            var arr = stations.ToArray();

            a += "Route stations:\n";
            foreach (var station in stations)
                a += station.ToString() + "\n";




            return a;
        }
        public void addStation(BusStationLine _station, int index, int distance, int time)
        {
            if (!stations.Exists(x => x.GetBusStationKey == _station.GetBusStationKey))
            {
                stations.Insert(index, _station);
                if (index == 0 && stations.Count >= 2)
                {
                    firstStation = _station;
                    stations[1].DistFromLastStation = distance;
                    stations[1].TimeSinceLastStation = time;
                }
                else if (index == stations.Count - 1)
                {
                    lastStation = _station;
                }
                else
                {

                }
            }
            else
            {
                //exception
            }

        }
        public void removeStation(BusStationLine _station)
        {
            if (stations.Exists(x => x.GetBusStationKey == _station.GetBusStationKey))
            {
                stations.Remove(_station);
            }
            else
            {
                //exeption
            }
        }

        public bool exist(int BusStationKey)
        {
            return stations.Exists(x => x.GetBusStationKey == BusStationKey);
        }

        public bool isReverse(BusLine _bus)
        {
            if (firstStation == _bus.lastStation && lastStation == _bus.firstStation)
                return true;
            return false;
        }

        public float distance(BusStationLine station1, BusStationLine station2)
        {
            int index1 = stations.FindIndex(x => station1 == x);
            int index2 = stations.FindIndex(x => station2 == x);
            float distance = 0;
            if (index1 >= index2)
            {
                index1 = index1 ^ index2;
                index2 = index1 ^ index2;
                index1 = index1 ^ index2;
            }
            if (index2 >= 0 && index1 >= 0)
            {
                for (int i = index1 + 1; i <= index2; i++)
                    distance += stations[i].DistFromLastStation;
                return distance;
            }
            else
            {
                //exeption
            }
        }

        public float time(BusStationLine station1, BusStationLine station2)
        {
            int index1 = stations.FindIndex(x => station1 == x);
            int index2 = stations.FindIndex(x => station2 == x);
            float time = 0;
            if (index1 >= index2)
            {
                index1 = index1 ^ index2;
                index2 = index1 ^ index2;
                index1 = index1 ^ index2;
            }
            if (index2 >= 0 && index1 >= 0)
            {

                foreach (var station in stations.GetRange(index1 + 1, index2 - index1))
                    time += station.TimeSinceLastStation;
                return time;

            }
            else
            {
                //exeption
            }

        }

        public BusLine subLine(BusStationLine station1, BusStationLine station2)
        {
            BusLine temp = new BusLine();
            int index1 = stations.FindIndex(x => station1 == x);
            int index2 = stations.FindIndex(x => station2 == x);
            int length = Math.Abs(index1 - index2) + 1;
            if (index2 >= 0 && index1 >= 0)
            {
                temp.stations = stations.GetRange(Math.Min(index1, index2), length);
                temp.area = area;
                temp.busLine = busLine;
                temp.firstStation = temp.stations[0];
                temp.lastStation = temp.stations[length - 1];

                return temp;
            }
            else
            {
                //exeption
            }

        }

        public static bool operator ==(BusLine bus1, BusLine bus2)//I need to do it
        {
            if (bus1._lineNumber == bus2._lineNumber &&)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(BusLine bus1, BusLine bus2)
        {
            return !(bus1 == bus2);
        }
        public int CompareTo(object obj)
        {
            return time(firstStation, lastStation).CompareTo((obj as BusLine).time((obj as BusLine).firstStation, (obj as BusLine).lastStation));
        }
        #endregion

    }


}

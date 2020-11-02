using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public enum Area { General, North, South, Center, Jerusalem };


namespace dotNet5781_02_2033_0032
{


    class BusStation
    {
        #region variables
        protected int busStationKey;
        protected float latitude;
        protected float longitude;
        protected string stationAddress;
        #endregion
        public override string ToString()
        {
            return (string)("Bus Station Code: " + busStationKey + " "
                             + latitude + "°N " + longitude + "°E");
        }
    }

    class BusStationLine : BusStation
    {
        private float distFromLastStation;
        private float timeSinceLastStation;

        public float getDistFromLastStation
        {
            get
            {
                return distFromLastStation;
            }
        }

        public float getTimeSinceLastStation
        {
            get
            {
                return getTimeSinceLastStation;
            }
        }
    }

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

            a += "Forth route stations:\n";
            for (int i = 0; i < stations.Count; i++)
                a += stations[i].ToString() + "\n";

            a += "Back route stations:\n";
            for (int i = stations.Count - 1; i >= 0; i--)
                a += stations[i].ToString() + "\n";


            return a;
        }
        public void addStation(BusStationLine _station, int index)
        {
            stations.Insert(index, _station);
        }
        public void removeStation(BusStationLine _station)
        {
            stations.Remove(_station);
        }

        public bool exist(BusStationLine _station)
        {
            return stations.Contains(_station);
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

            for (int i = index1 + 1; i <= index2; i++)
                distance += stations[i].getDistFromLastStation;

            return distance;

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

            for (int i = index1 + 1; i <= index2; i++)
                time += stations[i].getTimeSinceLastStation;

            return time;

        }

        public BusLine subLine(BusStationLine station1, BusStationLine station2)
        {
            BusLine temp = new BusLine();
            int index1 = stations.FindIndex(x => station1 == x);
            int index2 = stations.FindIndex(x => station2 == x);
            int length = Math.Abs(index1 - index2) + 1;

            temp.stations = stations.GetRange(Math.Min(index1, index2), length);
            temp.area = area;
            temp.busLine = busLine;
            temp.firstStation = temp.stations[0];
            temp.lastStation = temp.stations[length - 1];

            return temp;
        }
        #endregion

        BusLine IComparable.CompareTo(object obj)
        {
            
            
        }

    }
}
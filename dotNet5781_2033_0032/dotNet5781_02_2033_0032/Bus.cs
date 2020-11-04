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

        public float GetBusStationKey
        {
            get
            {
                return busStationKey;
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

            a += "Route stations:\n";
            foreach (var station in stations)
                a += station.ToString() + "\n";

           

           
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

        public bool exist(int BusStationKey)
        {
            var check = stations.Find(x => x.GetBusStationKey == BusStationKey);
            return check != null;
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

            foreach (var station in stations.GetRange(index1 + 1, index2 - index1))
                time += station.getTimeSinceLastStation;

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

        public int CompareTo(object obj)
        {
            return time(firstStation,lastStation).CompareTo((obj as BusLine).time((obj as BusLine).firstStation, (obj as BusLine).lastStation));
        }


    }

    class LineCollection : IEnumerable
    {
        #region varibles
        private List<BusLine> lines;
        #endregion 
        
        public void addLine(BusLine line)
        {
            if (lines.FindAll(x => x == line).Count <= 2)
                lines.Add(line);
            else throw new ArgumentException("Bus already exists twice.");

        }

        public void removeLine(BusLine line)
        {
            if (lines.FindAll(x => x == line).Count != 0)
                lines.Add(line);
            else throw new ArgumentException("Bus doesn't exist.");

        }

        public List<BusLine> checkStation(int busStationKey)
        {   
            var linesOfStation = lines.FindAll(x => x.exist(busStationKey));
            if (linesOfStation.Count != 0)
                return linesOfStation;
            else throw new ArgumentException("No bus passes this station.");

        }

        public List<BusLine> sortedStations()
        {

            lines.Sort(delegate (BusLine x, BusLine y)
            {
                return x.CompareTo(y);
            });

            return lines;
        }

        public BusLine this[int busStationKey] => lines.Find(x => x.exist(busStationKey));
        public IEnumerator GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        
        
        

    }
}
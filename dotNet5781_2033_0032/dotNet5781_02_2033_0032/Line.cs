using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
        bool direction;
        private List<BusStationLine> stations;
        #endregion

        #region ctors
        public BusLine(int num ,int _area=0)
        {
            stations = new List<BusStationLine>();
            busLine = num;
            direction = true;
            if (_area <= 4 && _area >= 0)
            {
                area = (Area)_area;
            }
            else throw new ArgumentOutOfRangeException("the regions numbers can be onely 0,1,2,3 or 4");
            
        }

        public BusLine()
        {
            stations = new List<BusStationLine>();
            direction = true;
        }
        #endregion

        #region methods


        public int _lineNumber
        { get { return busLine; } }
        public bool _direction
        {
          get{ return direction; }
          set { direction = value; }
        }
        public int _numStations
        { get { return stations.Count; } }
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

        public static string checkDist(bool help_directio)
        {

            if (help_directio)
            {
                return "";
            }
            return "A";
        }
        public override string ToString()
        {
            string a = "Bus line: " + busLine+ checkDist(direction)+ " Area: " + checkArea(area) + "\n";
            var arr = stations.ToArray();

            a += "Route stations:\n";
            foreach (var station in stations)
                a += station.ToString()+ station.time_str() + "\n";




            return a;
        }

        public void addStation(BusStationLine _station, int index, float distance, float time)
        {
            if (!stations.Exists(x => x.GetBusStationKey == _station.GetBusStationKey))
            {
                if (index<=stations.Count)
                {

                    stations.Insert(index, _station);
                    if (index == 0)
                    {
                        _station.DistFromLastStation = 0;
                        _station.TimeSinceLastStation = 0;
                        firstStation = _station;
                        if (stations.Count >= 2)
                        {
                            stations[1].DistFromLastStation = distance;
                            stations[1].TimeSinceLastStation = time;
                        }
                    }
                     if (index == stations.Count - 1)
                    {
                        lastStation = _station;
                    }
                    else
                    {
                        stations[index + 1].DistFromLastStation = distance;
                        stations[index + 1].TimeSinceLastStation = time;
                    }
                }
                else throw new IndexOutOfRangeException("your index is out of range");
            }
            else throw new ArgumentException ("this station is already in our line");

        }
        public void removeStation(BusStationLine _station,float _distance, float _time)
        {
            int indx = stations.FindIndex(x => x.GetBusStationKey == _station.GetBusStationKey);
            if (indx >= 0)
            {
                _station = stations[indx];
                stations.Remove(_station);
                if (indx!=stations.Count)
                {
                    stations[indx].DistFromLastStation = _distance;
                    stations[indx].TimeSinceLastStation = _time;
                    if (indx==0)
                    {
                        firstStation = stations[0];
                    }
                }
                else if(indx!=0)
                {
                    lastStation = stations[indx - 1];
                }

            }
            else throw new ArgumentOutOfRangeException("this station is not in this line");
        }

        public bool exist(int BusStationKey)
        {
            return stations.Exists(x => x.GetBusStationKey == BusStationKey);
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
            else throw new ArgumentOutOfRangeException("one or more of the stations is not in this line");
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
            else throw new ArgumentOutOfRangeException("one or more of the stations is not in this line");

        }

        public float totalTime()
        { return time(firstStation,lastStation); }

        public float totalDistance()
        { return distance(firstStation, lastStation); }

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
            else throw new ArgumentOutOfRangeException("one or more of the stations is not in this line");

        }

        public static bool operator ==(BusLine bus1, BusLine bus2) // does two given buses equal
        {
            bool help= (bus1._lineNumber == bus2._lineNumber && bus1._direction == bus2._direction);
            //bool help2 = (bus1.area == bus2.area) || bus1.area == 0 || bus2.area == 0;
            return help /*&& help2*/;
        }
        public static bool operator !=(BusLine bus1, BusLine bus2) // they wanted us to implement it so...
        {
            return !(bus1 == bus2) ;
        }

        public static bool operator ==(BusLine bus1, int _lineNumber) // 
        {
            return (bus1._lineNumber == _lineNumber);
        }
        public static bool operator !=(BusLine bus1, int _lineNumber)
        {
            return !(bus1 == _lineNumber);
        }
        public int CompareTo(object obj)
        {
            return totalTime().CompareTo((obj as BusLine).totalTime());
        }
        #endregion

    }
}

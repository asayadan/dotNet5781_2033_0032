using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    class BusLine
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
        #endregion

    }
}
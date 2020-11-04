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
        public BusStation(int _busStationKey)
        {
            busStationKey = _busStationKey;
            Random rnd = new Random(DateTime.Now.Millisecond);
            latitude = rnd.Next(310,333)/10;
            longitude = rnd.Next(343, 355) / 10;
        }

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


        public BusStationLine(int _busStationKey, int _distFromLastStation, int _timeSinceLastStation):base(_busStationKey)
        {
            distFromLastStation = _distFromLastStation;
            timeSinceLastStation = _timeSinceLastStation;
        }
        public float DistFromLastStation
        {
            get { return distFromLastStation; }
            set { distFromLastStation = value; }
        }

        public float TimeSinceLastStation
        {
            get { return timeSinceLastStation; }
            set { timeSinceLastStation = value; }
        }

        public float GetBusStationKey
        {
            get
            {
                return busStationKey;
            }
        }
    }

}
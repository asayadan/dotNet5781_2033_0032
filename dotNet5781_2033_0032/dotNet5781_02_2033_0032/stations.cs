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

        public float DistFromLastStation
        {
            get { return distFromLastStation; }
            set { distFromLastStation = value; }
        }

        public float TimeSinceLastStation
        {
            get { return TimeSinceLastStation; }
            set { TimeSinceLastStation = value; }
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
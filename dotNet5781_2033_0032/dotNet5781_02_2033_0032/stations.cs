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


        public BusStation(BusStation _station)
        {
            busStationKey = _station.busStationKey;
            latitude = _station.latitude;
            longitude = _station.longitude;
        }
        public BusStation(int _busStationKey,int _latitude, int _longitude)
        {
            if (_busStationKey <= 999999)
            {
                busStationKey = _busStationKey;
                Random rnd = new Random(DateTime.Now.Millisecond);
                latitude = _latitude;
                longitude = _longitude;
            }
            else throw new ArgumentException("the number is too bid");
        }
        public BusStation(int _busStationKey)
        {
            if (_busStationKey<=999999)
            {
                busStationKey = _busStationKey;
                Random rnd = new Random(DateTime.Now.Millisecond);
                latitude = (float)rnd.Next(310, 333) / 10;
                longitude = (float)rnd.Next(343, 355) / 10;
            }
            else throw  new ArgumentException("the number is too bid");
        }

        public int _key
        { get { return busStationKey; } }

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



        public BusStationLine(BusStation _busStation, float _distFromLastStation, float _timeSinceLastStation) : base(_busStation)
        {
            distFromLastStation = _distFromLastStation;
            timeSinceLastStation = _timeSinceLastStation;
        }
        public BusStationLine(int _busStationKey, float _distFromLastStation, float _timeSinceLastStation):base(_busStationKey)
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
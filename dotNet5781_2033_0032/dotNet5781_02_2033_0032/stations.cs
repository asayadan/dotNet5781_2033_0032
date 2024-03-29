﻿using System;

public enum Area { General, North, South, Center, Jerusalem };


namespace dotNet5781_02_2033_0032
{
    public class BusStation
    {
        #region variables
        private static Random rnd = new Random(DateTime.Now.Millisecond);
        protected int busStationKey;
        protected float latitude;
        protected float longitude;
        protected string stationAddress;
        #endregion

        #region ctors
        public BusStation(BusStation _station)
        {
            busStationKey = _station.busStationKey;
            latitude = _station.latitude;
            longitude = _station.longitude;
        }
        public BusStation(int _busStationKey, int _latitude, int _longitude)
        {
            if (_busStationKey <= 999999)
            {
                busStationKey = _busStationKey;
                latitude = _latitude;
                longitude = _longitude;
            }
            else throw new ArgumentException("the key is too big");
        }
        public BusStation(int _busStationKey)
        {
            if (_busStationKey <= 999999)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);
                busStationKey = _busStationKey;
                latitude = next_float(33.3, 31);
                longitude = next_float(34.3, 35.5);
            }
            else throw new ArgumentException("the key is too big");
        }
        #endregion

        #region methods
        public float next_float(double start, double end)
        {
            return (float)(rnd.NextDouble() * (end - start) + start);
        }

        public override string ToString()
        {
            return (string)("Bus Station Code: " + busStationKey + " "
                             + latitude + "°N " + longitude + "°E");
        }

        public static bool operator ==(BusStation left, BusStation right)
        {
            return left.busStationKey == right.busStationKey;

        }

        public static bool operator !=(BusStation left, BusStation right)
        {
            return !(left == right);

        }
        public int GetBusStationKey
        { get { return busStationKey; } }

        #endregion

    }

    public class BusStationLine : BusStation
    {
        #region variables
        private float distFromLastStation;
        private float timeSinceLastStation;
        #endregion

        #region ctors
        public BusStationLine(BusStation _busStation, float _distFromLastStation, float _timeSinceLastStation, string addr = null) : base(_busStation)
        {
            distFromLastStation = _distFromLastStation;
            timeSinceLastStation = _timeSinceLastStation;
        }
        public BusStationLine(int _busStationKey, float _distFromLastStation = 0, float _timeSinceLastStation = 0, string addr = null) : base(_busStationKey)
        {
            distFromLastStation = _distFromLastStation;
            timeSinceLastStation = _timeSinceLastStation;
            stationAddress = addr;
        }
        #endregion

        #region methods
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
        public string time_str()
        {
            string hour = ((int)(timeSinceLastStation / 60)).ToString();
            string minute = ((int)(timeSinceLastStation % 60) / 10).ToString() + ((int)(timeSinceLastStation % 60) % 10).ToString();
            string second = ((int)((timeSinceLastStation % 1) * 60) / 10).ToString() + ((int)((timeSinceLastStation % 1) * 60) % 10).ToString();
            return hour + ":" + minute + ":" + second;
        }
        public override string ToString()
        {
            return base.ToString() + "    " + time_str();
        }
        #endregion


    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        
        public int BusStationKey { get; set; }
        public float Latitude { get; set; }
        protected float Longitude { get; set; }
        protected string StationAddress { get; set; }



        //public float next_float(double start, double end)
        //{
        //    return (float)(rnd.NextDouble() * (end - start) + start);
        //}

        //public override string ToString()
        //{
        //    return (string)("Bus Station Code: " + busStationKey + " "
        //                     + latitude + "°N " + longitude + "°E");
        //}

        //public static bool operator==(BusStation left, BusStation right)
        //{
        //    return left.busStationKey == right.busStationKey;

        //}

        //public static bool operator !=(BusStation left, BusStation right)
        //{
        //    return !(left == right);

        //}


        

    }

}
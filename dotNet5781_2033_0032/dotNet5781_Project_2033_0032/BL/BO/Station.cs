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
    }

}
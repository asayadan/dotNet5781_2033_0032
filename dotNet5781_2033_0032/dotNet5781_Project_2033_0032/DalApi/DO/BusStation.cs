using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class BusStation
    {
        public int BusStationKey { get; set; }
        public float Latitude { get; set; }
        protected float Longitude { get; set; }
        protected string StationAddress { get; set; }
    }
}

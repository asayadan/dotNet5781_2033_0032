using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class Counters
    {
        public static int lines { set; get; } = 0;
        public static int trips { set; get; } = 0;
        public static int lineTrip { set; get; } = 0;
        public static int busOnTrips { set; get; } = 0;
    }
}
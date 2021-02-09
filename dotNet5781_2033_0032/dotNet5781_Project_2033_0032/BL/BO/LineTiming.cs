using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace BO
{
    public class LineTiming
    {
        public int LineId { set; get; }
        public int LineCode { set; get; }
        public TimeSpan TripStartTime { set; get; }
        public string LastStationName { set; get; }
        public TimeSpan TimeToStation { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class StationInLine
    {
        public double DistFromLastStation { get; set; }
        public TimeSpan TimeSinceLastStation { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int PrevStation { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}

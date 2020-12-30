using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class Line
    {
        public int BusNumber { get; set; }
        public AdjacentStations FirstStation { get; set; }
        public AdjacentStations LastStation { get; set; }
        public Areas Area { get; set; }
        public int Id { get; set; }
    }
}

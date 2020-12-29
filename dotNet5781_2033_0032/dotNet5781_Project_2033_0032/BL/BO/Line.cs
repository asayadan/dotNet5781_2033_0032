using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class BusLine
    {
        #region variables
        public int busLine;
        public BusStationLine firstStation;
        public BusStationLine lastStation;
        public Area area;
        public direction;
        public List<BusStationLine> stations;
        #endregion
    }
}

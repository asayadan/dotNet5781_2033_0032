using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2033_0032
{
    class LineCollection : IEnumerable
    {
        #region varibles
        private List<BusLine> lines;
        #endregion
        #region constactors
        public LineCollection()
        {
            lines = new List<BusLine>();
        
        }
        #endregion
        public void addLine(BusLine line)
        {
            int help = num_station(line._lineNumber);
            if ( help== 0)
                lines.Add(line);
            else if (help==1)
            {
                line._direction = false;
                lines.Add(line);
            }
            else throw new ArgumentException("Bus already exists twice.");
            
        }

        public void removeLine(BusLine line)
        {
            var tempLine = lines.Find(x => x == line);
            if (lines.Exists(x=>x==line) )
                lines.Remove(tempLine);
            else throw new ArgumentException("Bus doesn't exist.");

        }
        public int num_station(int line_key)
        { return lines.FindAll(x => x._lineNumber == line_key).Count;  }
        public List<BusLine> checkStation(int busStationKey)
        {
            var linesOfStation = lines.FindAll(x => x.exist(busStationKey));
            if (linesOfStation.Count != 0)
                return linesOfStation;
            else throw new ArgumentOutOfRangeException("No bus passes this station.");

        }

        public List<BusLine> sortedLines()
        {
            lines.Sort(delegate (BusLine x, BusLine y)
            {
                return x.CompareTo(y);
            });

            return lines;
        }

        public BusLine this[int busLine,bool help=true] =>lines[returnIndexer(busLine,help)];

        
        private int returnIndexer(int busline, bool help)
        {
            int indx = lines.FindIndex(x => x == busline&&x._direction==help);

            if (indx>=0)
            {
                 return indx;
            }
            else throw new ArgumentOutOfRangeException("we don't have this busLine");
        }

        public IEnumerator GetEnumerator()
        {
            return lines.GetEnumerator();
        }


    }
}


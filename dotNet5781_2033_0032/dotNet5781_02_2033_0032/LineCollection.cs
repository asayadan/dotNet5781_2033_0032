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

        #region ctors
        public LineCollection()
        {
            lines = new List<BusLine>();

        }
        #endregion

        #region methods
        public void addLine(BusLine line)
        {
            int help = num_station(line._lineNumber);
            if (help == 0)
                lines.Add(line);
            else if (help == 1)
            {
                line._direction = false;
                lines.Add(line);
            }
            else throw new ArgumentException("Bus already exists twice.");

        }

        public void removeLine(BusLine line)
        {
            int indx_help = lines.FindIndex(x => x == line);
            if (indx_help >= 0)
                lines.Remove(lines[indx_help]);
            else throw new ArgumentException("Bus doesn't exist.");

        }

        public int num_station(int line_key)
        { return lines.FindAll(x => x._lineNumber == line_key).Count; }

        public List<BusLine> checkStation(int busStationKey)
        {
            var linesOfStation = lines.FindAll(x => x.exist(busStationKey));
            if (linesOfStation.Count != 0)
                return linesOfStation;
            else throw new ArgumentOutOfRangeException("No bus passes this station.");

        }

        public List<BusLine> sortedLines() //Sort lines by the sort method
        {
            lines.Sort(delegate (BusLine x, BusLine y)
            {
                return x.CompareTo(y);
            });

            return lines;
        }

        public BusLine this[int busLine, bool help = true]  //help to check if back or forth
        {
            get
            {
                int indx = lines.FindIndex(x => x == busLine && x._direction == help);

                if (indx >= 0)
                {
                    return lines[indx];
                }
                else throw new ArgumentOutOfRangeException("we don't have this busLine");
            }
        }


        public IEnumerator GetEnumerator() 
        {
            return lines.GetEnumerator();
        }
        #endregion

    }
}
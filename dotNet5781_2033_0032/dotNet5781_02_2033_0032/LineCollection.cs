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

        public void addLine(BusLine line)
        {
            if (lines.FindAll(x => x._lineNumber == line._lineNumber).Count == 0)
                lines.Add(line);
            else if (lines.FindAll(x => x._lineNumber == line._lineNumber).Count == 1)
            {
                if (lines.Find(x => x._lineNumber == line._lineNumber).isReverse(line))//I will need to come back to it later
                {
                    lines.Add(line);
                }
                else
                {

                }
            }
            else throw new ArgumentException("Bus already exists twice.");

        }

        public void removeLine(BusLine line)
        {
            if (lines.Exists(x => x == line))
                lines.Remove(line);
            else throw new ArgumentException("Bus doesn't exist.");

        }

        public List<BusLine> checkStation(int busStationKey)
        {
            var linesOfStation = lines.FindAll(x => x.exist(busStationKey));
            if (linesOfStation.Count != 0)
                return linesOfStation;
            else throw new ArgumentOutOfRangeException("No bus passes this station.");

        }

        public List<BusLine> sortedStations()
        {
            lines.Sort(delegate (BusLine x, BusLine y)
            {
                return x.CompareTo(y);
            });

            return lines;
        }

        public BusLine this[int busLine] =>lines[returnIndexer(busLine)];

        private int returnIndexer(int busline)
        {
            int indx = lines.FindIndex(x => x == busline);

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


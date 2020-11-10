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
            if (lines.FindAll(x => x._lineNumber == line._lineNumber).Count == 0)
                lines.Add(line);
            else if (lines.Exists(x => x == line))
            {
                throw new ArgumentException("this bus is already in this collection");
            }
            else if (lines.FindAll(x => x._lineNumber == line._lineNumber).Count == 2)
            {
                throw new ArgumentException("Bus already exists twice.");
            }
            else if (lines.Find(x => x._lineNumber == line._lineNumber).isReverse(line))//I will need to come back to it later
            {
                lines.Add(line);
            }
            else throw new ArgumentException("this bus needs to be the  opposite of the other line with the same line number");
        }

        public void removeLine(BusLine line)
        {
            var tempLine = lines.Find(x => x == line);
            if (tempLine != null)
                lines.Remove(tempLine);
            else throw new ArgumentException("Bus doesn't exist.");

        }

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using System.Xml;
using System.Xml.Linq;


namespace DL
{
    class DalXml : IDL
    {
        #region singelton
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get; } = new DalXml();
        #endregion
        string AdjacentStationsPath = "";

        #region station
        /// <summary>
        /// we add to the memory ne AdjacentStations if it doesn't exist already
        /// </summary>
        /// <param name="adjacentStations">the stations we want to add</param>
        public void CreateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
           
            AdjacentStations thisStaions = (from stations in AdjacentStationsRootElem.Elements()
                                                 where stations.Equals(adjacentStations.Station1, adjacentStations.Station2)
                                                 select stations.ToAdjecentStation()
                                            ).FirstOrDefault();

            if (adjacentStations == null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2, "we already have this AdjecentStations");

            AdjacentStationsRootElem.Add(adjacentStations.ToXElement());

            XMLTools.SaveListToXMLElement(AdjacentStationsRootElem,AdjacentStationsPath);

        }
       /// <summary>
       /// searches for an AdjacentStations by stations ID
       /// </summary>
       /// <param name="station1"></param>
       /// <param name="station2"></param>
       /// <returns>the AdjacentStations instance as it appear in the memory</returns>
        public AdjacentStations RequestAdjacentStations(int station1, int station2)
        {
            try
            {

                XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

                AdjacentStations adjacentStations = (from stations in AdjacentStationsRootElem.Elements()
                                 where stations.Equals(station1,station2)
                                 select stations.ToAdjecentStation()
                                 ).FirstOrDefault();

                if (adjacentStations == null)
                    throw new DO.InvalidAdjacentStationIDException(station1, station2);

                return adjacentStations;
            }
            catch (FormatException ex)
            {
                throw new XMLFileFormatException(AdjacentStationsPath, "unexpected format error occurred", ex);
            }
        }

        public void RemoveAdjacentStations(AdjacentStations adjacentStatons, int linneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region line



        #endregion


        public void CreateBus(Bus NewBus)
        {
            throw new NotImplementedException();
        }

        public void CreateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void CreateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void CreateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Bus> RequestAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> RequestAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> RequestAllStations()
        {
            throw new NotImplementedException();
        }

        public Bus RequestBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> RequestBusBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Line RequestLine(int id)
        {
            throw new NotImplementedException();
        }

        public LineStation RequestLineStation(int stationId, int lineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> RequestLineStationsInLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public Station RequestStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> RequestStationBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public bool RequestUserPrivileges(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> LinesInStation(int stationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveLine(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveLineStation(int stationId, int lineId)
        {
            throw new NotImplementedException();
        }


        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }
    }
}

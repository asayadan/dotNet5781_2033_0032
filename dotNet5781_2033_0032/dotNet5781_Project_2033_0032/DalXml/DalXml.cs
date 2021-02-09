using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using System.Xml;

namespace DL
{
    class DalXml : IDL
    {
        #region singelton
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get; } = new DalXml();
        #endregion
        #region stations
        #endregion

        public void CreateAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

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

        public AdjacentStations RequestAdjacentStations(int station1, int station2)
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

        public void RemoveAdjacentStations(AdjacentStations adjacentStatons, int linneId)
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

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
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

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            throw new NotImplementedException();
        }
    }
}

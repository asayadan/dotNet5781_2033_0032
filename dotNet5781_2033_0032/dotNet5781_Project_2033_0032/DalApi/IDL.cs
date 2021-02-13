using DO;
using System;
using System.Collections.Generic;

namespace DLAPI
{
    public interface IDL
    {
        #region Bus
        IEnumerable<DO.Bus> RequestAllBuses();
        IEnumerable<DO.Bus> RequestBusBy(Predicate<DO.Bus> predicate);
        DO.Bus RequestBus(int licenseNum);
        void CreateBus(DO.Bus NewBus);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations
        void DeleteStation(int id);
        void CreateStation(DO.Station station);
        DO.Station RequestStation(int id);
        IEnumerable<DO.Station> RequestAllStations();
        IEnumerable<DO.Station> RequestStationBy(Predicate<DO.Station> predicate);
        IEnumerable<DO.Line> GetLinesInStation(int stationId);
        void CreateAdjacentStations(DO.AdjacentStations adjacentStations);
        void RemoveAdjacentStations(DO.AdjacentStations adjacentStatons);
        void UpdateStation(DO.Station station);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateLineStation(LineStation lineStation);
        DO.AdjacentStations RequestAdjacentStations(int station1, int station2);
        DO.LineStation RequestLineStation(int stationId, int lineId);
        IEnumerable<DO.LineStation> RequestLineStationsInLine(int lineId);
        void CreateLineStation(DO.LineStation lineStation);
        void RemoveLineStation(int stationId, int lineId);

        #endregion

        #region Line

        IEnumerable<DO.Line> RequestAllLines();
        DO.Line RequestLine(int id);
        void CreateLine(DO.Line line);
        void RemoveLine(int id);
        void UpdateLine(DO.Line line);
        #endregion

        #region User
        bool RequestUserPrivileges(string username, string password);
        void CreateUser(DO.User user);
        #endregion
        IEnumerable<LineTrip> GetAllLineTripsInLine(int lineId);

    }
}

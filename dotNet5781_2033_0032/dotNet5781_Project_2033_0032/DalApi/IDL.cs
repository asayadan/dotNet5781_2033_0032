using DO;
using System;
using System.Collections.Generic;

namespace DLAPI
{
    public interface IDL
    {
        #region Bus
        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<DO.Bus> GetBusBy(Predicate<DO.Bus> predicate);
        DO.Bus GetBus(int licenseNum);
        void CreateBus(DO.Bus NewBus);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations
        void DeleteStation(int id);
        void CreateStation(DO.Station station);
        DO.Station GetStation(int id);
        IEnumerable<DO.Station> GetAllStations();
        IEnumerable<DO.Station> GetStationBy(Predicate<DO.Station> predicate);
        IEnumerable<DO.Line> LinesInStation(int stationId);
        void CreateAdjacentStations(DO.AdjacentStations adjacentStations);
        void RemoveAdjacentStations(DO.AdjacentStations adjacentStatons, int linneId);
        void UpdateStation(DO.Station station);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateLineStation(LineStation lineStation);
        DO.AdjacentStations GetAdjacentStations(int station1, int station2);
        DO.LineStation GetLineStation(int stationId, int lineId);
        IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId);
        void CreateLineStation(DO.LineStation lineStation);
        void RemoveLineStation(int stationId, int lineId);

        #endregion

        #region Line

        IEnumerable<DO.Line> GetAllLines();
        DO.Line GetLine(int id);
        void CreateLine(DO.Line line);
        void RemoveLine(int id);
        void UpdateLine(DO.Line line);
        #endregion

        #region User
        bool GetUserPrivileges(string username, string password);
        void CreateUser(DO.User user);



        #endregion
    }
}

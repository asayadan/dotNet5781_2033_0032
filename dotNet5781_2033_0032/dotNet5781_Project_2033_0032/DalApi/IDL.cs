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
        void AddBus(DO.Bus NewBus);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations
        DO.Station GetStation(int id);
        IEnumerable<DO.Station> GetAllStations();
        IEnumerable<DO.Line> LinesInStation(int stationId);
        void AddAdjacentStations(DO.AdjacentStations adjacentStations);
        void RemoveAddAdjacentStations(DO.AdjacentStations adjacentStations, int linneId);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateLineStation(LineStation lineStation);
        DO.AdjacentStations GetAdjacentStations(int station1, int station2);
        DO.LineStation GetLineStation(int id);
        IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId);
        void AddLineStation(DO.LineStation lineStation);
        void RemoveLineStation(int stationId, int lineId);

        #endregion

        #region Line

        IEnumerable<DO.Line> GetAllLines();
        DO.Line GetLine(int id);
        void AddLine(DO.Line line);
        void RemoveLine(int id);
        void UpdateLine(DO.Line line);
        #endregion

        #region User
        bool GetUserPrivileges(string username, string password);
        void AddUser(DO.User user);



        #endregion



    }
}

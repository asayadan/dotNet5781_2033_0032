using BO;
using System;
using System.Collections.Generic;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        BO.Bus GetBus(int licenseNum);
        void AddBus(int licenseNum, DateTime fromTime, double fuel = 1200, double totalTrip = 0);
        void DeleteBus(int licenseNum);
        void UpdateBus(BO.Bus bus);
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate);
        void FuelBus(int id);
        void FixBus(int id);
        #endregion

        #region Stations
        void DeleteStation(int id);
        BO.Station GetStation(int id);
        IEnumerable<BO.Station> GetAllStations();
        BO.LineStation GetLineStation(int stationId, int lineId);
        IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        IEnumerable<BO.LineStation> GetLineStationsInLine(int lineId);
         IEnumerable<StationInLine> GetStationsInLine(int lineId);
        IEnumerable<BO.Line> GetAllLines();
        void UpdateLineStation(LineStation station);
        void AddStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation, double distanceUntilNextStation, TimeSpan timeUntilNextStatio);
        void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        IEnumerable<BO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        BO.Line GetLine(int id);
        void AddLine(int code, BO.Areas area, int firstStation, int lastStation, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        void RemoveLine(int id);
        void UpdateLine(Line line);

        #endregion

        #region User
        bool GetUserPrivileges(string userName, string password);
        void AddUser(string username, string password, string passwordValidation);
        #endregion

    }
}

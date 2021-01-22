using BO;
using System;
using System.Collections.Generic;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        BO.Bus RequestBus(int licenseNum);
        void CreateBus(int licenseNum, DateTime fromTime, double fuel = 1200, double totalTrip = 0);
        void DeleteBus(int licenseNum);
        void UpdateBus(BO.Bus bus);
        IEnumerable<BO.Bus> RequestAllBuses();
        IEnumerable<BO.Bus> RequestBusBy(Predicate<BO.Bus> predicate);
        void FuelBus(int id);
        void FixBus(int id);
        #endregion

        #region Stations
        void DeleteStation(int id);
        void CreateStation(int code, string name, double longitude, double latitude);
        BO.Station RequestStation(int id);
        IEnumerable<BO.Station> RequestAllStations();
        BO.LineStation RequestLineStation(int stationId, int lineId);
        IEnumerable<BO.Station> RequestStationsBy(Predicate<BO.Station> predicate);
        IEnumerable<BO.LineStation> RequestLineStationsInLine(int lineId);
         IEnumerable<StationInLine> RequestStationsInLine(int lineId);
        IEnumerable<BO.Line> RequestAllLines();
        void UpdateStation(Station station);
        void UpdateLineStation(LineStation station);
        void CreateStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation, double distanceUntilNextStation, TimeSpan timeUntilNextStatio);
        void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        IEnumerable<BO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        BO.Line RequestLine(int id);
        void CreateLine(int code, BO.Areas area, int firstStation, int lastStation, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        void RemoveLine(int id);
        void UpdateLine(Line line);

        #endregion

        #region User
        bool RequestUserPrivileges(string userName, string password);
        void CreateUser(string username, string password, string passwordValidation);
        #endregion

    }
}

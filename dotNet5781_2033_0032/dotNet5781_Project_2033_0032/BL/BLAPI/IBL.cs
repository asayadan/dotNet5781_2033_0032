﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        BO.Bus GetBus(int licenseNum);
        void AddBus(int licenseNum, DateTime fromTime);
        void DeleteBus(int licenseNum);
        void UpdateBusDetails(BO.Bus bus);
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate);
        void FuelBus(int id);
        void FixBus(int id);
        #endregion

        #region Stations
        BO.Station GetStation(int id);
        BO.LineStation GetLineStation(int id);
        IEnumerable<BO.LineStation> GetLineStationsInLine(int lineId);
        IEnumerable<BO.Line> GetAllLines();
        void AddStationToLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        void removeStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        IEnumerable<BO.Line> linesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        BO.Line GetLine(int id);
        void addLine(int code, BO.Areas area, int firstStation, int lastStation);
        void removeLine(int code);

        #endregion

        #region User
        BO.User GetUser(string userName, string password);
        #endregion

    }
}

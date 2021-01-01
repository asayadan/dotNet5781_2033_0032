using System;
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
        void UpdateBus(BO.Bus bus);
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
        void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        IEnumerable<BO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        BO.Line GetLine(int id);
        void AddLine(int id, BO.Areas area, int firstStation, int lastStation);
        void RemoveLine(int id);

        #endregion

        #region User
        bool GetUserPrivileges(string userName, string password);
        void AddUser(string username,string password, string passwordValidation);
        #endregion

    }
}

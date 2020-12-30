using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
    public interface IDL
    {
        #region Bus
        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<DO.Bus> GetBusBy(Predicate<DO.Bus> predicate);
        DO.Bus GetBus(int licenseNum);
        void CreateBus(int licenseNum, DateTime fromTime);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations

        DO.Station GetStation(int id);
        IEnumerable<DO.Line> GetAllLines();
        IEnumerable<DO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion
        #region LIine Station
        DO.LineStation GetLineStation(int id);
        IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId);
        void AddStationToLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        DO.Line GetLine(int id);
        void AddLine(DO.Line line);
        void RemoveLine(int id);
        #endregion

        #region User
        bool GetUserPrivileges(string username, string password);
        void CreateUser(DO.User user);

        #endregion



    }
}

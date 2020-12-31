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
        void AddBus(DO.Bus NewBus);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations

        DO.Station GetStation(int id);
        IEnumerable<DO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
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
        #endregion

        #region User
        bool GetUserPrivileges(string username, string password);
        void AddUser(DO.User user);

        #endregion



    }
}

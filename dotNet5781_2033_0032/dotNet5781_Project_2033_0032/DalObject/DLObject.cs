using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLAPI;
//using DO;
using DS; 

namespace DL
{
    sealed class DLObject : IDL
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }
        DLObject() { } 
        public static DLObject Instance { get => instance; }
        #endregion

        #region Bus
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses 
                   select bus.Clone();
        }
        public IEnumerable<DO.Bus> GetBusBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus) select bus.Clone();
        }
        public DO.Bus GetBus(int licenseNum)
        {
            DO.Bus helpBus = DataSource.ListBuses.Find(x => x.LicenseNum == licenseNum);
            if (helpBus != null)
            {
                return helpBus.Clone();
            }
            else throw new DO.InvalidBusLicenseNumberException(licenseNum);
        }
        public void CreateBus(int licenseNum, DateTime fromTime)
        { }
        public void DeleteBus(int licenseNum)
        { }
        public void UpdateBus(DO.Bus bus)
        { }

        #endregion

        #region Stations

        public DO.Station GetStation(int id) 
        { }
        public IEnumerable<DO.Line> GetAllLines()
        { }
        public IEnumerable<DO.Line> LinesInStation(int stationId)
        { }
        public void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
        { }

        #endregion
        #region LIine Station
        public DO.LineStation GetLineStation(int id);
        public IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId);
        public void AddStationToLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        public void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        public DO.Line GetLine(int id);
        public void AddLine(DO.Line line);
        public void RemoveLine(int id);
        #endregion

        #region User
        public bool GetUserPrivileges(string username, string password)
        {
            DO.User helpUser = DataSource.ListUsers.Find(x => x.UserName == username && x.Password == password);
            if (helpUser==null)
                throw new DO.BadUsernameOrPasswordException(username,password,"the password and username doesn't match");
            return helpUser.Admin;
        }
        public void CreateUser(DO.User user)
        {
        
        }

        #endregion







    }
}

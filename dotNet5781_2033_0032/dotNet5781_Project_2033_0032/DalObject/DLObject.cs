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
            else throw new DO.InvalidBusLicenseNumberException(licenseNum, "this license plaet number doesn't exists");
        }
        public void AddBus(DO.Bus NewBus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == NewBus.LicenseNum) != null)
                throw new DO.InvalidBusLicenseNumberException(NewBus.LicenseNum, "this license plaet number alredy exists");
            DataSource.ListBuses.Add(NewBus.Clone());
        }
        public void DeleteBus(int licenseNum)
        {
            DO.Bus helpBus = DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == licenseNum);
            if (helpBus == null)
                throw new DO.InvalidBusLicenseNumberException(licenseNum, "this license plaet number doesn't exists");
            else DataSource.ListBuses.Remove(helpBus);
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus helpBus = DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum);
            if (helpBus == null)
                throw new DO.InvalidBusLicenseNumberException(bus.LicenseNum, "this license plaet number doesn't exists");
            else
            {
                DataSource.ListBuses.Remove(helpBus);
                DataSource.ListBuses.Add(bus.Clone());
            }
        }

        #endregion

        #region Stations

        public DO.Station GetStation(int id)
        {
            DO.Station helpStation = DataSource.ListStations.Find(x => x.Code == id);
            if (helpStation != null)
            {
                return helpStation.Clone();
            }
            else throw new DO.InvalidStationIDException(id, "bad station id");
        }

        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations helpAdj = DataSource.ListAdjacentStations.Find(p => p.Station1 == adjacentStations.Station1 && p.Station2 == adjacentStations.Station2);
            if (helpAdj == null)
                throw new DO.InvalidStationLineIDException(adjacentStations.Station1,adjacentStations.Station2,"we doen't have this two adjacent station");
            else
            {
                DataSource.ListAdjacentStations.Remove(helpAdj);
                DataSource.ListAdjacentStations.Add(helpAdj.Clone());
            }
        }
        public DO.LineStation GetLineStation(int id)
        {
            DO.LineStation helpLineStation = DataSource.ListLineStations.Find(p => p.Id==id);
            if (helpLineStation == null)
                throw new DO.InvalidStationIDException(id, "we doen't have this line station");
            return helpLineStation.Clone();
        }
        public IEnumerable<DO.Line> LinesInStation(int stationId)
        {
            return from lineStations in DataSource.ListLineStations
                   where lineStations.Id == stationId
                   select GetLine(lineStations.LineId);
        }
        public IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId);
        public void AddStationToLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        public void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        { }
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

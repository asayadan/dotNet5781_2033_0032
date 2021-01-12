using DLAPI;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DL
{
    sealed class DLObject : IDL
    {
        #region singelton
        static DLObject() { }
        DLObject() { }
        public static DLObject Instance { get; } = new DLObject();
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
                   where predicate(bus)
                   select bus.Clone();
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
        public void AddAdjacentStations(DO.AdjacentStations adjacentStation)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => p.Station1 == adjacentStation.Station1 && p.Station2 == adjacentStation.Station2) != null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStation.Station1, adjacentStation.Station2, "the data base alredy has this adjacent station data structure");
            DataSource.ListAdjacentStations.Add(adjacentStation.Clone());
        }
        public void RemoveAddAdjacentStations(DO.AdjacentStations adjacentStation, int linneId)
        {
            int helpIndex = DataSource.ListAdjacentStations.FindIndex(p => p.Station1 == adjacentStation.Station1 && p.Station2 == adjacentStation.Station2);
            if (helpIndex == -1)
                throw new DO.InvalidAdjacentStationIDException(adjacentStation.Station1, adjacentStation.Station2, "this license line station  number doesn't exists");
            if (1 == DataSource.ListLineStations.FindAll(p => p.LineStationIndex != 0 && p.PrevStation == adjacentStation.Station1 && p.StationId == adjacentStation.Station2).Count)
            {
                DataSource.ListAdjacentStations.Remove(DataSource.ListAdjacentStations[helpIndex]);
            }

        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations helpAdj = DataSource.ListAdjacentStations.Find(p => p.Station1 == adjacentStations.Station1 && p.Station2 == adjacentStations.Station2);
            if (helpAdj == null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2, "we doen't have this two adjacent station");
            else
            {
                DataSource.ListAdjacentStations.Remove(helpAdj);
                DataSource.ListAdjacentStations.Add(helpAdj.Clone());
            }
        }
        public DO.LineStation GetLineStation(int id)
        {
            DO.LineStation helpLineStation = DataSource.ListLineStations.Find(p => p.StationId == id);
            if (helpLineStation == null)
                throw new DO.InvalidStationIDException(id, "we doen't have this line station");
            return helpLineStation.Clone();
        }
        public IEnumerable<DO.Line> LinesInStation(int stationId)
        {
            return from lineStations in DataSource.ListLineStations
                   where lineStations.StationId == stationId
                   select GetLine(lineStations.LineId);
        }
        public IEnumerable<DO.LineStation> GetLineStationsInLine(int lineId)
        {
            return from lineStation in DataSource.ListLineStations
                   where lineStation.LineId == lineId
                   orderby lineStation.LineStationIndex ascending
                   select lineStation.Clone();
        }

        public DO.AdjacentStations GetAdjacentStations(int station1, int station2)
        {
            var st = DataSource.ListAdjacentStations.Find(x => x.Station1 == station1 && x.Station2 == station2);
            if (st != null)
                return st;
            throw new DO.InvalidAdjacentStationIDException(station1, station2, "One or more of the stations not found");
        }

        public void UpdateLineStation(int id, DO.LineStation station)
        {
            var a = DataSource.ListLineStations.FindIndex(x => x.StationId == id);
            if (a != -1)
                DataSource.ListLineStations[a] = station.Clone();
            else throw new DO.InvalidLinesStationException(station.StationId, station.LineId, "Station doesn't exist.");
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.StationId == lineStation.StationId && p.LineId == lineStation.LineId) != null)
                throw new DO.InvalidLinesStationException(lineStation.StationId, lineStation.LineId, "the data base alredy has this line station");
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void RemoveLineStation(int stationId, int lineId)
        {
            int helpIndex = DataSource.ListLineStations.FindIndex(p => p.StationId == stationId && p.LineId == lineId);
            DO.LineStation helpLineStation = DataSource.ListLineStations[helpIndex];
            if (helpIndex == -1)
                throw new DO.InvalidLinesStationException(stationId, lineId, "this license line station  number doesn't exists");
            else DataSource.ListLineStations.Remove(helpLineStation);
        }

        #endregion

        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();
        }
        public DO.Line GetLine(int id)
        {

            DO.Line helpLine = DataSource.ListLines.Find(x => x.Id == id);
            if (helpLine != null)
            {
                return helpLine.Clone();
            }
            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(p => p.Id == line.Id) != null)
                throw new DO.InvalidLineIDException(line.Id, "the data base alredy has this line line");
            DataSource.ListLines.Add(line.Clone());
        }
        public void RemoveLine(int id)
        {
            int helpIndex = DataSource.ListLines.FindIndex(p => p.Id == id);
            DO.Line helpLine = DataSource.ListLines[helpIndex];
            if (helpLine == null)
                throw new DO.InvalidLineIDException(id, "this  line   number doesn't exist in our database");
            else DataSource.ListLines.Remove(helpLine);
        }
        #endregion

        #region User
        public bool GetUserPrivileges(string username, string password)
        {
            DO.User helpUser = DataSource.ListUsers.Find(x => x.UserName == username && x.Password == password);
            if (helpUser == null)
                throw new DO.BadUsernameOrPasswordException(username, password, "the password and username doesn't match");
            return helpUser.Admin;
        }
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName) != null)
                throw new DO.BadUsernameOrPasswordException(user.UserName, user.Password, "the data base alredy has this line line");
            DataSource.ListUsers.Add(user.Clone());
        }

        #endregion
    }
}

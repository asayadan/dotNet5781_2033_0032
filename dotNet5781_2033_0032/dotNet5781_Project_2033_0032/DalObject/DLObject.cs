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
        public IEnumerable<DO.Bus> RequestAllBuses()
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
        public IEnumerable<DO.Bus> RequestBusBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        public DO.Bus RequestBus(int licenseNum)
        {
            DO.Bus helpBus = DataSource.ListBuses.Find(x => x.LicenseNum == licenseNum);
            if (helpBus != null)
            {
                return helpBus.Clone();
            }
            else throw new DO.InvalidBusLicenseNumberException(licenseNum, "this license plaet number doesn't exists");
        }
        public void CreateBus(DO.Bus NewBus)
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
            int index = DataSource.ListBuses.FindIndex(p => p.LicenseNum == bus.LicenseNum);
            if (index == -1)
                throw new DO.InvalidBusLicenseNumberException(bus.LicenseNum, "this license plaet number doesn't exists");
            else
                DataSource.ListBuses[index] = bus.Clone();
            
        }

        #endregion

        #region Stations
        public void DeleteStation(int id)
        {
            var station = RequestStation(id);
            if (station != null)
                DataSource.ListStations.RemoveAll(x => x.Code == id);
            else throw new DO.InvalidStationIDException(id, "Station id not found.");
        }

        public void CreateStation(DO.Station station)
        {
            if (DataSource.ListStations.Find(p => p.Code == station.Code) != null)
                throw new DO.InvalidStationIDException(station.Code, "Station code already exists.");
            DataSource.ListStations.Add(station.Clone());
        }
        public DO.Station RequestStation(int id)
        {
            DO.Station helpStation = DataSource.ListStations.Find(x => x.Code == id);
            if (helpStation != null)
            {
                return helpStation.Clone();
            }
            else throw new DO.InvalidStationIDException(id, "bad station id");
        }
        public IEnumerable<DO.Station> RequestAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();
        }
        public IEnumerable<DO.Station> RequestStationBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }

        public void CreateAdjacentStations(DO.AdjacentStations adjacentStation)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => p.Station1 == adjacentStation.Station1 && p.Station2 == adjacentStation.Station2) != null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStation.Station1, adjacentStation.Station2, "the data base alredy has this adjacent station data structure");
            DataSource.ListAdjacentStations.Add(adjacentStation.Clone());
        }
        public void DeleteAdjacentStations(DO.AdjacentStations adjacentStation)
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
            var helpAdj = DataSource.ListAdjacentStations.FindIndex(p => p.Station1 == adjacentStations.Station1 && p.Station2 == adjacentStations.Station2);
            if (helpAdj == -1)
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2, "we doen't have this two adjacent station");
            else
            {
                DataSource.ListAdjacentStations[helpAdj] = adjacentStations;
            }
        }
        public DO.LineStation RequestLineStation(int stationId, int lineId)
        {
            DO.LineStation helpLineStation = DataSource.ListLineStations.Find(p => p.StationId == stationId && p.LineId == lineId);
            if (helpLineStation == null)
                throw new DO.InvalidStationIDException(stationId, "we doen't have this line station");
            return helpLineStation.Clone();
        }
        public IEnumerable<DO.Line> RequestLinesInStation(int stationId)
        {
            return from lineStations in DataSource.ListLineStations
                   where lineStations.StationId == stationId
                   select DataSource.ListLines.Find(x => x.Id == lineStations.LineId).Clone();
        }
        public IEnumerable<DO.LineStation> RequestLineStationsInLine(int lineId)
        {
            return from lineStation in DataSource.ListLineStations
                   where lineStation.LineId == lineId
                   orderby lineStation.LineStationIndex ascending
                   select lineStation.Clone();
        }

        public DO.AdjacentStations RequestAdjacentStations(int station1, int station2)
        {
            var st = DataSource.ListAdjacentStations.Find(x => x.Station1 == station1 && x.Station2 == station2);
            return st;
        }

        public void UpdateStation(DO.Station station)
        {
            var a = DataSource.ListStations.FindIndex(x => x.Code == station.Code);
            if (a != -1)
                DataSource.ListStations[a] = station.Clone();
            else throw new DO.InvalidStationIDException(station.Code, "Station doesn't exist.");
        }
        public void UpdateLineStation(DO.LineStation station)
        {
            var a = DataSource.ListLineStations.FindIndex(x => x.StationId == station.StationId && station.LineId == x.LineId);
            if (a != -1)
                DataSource.ListLineStations[a] = station.Clone();
            else throw new DO.InvalidLinesStationException(station.StationId, station.LineId, "Station doesn't exist.");
        }
        public void CreateLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.StationId == lineStation.StationId && p.LineId == lineStation.LineId) != null)
                throw new DO.InvalidLinesStationException(lineStation.StationId, lineStation.LineId, "the data base alredy has this line station");
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void DeleteLineStation(int stationId, int lineId)
        {
            int helpIndex = DataSource.ListLineStations.FindIndex(p => p.StationId == stationId && p.LineId == lineId);
            DO.LineStation helpLineStation = DataSource.ListLineStations[helpIndex];
            if (helpIndex == -1)
                throw new DO.InvalidLinesStationException(stationId, lineId, "this license line station  number doesn't exists");
            else DataSource.ListLineStations.Remove(helpLineStation);
        }

        #endregion

        #region Line
        public IEnumerable<DO.Line> RequestAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();
        }
        public DO.Line RequestLine(int id)
        {

            DO.Line helpLine = DataSource.ListLines.Find(x => x.Id == id);
            if (helpLine != null)
            {
                return helpLine.Clone();
            }
            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }
        public void CreateLine(DO.Line line)
        {
            if (DataSource.ListLines.Find(p => p.Id == line.Id) != null)
                throw new DO.InvalidLineIDException(line.Id, "Line number already exists.");
            DataSource.ListLines.Add(line.Clone());
        }
        public void DeleteLine(int id)
        {
            int helpIndex = DataSource.ListLines.FindIndex(p => p.Id == id);
            DO.Line helpLine = DataSource.ListLines[helpIndex];
            if (helpLine == null)
                throw new DO.InvalidLineIDException(id, "this  line   number doesn't exist in our database");
            else DataSource.ListLines.Remove(helpLine);
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line helpLine = DataSource.ListLines.FirstOrDefault(p => p.Id == line.Id);
            if (helpLine == null)
                throw new DO.InvalidLineIDException(line.Id, "this line doesn't exists");
            else
            {
                DataSource.ListLines.Remove(helpLine);
                DataSource.ListLines.Add(line.Clone());
            }
        }

        #endregion

        #region User
        public bool RequestUserPrivileges(string username, string password)
        {
            DO.User helpUser = DataSource.ListUsers.Find(x => x.UserName == username && x.Password == password);
            if (helpUser == null)
                throw new DO.BadUsernameOrPasswordException(username, password, "the password and username doesn't match");
            return helpUser.Admin;
        }
        public void CreateUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName) != null)
                throw new DO.BadUsernameOrPasswordException(user.UserName, user.Password, "the data base alredy has this line line");
            DataSource.ListUsers.Add(user.Clone());
        }



        #endregion

        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            return from lineTrip in DataSource.ListLineTrips
                   select lineTrip.Clone();
        }

        public IEnumerable<DO.LineTrip> RequestAllLineTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.LineTrip> RequestAllLineTripsInLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void CreateLineTrip(DO.LineTrip Newtrip)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int tripID)
        {
            throw new NotImplementedException();
        }

        public int RequestCounter(string type)
        {
            throw new NotImplementedException();
        }

        public void UpdateCounter(string type)
        {
            throw new NotImplementedException();
        }

        public void CreateTrip(DO.Trip newTrip)
        {
            throw new NotImplementedException();
        }
    }
}

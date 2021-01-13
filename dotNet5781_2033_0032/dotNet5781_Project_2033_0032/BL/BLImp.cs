using BLAPI;
using BO;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();


        #region Bus
        public BO.Bus GetBus(int licenseNum)
        {
            try
            {
                return dl.GetBus(licenseNum).CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
            }
            catch (DO.InvalidBusLicenseNumberException ex)
            {
                throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
            }
        }
        public void AddBus(int licenseNum, DateTime fromTime)
        {
            if (((fromTime.Year >= 2018) && (licenseNum > 9999999)) && (licenseNum <= 99999999) ||//the BO.Bus registered after 2018 and has 8 digits
                ((fromTime.Year < 2018) && (licenseNum <= 9999999)) && (licenseNum > 999999))//the BO.Bus registered before 2018 and has 7 digits
                try
                {
                    dl.AddBus(new DO.Bus { LicenseNum = licenseNum, FromDate = fromTime });
                }
                catch (InvalidBusLicenseNumberException ex)
                {
                    throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
                }
            else
            {
                throw new InvalidBusLicenseNumberException(licenseNum, "The license plate number isn't valid to that date.");
            }
        }
        public void DeleteBus(int licenseNum)
        {
            try
            {
                dl.DeleteBus(licenseNum);
            }
            catch (InvalidBusLicenseNumberException ex)
            {
                throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
            }

        }
        public void UpdateBus(BO.Bus bus)
        {
            try
            {
                dl.UpdateBus(bus.CopyPropertiesToNew(typeof(DO.Bus)) as DO.Bus);
            }
            catch (DO.InvalidBusLicenseNumberException ex)
            {
                throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
            }
        }
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            foreach (var bus in dl.GetAllBuses())
            {
                yield return bus.CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
            }
        }
        public IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate)
        {

            foreach (var bus in dl.GetBusBy(predicate.CopyPropertiesToNew
                (typeof(Predicate<BO.Bus>)) as Predicate<DO.Bus>))
            {
                yield return bus.CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
            }

        }
        public void FuelBus(int id)
        {
            var a = dl.GetBus(id);
            a.FuelRemaining = DO.Bus.FullGasTank;
            dl.UpdateBus(a);
        }
        public void FixBus(int id)
        {
            var a = dl.GetBus(id);
            a.LastTreatment = DateTime.Now;
            dl.UpdateBus(a);
        }
        #endregion

        #region Stations
        public BO.Station GetStation(int id)
        {
            try
            {
                return dl.GetStation(id).CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            foreach (var station in dl.GetAllStations())
            {
                yield return station.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
            }
        }
        public BO.LineStation GetLineStation(int id)
        {
            try
            {
                return dl.GetLineStation(id).CopyPropertiesToNew(typeof(DO.LineStation)) as BO.LineStation;
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public void UpdateLineStation(int id, BO.LineStation station)
        {
            try
            {
                dl.UpdateLineStation(id, station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }

        public IEnumerable<BO.LineStation> GetLineStationsInLine(int lineId)
        {
            foreach (var station in dl.GetLineStationsInLine(lineId))
            {
                yield return station.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
            }
        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            foreach (var line in dl.GetAllLines())
            {
                yield return line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
        }
        public void AddStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation, double distanceUntilNextStation, TimeSpan timeUntilNextStatio)
        {
            try
            {
                var curLine = GetLine(lineId);
                var stations = GetLineStationsInLine(lineId);
                if (index == 0)
                    curLine.FirstStation = stationId;

                if (index == stations.Count() - 1)
                    curLine.LastStation = stationId;

                foreach (var station in stations)
                {
                    if (station.LineStationIndex > index)
                    {
                        station.LineStationIndex++;
                        UpdateLineStation(station.StationId, station);
                    }
                }

                var nextStation = stations.Where(x => x.LineStationIndex == index + 1).First();
                var prevStation = stations.Where(x => x.LineStationIndex == index - 1).First();
                nextStation.PrevStation = stationId;
                prevStation.NextStation = stationId;
                UpdateLineStation(nextStation.StationId, nextStation);
                UpdateLineStation(prevStation.StationId, prevStation);


                dl.AddLineStation(new DO.LineStation
                {
                    LineStationIndex = index,
                    LineId = lineId,
                    StationId = stationId,
                    NextStation = nextStation.StationId,
                    PrevStation = prevStation.StationId
                });

                try
                {
                    dl.AddAdjacentStations(new DO.AdjacentStations
                    {
                        DistFromLastStation = distanceSinceLastStation,
                        Station1 = prevStation.StationId,
                        Station2 = stationId,
                        TimeSinceLastStation = timeSinceLastStation
                    });


                    dl.AddAdjacentStations(new DO.AdjacentStations
                    {
                        DistFromLastStation = distanceUntilNextStation,
                        Station1 = stationId,
                        Station2 = nextStation.StationId,
                        TimeSinceLastStation = timeUntilNextStatio
                    });
                }

                catch (DO.InvalidAdjacentStationIDException) { }

                if (GetAllLines().Where(x => GetLineStationsInLine(x.Id).
                    Where(y => y.StationId == prevStation.StationId && y.NextStation == nextStation.StationId).Count() > 0).Count() == 0)
                    dl.RemoveAddAdjacentStations(dl.GetAdjacentStations(prevStation.StationId, nextStation.StationId), lineId);
            }



            catch (DO.InvalidAdjacentStationIDException ex)
            {
                throw new BO.InvalidAdjacentLineIDException(ex.ID1, ex.ID2, ex.Message);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidLinesStationException(ex.ID, ex.lineId, ex.Message);
            }
        }
        public void RemoveStationFromLine(int lineId, int stationId, double distanceFromLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                var curLine = GetLine(lineId);
                var stations = GetLineStationsInLine(lineId);
                var station = stations.Where(x => x.LineId == lineId).First();
                if (station.LineStationIndex == 0)
                    curLine.FirstStation = station.NextStation;

                if (station.LineStationIndex == stations.Count() - 1)
                    curLine.LastStation = station.PrevStation;

                foreach (var st in stations)
                {
                    if (st.LineStationIndex > station.LineStationIndex)
                    {
                        st.LineStationIndex--;
                        UpdateLineStation(st.StationId, st);
                    }
                }

                var nextStation = stations.Where(x => x.LineStationIndex == station.StationId + 1).First();
                var prevStation = stations.Where(x => x.LineStationIndex == station.StationId - 1).First();
                nextStation.PrevStation = station.NextStation;
                prevStation.NextStation = station.PrevStation;
                UpdateLineStation(nextStation.StationId, nextStation);
                UpdateLineStation(prevStation.StationId, prevStation);


                dl.RemoveLineStation(station.StationId, station.LineId);




            }
            catch (DO.InvalidAdjacentStationIDException ex)
            {
                throw new BO.InvalidAdjacentLineIDException(ex.ID1, ex.ID2, ex.Message);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidLinesStationException(ex.ID, ex.lineId, ex.Message);
            }
        }
        public IEnumerable<BO.Line> LinesInStation(int stationId)
        {
            foreach (var line in dl.LinesInStation(stationId))
            {
                yield return line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
        }
        public void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                dl.UpdateAdjacentStations(new DO.AdjacentStations
                {
                    DistFromLastStation = distanceSinceLastStation,
                    Station1 = station1,
                    Station2 = station2,
                    TimeSinceLastStation = timeSinceLastStation
                });
            }

            catch (DO.InvalidAdjacentStationIDException ex)
            {
                throw new BO.InvalidAdjacentLineIDException(ex.ID1, ex.ID2, ex.Message);
            }
        }
        #endregion

        #region Line
        public BO.Line GetLine(int id)
        {
            try
            {
                return dl.GetLine(id).CopyPropertiesToNew(typeof(DO.Line)) as BO.Line;
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void AddLine(int code, BO.Areas area, int firstStation, int lastStation)
        {
            try
            {
                dl.AddLine(new DO.Line
                {
                    Area = (DO.Areas)area,
                    Code = code,
                    FirstStation = firstStation,
                    LastStation = lastStation,
                    Id = Counters.lines++
                });

                dl.AddLineStation(new DO.LineStation
                {
                    LineId = Counters.lines - 1,
                    LineStationIndex = 0,
                    NextStation = lastStation,
                    PrevStation = 0,
                    StationId = firstStation
                });

                dl.AddLineStation(new DO.LineStation
                {
                    LineId = Counters.lines - 1,
                    LineStationIndex = 0,
                    NextStation = 0,
                    PrevStation = firstStation,
                    StationId = lastStation
                });
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }

        }
        public void RemoveLine(int id)
        {
            try
            {
                dl.RemoveLine(id);
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }

        #endregion

        #region User
        public bool GetUserPrivileges(string userName, string password)
        {
            try
            {
                return dl.GetUserPrivileges(userName, password);
            }
            catch (DO.BadUsernameOrPasswordException ex)
            {
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message);
            }
        }
        public void AddUser(string userName, string password, string passwordValidation)
        {
            if (password != passwordValidation)
                throw new BadUsernameOrPasswordException(userName, password, "Passwords aren't equal.");
            try
            {
                dl.AddUser(new DO.User { Admin = false, Password = password, UserName = userName });
            }
            catch (DO.BadUsernameOrPasswordException ex)
            {
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message);
            }
        }
        #endregion
    }
}

﻿using BLAPI;
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
            return from station in dl.GetAllStations() 
                   orderby station.Code
                   select station.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;

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
        public void UpdateLineStation(BO.LineStation station)
        {
            try
            {
                dl.UpdateLineStation(station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }

        public IEnumerable<StationInLine> GetLineStationsInLine(int lineId)
        {
            return from station in dl.GetLineStationsInLine(lineId)
                   orderby station.LineStationIndex
                   let lastStationAdj=dl.GetAdjacentStations(station.PrevStation,lineId).CopyPropertiesToNew(typeof(BO.AdjacentStations)) as BO.AdjacentStations
                   let lineSt= station.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                   let thisStation=GetStation(station.StationId).CopyPropertiesToNew(typeof(BO.Station)) as BO.Station
                   select new StationInLine{
                        DistFromLastStation=lastStationAdj.DistFromLastStation,
                        TimeSinceLastStation=lastStationAdj.TimeSinceLastStation,
                        LineId=lineId,
                        StationId =station.StationId,
                        PrevStation= station.PrevStation,
                        Code=thisStation.Code ,
                        Name=thisStation.Name
                    };
            
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
                    if (station.LineStationIndex >= index)
                    {
                        station.LineStationIndex++;
                        UpdateLineStation(station);
                    }
                }
                int helpNext, helpPrev;
                if (index < stations.Count() - 1)
                {
                    var nextStation = stations.Where(x => x.LineStationIndex == index + 1).First();
                    nextStation.PrevStation = stationId;
                    UpdateLineStation(nextStation);
                     helpNext = nextStation.StationId;
                }
                else
                {
                    distanceUntilNextStation = 0;
                    timeUntilNextStatio = new TimeSpan(0);
                    helpNext = stationId;
                }
                if (index > 0 )
                {
                    var prevStation = stations.Where(x => x.LineStationIndex == index - 1).First();
                    prevStation.NextStation = stationId;
                    UpdateLineStation(prevStation);
                    helpPrev = prevStation.StationId;
                }
                else
                {
                    distanceSinceLastStation = 0;
                    timeSinceLastStation = new TimeSpan(0);
                    helpPrev = stationId;
                }

                dl.AddLineStation(new DO.LineStation
                {
                    LineStationIndex = index,
                    LineId = lineId,
                    StationId = stationId,
                    NextStation = helpNext,
                    PrevStation = helpPrev
                });

                try
                {
                    dl.AddAdjacentStations(new DO.AdjacentStations
                    {
                        DistFromLastStation = distanceSinceLastStation,
                        Station1 = helpPrev,
                        Station2 = stationId,
                        TimeSinceLastStation = timeSinceLastStation
                    });


                    dl.AddAdjacentStations(new DO.AdjacentStations
                    {
                        DistFromLastStation = distanceUntilNextStation,
                        Station1 = stationId,
                        Station2 = helpNext,
                        TimeSinceLastStation = timeUntilNextStatio
                    });
                }

                catch (DO.InvalidAdjacentStationIDException) { }

                if (GetAllLines().Where(x => GetLineStationsInLine(x.Id).
                    Where(y => y.StationId == helpPrev && y.NextStation == helpNext).Count() > 0).Count() == 0)
                    dl.RemoveAddAdjacentStations(dl.GetAdjacentStations(helpPrev,helpNext), lineId);
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
                        UpdateLineStation(st);
                    }
                }

                var nextStation = stations.Where(x => x.LineStationIndex == station.StationId + 1).First();
                var prevStation = stations.Where(x => x.LineStationIndex == station.StationId - 1).First();
                nextStation.PrevStation = station.NextStation;
                prevStation.NextStation = station.PrevStation;
                UpdateLineStation(nextStation);
                UpdateLineStation(prevStation);


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
            return from line in dl.LinesInStation(stationId)
                  select  line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            
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
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            
        }
        public BO.Line GetLine(int id)
        {
            try
            {
                return dl.GetLine(id).CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void AddLine(int code, BO.Areas area, int firstStation, int lastStation, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
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
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidLinesStationException(firstStation,lastStation,"the stations are invalid");
            }

        }
        public void RemoveLine(int id)
        {
            try
            {
                foreach (var station in GetLineStationsInLine(id))
                {
                    dl.RemoveLineStation(station.StationId, id);
                }
                dl.RemoveLine(id);
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void UpdateLine(int id, int code, BO.Areas area, int firstStation, int lastStation)
        {
            try
            {
                dl.UpdateLine(new DO.Line
                {
                    Area = (DO.Areas)area,
                    Code = code,
                    FirstStation = firstStation,
                    LastStation = lastStation,
                    Id = id
                });

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
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message,ex);
            }
        }
        #endregion
    }
}

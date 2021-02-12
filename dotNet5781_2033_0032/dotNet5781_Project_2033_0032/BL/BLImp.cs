using BLAPI;
using BO;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Media.Animation;

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();
        internal static volatile bool Cancel = true;

        public void StartSimulator(TimeSpan startTime, int speed, Action<TimeSpan> func)
        {
            SimulationClock.GetTime = startTime;
            Cancel = false;
            while (!Cancel)
            {
                Thread.Sleep(1000);
                SimulationClock.GetTime += new TimeSpan(0, 0, speed);
                func(SimulationClock.GetTime);
            }
        }

        public void StopSimulator()
        {
            Cancel = true;
        }

        public bool IsSimulationActivated()
        {
            return !Cancel;
        }

        #region Bus


        public BO.Bus RequestBus(int licenseNum)
        {
            try
            {
                return dl.RequestBus(licenseNum).CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
            }
            catch (DO.InvalidBusLicenseNumberException ex)
            {
                throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
            }
        }
        public void CreateBus(int licenseNum, DateTime fromTime, double fuel = 1200, double totalTrip = 0)
        {
            if (((fromTime.Year >= 2018) && (licenseNum > 9999999)) && (licenseNum <= 99999999) ||//the BO.Bus registered after 2018 and has 8 digits
                ((fromTime.Year < 2018) && (licenseNum <= 9999999)) && (licenseNum > 999999))//the BO.Bus registered before 2018 and has 7 digits
                try
                {
                    dl.CreateBus(new DO.Bus {isActive=true, LicenseNum = licenseNum, FromDate = fromTime, Status = DO.Status.Ready, FuelRemaining = fuel, TotalTrip = totalTrip });
                }
                catch (DO.InvalidBusLicenseNumberException ex)
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
        public IEnumerable<BO.Bus> RequestAllBuses()
        {
            return from bus in dl.RequestAllBuses()
                   select bus.CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
        }
        public IEnumerable<BO.Bus> RequestBusBy(Predicate<BO.Bus> predicate)
        {

            return from bus in dl.RequestBusBy(predicate.CopyPropertiesToNew
                (typeof(Predicate<BO.Bus>)) as Predicate<DO.Bus>)
                   select bus.CopyPropertiesToNew(typeof(BO.Bus)) as BO.Bus;
        }
        public void FuelBus(int id)
        {
            var a = dl.RequestBus(id);
            a.FuelRemaining = DO.Bus.FullGasTank;
            dl.UpdateBus(a);
        }
        public void FixBus(int id)
        {
            var a = dl.RequestBus(id);
            a.LastTreatment = DateTime.Now;
            dl.UpdateBus(a);
        }
        #endregion

        #region Stations
        public void DeleteStation(int id)
        {
            try
            {
                dl.DeleteStation(id);
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public void CreateStation(int code, string name, double longitude, double latitude)
        {
            try
            {
                dl.CreateStation(new DO.Station
                {
                    isActive = true,
                    Code = code,
                    Name = name,
                    Longitude = longitude,
                    Latitude = latitude
                });
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public BO.Station RequestStation(int id)
        {
            try
            {
                return dl.RequestStation(id).CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public IEnumerable<BO.Station> RequestAllStations()
        {
            return from station in dl.RequestAllStations()
                   orderby station.Code
                   select station.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;

        }
        public IEnumerable<BO.Station> RequestStationsBy(Predicate<BO.Station> predicate)
        {
            return from station in dl.RequestAllStations()
                   let newStation = station.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station
                   where predicate(newStation)
                   select newStation;
        }

        public BO.LineStation RequestLineStation(int stationId, int lineId)
        {
            try
            {
                return dl.RequestLineStation(stationId, lineId).CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
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
        public IEnumerable<BO.LineStation> RequestLineStationsInLine(int lineId)
        {
            return from station in dl.RequestLineStationsInLine(lineId)
                   orderby station.LineStationIndex
                   select station.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
        }
        public IEnumerable<StationInLine> RequestStationsInLine(int lineId)
        {
            return from station in dl.RequestLineStationsInLine(lineId)
                   orderby station.LineStationIndex
                   let lastStationAdj = dl.RequestAdjacentStations(station.PrevStation, station.StationId).CopyPropertiesToNew(typeof(BO.AdjacentStations)) as BO.AdjacentStations
                   let lineSt = station.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                   let thisStation = RequestStation(station.StationId).CopyPropertiesToNew(typeof(BO.Station)) as BO.Station
                   select new StationInLine
                   {
                       DistFromLastStation = lastStationAdj.DistFromLastStation,
                       TimeSinceLastStation = lastStationAdj.TimeSinceLastStation,
                       LineId = lineId,
                       LineStationIndex = station.LineStationIndex,
                       StationId = station.StationId,
                       PrevStation = station.PrevStation,
                       Code = thisStation.Code,
                       Name = thisStation.Name
                   };

        }

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            return from lineTrip in dl.GetAllLineTrips()
                   orderby lineTrip.StartAt
                   select lineTrip.CopyPropertiesToNew(typeof(BO.LineTrip)) as LineTrip;
                   

        }
        public (LineTrip, int) GetClosestLineTripByLine(int lineId, TimeSpan timeToStation)
        {
            var lineTrip = GetAllLineTrips().Where(p => p.LineId == lineId).FirstOrDefault();
            int min = 0;
            while (SimulationClock.GetTime >
                lineTrip.StartAt + TimeSpan.FromMilliseconds(min * lineTrip.Frequency.TotalMilliseconds)
                + timeToStation)
                    min++;
            return (lineTrip, min);
        }

        public IEnumerable<LineTiming> RequestLineTimingFromStation(int stationId)
        {
            return from line in LinesInStation(stationId)
                   let index = RequestLineStation(stationId, line.Id).LineStationIndex
                   let TimeInStation = TimeSpan.FromMilliseconds(RequestStationsInLine(line.Id).
                        Where(p => p.LineStationIndex <= index).
                        Sum(p => p.TimeSinceLastStation.TotalMilliseconds))
                   let closestTrip = GetClosestLineTripByLine(line.Id, TimeInStation)
                   let lastStationName = RequestStation(line.LastStation).Name
                   let timeInFirstStation = TimeSpan.FromMilliseconds(closestTrip.Item1.StartAt.TotalMilliseconds +
                                                closestTrip.Item1.Frequency.TotalMilliseconds * closestTrip.Item2)
                   orderby timeInFirstStation + TimeInStation
                   select new LineTiming
                   {
                       LastStationName = lastStationName,
                       LineCode = line.Code,
                       LineId = line.Id,
                       TimeToStation = timeInFirstStation + TimeInStation - SimulationClock.GetTime,
                       TripStartTime = timeInFirstStation
                   };

        }      

        public void CreateStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation, double distanceUntilNextStation, TimeSpan timeUntilNextStatio)
        {
            try
            {
                var curLine = RequestLine(lineId);
                var stations = RequestLineStationsInLine(lineId);
                if (index == 0)
                    curLine.FirstStation = stationId;

                if (index == stations.Count() )
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
                stations = RequestLineStationsInLine(lineId);
                if (index < stations.Count())
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
                if (index > 0)
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

                dl.CreateLineStation(new DO.LineStation
                {
                    isActive = true,
                    LineStationIndex = index,
                    LineId = lineId,
                    StationId = stationId,
                    NextStation = helpNext,
                    PrevStation = helpPrev
                });

                try
                {
                    dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = distanceSinceLastStation,
                        Station1 = helpPrev,
                        Station2 = stationId,
                        TimeSinceLastStation = timeSinceLastStation
                    });

                }

                catch (DO.InvalidAdjacentStationIDException) { }
                try
                {
                    dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive=true,
                        DistFromLastStation = distanceUntilNextStation,
                        Station1 = stationId,
                        Station2 = helpNext,
                        TimeSinceLastStation = timeUntilNextStatio
                    });
                }

                catch (DO.InvalidAdjacentStationIDException) { }

                if (RequestAllLines().Where(x => RequestLineStationsInLine(x.Id).
                    Where(y => y.StationId == helpPrev && y.NextStation == helpNext).Count() > 0).Count() == 0)
                    dl.RemoveAdjacentStations(dl.RequestAdjacentStations(helpPrev, helpNext));
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
                var curLine = RequestLine(lineId);
                var stations = RequestLineStationsInLine(lineId);
                var station = stations.Where(x => x.LineId == lineId && x.StationId == stationId).FirstOrDefault();


                foreach (var st in stations)
                {
                    if (st.LineStationIndex > station.LineStationIndex)
                    {
                        st.LineStationIndex--;
                        UpdateLineStation(st);
                    }
                }
                stations = RequestLineStationsInLine(lineId);
                if (station.LineStationIndex != 0 && station.LineStationIndex != stations.Count() - 1)
                {
                    var nextStation = stations.Where(x => x.LineStationIndex == station.LineStationIndex && x.StationId != stationId).First();
                    var prevStation = stations.Where(x => x.LineStationIndex == station.LineStationIndex - 1).First();
                    nextStation.PrevStation = station.PrevStation;
                    prevStation.NextStation = station.NextStation;
                    UpdateLineStation(nextStation);
                    UpdateLineStation(prevStation);
                    if (dl.RequestAdjacentStations(prevStation.StationId, nextStation.StationId) != null)
                        dl.UpdateAdjacentStations(new DO.AdjacentStations
                        {
                            isActive = true,
                            DistFromLastStation = distanceFromLastStation,
                            Station1 = prevStation.StationId,
                            Station2 = nextStation.StationId,
                            TimeSinceLastStation = timeSinceLastStation
                        });
                    else dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = distanceFromLastStation,
                        Station1 = prevStation.StationId,
                        Station2 = nextStation.StationId,
                        TimeSinceLastStation = timeSinceLastStation
                    });
                    dl.RemoveLineStation(station.StationId, station.LineId);


                }

                else
                {
                    if (station.LineStationIndex == 0)
                    {
                        curLine.FirstStation = station.NextStation;
                        var nextStation = RequestLineStation(station.NextStation, station.LineId);
                        nextStation.PrevStation = nextStation.StationId;
                        if (dl.RequestAdjacentStations(station.NextStation, station.NextStation) == null)
                        {
                            dl.CreateAdjacentStations(new DO.AdjacentStations
                            {
                                isActive = true,
                                DistFromLastStation = 0,
                                Station1 = station.NextStation,
                                Station2 = station.NextStation,
                                TimeSinceLastStation = TimeSpan.Zero
                            });
                        }
                        UpdateLineStation(nextStation);
                    }

                    if (station.LineStationIndex == stations.Count() - 1)
                    {
                        curLine.LastStation = station.PrevStation;
                        var prevStation = RequestLineStation(station.PrevStation, station.LineId);
                        prevStation.NextStation = prevStation.StationId;
                        if (dl.RequestAdjacentStations(station.PrevStation, station.PrevStation) == null)
                        {
                            dl.CreateAdjacentStations(new DO.AdjacentStations
                            {
                                isActive = true,
                                DistFromLastStation = stations.Count() - 2,
                                Station1 = station.PrevStation,
                                Station2 = station.PrevStation,
                                TimeSinceLastStation = TimeSpan.Zero
                            });
                        }
                        UpdateLineStation(prevStation);
                    }
                     dl.RemoveLineStation(station.StationId, station.LineId);
                    UpdateLine(curLine);


                }

            }
            catch (AggregateException) { }
            //       catch (DO.InvalidAdjacentStationIDException ex)
            //     {
            //       throw new BO.InvalidAdjacentLineIDException(ex.ID1, ex.ID2, ex.Message);
            //      }
           // catch (DO.InvalidLinesStationException ex)
            {
         //       throw new InvalidLinesStationException(ex.ID, ex.lineId, ex.Message);
            }
        }
        public IEnumerable<BO.Line> LinesInStation(int stationId)
        {
            return from line in dl.GetLinesInStation(stationId)
                   select line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;

        }
        public void UpdateStation(Station station)
        {
            try
            {
                dl.UpdateStation(station.CopyPropertiesToNew(typeof(DO.Station)) as DO.Station);
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                dl.UpdateAdjacentStations(new DO.AdjacentStations
                {
                    isActive = true,
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
        public IEnumerable<BO.Line> RequestAllLines()
        {
            return from line in dl.RequestAllLines()
                   orderby line.Code
                   select line.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;

        }
        public BO.Line RequestLine(int id)
        {
            try
            {
                return dl.RequestLine(id).CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void CreateLine(int code, BO.Areas area, int firstStation, int lastStation, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                dl.CreateLine(new DO.Line
                {
                    isActive = true,
                    Area = (DO.Areas)area,
                    Code = code,
                    FirstStation = firstStation,
                    LastStation = lastStation,
                    Id = Counters.lines++
                }) ;

                dl.CreateLineStation(new DO.LineStation
                {
                    isActive=true,
                    LineId = Counters.lines - 1,
                    LineStationIndex = 1,
                    NextStation = lastStation,
                    PrevStation = firstStation,
                    StationId = firstStation
                });

                dl.CreateLineStation(new DO.LineStation
                {
                    isActive = true,
                    LineId = Counters.lines - 1,
                    LineStationIndex = 0,
                    NextStation = lastStation,
                    PrevStation = firstStation,
                    StationId = lastStation
                });
                try
                {
                    dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = 0,
                        TimeSinceLastStation = new TimeSpan(0),
                        Station1 = firstStation,
                        Station2 = firstStation
                    }); ;
                }
                catch (DO.InvalidAdjacentStationIDException) { }
                try
                {
                    dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = distanceSinceLastStation,
                        TimeSinceLastStation = timeSinceLastStation,
                        Station1 = firstStation,
                        Station2 = lastStation
                    }); ;
                }
                catch (DO.InvalidAdjacentStationIDException) { }
                try
                {
                    dl.CreateAdjacentStations(new DO.AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = 0,
                        TimeSinceLastStation = new TimeSpan(0),
                        Station1 = lastStation,
                        Station2 = lastStation
                    }); ;
                }
                catch (DO.InvalidAdjacentStationIDException) { }
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidLinesStationException(firstStation, lastStation, ex.Message);
            }

        }
        public void RemoveLine(int id)
        {
            try
            {
                foreach (var station in RequestLineStationsInLine(id))
                    dl.RemoveLineStation(station.StationId, id);
                dl.RemoveLine(id);
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void UpdateLine(BO.Line line)
        {
            try
            {
                dl.UpdateLine(new DO.Line
                {
                    isActive = true,
                    Area = (DO.Areas)line.Area,
                    Code = line.Code,
                    FirstStation = line.FirstStation,
                    LastStation = line.LastStation,
                    Id = line.Id
                });

            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }

        }

        #endregion

        #region User
        public bool RequestUserPrivileges(string userName, string password)
        {
            try
            {
                return dl.RequestUserPrivileges(userName, password);
            }
            catch (DO.BadUsernameOrPasswordException ex)
            {
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message);
            }
        }
        public void CreateUser(string userName, string password, string passwordValidation)
        {
            if (password != passwordValidation)
                throw new BadUsernameOrPasswordException(userName, password, "Passwords aren't equal.");
            try
            {
                dl.CreateUser(new DO.User { isActive = true, Admin = false, Password = password, UserName = userName });
            }
            catch (DO.BadUsernameOrPasswordException ex)
            {
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message, ex);
            }
        }


        #endregion
    }
}

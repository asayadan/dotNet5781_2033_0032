using System;
using System.Collections.Generic;
using System.Linq;
using BLAPI;
using System.Threading;
using BO;
using DLAPI;

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
                return DeepCopyUtilities.CopyPropertiesToNew<DO.Bus>(dl.GetBus(licenseNum), typeof(BO.Bus)) as BO.Bus;
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
                dl.UpdateBus(DeepCopyUtilities.CopyPropertiesToNew<BO.Bus>(bus, typeof(DO.Bus)) as DO.Bus);
            }
            catch (DO.InvalidBusLicenseNumberException ex)
            {
                throw new InvalidBusLicenseNumberException(ex.LicenseNum, ex.Message);
            }
        }
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Bus>>(dl.GetAllBuses(),
                typeof(IEnumerable<BO.Bus>)) as IEnumerable<BO.Bus>;
        }
        public IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate)
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Bus>>
                (dl.GetBusBy(DeepCopyUtilities.CopyPropertiesToNew
                <Predicate<BO.Bus>>(predicate, typeof(Predicate<BO.Bus>)) as Predicate<DO.Bus>),
                typeof(IEnumerable<BO.Bus>)) as IEnumerable<BO.Bus>;
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
                return DeepCopyUtilities.CopyPropertiesToNew<DO.Station>(dl.GetStation(id),
                typeof(DO.Station)) as BO.Station;
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public BO.LineStation GetLineStation(int id)
        {
            try
            {
                return DeepCopyUtilities.CopyPropertiesToNew<DO.LineStation>(dl.GetLineStation(id),
                    typeof(DO.LineStation)) as BO.LineStation;
            }
            catch (DO.InvalidStationIDException ex)
            {
                throw new InvalidStationIDException(ex.ID, ex.Message);
            }
        }
        public IEnumerable<BO.LineStation> GetLineStationsInLine(int lineId)
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.LineStation>>(dl.GetLineStationsInLine(lineId),
                typeof(IEnumerable<BO.LineStation>)) as IEnumerable<BO.LineStation>;

        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Line>>(dl.GetAllLines(),
                typeof(IEnumerable<BO.Line>)) as IEnumerable<BO.Line>;
        }
        public void AddStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                var curStation = (dl.GetLineStationsInLine(lineId)).ToArray()[index];
                dl.AddLineStation(new DO.LineStation
                {
                    LineId = lineId,
                    Id = stationId,
                    NextStation = curStation.Id,
                    LineStationIndex = index,
                    PrevStation = curStation.NextStation
                });
            }
            catch (DO.InvalidLinesStationException ex) {
                throw new InvalidLinesStationException(ex.ID, ex.lineId, ex.Message);
            }
        }
        public void RemoveStationFromLine(int lineId, int stationId, double distanceFromLastStation, TimeSpan timeSinceLastStation)
        {
            try
            {
                dl.RemoveLineStation(stationId, lineId);
            }
            catch (DO.InvalidLinesStationException ex)
            {
                throw new InvalidLinesStationException(ex.ID, ex.lineId, ex.Message);
            }
        }
        public IEnumerable<BO.Line> LinesInStation(int stationId)
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Line>>(dl.LinesInStation(stationId),
                typeof(IEnumerable<BO.Line>)) as IEnumerable<BO.Line>;
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
                throw new InvalidAdjacentLineIDException(ex.ID1, ex.ID2, ex.Message);
            }
        }
        #endregion

        #region Line
        public BO.Line GetLine(int id)
        {
            try
            {
                return DeepCopyUtilities.CopyPropertiesToNew<DO.Line>(dl.GetLine(id),
                    typeof(DO.Line)) as BO.Line;
            }
            catch (DO.InvalidLineIDException ex)
            {
                throw new BO.InvalidLineIDException(ex.ID, ex.Message);
            }
        }
        public void AddLine(int id, BO.Areas area, int firstStation, int lastStation)
        {
            try
            {
                dl.AddLine(new DO.Line
                {
                    Area = (DO.Areas)area,
                    Code = id,
                    FirstStation = firstStation,
                    LastStation = lastStation,
                    Id = Counters.lines++
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

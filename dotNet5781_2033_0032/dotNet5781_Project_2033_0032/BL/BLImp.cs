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
        BO.Bus GetBus(int licenseNum)
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
        void AddBus(int licenseNum, DateTime fromTime)
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
        void DeleteBus(int licenseNum)
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
        void UpdateBus(BO.Bus bus)
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
        IEnumerable<BO.Bus> GetAllBuses()
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Bus>>(dl.GetAllBuses(),
                typeof(IEnumerable<BO.Bus>)) as IEnumerable<BO.Bus>;
        }
        IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate)
        {
            return DeepCopyUtilities.CopyPropertiesToNew<IEnumerable<DO.Bus>>
                (dl.GetBusBy(DeepCopyUtilities.CopyPropertiesToNew
                <Predicate<BO.Bus>>(predicate,typeof(Predicate<BO.Bus>)) as Predicate<DO.Bus>),
                typeof(IEnumerable<BO.Bus>)) as IEnumerable<BO.Bus>;
        }
        void FuelBus(int id)
        {
            var a = dl.GetBus(id);
            a.FuelRemaining = DO.Bus.FullGasTank;
            dl.UpdateBus(a);
        }
        void FixBus(int id)
        {
            var a = dl.GetBus(id);
            a.LastTreatment = DateTime.Now;
            dl.UpdateBus(a);
        }
        #endregion

        #region Stations
        BO.Station GetStation(int id);
        BO.LineStation GetLineStation(int id);
        IEnumerable<BO.LineStation> GetLineStationsInLine(int lineId);
        IEnumerable<BO.Line> GetAllLines();
        void AddStationToLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        void RemoveStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        IEnumerable<BO.Line> LinesInStation(int stationId);
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);

        #endregion

        #region Line
        BO.Line GetLine(int id);
        void AddLine(int id, BO.Areas area, int firstStation, int lastStation);
        void RemoveLine(int id);

        #endregion

        #region User
        bool GetUserPrivileges(string userName, string password)
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
        void CreateUser(string userName, string password, string passwordValidation)
        {
            if (password != passwordValidation)
                throw new BadUsernameOrPasswordException(userName, password, "Passwords aren't equal.");
            try
            {
                dl.CreateUser(new DO.User { Admin = false, Password = password, UserName = userName });
            }
            catch (DO.BadUsernameOrPasswordException ex)
            {
                throw new BadUsernameOrPasswordException(ex.Username, ex.Password, ex.Message);
            }
        }
        #endregion
    }
}

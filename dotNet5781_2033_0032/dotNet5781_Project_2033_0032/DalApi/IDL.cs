using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDL
    {
        #region Bus

        DO.Bus GetBus(int licenseNum);
        void CreateBus(int licenseNum, DateTime fromTime);
        void DeleteBus(int licenseNum);
        void UpdateBusDetails(DO.Bus bus);
        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<DO.Bus> GetBusBy(Predicate<DO.Bus> predicate);
        void FuelBus(int id);
        void FixBus(int id);
        #endregion

        #region Stations


        #endregion

        #region Line


        #endregion

        #region User


        #endregion



    }
}

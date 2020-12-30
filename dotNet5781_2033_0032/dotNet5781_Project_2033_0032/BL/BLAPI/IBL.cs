using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        BO.Bus GetBus(int licenseNumber);
        IEnumerable<BO.Bus> GetAllBuses();
        void UpdateBusDetails(BO.Bus bus);
        IEnumerable<BO.Bus> GetBusBy(Predicate<BO.Bus> predicate);
        void DeleteBus(int licenseNumber);
        #endregion

        #region Line
        #endregion

    }
}

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
        IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource


        }
        IEnumerable<DO.Bus> GetBusBy(Predicate<DO.Bus> predicate);
        DO.Bus GetBus(int licenseNum);
        void CreateBus(int licenseNum, DateTime fromTime);
        void DeleteBus(int licenseNum);
        void UpdateBus(DO.Bus bus);

        #endregion

        #region Stations


        #endregion

        #region Line
        DO.Line GetLine(int id);
        void AddLine(DO.Line line);
        void RemoveLine(int id);
        #endregion

        #region User
        DO.User GetUser(string username, string password);
        void CreateUser(DO.User user);

        #endregion







    }
}

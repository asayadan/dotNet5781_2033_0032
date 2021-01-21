using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using System.Xml;

namespace Dal
{
    class DalXml : IDL
    {
        #region singelton
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get; } = new DalXml();
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Bus
    {
        #region variables
        public int licensePlateNumber { get; set; }
        public DateTime registreationDate { get; set; }
        public float fuel { get; set; }
        public float mileage { get; set; }
        public float maileageInLastTreatment { get; set; }
        public DateTime timeOfLastTreatment { get; set; }
        public int whenWillBeReady { get; set; }
        public double start { get; set; }
        public Status status { get; set; }

         static int FULL_GAS_TANK = 1200;//const the size of full gas tank
        #endregion

    }
}

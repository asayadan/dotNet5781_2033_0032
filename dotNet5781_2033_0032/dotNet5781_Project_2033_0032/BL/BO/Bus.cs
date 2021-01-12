using System;

namespace BO
{
    public class Bus
    {
        #region variables
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime LastTreatment { get; set; }
        public float FuelRemaining { get; set; }
        public float TotalTrip { get; set; }
        public Status Status { get; set; }

        static int FULL_GAS_TANK = 1200;//const the size of full gas tank
        #endregion
    }
}

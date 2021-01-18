using System;

namespace DO
{
    public class Bus
    {
        #region variables
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime LastTreatment { get; set; }
        public double FuelRemaining { get; set; }
        public double TotalTrip { get; set; }
        public Status Status { get; set; }

        public const int FullGasTank = 1200;//const the size of full gas tank
        #endregion
    }
}

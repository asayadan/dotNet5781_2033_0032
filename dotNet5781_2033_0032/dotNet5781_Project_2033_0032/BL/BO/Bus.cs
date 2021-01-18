using System;

namespace BO
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
        #endregion
    }
}

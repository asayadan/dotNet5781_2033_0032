using System;

namespace DO
{
    public class AdjacentStations
    {
        public bool isActive;
        public int Station1 { get; set; }
        public int Station2 { get; set; }
        public double DistFromLastStation { get; set; }
        public TimeSpan TimeSinceLastStation { get; set; }
    }
}

﻿using System;



namespace BO
{
    public class AdjacentStations
    {

        public int Station1 { get; set; }
        public int Station2 { get; set; }
        public double DistFromLastStation { get; set; }
        public TimeSpan TimeSinceLastStation { get; set; }


        //public override string ToString()
        //{
        //    return base.ToString()+"    "+ time_str();
        //}
    }
}
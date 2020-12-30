using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



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
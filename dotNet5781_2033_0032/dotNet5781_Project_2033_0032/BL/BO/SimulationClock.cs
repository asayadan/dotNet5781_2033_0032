using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public sealed class SimulationClock
    {
        public static event EventHandler valueChanged = delegate { };
        private static TimeSpan myTimeSpan;
        public static TimeSpan GetTime {
            set { myTimeSpan = value; valueChanged(myTimeSpan, new EventArgs()); }
            get { return myTimeSpan; } }

        static SimulationClock() { }

    }
}

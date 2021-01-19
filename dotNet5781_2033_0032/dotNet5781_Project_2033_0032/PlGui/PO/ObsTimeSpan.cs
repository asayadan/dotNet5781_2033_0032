using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlGui.PO
{
    class ObsTimeSpan : IObservable<TimeSpan>
    {
        public IDisposable Subscribe(IObserver<TimeSpan> observer)
        {
            throw new NotImplementedException();
        }
    }
}

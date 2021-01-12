using System.Collections.Generic;

namespace dotNet5781_03B_2033_0032
{

    public class comparefuel : IComparer<Bus>
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<Bus>.Compare(Bus x, Bus y)
        {
            return x._fuel.CompareTo(y._fuel);
        }
    }

    public class compareDate : IComparer<Bus>
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<Bus>.Compare(Bus x, Bus y)
        {
            return (int)x._registreationDate.CompareTo(y._registreationDate);
        }
    }

    public class CompareMileage : IComparer<Bus>
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<Bus>.Compare(Bus x, Bus y)
        {
            return x._mileage.CompareTo(y._mileage);
        }
    }

    public class CompareTimeTreatment : IComparer<Bus>
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<Bus>.Compare(Bus x, Bus y)
        {
            return x.LastTreatment.CompareTo(y.LastTreatment);
        }
    }
}

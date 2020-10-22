using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusClass;

namespace dotNet5781_01_2033_0032
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date1 = new DateTime(2008, 5, 1, 7, 0, 0);
            Bus fgdfgdfg = new Bus(1234568, date1);
            fgdfgdfg.Print_licensePlateNumber();
            Console.WriteLine(date1.ToString());
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_2033_0032
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2033();
            Welcome0032();
            Console.ReadKey();

        }
        private static void Welcome0032();
        private static void Welcome2033()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", name);
        }
    }
}

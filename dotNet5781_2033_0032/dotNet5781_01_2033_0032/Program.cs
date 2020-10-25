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
            List<Bus> buses = new List<Bus>();
            char ch;
            bool flag = true;
            while (flag)
	        {
                Console.WriteLine("Choose an option:\na. Add a bus.\nb. Choose a bus for a ride.\nc.Fuel or fix a bus.\nd. Print mileage of every bus. \ne. Exit.");
                char.TryParse(Console.ReadLine(), out ch);
                switch (ch)
	            {
                    case 'a':
                        DateTime date = new DateTime();
                        int licensePlateNumber;
                        Console.WriteLine("Enter a date in the format (YYYY-MM-DD):");
                        DateTime.TryParse(Console.ReadLine(), out date);
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        if (checkValidation(licensePlateNumber, date))
                            buses.Add(new Bus(licensePlateNumber, date));
                        break;
                    case 'b':
                        int licensePlateNumber, randNum;
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        randNum = rnd.Next();
                        
                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e':
                        flag = false;
                        break;
		            default:
                        Console.WriteLine("Option not found.");
                        break;
	            }
	        }

            DateTime date1 = new DateTime(2008, 5, 1, 7, 0, 0);
            Bus fgdfgdfg = new Bus(1234568, date1);
            fgdfgdfg.Print_licensePlateNumber();
            Console.WriteLine(fgdfgdfg._mileage.ToString());
            Console.ReadKey();
        }

        public static bool checkValidation(int _licensePlateNumber, DateTime _date) {
            if ((_date.Year >= 2018) && (_licensePlateNumber > 9999999) ||
                (_date.Year < 2018) && (_licensePlateNumber <= 9999999))
                return true;
            else {
                Console.WriteLine("error: can't have this license Plate Number");
                return false;
            }
        }
    }
}

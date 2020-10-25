using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusClass;

//enum
enum states
{
    Add_Bus,
    ride,
    fuel_or_fix,
    print_mileage,
    exit
}

namespace dotNet5781_01_2033_0032
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            int i;
            bool flag = true;
            while (flag)
	        {
                Console.WriteLine("Choose an option:\n0. Add a bus.\n1. Choose a bus for a ride.\n2.Fuel or fix a bus.\n3. Print mileage of every bus. \n4. Exit.");
                int.TryParse(Console.ReadLine(), out i);
                switch (i)
	            {
                    case (int)states.Add_Bus:
                        DateTime date = new DateTime();
                        int licensePlateNumber;
                        Console.WriteLine("Enter a date in the format (YYYY-MM-DD):");
                        DateTime.TryParse(Console.ReadLine(), out date);
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        if (checkValidation(licensePlateNumber, date))
                            buses.Add(new Bus(licensePlateNumber, date));
                        break;
                    case (int)states.ride:
                        int randNum;
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        randNum = rnd.Next();

                        
                        break;
                    case (int)states.fuel_or_fix:
                        break;
                    case (int)states.print_mileage:
                        break;
                    case (int)states.exit:
                        flag = false;
                        break;
		            default:
                        Console.WriteLine("Option not found.");
                        break;
	            }
	        }


        }

        //checks if the input is valid
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

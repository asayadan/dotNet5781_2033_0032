using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusClass;

//enum
enum states
{
    addBus,
    ride,
    fuelOrTreatment,
    printMileage,
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
                Console.WriteLine("\nChoose an option:\n0. Add a bus.\n1. Choose a bus for a ride.\n2. Fuel or Treat a bus.\n3. Print mileage of every bus. \n4. Exit.");
                int.TryParse(Console.ReadLine(), out i);
                switch (i)
	            {
                    case (int)states.addBus:
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
                        int randKM;
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        randKM = rnd.Next(2400);

                        Bus bus = buses.Find(x => x.licensePlate.Equals(licensePlateNumber));
                        if (bus == null)
                            Console.WriteLine("Bus not found!");
                        else bus.rideKM(randKM);
                        break;

                    case (int)states.fuelOrTreatment:
                        int option;
                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);
                        Console.WriteLine("Choose an option:\n1. Fuel\n2. Treat");
                        int.TryParse(Console.ReadLine(), out option);
                        Bus myBus = buses.Find(x => x.licensePlate.Equals(licensePlateNumber));
                        if (myBus != null)
                            switch (option)
	                        {
                                case 1:
                                    myBus.refuel();
                                    break;
                                case 2:
                                    myBus.treatment(DateTime.Now);
                                    break;
		                        default:
                                    Console.WriteLine("Option not found.");
                                    break;
	                        }
                        else
                            Console.WriteLine("Bus not found.");
                        break;

                    case (int)states.printMileage:
                        foreach (Bus aBus in buses) {
                            aBus.Print_licensePlateNumber();
                            Console.Write(" | {0}", aBus.mileageSinceTreatment);
                        }
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

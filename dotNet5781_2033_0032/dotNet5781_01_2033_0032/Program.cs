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
                    case (int)states.addBus://adding new bus

                        DateTime date = new DateTime();
                        int licensePlateNumber;

                        Console.WriteLine("Enter a date in the format (YYYY-MM-DD):");
                        DateTime.TryParse(Console.ReadLine(), out date);//we get the date of registration

                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);//we get the license plate number for the new bus as an integer

                        if (checkValidation(licensePlateNumber, date) && (buses.Count == 0 || buses.Find(x => x.licensePlate.Equals(licensePlateNumber)) == null))//if we can addd this bus i.e. the bus doesn't exists already and the license plate number and the year fit 
                            buses.Add(new Bus(licensePlateNumber, date));//adding  the new bus

                        else Console.WriteLine("Invalid license plate number.");//if we can't add this bus

                        break;

                    case (int)states.ride://we make a new ride

                        int randKM;

                        Console.WriteLine("Enter license plate number(without hyphen):");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);//we get the license Plate Number that will drive

                        Random rnd = new Random(DateTime.Now.Millisecond);
                        randKM = rnd.Next(Bus.FULL_GAS_TANK);//we get random number of km for this ride(with max km of the capacity of the gas tank so it wont take to much to find a number that will actualy enable us to drive)

                        Bus bus = buses.Find(x => x.licensePlate.Equals(licensePlateNumber));//we get the bus we want to drive
                        if (bus == null)
                            Console.WriteLine("Bus not found!");//this bus is not in the system

                        else bus.rideKM(randKM);//we make the ride
                        break;

                    case (int)states.fuelOrTreatment://we are refuling or treating the bus

                        int option;

                        Console.WriteLine("Enter license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlateNumber);//we get the number of the bus we want

                        Console.WriteLine("Choose an option:\n1. Fuel\n2. Treat");
                        int.TryParse(Console.ReadLine(), out option);//the user selects betwen refuling and and treating the bus

                        Bus myBus = buses.Find(x => x.licensePlate.Equals(licensePlateNumber));//we find the requested bus


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
                            Console.WriteLine("Bus not found.");//this bus is not in the system
                        break;

                    case (int)states.printMileage:


                        foreach (Bus aBus in buses) {//for each bus
                            aBus.Print_licensePlateNumber();//we print the license palte number
                            Console.WriteLine(" | {0}", aBus._mileage);//we print the mileage
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

        //checks if the license plate number is valid
        public static bool checkValidation(int _licensePlateNumber, DateTime _date) {
            if (((_date.Year >= 2018) && (_licensePlateNumber > 9999999))&& (_licensePlateNumber <= 99999999) ||//the bus registered after 2018 and has 8 digits
                ((_date.Year < 2018) && (_licensePlateNumber <= 9999999))&& (_licensePlateNumber > 999999))//the bus registered before 2018 and has 7 digits
                return true;//the license plate number is valid
            else {
                return false;
            }
        }
    }
}

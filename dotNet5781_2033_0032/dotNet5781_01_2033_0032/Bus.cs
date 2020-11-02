using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace BusClass
{
    class Bus
    {
        #region variables
        private int licensePlateNumber;
        private DateTime registreationDate;
        private float fuel;
        private float mileage;
        private float maileageInLastTreatment;
        private DateTime timeOfLastTreatment;

        public static int FULL_GAS_TANK = 1200;//const the size of full gas tank
        #endregion


        #region ctor
        /*
            * Constructor.
            * checkes  if the input is valid 
            * and Initializes the date and the license plate number

       */
        public Bus(int _licensePlateNumber, DateTime _date)
        {   
                licensePlateNumber = _licensePlateNumber;
                registreationDate = _date;
                timeOfLastTreatment = _date;
                mileage = 0;
                maileageInLastTreatment = 0;
                fuel = FULL_GAS_TANK;//we assume that every bus starts with full gas tank
            
        }
        #endregion


        #region properties
        public int licensePlate//returns the license plate number
        {get { return licensePlateNumber; }}

        public float _mileage//returns the number of km the bus drove untile now
        { get { return mileage; }}

        public float _fuel//returns the number of km the bus can ride without Refueling
        { get { return fuel; } }

        public float mileageSinceTreatment//returns the mileage since last treatment
        { get { return mileage - maileageInLastTreatment; } }

        //add ride  getting the number of km to ride
        public void rideKM(int value)
        {
            if (mileageSinceTreatment > 20000 || _fuel < value)//checking if the bus can do this ride
                Console.WriteLine("This bus can't be used!");
            else {
                mileage += value;//adding the number of km who were being traveled to the mileage
                fuel -= value;//Subtracting  the number of km who were being traveled from the amount we can ride without Refueling 
                Console.WriteLine("Ride finished succesfuly!");
            }
        }

        public void refuel()//we set the number of km the bus can ride without Refueling to the maximum value
        {
            fuel =FULL_GAS_TANK;
        }



        public void treatment(DateTime _date)
        {
            maileageInLastTreatment = mileage;
            timeOfLastTreatment = _date;
        }


        //we print the license plate number on the right format
        public void Print_licensePlateNumber()
        {
            if (registreationDate.Year>=2018)//the number has 8 digits
            {
                Console.Write((licensePlateNumber/100000).ToString()+"-");//first three numbers and hyphen
                Console.Write(((licensePlateNumber / 1000)%100).ToString() + "-");// the two middle numbers numbers and hyphen
                Console.Write((licensePlateNumber % 1000).ToString());//the last three numbers

            }
            else  //the number has 7 digits (the license plate number is valid)
            {
                Console.Write((licensePlateNumber / 100000).ToString() + "-");//first two numbers and hyphen
                Console.Write(((licensePlateNumber / 100) % 1000).ToString() + "-");// the three middle numbers numbers and hyphen
                Console.Write((licensePlateNumber % 100).ToString());//the last two numbers

            }
        }

    }
    #endregion
}

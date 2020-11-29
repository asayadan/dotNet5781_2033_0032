using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public enum Status
{
    ready, working, refueling, fixing
};

namespace dotNet5781_03B_2033_0032
{
    public class Bus
    {
        #region variables
        private int licensePlateNumber;
        private DateTime registreationDate;
        private float fuel;
        private float mileage;
        private float maileageInLastTreatment;
        private DateTime timeOfLastTreatment;
        public double whenWillBeReady;
        public double start;
        private Status status;
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
            status = Status.ready;
            whenWillBeReady = 0;

        }

        public Bus(int _licensePlateNumber, DateTime _date, DateTime _lastTreatment, float _mileage, float _maileageInLastTreatment, float _fuel)
        {
            if (_date<=_lastTreatment&&_mileage>=0&&_mileage>=_maileageInLastTreatment&&_fuel<=FULL_GAS_TANK&&_fuel>=0)
            {
                licensePlateNumber = _licensePlateNumber;
                registreationDate = _date;
                timeOfLastTreatment = _lastTreatment;
                mileage = _mileage;
                maileageInLastTreatment = _maileageInLastTreatment;
                fuel = _fuel;
                status = Status.ready;
                whenWillBeReady = 0;
            }
            else throw new ArgumentException("invalid arguments");

        }
        #endregion


        #region properties
        public DateTime _registreationDate//returns the license plate number
        {
            get { return registreationDate; }
            set { registreationDate = value; }
        }

        public int licensePlate//returns the license plate number
        { get { return licensePlateNumber; } }

        public DateTime LastTreatment//returns the license plate number
        {
            get { return timeOfLastTreatment; }
            set { timeOfLastTreatment = value; }
        }
        public Status curStatus//returns the license plate number
        {
            get { return status; }
            set { status = value; }
        }
        public float _mileage//returns the number of km the bus drove untile now
        { get { return mileage; } }

        public float _fuel//returns the number of km the bus can ride without Refueling
        { get { return fuel; } }

        public float mileageSinceTreatment//returns the mileage since last treatment
        { get { return mileage - maileageInLastTreatment; } }

        //add ride  getting the number of km to ride
        public void rideKM(int value)
        {
            if (status == Status.ready)
            {
                if (mileageSinceTreatment > 20000 || _fuel < value || DateTime.Now > timeOfLastTreatment.AddYears(1))//checking if the bus can do this ride                                                                           //Console.WriteLine("This bus can't be used!");
                    throw new ArgumentException("you need to refule of to treat the bus");
                else
                {
                    mileage += value;//adding the number of km who were being traveled to the mileage
                    fuel -= value;//Subtracting  the number of km who were being traveled from the amount we can ride without Refueling 
                                  //Console.WriteLine("Ride finished succesfuly!");
                }
            }
        }

        public void refuel()//we set the number of km the bus can ride without Refueling to the maximum value
        {
            fuel = FULL_GAS_TANK;
        }



        public void treatment(DateTime _date)
        {
            maileageInLastTreatment = mileage;
            timeOfLastTreatment = _date;
        }


        //we print the license plate number on the right format
        public string t_licensePlateNumber
        {
            get
            {
                string help = "";
                if (registreationDate.Year >= 2018)//the number has 8 digits
                {
                    help += (((licensePlateNumber / 100000) / 100).ToString() + (((licensePlateNumber / 100000) / 10) % 10).ToString() + ((licensePlateNumber / 100000) % 10).ToString() + "-");//first three numbers and hyphen
                    help += ((((licensePlateNumber / 1000) % 100) / 10).ToString() + (((licensePlateNumber / 1000) % 100) % 10).ToString() + "-");// the two middle numbers numbers and hyphen
                    help += ((licensePlateNumber % 1000).ToString());//the last three numbers

                }
                else  //the number has 7 digits (the license plate number is valid)
                {
                    help += ((licensePlateNumber / 100000).ToString() + "-");//first two numbers and hyphen
                    help += (((licensePlateNumber / 100) % 1000).ToString() + "-");// the three middle numbers numbers and hyphen
                    help += ((licensePlateNumber % 100).ToString());//the last two numbers

                }
                return help;
            }
        }
        public void Print_licensePlateNumber()
        {
            Console.WriteLine(t_licensePlateNumber);
        }

        public void Event(Status _status, float dist = 0)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            if (_status == Status.fixing)
                start = 60 * 60 * 24;
            else if (_status == Status.refueling)
                start = 60 * 60 * 2;
            else if (_status == Status.working)
            {
                if (dist > 0)
                {
                    start = dist/ rnd.Next(20, 50) * 60 * 60;
                }
                else throw new ArgumentException("you can't drive a negative number of kilometers");
            }
            curStatus = _status;
            whenWillBeReady = start;
        }
        #endregion

    }
}
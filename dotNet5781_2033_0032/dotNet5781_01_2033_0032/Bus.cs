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
        private DateTime inceptionDate;
        private float fuel;
        private float mileage;
        private float maileageInLastTreatment;
        private DateTime timeOfLastTreatment;

        static int FULL_GAS_TANK = 1200;
        #endregion



        /*
            * Constructor.
            * checkes  if the input is valid 
            * and Initializes the date and the license plate number

       */
        public Bus(int _licensePlateNumber, DateTime _date)
        {   
                licensePlateNumber = _licensePlateNumber;
                inceptionDate = _date;
                timeOfLastTreatment = _date;
                mileage = 0;
                maileageInLastTreatment = 0;
                fuel = FULL_GAS_TANK;//i assume that every bus starts with full gas tank
            
        }


        public int licensePlate
        {get { return licensePlateNumber; }}

        public float _mileage
        { get { return mileage; }}

        public float _fuel
        { get { return fuel; } }


        public float rideKM
        {
            set
            {
                mileage += value;
                fuel -= value;
            }
        }

        public void refuel()
        {
            fuel = 1200;
        }

        public void treatment(DateTime _date)
        {
            maileageInLastTreatment = mileage;
            timeOfLastTreatment = _date;
        }



        public void Print_licensePlateNumber()
        {
            if (inceptionDate.Year>=2018)
            {
                Console.Write((licensePlateNumber/100000).ToString()+"-");
                Console.Write(((licensePlateNumber / 1000)%100).ToString() + "-");
                Console.WriteLine((licensePlateNumber % 1000).ToString());

            }
            else   
            {
                Console.Write((licensePlateNumber / 100000).ToString() + "-");
                Console.Write(((licensePlateNumber / 100) % 1000).ToString() + "-");
                Console.WriteLine((licensePlateNumber % 100).ToString());

            }
        }

    }
}

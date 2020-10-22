﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace BusClass
{
    class Bus
    {
        private int licensePlateNumber;
        private DateTime inceptionDate;
        private float fuel;
        private float mileage;
        private float maileageInLastRepair;
        private DateTime timeOfRefuel;

        static int FULL_GAS_TANK = 1200;
        /*
            * Constructor.
            * checkes  if the input is valid 
            * and Initializes the date and the license plate number

       */
        public Bus(int _licensePlateNumber, DateTime _date)
        {

            if ((_date.Year >= 2018) && (_licensePlateNumber > 9999999) ||
                (_date.Year < 2018) && (_licensePlateNumber <= 9999999))
            {
                licensePlateNumber = _licensePlateNumber;
                inceptionDate = _date;
                timeOfRefuel = _date;
                mileage = 0;
                maileageInLastRepair = 0;
                fuel =FULL_GAS_TANK;//i assume that every bus starts with full gas tank
            }
            else
            {
                Console.WriteLine("error: can't have this license Plate Number");
                return;
            }
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

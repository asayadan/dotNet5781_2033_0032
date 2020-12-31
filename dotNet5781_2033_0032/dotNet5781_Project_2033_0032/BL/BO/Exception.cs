﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    [Serializable]
    public class BadUsernameOrPasswordException : Exception
    {
        string Username;
        string Password;
        public BadUsernameOrPasswordException(string username, string password) : base() { Username = username; Password = password; }
        public BadUsernameOrPasswordException(string username, string password, string message) : base(message) { Username = username; Password = password; }
        public BadUsernameOrPasswordException(string username, string password, string message, Exception inner) : base(message, inner) { Username = username; Password = password; }
    }

    [Serializable]
    public class InvalidBusLicenseNumberException : Exception
    {
        public int LicenseNum;
        public InvalidBusLicenseNumberException(int licenseNum) : base() { LicenseNum = licenseNum; }
        public InvalidBusLicenseNumberException(int licenseNum, string message) : base(message) { LicenseNum = licenseNum; }
        public InvalidBusLicenseNumberException(int licenseNum, string message, Exception inner) : base(message, inner) { LicenseNum = licenseNum; }
        public override string ToString() => base.ToString() + "bad bus license number:" + LicenseNum.ToString();
    }

    [Serializable]
    public class InvalidStationIDException : Exception
    {
        public int ID;
        public InvalidStationIDException(int id) : base() { ID = id; }
        public InvalidStationIDException(int id, string message) : base(message) { ID = id; }
        public InvalidStationIDException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        public override string ToString() => base.ToString() + "bad station ID:" + ID.ToString();
    }

    public class InvalidStationLineIDException : Exception
    {
        public int ID1;
        public int ID2;
        public InvalidStationLineIDException(int id1, int id2) : base() { ID1 = id1; ID2 = id2; }
        public InvalidStationLineIDException(int id1, int id2, string message) : base(message) { ID1 = id1; ID2 = id2; }
        public InvalidStationLineIDException(int id1, int id2, string message, Exception inner) : base(message, inner) { ID1 = id1; ID2 = id2; }
        public override string ToString() => base.ToString() + "bad station ID:" + ID1.ToString() + " or " + ID2.ToString();
    }
}
}

using System;

namespace DO
{
    [Serializable]
    public class BadUsernameOrPasswordException : Exception
    {
        public string Username;
        public string Password;
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
    [Serializable]
    public class InvalidAdjacentStationIDException : Exception
    {
        public int ID1;
        public int ID2;
        public InvalidAdjacentStationIDException(int id1, int id2) : base() { ID1 = id1; ID2 = id2; }
        public InvalidAdjacentStationIDException(int id1, int id2, string message) : base(message) { ID1 = id1; ID2 = id2; }
        public InvalidAdjacentStationIDException(int id1, int id2, string message, Exception inner) : base(message, inner) { ID1 = id1; ID2 = id2; }
        public override string ToString() => base.ToString() + "bad station ID:" + ID1.ToString() + " or " + ID2.ToString();
    }

    [Serializable]
    public class InvalidLinesStationException : Exception
    {
        public int ID;
        public int lineId;
        public InvalidLinesStationException(int id1, int id2) : base() { ID = id1; lineId = id2; }
        public InvalidLinesStationException(int id1, int id2, string message) : base(message) { ID = id1; lineId = id2; }
        public InvalidLinesStationException(int id1, int id2, string message, Exception inner) : base(message, inner) { ID = id1; lineId = id2; }
        public override string ToString() => base.ToString() + "the station " + ID.ToString() + "isn't in line" + lineId.ToString();
    }
    [Serializable]
    public class InvalidLineIDException : Exception
    {
        public int ID;
        public InvalidLineIDException(int id) : base() { ID = id; }
        public InvalidLineIDException(int id, string message) : base(message) { ID = id; }
        public InvalidLineIDException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        public override string ToString() => base.ToString() + "bad sline ID:" + ID.ToString();
    }

    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

    [Serializable]
    public class XMLFileFormatException : Exception
    {
        public string xmlFilePath;
        public XMLFileFormatException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileFormatException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileFormatException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to  xml file: {xmlFilePath}";
    }

}

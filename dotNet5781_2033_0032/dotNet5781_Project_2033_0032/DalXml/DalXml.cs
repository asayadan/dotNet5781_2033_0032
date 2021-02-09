using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using System.Xml;
using System.Xml.Linq;


namespace DL
{
    class DalXml : IDL
    {
        #region singelton
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get; } = new DalXml();
        #endregion
        string AdjacentStationsPath = "";
        string BusPath="";
        string LinePath = "";
        string UserPath = "";


        #region station
        /// <summary>
        /// we add to the memory ne AdjacentStations if it doesn't exist already
        /// </summary>
        /// <param name="adjacentStations">the stations we want to add</param>
        public void CreateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
           
            AdjacentStations thisStaions = (from stations in AdjacentStationsRootElem.Elements()
                                                 where stations.Equal(adjacentStations.Station1, adjacentStations.Station2)
                                                 select stations.ToAdjecentStation()
                                            ).FirstOrDefault();

            if (adjacentStations != null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2, "we already have this AdjecentStations");

            AdjacentStationsRootElem.Add(adjacentStations.ToXElement());

            XMLTools.SaveListToXMLElement(AdjacentStationsRootElem,AdjacentStationsPath);

        }
       /// <summary>
       /// searches for an AdjacentStations by stations ID
       /// </summary>
       /// <param name="station1"></param>
       /// <param name="station2"></param>
       /// <returns>the AdjacentStations instance as it appear in the memory</returns>
        public AdjacentStations RequestAdjacentStations(int station1, int station2)
        {
            try
            {

                XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

                AdjacentStations adjacentStations = (from stations in AdjacentStationsRootElem.Elements()
                                 where stations.Equal(station1,station2)
                                 select stations.ToAdjecentStation()
                                 ).FirstOrDefault();

                if (adjacentStations == null)
                    throw new DO.InvalidAdjacentStationIDException(station1, station2);

                return adjacentStations;
            }
            catch (FormatException ex)
            {
                throw new XMLFileFormatException(AdjacentStationsPath, "unexpected format error occurred", ex);
            }
        }

        public void RemoveAdjacentStations(AdjacentStations adjacentStatons)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            XElement helptations = (from stations in AdjacentStationsRootElem.Elements()
                            where stations.Equal(adjacentStatons.Station1,adjacentStatons.Station2)
                                         select stations).FirstOrDefault();

            if (helptations != null)
            {
                helptations.Remove(); //<==>   Remove stations from the XML file

                XMLTools.SaveListToXMLElement(AdjacentStationsRootElem, AdjacentStationsPath);
            }
            else
                throw new DO.InvalidAdjacentStationIDException(adjacentStatons.Station1, adjacentStatons.Station2);
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            XElement helptations = (from stations in AdjacentStationsRootElem.Elements()
                                         where stations.Equal(adjacentStations.Station1, adjacentStations.Station2)
                                         select stations).FirstOrDefault();

            if (helptations != null)
            {
                helptations = adjacentStations.ToXElement();

                XMLTools.SaveListToXMLElement(AdjacentStationsRootElem, AdjacentStationsPath);
            }
            else
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2);
        }

        #endregion

        #region line
        public void CreateAllLine(List<Line> listLines)
        {
                XMLTools.SaveListToXMLSerializer(listLines, LinePath);
        }
        public void CreateLine(Line Newline)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            Line helpLine = (from line in ListLines
                             where line.Id == Newline.Id
                             select line).FirstOrDefault();
            if (helpLine != null)
                throw new DO.InvalidLineIDException(Newline.Id, "this line id exists in our database");
            else
            {
                ListLines.Add(Newline);
                XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
            }
        }

        public IEnumerable<Line> RequestAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            return from line in ListLines
                      select line;
        }

        public Line RequestLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            Line helpLine = (from line in ListLines
                   where line.Id == id
                   select line).FirstOrDefault();
            if (helpLine != null)
                return helpLine; //no need to Clone()

            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }
        public void RemoveLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            Line helpLine = (from line in ListLines
                             where line.Id == id
                             select line).FirstOrDefault();
            if (helpLine != null)
            {
                ListLines.Remove(helpLine);
                XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
            }
            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }
        #endregion

        #region bus
        public void CreateAllBusses(List<Bus> listBusses)
        {
            XMLTools.SaveListToXMLSerializer(listBusses, BusPath);
        }

        public void CreateBus(Bus NewBus)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            Bus thisSus = (from bus in BusRootElem.Elements()
                           where bus.IsActive()&&bus.Equal(NewBus.LicenseNum)
                           select bus.ToBus()).FirstOrDefault();

            if (thisSus != null)
                throw new DO.InvalidBusLicenseNumberException(NewBus.LicenseNum, "we already have this bus");

            BusRootElem.Add(thisSus.ToXElement());

            XMLTools.SaveListToXMLElement(BusRootElem, BusPath);
        }


        public void DeleteBus(int licenseNum)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);


            Bus thisBus = (from bus in BusRootElem.Elements()
                           where bus.IsActive() && bus.Equal(licenseNum)
                           select bus.ToBus()).FirstOrDefault();

            if (thisBus != null)
            {
                BusRootElem.Remove(); //<==>   Remove stations from the XML file

                XMLTools.SaveListToXMLElement(BusRootElem, BusPath);
            }
            else
                throw new DO.InvalidBusLicenseNumberException(licenseNum, "we don't have this bus");
        }

        public Bus RequestBus(int licenseNum)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            Bus thisBus = (from bus in BusRootElem.Elements()
                           where bus.IsActive() && bus.Equal(licenseNum)
                           select bus.ToBus()).FirstOrDefault();

            if (thisBus != null)
            {
                return thisBus;
            }
            else
                throw new DO.InvalidBusLicenseNumberException(licenseNum, "we don't have this bus");

        }
            public IEnumerable<Bus> RequestAllBuses()
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);
            return (from bus in BusRootElem.Elements()
                    where bus.IsActive()
                    select bus.ToBus());
       }

        public void UpdateBus(Bus Newbus)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement helpBus = (from bus in BusRootElem.Elements()
                           where bus.IsActive() && bus.Equal(Newbus.LicenseNum)
                           select bus).FirstOrDefault();

            if (helpBus != null)
            {
                helpBus = Newbus.ToXElement();

                XMLTools.SaveListToXMLElement(BusRootElem, BusPath);
            }
            else
                throw new DO.InvalidBusLicenseNumberException(Newbus.LicenseNum, "we don't have this bus");
        }
        #endregion

        #region user

        public void CreateAllUsers(List<User> listUsers)
        {
            XMLTools.SaveListToXMLSerializer(listUsers, UserPath);
        }
        public void CreateUser(User NewUser)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            User helpUser = (from user in ListUsers
                             where user.isActive&&user.UserName == NewUser.UserName
                             select user).FirstOrDefault();
            if (helpUser != null)
                throw new DO.BadUsernameOrPasswordException(NewUser.UserName,NewUser.Password, "this username id doesn't exists");
            else
            {
                ListUsers.Add(helpUser);
                XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
            }
        }

        public bool RequestUserPrivileges(string username, string password)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User helpUser = (from user in ListUsers
                               where user.isActive&&user.UserName == username && user.Password == password
                                select user).FirstOrDefault();
            if (helpUser == null)
                throw new DO.BadUsernameOrPasswordException(username, password, "the password and username doesn't match");
            return helpUser.Admin;
        }
        #endregion

        #region Trip
        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            throw new NotImplementedException();
        }
        #endregion
        public void CreateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void CreateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> RequestAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> RequestBusBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public LineStation RequestLineStation(int stationId, int lineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> RequestLineStationsInLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public Station RequestStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> RequestStationBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetLinesInStation(int stationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveLineStation(int stationId, int lineId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }
    }
}

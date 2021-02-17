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
    public class DalXml : IDL
    {
        #region singelton
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get; } = new DalXml();
        #endregion
        string AdjacentStationsPath = @"AdjacentStationsXml.xml";//XElement
        string BusPath = @"BusXml.xml";//XElement
        string LinePath = @"LineXml.xml";//
        string UserPath = @"UserhXml.xml";//
        string LineTripPath = @"LineTripXml.xml";//XElement
        string TripPath = @"TripXml.xml";//XElement
        string StationPath = @"StationXml.xml";//
        string LineStationsPath = @"LineStationsXml.xml";//
        string CounterPath = @"CounterXML.xml";

        #region AdjacentStations
        /// <summary>
        /// we add to the memory ne AdjacentStations if it doesn't exist already
        /// </summary>
        /// <param name="adjacentStations">the stations we want to add</param>
        public void CreateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            AdjacentStations thisStaions = (from stations in AdjacentStationsRootElem.Elements()
                                            where stations.Equal(adjacentStations.Station1, adjacentStations.Station2) && stations.IsActive()
                                            select stations.ToAdjecentStation()
                                            ).FirstOrDefault();

            if (thisStaions != null)
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2, "we already have this AdjecentStations");

            AdjacentStationsRootElem.Add(adjacentStations.ToXElement());

            XMLTools.SaveListToXMLElement(AdjacentStationsRootElem, AdjacentStationsPath);

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
                                                     where stations.Equal(station1, station2) && stations.IsActive()
                                                     select stations.ToAdjecentStation()
                                 ).FirstOrDefault();

                return adjacentStations;
            }
            catch (FormatException ex)
            {
                throw new XMLFileFormatException(AdjacentStationsPath, "unexpected format error occurred", ex);
            }
        }

        public void DeleteAdjacentStations(AdjacentStations adjacentStatons)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            XElement helptations = (from stations in AdjacentStationsRootElem.Elements()
                                    where stations.Equal(adjacentStatons.Station1, adjacentStatons.Station2) && stations.IsActive()
                                    select stations).FirstOrDefault();

            if (helptations != null)
            {
                helptations.Element("isActive").Value = false.ToString();

                XMLTools.SaveListToXMLElement(AdjacentStationsRootElem, AdjacentStationsPath);
            }
            else
                throw new DO.InvalidAdjacentStationIDException(adjacentStatons.Station1, adjacentStatons.Station2);
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            XElement helptations = (from stations in AdjacentStationsRootElem.Elements()
                                    where stations.Equal(adjacentStations.Station1, adjacentStations.Station2) && stations.IsActive()
                                    select stations).FirstOrDefault();

            if (helptations != null)
            {
                adjacentStations.isActive = true;
                helptations.ReplaceWith(adjacentStations.ToXElement());

                XMLTools.SaveListToXMLElement(AdjacentStationsRootElem, AdjacentStationsPath);
            }
            else
                throw new DO.InvalidAdjacentStationIDException(adjacentStations.Station1, adjacentStations.Station2);
        }

        #endregion

        #region line

        public void CreateLine(Line Newline)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            Line helpLine = (from line in ListLines
                             where line.Id == Newline.Id && line.isActive
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
                   where line.isActive
                   select line;
        }

        public Line RequestLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            Line helpLine = (from line in ListLines
                             where line.Id == id && line.isActive
                             select line).FirstOrDefault();
            if (helpLine != null)
                return helpLine; //no need to Clone()

            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }
        public void DeleteLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            Line helpLine = (from line in ListLines
                             where line.Id == id && line.isActive
                             select line).FirstOrDefault();
            if (helpLine != null)
            {
                ListLines.Remove(helpLine);
                helpLine.isActive = false;
                ListLines.Add(helpLine);
                XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
            }
            else throw new DO.InvalidLineIDException(id, "this line id doesn't exists");
        }

        public void UpdateLine(Line Uline)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            Line helpLine = (from line in ListLines
                             where line.Id == Uline.Id && line.isActive
                             select line).FirstOrDefault();
            if (helpLine != null)
            {
                Uline.isActive = true;
                ListLines.Remove(helpLine);
                ListLines.Add(Uline);
                XMLTools.SaveListToXMLSerializer<Line>(ListLines, LinePath);
            }
            else throw new DO.InvalidLineIDException(Uline.Id, "this line id doesn't exists");
        }
        #endregion

        #region bus

        public void CreateBus(Bus NewBus)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            Bus thisSus = (from bus in BusRootElem.Elements()
                           where bus.IsActive() && bus.Equal(NewBus.LicenseNum)
                           select bus.ToBus()).FirstOrDefault();

            if (thisSus != null)
                throw new DO.InvalidBusLicenseNumberException(NewBus.LicenseNum, "we already have this bus");

            BusRootElem.Add(thisSus.ToXElement());

            XMLTools.SaveListToXMLElement(BusRootElem, BusPath);
        }


        public void DeleteBus(int licenseNum)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);


            XElement thisBus = (from bus in BusRootElem.Elements()
                                where bus.IsActive() && bus.Equal(licenseNum)
                                select bus).FirstOrDefault();

            if (thisBus != null)
            {
                thisBus.Element("isActive").Value = false.ToString();

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
        public IEnumerable<Bus> RequestBusBy(Predicate<Bus> predicate)
        {
            XElement BusRootElem = XMLTools.LoadListFromXMLElement(BusPath);
            return (from bus in BusRootElem.Elements()
                    where bus.IsActive() && predicate(bus.ToBus())
                    select bus.ToBus());
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
                Newbus.isActive = true;
                helpBus.ReplaceWith(Newbus.ToXElement());

                XMLTools.SaveListToXMLElement(BusRootElem, BusPath);
            }
            else
                throw new DO.InvalidBusLicenseNumberException(Newbus.LicenseNum, "we don't have this bus");
        }
        #endregion

        #region user


        public void CreateUser(User NewUser)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            User helpUser = (from user in ListUsers
                             where user != null && user.isActive && user.UserName == NewUser.UserName
                             select user).FirstOrDefault();
            if (helpUser != null)
                throw new DO.BadUsernameOrPasswordException(NewUser.UserName, NewUser.Password, "this username id doesn't exists");
            else
            {
                ListUsers.Add(NewUser);
                XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
            }
        }

        public bool RequestUserPrivileges(string username, string password)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User helpUser = (from user in ListUsers
                                where user != null && user.isActive && user.UserName == username && user.Password == password
                                select user).FirstOrDefault();
            if (helpUser == null)
                throw new DO.BadUsernameOrPasswordException(username, password, "the password and username doesn't match");
            return helpUser.Admin;
        }
        #endregion

        #region Trip

        public IEnumerable<LineTrip> RequestAllLineTrips()
        {
            try
            {
                XElement BusRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);
                return (from lineTrip in BusRootElem.Elements()
                        where lineTrip.IsActive()
                        select lineTrip.ToLineTrip());
            }
            catch (XMLFileLoadCreateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new XMLFileFormatException(LineTripPath, "unexpected problem in lineTrip xml", ex);
            }
        }
        public IEnumerable<LineTrip> RequestAllLineTripsInLine(int lineId)
        {
            try
            {


                XElement BusRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);
                return (from lineTrip in BusRootElem.Elements()
                        where lineTrip.IsActive() && int.Parse(lineTrip.Element("LineID").Value) == lineId
                        select lineTrip.ToLineTrip());
            }
            catch (XMLFileLoadCreateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new XMLFileFormatException(LineTripPath, "unexpected problem in lineTrip xml", ex);
            }
        }

        public void CreateLineTrip(LineTrip Newtrip)
        {
            try
            {
                XElement tripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);
                var helpTrip = (from lineTrip in tripRootElem.Elements()
                                where lineTrip.IsActive() && lineTrip.Equal(Newtrip)
                                select lineTrip.ToLineTrip()).FirstOrDefault();
                if (helpTrip != null)
                    throw new DO.BadLineTripException(Newtrip.Id, Newtrip.LineId, "id already exists");
                else
                {
                    Newtrip.isActive = true;
                    tripRootElem.Add(Newtrip.ToXElement());

                    XMLTools.SaveListToXMLElement(tripRootElem, LineTripPath);
                }
            }
            catch (XMLFileLoadCreateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new XMLFileFormatException(LineTripPath, "unexpected problem in lineTrip xml", ex);
            }
        }
        public void DeleteLineTrip(int tripID)
        {
            try
            {
                XElement tripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);
                var helpTrip = (from lineTrip in tripRootElem.Elements()
                                where lineTrip.IsActive() && int.Parse(lineTrip.Element("ID").Value) == tripID
                                select lineTrip).FirstOrDefault();
                if (helpTrip == null)
                    throw new DO.BadLineTripException(tripID, -1, "this line trip doesn't exists");
                else
                {
                    helpTrip.Element("isActive").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(tripRootElem, LineTripPath);
                }
            }
            catch (XMLFileLoadCreateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new XMLFileFormatException(LineTripPath, "unexpected problem in lineTrip xml", ex);
            }

        }

        public void CreateTrip(Trip newTrip)
        {
            try
            {
                XElement tripRootElem = XMLTools.LoadListFromXMLElement(TripPath);
                var helpTrip = (from trip in tripRootElem.Elements()
                                where trip.Equal(newTrip)
                                select trip.ToLineTrip()).FirstOrDefault();
                if (helpTrip != null)
                    throw new DO.BadLineTripException(newTrip.Id, newTrip.LineId, "Trip id already exists");
                else
                {
                    tripRootElem.Add(newTrip.ToXElement());
                    XMLTools.SaveListToXMLElement(tripRootElem, LineTripPath);
                }
            }
            catch (XMLFileLoadCreateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new XMLFileFormatException(LineTripPath, "unexpected problem in trip xml", ex);
            }
        }


        #endregion

        #region LineStations
        public void CreateLineStation(LineStation lineStation)
        {
            List<LineStation> ListStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            LineStation helpStation = (from station in ListStation
                                       where station.StationId == lineStation.StationId && station.LineId == lineStation.LineId
                                       && station.isActive
                                       select station).FirstOrDefault();
            if (helpStation != null)
                throw new DO.InvalidLinesStationException(lineStation.StationId, lineStation.LineId, "the data base alredy has this linestation");
            else
            {
                ListStation.Add(lineStation);
                XMLTools.SaveListToXMLSerializer(ListStation, LineStationsPath);
            }
        }
        public LineStation RequestLineStation(int stationId, int lineId)
        {
            List<LineStation> ListStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            LineStation helpStation = (from station in ListStation
                                       where station.StationId == stationId && station.LineId == lineId && station.isActive
                                       && station.isActive
                                       select station).FirstOrDefault();
            if (helpStation == null)
                throw new DO.InvalidLinesStationException(stationId, lineId, "this line station id doesn't exists");
            else
                return helpStation;
        }
        public IEnumerable<LineStation> RequestLineStationsInLine(int lineId)
        {
            List<LineStation> ListStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            return from lineStation in ListStation
                   where lineStation.LineId == lineId && lineStation.isActive
                   orderby lineStation.LineStationIndex ascending
                   select lineStation;
        }
        public void DeleteLineStation(int stationId, int lineId)
        {
            List<LineStation> ListStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            LineStation helpStation = (from station in ListStation
                                       where station.StationId == stationId && station.LineId == lineId
                                       && station.isActive
                                       select station).FirstOrDefault();
            if (helpStation == null)
                throw new DO.InvalidLinesStationException(stationId, lineId, "this line station id doesn't exists");
            else
            {
                ListStation.Remove(helpStation);
                helpStation.isActive = false;
                ListStation.Add(helpStation);
                XMLTools.SaveListToXMLSerializer(ListStation, LineStationsPath);
            }
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            List<LineStation> ListStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            //LineStation helpStation = (from station in ListStation
            //                           where station.StationId == lineStation.StationId && station.LineId == lineStation.LineId && station.isActive
            //                           && station.isActive
            //                           select station).FirstOrDefault();
            var i = ListStation.FindIndex(p => p.StationId == lineStation.StationId && p.LineId == lineStation.LineId && p.isActive);
            if (i == -1)
                throw new DO.InvalidLinesStationException(lineStation.StationId, lineStation.LineId, "this line station id doesn't exists");
            else
            {
                lineStation.isActive = true;
                ListStation[i] = lineStation;
                XMLTools.SaveListToXMLSerializer(ListStation, LineStationsPath);
            }
        }
        #endregion

        #region station
        public void CreateStation(Station newStation)
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            Station helpStation = (from station in ListStation
                                   where station.isActive && station.Code == newStation.Code && station.isActive
                                   select station).FirstOrDefault();
            if (helpStation != null)
                throw new DO.InvalidStationIDException(newStation.Code, "Station code already exists.");
            else
            {
                ListStation.Add(newStation);
                XMLTools.SaveListToXMLSerializer(ListStation, StationPath);
            }
        }

        public void DeleteStation(int id)
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            Station helpStation = (from station in ListStation
                                   where station.isActive && station.Code == id
                                   select station).FirstOrDefault();
            if (helpStation == null)
                throw new DO.InvalidStationIDException(id, "Station id not found.");
            else
            {
                ListStation.Remove(helpStation);
                helpStation.isActive = false;
                ListStation.Add(helpStation);
                XMLTools.SaveListToXMLSerializer(ListStation, StationPath);
            }
        }

        public IEnumerable<Station> RequestAllStations()
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            return from station in ListStation
                   where station.isActive
                   select station;
        }
        public Station RequestStation(int id)
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            Station helpStation = (from station in ListStation
                                   where station.isActive && station.Code == id
                                   select station).FirstOrDefault();
            if (helpStation == null)
                throw new DO.InvalidStationIDException(id, "Station id not found.");
            else return helpStation;
        }

        public IEnumerable<Station> RequestStationBy(Predicate<Station> predicate)
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            return from station in ListStation
                   where station.isActive && predicate(station)
                   select station;
        }

        public IEnumerable<Line> RequestLinesInStation(int stationId)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            return from lineStations in ListLineStation
                   where lineStations.StationId == stationId && lineStations.isActive
                   select ListLines.Find(x => x.Id == lineStations.LineId);
        }
        public void UpdateStation(Station Ustation)
        {
            List<Station> ListStation = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            Station helpStation = (from station in ListStation
                                   where station.isActive && station.Code == Ustation.Code
                                   select station).FirstOrDefault();
            if (helpStation == null)
                throw new DO.InvalidStationIDException(Ustation.Code, "Station id not found.");
            else
            {
                Ustation.isActive = true;
                ListStation.Remove(helpStation);
                ListStation.Add(Ustation);
                XMLTools.SaveListToXMLSerializer(ListStation, StationPath);
            }
        }
        #endregion
        #region Counters

        public int RequestCounter(string type)
        {
            XElement CountersRootElem = XMLTools.LoadListFromXMLElement(CounterPath);

            return int.Parse(CountersRootElem.Element(type).Value);
        }

        public void UpdateCounter(string type)
        {
            XElement CountersRootElem = XMLTools.LoadListFromXMLElement(CounterPath);

            CountersRootElem.Element(type).Value = (int.Parse(CountersRootElem.Element(type).Value) + 1).ToString();
            XMLTools.SaveListToXMLElement(CountersRootElem, CounterPath);
        }
        #endregion





    }
}

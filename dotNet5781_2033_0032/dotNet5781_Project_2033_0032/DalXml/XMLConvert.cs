using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL
{
    static class ExtensionMethods
    {
        public static bool IsActive(this XElement xElement)
        {
            return bool.Parse(xElement.Element("isActive").Value);
        }
        #region TimeSpan
        /// <summary>
        /// converts from XElement to DateTime
        /// </summary>
        /// <param name="stationElement">the Xelement thet needs to be converted</param>
        /// <returns>the converted DateTime</returns>
        /// <exception cref="FormatException"/>
        public static DateTime ToDateTime(this XElement DateTimeElement)
        {
            return new DateTime(
                        int.Parse(DateTimeElement.Element("Year").Value),
                        int.Parse(DateTimeElement.Element("Month").Value),
                        int.Parse(DateTimeElement.Element("Day").Value));

        }
        /// <summary>
        /// converts from  TimeSpan to XElement
        /// </summary>
        /// <param name="dateTime">the DateTime thet needs to be converted</param>
        /// <returns>the converted XElement</returns>
        public static XElement ToXElement (this DateTime dateTime,string name)
        {
            return new XElement(name,
                            new XElement("Year", dateTime.Year),
                            new XElement("Month", dateTime.Month),
                            new XElement("Day", dateTime.Day));
        }
        #endregion
        #region TimeSpan
        /// <summary>
        /// converts from XElement to TimeSpan
        /// </summary>
        /// <param name="TimeElement">the Xelement thet needs to be converted</param>
        /// <returns>the converted TimeSpan</returns>
        /// <exception cref="FormatException"/>
        public static TimeSpan ToTimeSpan(this XElement TimeElement)
        {
            return new TimeSpan(
                        int.Parse(TimeElement.Element("Hours").Value),
                        int.Parse(TimeElement.Element("Minutes").Value),
                        int.Parse(TimeElement.Element("Seconds").Value));

        }
        /// <summary>
        /// converts from  TimeSpan to XElement
        /// </summary>
        /// <param name="timeSpan">the TimeSpan thet needs to be converted</param>
        /// <returns>the converted XElement</returns>
        public static XElement ToXElement(this TimeSpan timeSpan, string name)
        {
            return new XElement(name,
                            new XElement("Hours", timeSpan.Hours),
                            new XElement("Minutes", timeSpan.Minutes),
                            new XElement("Seconds", timeSpan.Seconds));
        }
        #endregion
        #region AdjacentStations
        /// <summary>
        /// converts from XElement to AdjecentStations
        /// </summary>
        /// <param name="stationElement">the Xelement thet needs to be converted</param>
        /// <returns>the converted AdjacentStaation</returns>
        /// <exception cref="FormatException"/>
        public static AdjacentStations ToAdjecentStation(this XElement stationElement)
        {
                return new AdjacentStations()
                {
                    Station1 = int.Parse(stationElement.Element("Station1").Value),
                    Station2 = int.Parse(stationElement.Element("Station2").Value),
                    DistFromLastStation = double.Parse(stationElement.Element("Distance").Value),
                    TimeSinceLastStation = stationElement.Element("Time").ToTimeSpan()
                };
        }
        /// <summary>
        /// converts from  AdjecentStations to XElement
        /// </summary>
        /// <param name="stations">the AdjacentStations thet needs to be converted</param>
        /// <returns>the converted XElement</returns>
        public static XElement ToXElement(this AdjacentStations stations)
        {
            return new XElement("AdjecentStations",
                        new XElement("Station1", stations.Station1),
                        new XElement("Station2", stations.Station2),
                        new XElement("Distance", stations.DistFromLastStation),
                        stations.TimeSinceLastStation.ToXElement("Time"));
        }
        /// <summary>
        /// checkes wether or not the element in the XElement contains the AdjacentStations represented by 
        /// this stations
        /// </summary>
        /// <param name="stationElement">the element</param>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns>true if they are the same false otherwise</returns>
        public static bool Equal(this XElement stationElement, int station1, int station2)
        {
            return station1 == int.Parse(stationElement.Element("Station1").Value) &&
                    station2 == int.Parse(stationElement.Element("Station2").Value);
        }
        #endregion
        #region Trip

        /// <summary>
        /// converts from XElement to Trip
        /// </summary>
        /// <param name="stationElement">the Xelement thet needs to be converted</param>
        /// <returns>the converted Trip</returns>
        /// <exception cref="FormatException"/>
        public static Trip ToTrip(this XElement tripElement)
        {
            return new Trip()
            {
                Id = int.Parse(tripElement.Element("ID").Value),
                UserName = tripElement.Element("UserName").Value,
                LineId = int.Parse(tripElement.Element("LineID").Value),
                InStation = int.Parse(tripElement.Element("InStations").Value),
                InAt = tripElement.Element("TimeIn").ToTimeSpan(),
                OutStation=int.Parse(tripElement.Element("OutStation").Value),
                OutAt = tripElement.Element("TimeOut").ToTimeSpan(),

            };
        }
        /// <summary>
        /// converts from  AdjecentStations to XElement
        /// </summary>
        /// <param name="stations">the AdjacentStations thet needs to be converted</param>
        /// <returns>the converted XElement</returns>
        public static XElement ToXElement(this Trip trip)
        {
            return new XElement("Trip",
                        new XElement("ID", trip.Id),
                        new XElement("UserName", trip.UserName),
                        new XElement("LineID", trip.LineId),
                        new XElement("InStations", trip.InStation),
                        trip.InAt.ToXElement("TimeIn"),
                        new XElement("OutStation", trip.OutStation),
                        trip.OutAt.ToXElement("TimeOut"));
        }
        ///// <summary>
        ///// checkes wether or not the element in the XElement contains the AdjacentStations represented by 
        ///// this stations
        ///// </summary>
        ///// <param name="stationElement">the element</param>
        ///// <param name="station1"></param>
        ///// <param name="station2"></param>
        ///// <returns>true if they are the same false otherwise</returns>
        //public static bool Equals(this XElement stationElement, int station1, int station2)
        //{
        //    return station1 == int.Parse(stationElement.Element("Station1").Value) &&
        //            station2 == int.Parse(stationElement.Element("Station2").Value);
        //}
        #endregion
        #region Bus
        /// <summary>
        /// converts from XElement to Bus
        /// </summary>
        /// <param name="busElement">the Xelement thet needs to be converted</param>
        /// <returns>the converted Bus</returns>
        /// <exception cref="FormatException"/>
        public static Bus ToBus(this XElement busElement)
        {
            return new Bus()
            {
                isActive = bool.Parse(busElement.Element("isActive").Value),
                LicenseNum = int.Parse(busElement.Element("license plate number").Value),
                FuelRemaining = double.Parse(busElement.Element("Fuel").Value),
                TotalTrip= double.Parse(busElement.Element("mileage").Value),
                FromDate = busElement.Element("FromDate").ToDateTime(),
                LastTreatment = busElement.Element("last treatment").ToDateTime(),
                Status = (Status)Enum.Parse(typeof(Status), busElement.Element("Bus status").Value)
            };
        }
        /// <summary>
        /// converts from  Bus to XElement
        /// </summary>
        /// <param name="bus">the Bus thet needs to be converted</param>
        /// <returns>the converted XElement</returns>
        public static XElement ToXElement(this Bus bus)
        {
            return new XElement("AdjecentStations",
                        new XElement("isActive", bus.isActive),
                        new XElement("license plate number", bus.LicenseNum),
                        new XElement("Fuel", bus.FuelRemaining),
                        new XElement("mileage", bus.TotalTrip),
                        bus.FromDate.ToXElement("FromDate"),
                        bus.LastTreatment.ToXElement("last treatment"),
                        new XElement("Bus status", bus.Status.ToString()));
        }

        public static bool Equal(this XElement BusElement,int LicenseNum)
        {
            return LicenseNum == int.Parse(BusElement.Element("license plate number").Value);
        }

        #endregion
    }
}

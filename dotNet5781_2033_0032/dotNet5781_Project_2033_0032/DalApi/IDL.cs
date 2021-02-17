using DO;
using System;
using System.Collections.Generic;

namespace DLAPI
{
    public interface IDL
    {
        int RequestCounter(string type);
        void UpdateCounter(string type);
        #region Bus
        /// <summary>
        /// Returns all existing buses.
        /// </summary>
        /// <returns>An IEnumerable of all the buses.</returns>
        IEnumerable<DO.Bus> RequestAllBuses();
        /// <summary>
        /// Refueles a bus (fueling its tank to full tank) by its license number.
        /// </summary>
        /// <param name="licenseNum">The license number of the bus to be refueled.</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        IEnumerable<DO.Bus> RequestBusBy(Predicate<DO.Bus> predicate);
        /// <summary>
        /// Returns a bus according to the bus license number.
        /// </summary>
        /// <param name="licenseNum">The license number of the requested bus</param>
        /// <returns>The requested bus.</returns>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        DO.Bus RequestBus(int licenseNum);
        /// <summary>
        /// Creates a new bus and saves it in the database.
        /// </summary>
        /// <param name="newBus">The license number of the new bus.</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void CreateBus(DO.Bus newBus);
        /// <summary>
        /// Deletes the bus from the database according to its license number.
        /// </summary>
        /// <param name="licenseNum">The license of the bus to be deleted.</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void DeleteBus(int licenseNum);
        /// <summary>
        /// Updates some bus details in the database according to its license number.
        /// </summary>
        /// <param name="bus">The bus to be updated</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void UpdateBus(DO.Bus bus);
        #endregion

        #region Stations
        /// <summary>
        /// Deletes the station from the database according to its ID (code).
        /// </summary>
        /// <param name="id">The id (code) of the line to be deketed.</param>
        /// <exception cref="InvalidStationIDException"/>
        void DeleteStation(int id);
        /// <summary>
        /// Creates a new station and saves it in the database.
        /// </summary>
        /// <param name="station">The new station.</param>
        /// <exception cref="InvalidStationIDException"/>
        void CreateStation(DO.Station station);
        /// <summary>
        /// Returns a station according to the station ID.
        /// </summary>
        /// <param name="id">The id (code) of the requested station</param>
        /// <returns>The requested station.</returns>
        /// <exception cref="InvalidStationIDException"/>
        DO.Station RequestStation(int id);
        /// <summary>
        /// Returns all existing stations.
        /// </summary>
        /// <returns>An IEnumerable of all the stations.</returns>
        IEnumerable<DO.Station> RequestAllStations();
        /// <summary>
        /// Returns a collection of stations that meets a certain condition.
        /// </summary>
        /// <param name="predicate">The condition the requested stations needs to meet.</param>
        /// <returns>An IEnumerable of stations that meets the condition.</returns>
        IEnumerable<DO.Station> RequestStationBy(Predicate<DO.Station> predicate);
        /// <summary>
        /// Returns all lines that pass in a given station Id (code).
        /// </summary>
        /// <param name="stationId">The station Id where the requested lines are passing.</param>
        /// <returns>An IEnumerable of lines that pass in the station.</returns>
        IEnumerable<DO.Line> RequestLinesInStation(int stationId);
        /// <summary>
        /// Creates a new adjacent stations instance and saves it in the database.
        /// </summary>
        /// <param name="adjacentStations">The new instance.</param>
        /// <exception cref="InvalidAdjacentStationIDException"/>
        void CreateAdjacentStations(DO.AdjacentStations adjacentStations);
        /// <summary>
        /// Deletes the adjacent stations from the database according to station1 id (code) and station2 id (code).
        /// </summary>
        /// <param name="adjacentStatons">The adjacent station to be deleted.</param>
        /// <exception cref="InvalidAdjacentStationIDException"/>
        void DeleteAdjacentStations(DO.AdjacentStations adjacentStatons);
        /// <summary>
        /// Updates some line station details in the database according to its station id (code) and line id.
        /// </summary>
        /// <param name="station">The line station to be updated</param>
        /// <exception cref="InvalidStationIDException"/>
        void UpdateStation(DO.Station station);
        /// <summary>
        /// Updates somen adjacent station details in the database according to its station1 id (code) and station2 id (code).
        /// </summary>
        /// <param name="adjacentStations">The adjacent stations to be updated.</param>
        /// <exception cref="InvalidAdjacentStationIDException"/>
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        /// <summary>
        /// Updates some line station details in the database according to its station id (code) and line id.
        /// </summary>
        /// <param name="lineStation">The line station to be updated</param>
        /// <exception cref="InvalidLinesStationException"/>
        void UpdateLineStation(LineStation lineStation);
        /// <summary>
        /// Returns an adjacent station according to the station code and line ID.
        /// </summary>
        /// <param name="station1">The station ID (code) of the first station.</param>
        /// <param name="station2">The station ID (code) of the second station.</param>
        /// <returns>The requested line station.</returns>
        /// <exception cref="InvalidAdjacentStationIDException"/>
        DO.AdjacentStations RequestAdjacentStations(int station1, int station2);
        /// <summary>
        /// Returns a line station according to the station code and line ID.
        /// </summary>
        /// <param name="stationId">The station ID (code) of the requested line station.</param>
        /// <param name="lineId">The line ID of the requested line station.</param>
        /// <returns>The requested line station.</returns>
        /// <exception cref="InvalidLinesStationException"/>
        DO.LineStation RequestLineStation(int stationId, int lineId);
        /// <summary>
        /// Returns all line stations in a given line.
        /// </summary>
        /// <param name="lineId">The line ID of the requested line stations.</param>
        /// <returns>An IEnumerable of line stations.</returns>
        IEnumerable<DO.LineStation> RequestLineStationsInLine(int lineId);
        /// <summary>
        /// Creates a new line station and saves it in the database.
        /// </summary>
        /// <param name="lineStation">The new line station.</param>
        /// <exception cref="InvalidLinesStationException"/>
        void CreateLineStation(DO.LineStation lineStation);
        /// <summary>
        /// Deletes the line station from the database according to its line id and station id (code).
        /// </summary>
        /// <param name="stationId">The station id of the line station to be deleted.</param>
        /// <param name="lineId">The line id of the line station to be deleted.</param>
        /// <exception cref="InvalidLinesStationException"/>
        void DeleteLineStation(int stationId, int lineId);

        #endregion

        #region Line
        /// <summary>
        /// Returns all existing lines.
        /// </summary>
        /// <returns>An IEnumerable of all lines.</returns>
        IEnumerable<DO.Line> RequestAllLines();
        /// <summary>
        /// Returns a line according to the line ID.
        /// </summary>
        /// <param name="id">The idof the requested line.</param>
        /// <returns>The requested line.</returns>
        /// <exception cref="InvalidLineIDException"/>
        DO.Line RequestLine(int id);
        /// <summary>
        /// Creates a new line and saves it in the database.
        /// </summary>
        /// <param name="line">The new line.</param>
        /// <exception cref="InvalidLineIDException"/>
        void CreateLine(DO.Line line);
        /// <summary>
        /// Deletes the line from the database according to its ID.
        /// </summary>
        /// <param name="id">The id of the new station.</param>
        /// <exception cref="InvalidLineIDException"/>
        void DeleteLine(int id);
        /// <summary>
        /// Updates some line details in the database according to its id.
        /// </summary>
        /// <param name="line">The line to be updated</param>
        /// <exception cref="InvalidLineIDException"/>
        void UpdateLine(DO.Line line);
        #endregion

        #region User
        /// <summary>
        /// Checks if a user is an admin.
        /// </summary>
        /// <param name="username">The username of the checked user</param> 
        /// <param name="password">The password of the checked user</param> 
        /// <returns>True if the user is an admin, False otherwise.</returns>
        bool RequestUserPrivileges(string username, string password);
        /// <summary>
        /// Creates a new user and saves it in the database.
        /// </summary>
        /// <param name="user">The new user</param> 
        void CreateUser(DO.User user);
        #endregion

        #region Trip
        /// <summary>
        /// Returns all existing line trips.
        /// </summary>
        /// <returns>An IEnumerable of all the line trips.</returns>
        IEnumerable<LineTrip> RequestAllLineTrips();
        /// <summary>
        /// Checks the line trips of the given line.
        /// </summary>
        /// <param name="lineId">The line to check its line trips.</param>
        /// <returns>An IEnumerable of line trips sorted by their start time.</returns>
        /// <exception cref="BadLineTripException"/>
        IEnumerable<LineTrip> RequestAllLineTripsInLine(int lineId);
        /// <summary>
        /// Creates a new line trip and saves it in the database.
        /// </summary>
        /// <param name="Newtrip">The new trip.</param>
        /// <exception cref="BadLineTripException"/>
        void CreateLineTrip(LineTrip Newtrip);
        /// <summary>
        /// Deletes the line trip from the database according to its id.
        /// </summary>
        /// <param name="tripID">The id of the line trip to be deleted.</param>
        /// <exception cref="BadLineTripException"/>
        void DeleteLineTrip(int tripID);
        /// <summary>
        /// Creates a new trip and saves it in the database.
        /// </summary>
        /// <param name="trip">The new trip.</param>
        /// <exception cref="BadTripException"/>
        void CreateTrip(Trip newTrip);
        #endregion
    }
}

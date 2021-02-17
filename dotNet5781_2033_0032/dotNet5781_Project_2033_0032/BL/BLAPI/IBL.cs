using BO;
using System;
using System.Collections.Generic;

namespace BLAPI
{
    public interface IBL
    {
        /// <summary>
        /// gets the counter 
        /// </summary>
        /// <param name="coun">the name of teh object we want the counter for</param>
        /// <returns>the counter</returns>
        int GetCounters(string coun);
        /// <summary>
        /// gets the counter and adds one 
        /// </summary>
        /// <param name="coun">the name of teh object we want the counter for</param>
        /// <returns>the counter</returns>
        int GetCounterAndAdd(string coun);
        #region LineTrip
        /// <summary>
        /// Checks the lines that are suppose to pass in the given station.
        /// </summary>
        /// <param name="stationId">The station to check which lines go throught.</param>
        /// <returns>An IEnumerable of LineTiming sorted by first to last to get to the station.</returns>
        IEnumerable<LineTiming> RequestLineTimingFromStation(int stationId);
        /// <summary>
        /// Checks the line trips of the given line.
        /// </summary>
        /// <param name="lineId">The line to check its line trips.</param>
        /// <returns>An IEnumerable of line trips sorted by their start time.</returns>
        /// <exception cref="BadLineTripException"/>
        IEnumerable<LineTrip> RequestLineTripInLine(int lineId);
        /// <summary>
        /// Creates a new line trip and saves it in the database.
        /// </summary>
        /// <param name="lineId">The line of the line trip.</param>
        /// <param name="startAt">The start time of the trip.</param>
        /// <param name="frequency">The frequency of the trip in the current time.</param>
        /// <param name="finishedAt">The finish time of the trip.</param>
        /// <exception cref="BadLineTripException"/>
        void CreateLineTrip(int lineId, TimeSpan startAt, TimeSpan frequency, TimeSpan finishedAt);
        /// <summary>
        /// Deletes the line trip from the database according to its id.
        /// </summary>
        /// <param name="lineTripId">The id of the line trip to be deleted.</param>
        /// <exception cref="BadLineTripException"/>
        void DeleteLineTrip(int lineTripId);

        #endregion

        #region Simulator
        /// <summary>
        /// Starts the simulator.
        /// </summary>
        /// <param name="start">The start time of the simulator.</param>
        /// <param name="speed">The speed of the simulator (how many seconds in a real second).</param>
        /// <param name="func">A function to update the clock.</param>
        void StartSimulator(TimeSpan start, int speed, Action<TimeSpan> func);
        /// <summary>
        /// Stops the simulator.
        /// </summary>
        void StopSimulator();
        /// <summary>
        /// Checks if the simulator is activated and running.
        /// </summary>
        /// <returns>True if active, False otherwise.</returns>
        bool IsSimulationActivated();
        #endregion

        #region Bus
        /// <summary>
        /// Returns a bus according to the bus license number.
        /// </summary>
        /// <param name="licenseNum">The license number of the requested bus</param>
        /// <returns>The requested bus.</returns>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        BO.Bus RequestBus(int licenseNum);
        /// <summary>
        /// Creates a new bus and saves it in the database.
        /// </summary>
        /// <param name="licenseNum">The license number of the new bus.</param>
        /// <param name="fromTime">The production date of the new bus.</param>
        /// <param name="fuel">The fuel amount of the new bus (default = 1200).</param>
        /// <param name="totalTrip">The mileage (total trip) of the new bus (default = 0).</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void CreateBus(int licenseNum, DateTime fromTime, double fuel = 1200, double totalTrip = 0);
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
        void UpdateBus(BO.Bus bus);
        /// <summary>
        /// Returns all existing buses.
        /// </summary>
        /// <returns>An IEnumerable of all the buses.</returns>
        IEnumerable<BO.Bus> RequestAllBuses();
        /// <summary>
        /// Returns a collection of buses that meets a certain condition.
        /// </summary>
        /// <param name="predicate">The condition the requested buses needs to meet.</param>
        /// <returns>An IEnumerable of buses that meets the condition.</returns>
        IEnumerable<BO.Bus> RequestBusBy(Predicate<BO.Bus> predicate);
        /// <summary>
        /// Refueles a bus (fueling its tank to full tank) by its license number.
        /// </summary>
        /// <param name="licenseNum">The license number of the bus to be refueled.</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void FuelBus(int licenseNum);
        /// <summary>
        /// Fixes a bus (sets the last treatment date to current time) by its license number.
        /// </summary>
        /// <param name="licenseNum">The license number of the bus to be fixed.</param>
        /// <exception cref="InvalidBusLicenseNumberException"/>
        void FixBus(int id);
        #endregion

        #region Stations
        /// <summary>
        /// Deletes the station from the database according to its ID (code).
        /// </summary>
        /// <param name="id">The id (code) of the line to be deleted.</param>
        /// <exception cref="InvalidStationIDException"/>
        void DeleteStation(int id);
        /// <summary>
        /// Creates a new station and saves it in the database.
        /// </summary>
        /// <param name="code">The id (code) of the new station.</param>
        /// <param name="name">The name of the new station.</param>
        /// <param name="longitude">The longitude coordinate of the new station.</param>
        /// <param name="latitude">The latitude coordinate of the new station.</param>
        /// <exception cref="InvalidStationIDException"/>
        void CreateStation(int code, string name, double longitude, double latitude);
        /// <summary>
        /// Returns a station according to the station ID.
        /// </summary>
        /// <param name="id">The id (code) of the requested station</param>
        /// <returns>The requested station.</returns>
        /// <exception cref="InvalidStationIDException"/>
        BO.Station RequestStation(int id);
        /// <summary>
        /// Returns all existing stations.
        /// </summary>
        /// <returns>An IEnumerable of all the stations.</returns>
        IEnumerable<BO.Station> RequestAllStations();
        /// <summary>
        /// Returns all lines that pass in two given stations by that order.
        /// </summary>
        /// <returns>An IEnumerable of tuple of the first and second lineTiming.</returns>
        IEnumerable<(BO.LineTiming, BO.LineTiming)> LinesInTwoStations(int station1Id, int station2Id);

        /// <summary>
        /// Returns a line station according to the station code and line ID.
        /// </summary>
        /// <param name="stationId">The station ID (code) of the requested line station.</param>
        /// <param name="lineId">The line ID of the requested line station.</param>
        /// <returns>The requested line station.</returns>
        /// <exception cref="InvalidLinesStationException"/>

        BO.LineStation RequestLineStation(int stationId, int lineId);
        /// <summary>
        /// Returns a collection of stations that meets a certain condition.
        /// </summary>
        /// <param name="predicate">The condition the requested stations needs to meet.</param>
        /// <returns>An IEnumerable of stations that meets the condition.</returns>
        IEnumerable<BO.Station> RequestStationsBy(Predicate<BO.Station> predicate);
        /// <summary>
        /// Returns all line stations in a given line.
        /// </summary>
        /// <param name="lineId">The line ID of the requested line stations.</param>
        /// <returns>An IEnumerable of line stations.</returns>
        IEnumerable<BO.LineStation> RequestLineStationsInLine(int lineId);
        /// <summary>
        /// Returns all stations in line in a given line.
        /// </summary>
        /// <param name="lineId">The line ID of the requested stations in line.</param>
        /// <returns>An IEnumerable of stations in line.</returns>
        IEnumerable<StationInLine> RequestStationsInLineByLine(int lineId);
        /// <summary>
        /// Updates some station details in the database according to its id.
        /// </summary>
        /// <param name="station">The station to be updated</param>
        /// <exception cref="InvalidStationIDException"/>
        void UpdateStation(Station station);
        /// <summary>
        /// Updates some line station details in the database according to its station id (code) and line id.
        /// </summary>
        /// <param name="station">The line station to be updated</param>
        /// <exception cref="InvalidLinesStationException"/>
        void UpdateLineStation(LineStation station);
        /// <summary>
        /// Adds a station to a given line.
        /// </summary>
        /// <param name="lineId">The line the new station need to be added.</param>
        /// <param name="stationId">The id of the station to be added.</param>
        /// <param name="index">The index where the station needs to be added.</param>
        /// <param name="distanceSinceLastStation">The distance from the previous station.</param>
        /// <param name="timeSinceLastStation">The time to get from the previous station.</param>
        /// <param name="distanceUntilNextStation">The distance to the next station.</param>
        /// <param name="timeUntilNextStation">The time to get to the next station.</param>
        /// <exception cref="InvalidAdjacentLineIDException"/>
        /// <exception cref="InvalidLinesStationException"/>
        void CreateStationToLine(int lineId, int stationId, int index, double distanceSinceLastStation, TimeSpan timeSinceLastStation, double distanceUntilNextStation, TimeSpan timeUntilNextStation);
        /// <summary>
        /// Deletes a station from a line.
        /// </summary>
        /// <param name="lineId">The line where the station needs to be deleted.</param>
        /// <param name="stationId">The id of the station to be deleted.</param>
        /// <param name="distanceSinceLastStation">Distance between the next and previous stations of the deleted station.</param>
        /// <param name="timeSinceLastStation">Time between the next and previous stations of the deleted station.</param>
        void DeleteStationFromLine(int lineId, int stationId, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        /// <summary>
        /// Updates somen adjacent station details in the database according to its station1 id (code) and station2 id (code).
        /// </summary>
        /// <param name="station1">The ID (code) of the first station.</param>
        /// <param name="station2">The ID (code) of the second station.</param>
        /// <param name="distanceSinceLastStation">Distance between the stations to be updated.</param>
        /// <param name="timeSinceLastStation">Time between the stations to be updated.</param>
        /// <exception cref="InvalidAdjacentLineIDException"/>
        void UpdateAdjacentStations(int station1, int station2, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
        #endregion

        #region Line
        /// <summary>
        /// Returns a line according to the line ID.
        /// </summary>
        /// <param name="id">The idof the requested line.</param>
        /// <returns>The requested line.</returns>
        /// <exception cref="InvalidLineIDException"/>
        BO.Line RequestLine(int id);
        /// <summary>
        /// Returns all existing lines.
        /// </summary>
        /// <returns>An IEnumerable of all lines.</returns>
        IEnumerable<BO.Line> RequestAllLines();
        /// <summary>
        /// Returns all lines that pass in a given station Id (code).
        /// </summary>
        /// <param name="stationId">The station Id where the requested lines are passing.</param>
        /// <returns>An IEnumerable of lines that pass in the station.</returns>
        IEnumerable<BO.Line> LinesInStation(int stationId);
        /// <summary>
        /// Creates a new line and saves it in the database.
        /// </summary>
        /// <param name="code">The code of the new line.</param>
        /// <param name="area">The area of the new line.</param>
        /// <param name="firstStation">The first station id (code).</param>
        /// <param name="lastStation">The last station id (code).</param>
        /// <param name="distanceSinceLastStation">The distance between the first and last station.</param>
        /// <param name="timeSinceLastStation">The time between the first and last station.</param>
        /// <exception cref="InvalidLinesStationException"/>
        /// <exception cref="InvalidLineIDException"/>
        void CreateLine(int code, BO.Areas area, int firstStation, int lastStation, double distanceSinceLastStation, TimeSpan timeSinceLastStation);
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
        void UpdateLine(Line line);

        #endregion

        #region User
        /// <summary>
        /// Checks if a user is an admin.
        /// </summary>
        /// <param name="userName">The username of the checked user</param> 
        /// <param name="password">The password of the checked user</param> 
        /// <returns>True if the user is an admin, False otherwise.</returns>
        bool RequestUserPrivileges(string userName, string password);
        /// <summary>
        /// Creates a new user and saves it in the database.
        /// </summary>
        /// <param name="username">The username of the new user</param> 
        /// <param name="password">The password of the new user</param> 
        /// <param name="passwordValidation">The password repeated, to check that the user can write his password twice and didn't make a mistake.</param> 
        void CreateUser(string username, string password, string passwordValidation);
        #endregion

    }
}

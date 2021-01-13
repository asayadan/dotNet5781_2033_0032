using DO;
using System;
using System.Collections.Generic;

namespace DS
{
    public static class DataSource
    {
        public static List<Station> ListStations;
        public static List<LineStation> ListLineStations;
        public static List<AdjacentStations> ListAdjacentStations;
        public static List<Bus> ListBuses;
        public static List<Line> ListLines;
        public static List<Trip> ListTrips;
        public static List<LineTrip> ListLineTrips;

        public static List<BusOnTrip> ListBusesOnTrips;
        public static List<User> ListUsers;


        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            ListStations = new List<Station>
            {
                new Station
                {
                    Code = 1,
                    Name = "Masof Eged/Canfei Nesharim",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 2,
                    Name = "Beit Inbar/Canfei Nesharim",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 3,
                    Name = "Merkaz Shetner/Canfei Nesharim",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 4,
                    Name = "Yamin Avot",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 5,
                    Name = "Harav Tzvi Yehuda/Shd' Ha'Meiri",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 6,
                    Name = "Mercaz Harav/Harav Tzvi Yehuda",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 7,
                    Name = "Gesher Ha'Meitarim/Shd' Hertzel",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 8,
                    Name = "T. Merkazit Jerusalem/Yafo",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 9,
                    Name = "Binyanei Ha'uma/Ha'nasi Ha'Shishi",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 10,
                    Name = "Henyon Ha'leom/Shderot Ha'nasi Ha'Shishi",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 11,
                    Name = "Misrad Ha'hutz/Shd' Rabin",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 12,
                    Name = "Shahal/Heler",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 13,
                    Name = "Shahal Alef",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 14,
                    Name = "Shahal/Harav Gold",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 15,
                    Name = "Shahal Beit",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                },

                new Station
                {
                    Code = 16,
                    Name = "Shahal / Harav Hertzog",
                    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
                    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
                }
            };

            ListLines = new List<Line>
            {
                new Line
                {
                    Id = 1,
                    Code = 111,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = 1,
                    LastStation = 6

                },

                new Line
                {
                    Id = 2,
                    Code = 222,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = 7,
                    LastStation = 12

                },

                new Line
                {
                    Id = 3,
                    Code = 333,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = 5,
                    LastStation = 10

                },

                new Line
                {
                    Id = 4,
                    Code = 444,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = 15,
                    LastStation = 3

                },

                new Line
                {
                    Id = 5,
                    Code = 555,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = 11,
                    LastStation = 16

                },
            };

            ListLineStations = new List<LineStation>();

            for (int i = 0; i < ListLines.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ListLineStations.Add(
                    new LineStation
                    {
                        LineId = ListLines[i].Id,
                        LineStationIndex = j,
                        StationId = ListStations[(i * 4 + j + 1) % ListStations.Count].Code,
                        NextStation = (j == 4 - 1) ? 0 : ListStations[(i * 4 + j + 2) % ListStations.Count].Code
                    }
                    );
                }

            }

            ListAdjacentStations = new List<AdjacentStations>();

            for (int i = 0; i < ListLineStations.Count - 1; i++)
            {
                if (ListLineStations[i].NextStation == ListLineStations[i + 1].StationId)
                {
                    var station1 = ListStations.Find(x => x.Code == ListLineStations[i].StationId);
                    var station2 = ListStations.Find(x => x.Code == ListLineStations[i + 1].StationId);
                    var dist = DistanceBetween(station1, station2);
                    ListAdjacentStations.Add(
                        new AdjacentStations
                        {
                            DistFromLastStation = dist,
                            Station1 = ListLineStations[i].StationId,
                            Station2 = ListLineStations[i + 1].StationId,
                            TimeSinceLastStation = DateTime.Now.AddMinutes(dist * 1000 / rnd.Next(30, 50)) - DateTime.Now
                        });
                }
            }

            ListBuses = new List<Bus>();
            int plateNumber, range, mileage, mileageInLastTreat, fuel;
            DateTime date;
            for (int i = 0; i < 10; i++)
            {
                DateTime start = new DateTime(2016, 1, 1);
                range = (DateTime.Today - start).Days;
                date = start.AddDays(rnd.Next(range));

                if (date.Year < 2018)
                    plateNumber = rnd.Next(1000000, 9999999);
                else plateNumber = rnd.Next(10000000, 99999999);

                //start = date;
                //range = (DateTime.Today - date).Days;
                //lastTreatment = start.AddDays(rnd.Next(range));

                mileage = rnd.Next(5000);
                mileageInLastTreat = rnd.Next(mileage);
                fuel = rnd.Next(1200);

                ListBuses.Add(new Bus
                {
                    LicenseNum = plateNumber,
                    FromDate = date,
                    LastTreatment = date,
                    FuelRemaining = fuel,
                    Status = Status.Ready,
                    TotalTrip = mileageInLastTreat
                });
            }

            ListUsers = new List<User>()
            {
                new User
                {
                    Admin = true,
                    Password = "Admin",
                    UserName = "Admin"
                },

                 new User
                {
                    Admin = true,
                    Password = "#MAGA",
                    UserName = "Donald J Trump"
                },

                new User
                {
                    Admin = false,
                    UserName = "Noam",
                    Password = "qwerty"
                },

                new User
                {
                    Admin = false,
                    UserName = "Achiya",
                    Password = "12345678"
                },
            };

            //ListTrips = new List<Trip>(); Shlav 2
            //ListLineTrips = new List<LineTrip>(); shlav 2
            // ListBusesOnTrips = new List<BusOnTrip>(); Shlav 2


        }


        static double DistanceBetween(Station station1, Station station2)
        {
            return Math.Sqrt(Math.Pow((station1.Latitude - station2.Latitude), 2) +
                Math.Pow((station1.Longitude - station2.Longitude), 2));
        }
    }
}

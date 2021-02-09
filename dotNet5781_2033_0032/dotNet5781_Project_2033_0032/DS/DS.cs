using DO;
using System;
using System.Collections.Generic;
using DLAPI;
using System.Linq;
using System.Xml.Linq;

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
        private static readonly int StationsInEachBus = 10;

        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            ListStations = new List<Station>
            {
                #region Imported
                new Station
                {
                    Code = 38831,
                    Name = "בי''ס בר לב/בן יהודה",
                    Latitude = 32.183921,
                    Longitude = 34.917806
                },

                new Station
                {
                    Code = 38832,
                    Name = "הרצל/צומת בילו",
                    Latitude = 31.870034,
                    Longitude = 34.819541
                },

                new Station
                {
                    Code = 38833,
                    Name = "הנחשול/הדייגים",
                    Latitude = 31.984553,
                    Longitude = 34.782828
                },

                new Station
                {
                    Code = 38834,
                    Name = "פריד/ששת הימים",
                    Latitude = 31.88855,
                    Longitude = 34.790904
                },

                new Station
                {
                    Code = 38836,
                    Name = "ת. מרכזית לוד/הורדה",
                    Latitude = 31.956392,
                    Longitude = 34.898098
                },

                new Station
                {
                    Code = 38837,
                    Name = "חנה אברך/וולקני",
                    Latitude = 31.892166,
                    Longitude = 34.796071
                },

                new Station
                {
                    Code = 38838,
                    Name = "הרצל/משה שרת",
                    Latitude = 31.857565,
                    Longitude = 34.824106
                },

                new Station
                {
                    Code = 38839,
                    Name = "הבנים/אלי כהן",
                    Latitude = 31.862305,
                    Longitude = 34.821857
                },

                new Station
                {
                    Code = 38840,
                    Name = "ויצמן/הבנים",
                    Latitude = 31.865085,
                    Longitude = 34.822237
                },

                new Station
                {
                    Code = 38841,
                    Name = "האירוס/הכלנית",
                    Latitude = 31.865222,
                    Longitude = 34.818957
                },

                new Station
                {
                    Code = 38842,
                    Name = "הכלנית/הנרקיס",
                    Latitude = 31.867597,
                    Longitude = 34.818392
                },

                new Station
                {
                    Code = 38844,
                    Name = "אלי כהן/לוחמי הגטאות",
                    Latitude = 31.86244,
                    Longitude = 34.827023
                },

                new Station
                {
                    Code = 38845,
                    Name = "שבזי/שבת אחים",
                    Latitude = 31.863501,
                    Longitude = 34.828702
                },

                new Station
                {
                    Code = 38846,
                    Name = "שבזי/ויצמן",
                    Latitude = 31.865348,
                    Longitude = 34.827102
                },

                new Station
                {
                    Code = 38847,
                    Name = "חיים בר לב/שדרות יצחק רבין",
                    Latitude = 31.977409,
                    Longitude = 34.763896
                },

                new Station
                {
                    Code = 38848,
                    Name = "מרכז לבריאות הנפש לב השרון",
                    Latitude = 32.300345,
                    Longitude = 34.912708
                },

                new Station
                {
                    Code = 38849,
                    Name = "מרכז לבריאות הנפש לב השרון",
                    Latitude = 32.301347,
                    Longitude = 34.912602
                },

                new Station
                {
                    Code = 38852,
                    Name = "הולצמן/המדע",
                    Latitude = 31.914255,
                    Longitude = 34.807944
                },

                new Station
                {
                    Code = 38854,
                    Name = "מחנה צריפין/מועדון",
                    Latitude = 31.963668,
                    Longitude = 34.836363
                },

                new Station
                {
                    Code = 38855,
                    Name = "הרצל/גולני",
                    Latitude = 31.856115,
                    Longitude = 34.825249
                },

                new Station
                {
                    Code = 38856,
                    Name = "הרותם/הדגניות",
                    Latitude = 31.874963,
                    Longitude = 34.81249
                },

                new Station
                {
                    Code = 38859,
                    Name = "הערבה",
                    Latitude = 32.300035,
                    Longitude = 34.910842
                },

                new Station
                {
                    Code = 38860,
                    Name = "מבוא הגפן/מורד התאנה",
                    Latitude = 32.305234,
                    Longitude = 34.948647
                },

                new Station
                {
                    Code = 38861,
                    Name = "מבוא הגפן/ההרחבה",
                    Latitude = 32.304022,
                    Longitude = 34.943393
                },

                new Station
                {
                    Code = 38862,
                    Name = "ההרחבה א",
                    Latitude = 32.302957,
                    Longitude = 34.940529
                },

                new Station
                {
                    Code = 38863,
                    Name = "ההרחבה ב",
                    Latitude = 32.300264,
                    Longitude = 34.939512
                },

                new Station
                {
                    Code = 38864,
                    Name = "ההרחבה/הותיקים",
                    Latitude = 32.298171,
                    Longitude = 34.938705
                },

                new Station
                {
                    Code = 38865,
                    Name = "רשות שדות התעופה/העליה",
                    Latitude = 31.990876,
                    Longitude = 34.8976
                },

                new Station
                {
                    Code = 38866,
                    Name = "כנף/ברוש",
                    Latitude = 31.998767,
                    Longitude = 34.879725
                },

                new Station
                {
                    Code = 38867,
                    Name = "החבורה/דב הוז",
                    Latitude = 31.883019,
                    Longitude = 34.818708
                },

                new Station
                {
                    Code = 38869,
                    Name = "בית הלוי ה",
                    Latitude = 32.349776,
                    Longitude = 34.926837
                },

                new Station
                {
                    Code = 38870,
                    Name = "הראשונים/כביש 5700",
                    Latitude = 32.352953,
                    Longitude = 34.899465
                },

                new Station
                {
                    Code = 38872,
                    Name = "הגאון בן איש חי/צאלון",
                    Latitude = 31.897286,
                    Longitude = 34.775083
                },

                new Station
                {
                    Code = 38873,
                    Name = "עוקשי/לוי אשכול",
                    Latitude = 31.883941,
                    Longitude = 34.807039
                },

                new Station
                {
                    Code = 38875,
                    Name = "מנוחה ונחלה/יהודה גורודיסקי",
                    Latitude = 31.896762,
                    Longitude = 34.816752
                },

                new Station
                {
                    Code = 38876,
                    Name = "גורודסקי/יחיאל פלדי",
                    Latitude = 31.898463,
                    Longitude = 34.823461
                },

                new Station
                {
                    Code = 38877,
                    Name = "דרך מנחם בגין/יעקב חזן",
                    Latitude = 32.076535,
                    Longitude = 34.904907
                },

                new Station
                {
                    Code = 38878,
                    Name = "דרך הפארק/הרב נריה",
                    Latitude = 32.299994,
                    Longitude = 34.878765
                },

                new Station
                {
                    Code = 38879,
                    Name = "התאנה/הגפן",
                    Latitude = 31.865457,
                    Longitude = 34.859437
                },

                new Station
                {
                    Code = 38880,
                    Name = "התאנה/האלון",
                    Latitude = 31.866772,
                    Longitude = 34.864555
                },

                new Station
                {
                    Code = 38881,
                    Name = "דרך הפרחים/יסמין",
                    Latitude = 31.809325,
                    Longitude = 34.784347
                },

                new Station
                {
                    Code = 38883,
                    Name = "יצחק רבין/פנחס ספיר",
                    Latitude = 31.80037,
                    Longitude = 34.778239
                },

                new Station
                {
                    Code = 38884,
                    Name = "מנחם בגין/יצחק רבין",
                    Latitude = 31.799224,
                    Longitude = 34.782985
                },

                new Station
                {
                    Code = 38885,
                    Name = "חיים הרצוג/דולב",
                    Latitude = 31.800334,
                    Longitude = 34.785069
                },

                new Station
                {
                    Code = 38886,
                    Name = "בית ספר גוונים/ארז",
                    Latitude = 31.802319,
                    Longitude = 34.786735
                },

                new Station
                {
                    Code = 38887,
                    Name = "דרך האילנות/אלון",
                    Latitude = 31.804595,
                    Longitude = 34.786623
                },

                new Station
                {
                    Code = 38888,
                    Name = "דרך האילנות/מנחם בגין",
                    Latitude = 31.805041,
                    Longitude = 34.785098
                },

                new Station
                {
                    Code = 38889,
                    Name = "העצמאות/וייצמן",
                    Latitude = 31.816751,
                    Longitude = 34.782252
                },

                new Station
                {
                    Code = 38890,
                    Name = "וייצמן/מרבד הקסמים",
                    Latitude = 31.816579,
                    Longitude = 34.779753
                },

                new Station
                {
                    Code = 38891,
                    Name = "צאלה/אלמוג",
                    Latitude = 31.801182,
                    Longitude = 34.787199
                },

                new Station
                {
                    Code = 38892,
                    Name = "גן חצב/צאלה",
                    Latitude = 31.802279,
                    Longitude = 34.786055
                },

                new Station
                {
                    Code = 38893,
                    Name = "פינס/לוינסון",
                    Latitude = 31.814676,
                    Longitude = 34.777574
                },

                new Station
                {
                    Code = 38894,
                    Name = "פיינברג/שכביץ",
                    Latitude = 31.813285,
                    Longitude = 34.775928
                },

                new Station
                {
                    Code = 38895,
                    Name = "בן גוריון/פוקס",
                    Latitude = 31.806959,
                    Longitude = 34.773504
                },

                new Station
                {
                    Code = 38898,
                    Name = "לוי אשכול/הרב דוד ישראל",
                    Latitude = 31.884187,
                    Longitude = 34.805494
                },

                new Station
                {
                    Code = 38899,
                    Name = "שושן/אופנהיימר",
                    Latitude = 31.910118,
                    Longitude = 34.805809
                },

                new Station
                {
                    Code = 38901,
                    Name = "הרב דוד ישראל/אריה דולצין",
                    Latitude = 31.882474,
                    Longitude = 34.80506
                },

                new Station
                {
                    Code = 38903,
                    Name = "קרוננברג/ארגמן",
                    Latitude = 31.878667,
                    Longitude = 34.81138
                },

                new Station
                {
                    Code = 38904,
                    Name = "יעקב פריימן/בנימין שמוטקין",
                    Latitude = 31.975479,
                    Longitude = 34.813355
                },

                new Station
                {
                    Code = 38905,
                    Name = "אנילביץ'/שלום אש",
                    Latitude = 31.982177,
                    Longitude = 34.789445
                },

                new Station
                {
                    Code = 38906,
                    Name = "נחמיה/בית העלמין",
                    Latitude = 31.948552,
                    Longitude = 34.822422
                },

                new Station
                {
                    Code = 38907,
                    Name = "יהודה הלוי/יוחנן הסנדלר",
                    Latitude = 31.967732,
                    Longitude = 34.816339
                },

                new Station
                {
                    Code = 38908,
                    Name = "ההגנה/חי''ש",
                    Latitude = 31.893823,
                    Longitude = 34.824617
                },

                new Station
                {
                    Code = 38909,
                    Name = "קרית עקרון/כביש 411",
                    Latitude = 31.854169,
                    Longitude = 34.824714
                },

                new Station
                {
                    Code = 38910,
                    Name = "צומת חולדה/כביש 411",
                    Latitude = 31.811907,
                    Longitude = 34.900793
                },

                new Station
                {
                    Code = 38911,
                    Name = "גרינשפן/יגאל אלון",
                    Latitude = 31.956842,
                    Longitude = 34.814839
                },

                new Station
                {
                    Code = 38912,
                    Name = "השומר/האבות",
                    Latitude = 31.959821,
                    Longitude = 34.814747
                },

                new Station
                {
                    Code = 38913,
                    Name = "משה שרת/יעקב קנר",
                    Latitude = 31.992306,
                    Longitude = 34.75691
                },

                new Station
                {
                    Code = 38914,
                    Name = "הדייגים/הנחשול",
                    Latitude = 31.98497,
                    Longitude = 34.78262
                },

                new Station
                {
                    Code = 38916,
                    Name = "יוסף בורג/משואות יצחק",
                    Latitude = 31.968049,
                    Longitude = 34.818099
                },

                new Station
                {
                    Code = 38917,
                    Name = "יוסף בורג/כתריאל רפפורט",
                    Latitude = 31.968936,
                    Longitude = 34.820043
                },

                new Station
                {
                    Code = 38919,
                    Name = "וייצמן/דרך יצחק רבין",
                    Latitude = 31.923041,
                    Longitude = 34.798033
                },

                new Station
                {
                    Code = 38920,
                    Name = "שדרות משה דיין/יוסף לישנסקי",
                    Latitude = 31.98568,
                    Longitude = 34.764014
                },

                new Station
                {
                    Code = 38921,
                    Name = "השר חיים שפירא/יוסף ספיר",
                    Latitude = 31.992583,
                    Longitude = 34.751999
                },

                new Station
                {
                    Code = 38922,
                    Name = "השר חיים שפירא/הרב שלום ג'רופי",
                    Latitude = 31.990757,
                    Longitude = 34.755683
                },

                new Station
                {
                    Code = 39001,
                    Name = "שדרות יעקב/יוסף הנשיא",
                    Latitude = 31.950254,
                    Longitude = 34.819244
                },

                new Station
                {
                    Code = 39002,
                    Name = "שדרות יעקב/עזרא",
                    Latitude = 31.95111,
                    Longitude = 34.819766
                },

                new Station
                {
                    Code = 39004,
                    Name = "לייב יוספזון/יעקב ברמן",
                    Latitude = 31.905052,
                    Longitude = 34.818909
                },

                new Station
                {
                    Code = 39005,
                    Name = "הרב יעקב ברמן/הרב יהודה צבי מלצר",
                    Latitude = 31.901879,
                    Longitude = 34.819443
                },

                new Station
                {
                    Code = 39006,
                    Name = "ברמן/מלצר",
                    Latitude = 31.90281,
                    Longitude = 34.818922
                },

                new Station
                {
                    Code = 39007,
                    Name = "הנשיא הראשון/מכון ויצמן",
                    Latitude = 31.904567,
                    Longitude = 34.815296
                },

                new Station
                {
                    Code = 39008,
                    Name = "הנשיא הראשון/קיפניס",
                    Latitude = 31.904755,
                    Longitude = 34.816661
                },

                new Station
                {
                    Code = 39012,
                    Name = "הירדן/הערבה",
                    Latitude = 31.937387,
                    Longitude = 34.838609
                },

                new Station
                {
                    Code = 39013,
                    Name = "הירדן/חרוד",
                    Latitude = 31.936925,
                    Longitude = 34.838341
                },

                new Station
                {
                    Code = 39014,
                    Name = "האלונים/הדקל",
                    Latitude = 31.939037,
                    Longitude = 34.831964
                },

                new Station
                {
                    Code = 39017,
                    Name = "האלונים א/הדקל",
                    Latitude = 31.939656,
                    Longitude = 34.832104
                },

                new Station
                {
                    Code = 39018,
                    Name = "פארק תעשיות שילת",
                    Latitude = 31.914324,
                    Longitude = 35.023589
                },

                new Station
                {
                    Code = 39019,
                    Name = "פארק תעשיות שילת",
                    Latitude = 31.914816,
                    Longitude = 35.023028
                },

                new Station
                {
                    Code = 39024,
                    Name = "עיריית מודיעין מכבים רעות",
                    Latitude = 31.908499,
                    Longitude = 35.007955
                },

                new Station
                {
                    Code = 39028,
                    Name = "חיים ברלב/מרדכי מקלף",
                    Latitude = 31.907828,
                    Longitude = 35.000614
                },

                new Station
                {
                    Code = 39029,
                    Name = "חיים ברלב/מרדכי מקלף",
                    Latitude = 31.907603,
                    Longitude = 35.000816
                },

                new Station
                {
                    Code = 39035,
                    Name = "אלמוג סנטר/אפרים קישון",
                    Latitude = 31.941611,
                    Longitude = 34.843114
                },

                new Station
                {
                    Code = 39039,
                    Name = "גן חק''ל/רבאט",
                    Latitude = 31.931068,
                    Longitude = 34.884936
                },

                new Station
                {
                    Code = 39040,
                    Name = "גן חק''ל/רבאט",
                    Latitude = 31.931204,
                    Longitude = 34.884956
                },

                new Station
                {
                    Code = 39041,
                    Name = "קניון צ. רמלה לוד/דוכיפת",
                    Latitude = 31.933379,
                    Longitude = 34.887207
                },

                new Station
                {
                    Code = 39042,
                    Name = "היצירה/התקווה",
                    Latitude = 31.929318,
                    Longitude = 34.880069
                },

                new Station
                {
                    Code = 39043,
                    Name = "היצירה/התקווה",
                    Latitude = 31.929199,
                    Longitude = 34.879993
                },

                new Station
                {
                    Code = 39044,
                    Name = "עמל/היצירה",
                    Latitude = 31.932402,
                    Longitude = 34.881442
                },

                new Station
                {
                    Code = 39049,
                    Name = "פרנקל/ויתקין",
                    Latitude = 31.936159,
                    Longitude = 34.864906
                },

                new Station
                {
                    Code = 39050,
                    Name = "פרנקל/ויתקין",
                    Latitude = 31.936022,
                    Longitude = 34.86495
                },

                new Station
                {
                    Code = 39051,
                    Name = "ישראל פרנקל/דוב הוז",
                    Latitude = 31.935488,
                    Longitude = 34.863972
                },

                new Station
                {
                    Code = 39052,
                    Name = "יוספטל/הדס",
                    Latitude = 31.936109,
                    Longitude = 34.857638
                },

                new Station
                {
                    Code = 39056,
                    Name = "אהרון בוגנים/משה שרת",
                    Latitude = 31.933413,
                    Longitude = 34.853906
                },

                new Station
                {
                    Code = 39057,
                    Name = "גרשון ש''ץ/שמחה הולצברג",
                    Latitude = 31.932532,
                    Longitude = 34.853223
                },

                new Station
                {
                    Code = 39058,
                    Name = "הולצברג/שץ",
                    Latitude = 31.93166,
                    Longitude = 34.853149
                },

                new Station
                {
                    Code = 39059,
                    Name = "אשכול/הרב שפירא",
                    Latitude = 31.929827,
                    Longitude = 34.857194
                },

                new Station
                {
                    Code = 39060,
                    Name = "יהודה שטיין/קרן היסוד",
                    Latitude = 31.926545,
                    Longitude = 34.855866
                },

                new Station
                {
                    Code = 39066,
                    Name = "שמשון הגיבור/המסגד",
                    Latitude = 31.926441,
                    Longitude = 34.866014
                },

                new Station
                {
                    Code = 39068,
                    Name = "ביאליק/חניתה",
                    Latitude = 31.924484,
                    Longitude = 34.870366
                },

                new Station
                {
                    Code = 39069,
                    Name = "ביאליק/ירמיהו",
                    Latitude = 31.92055,
                    Longitude = 34.868205
                },

                new Station
                {
                    Code = 39070,
                    Name = "א.ס לוי/סעדיה מרדכי",
                    Latitude = 31.9209,
                    Longitude = 34.861221
                },

                new Station
                {
                    Code = 39071,
                    Name = "רזיאל/זכריה",
                    Latitude = 31.923666,
                    Longitude = 34.862813
                },

                new Station
                {
                    Code = 39072,
                    Name = "חרוד א",
                    Latitude = 31.912572,
                    Longitude = 34.850134
                },

                new Station
                {
                    Code = 39073,
                    Name = "חרוד/הירדן",
                    Latitude = 31.915977,
                    Longitude = 34.848217
                },

                new Station
                {
                    Code = 39075,
                    Name = "הירדן/דן",
                    Latitude = 31.915489,
                    Longitude = 34.852139
                },

                new Station
                {
                    Code = 39076,
                    Name = "עוזי חיטמן/שושנה דמארי",
                    Latitude = 31.917022,
                    Longitude = 34.868261
                },

                new Station
                {
                    Code = 39077,
                    Name = "עוזי חיטמן/דליה רביקוביץ",
                    Latitude = 31.918782,
                    Longitude = 34.870995
                },

                new Station
                {
                    Code = 39078,
                    Name = "עוזי חיטמן/חנוך לוין",
                    Latitude = 31.918696,
                    Longitude = 34.873447
                },

                new Station
                {
                    Code = 39079,
                    Name = "אהוד מנור/חיטמן",
                    Latitude = 31.916945,
                    Longitude = 34.874461
                },

                new Station
                {
                    Code = 39080,
                    Name = "שושנה דמארי/אהוד מנור",
                    Latitude = 31.916141,
                    Longitude = 34.872134
                },

                new Station
                {
                    Code = 39081,
                    Name = "שושנה דמארי/אהוד מנור",
                    Latitude = 31.916235,
                    Longitude = 34.873652
                },

                new Station
                {
                    Code = 39082,
                    Name = "אהוד מנור/עוזי חיטמן",
                    Latitude = 31.917837,
                    Longitude = 34.874646
                },

                new Station
                {
                    Code = 39083,
                    Name = "עוזי חיטמן/שושנה דמארי",
                    Latitude = 31.917072,
                    Longitude = 34.868374
                },

                new Station
                {
                    Code = 39086,
                    Name = "דרך האורנים/ברוש",
                    Latitude = 31.92072,
                    Longitude = 35.016294
                },

                new Station
                {
                    Code = 39087,
                    Name = "שקד/האורנים",
                    Latitude = 31.922468,
                    Longitude = 35.01438
                },

                new Station
                {
                    Code = 39088,
                    Name = "דרך החרוב/גפן",
                    Latitude = 31.919711,
                    Longitude = 35.020017
                },

                new Station
                {
                    Code = 39089,
                    Name = "דרך החרוב/גפן",
                    Latitude = 31.919795,
                    Longitude = 35.019979
                },

                new Station
                {
                    Code = 39090,
                    Name = "החרוב א",
                    Latitude = 31.919163,
                    Longitude = 35.02323
                },

                new Station
                {
                    Code = 39091,
                    Name = "החרוב א",
                    Latitude = 31.919207,
                    Longitude = 35.0234
                },

                new Station
                {
                    Code = 39092,
                    Name = "כפר רות",
                    Latitude = 31.910544,
                    Longitude = 35.034349
                },

                new Station
                {
                    Code = 39093,
                    Name = "כפר רות",
                    Latitude = 31.910485,
                    Longitude = 35.034441
                },

                new Station
                {
                    Code = 39100,
                    Name = "החוף הירוק",
                    Latitude = 32.270586,
                    Longitude = 34.837598
                },

                new Station
                {
                    Code = 39101,
                    Name = "רמת פולג",
                    Latitude = 32.272632,
                    Longitude = 34.838726
                },

                new Station
                {
                    Code = 39102,
                    Name = "רמת פולג",
                    Latitude = 32.27242,
                    Longitude = 34.838508
                },

                new Station
                {
                    Code = 39103,
                    Name = "אהוד מנור/מנחם בגין",
                    Latitude = 32.274543,
                    Longitude = 34.8413
                },

                new Station
                {
                    Code = 39104,
                    Name = "אהוד מנור/מנחם בגין",
                    Latitude = 32.27487,
                    Longitude = 34.842868
                },

                new Station
                {
                    Code = 39105,
                    Name = "שזר/אמנון ותמר",
                    Latitude = 32.27572,
                    Longitude = 34.845201
                },

                new Station
                {
                    Code = 39106,
                    Name = "זלמן שז''ר/אמנון ותמר",
                    Latitude = 32.276562,
                    Longitude = 34.846404
                },

                new Station
                {
                    Code = 39107,
                    Name = "אמנון ותמר/זלמן שז''ר",
                    Latitude = 32.276837,
                    Longitude = 34.847509
                },

                new Station
                {
                    Code = 39108,
                    Name = "אודם/שהם",
                    Latitude = 32.256581,
                    Longitude = 34.864843
                },

                new Station
                {
                    Code = 39109,
                    Name = "שדרות גולדה מאיר/משעול קיפניס",
                    Latitude = 32.27659,
                    Longitude = 34.85165
                },

                new Station
                {
                    Code = 39110,
                    Name = "צומת אביר/כביש 553",
                    Latitude = 32.272698,
                    Longitude = 34.856933
                },

                new Station
                {
                    Code = 39111,
                    Name = "יהלום/ספיר",
                    Latitude = 32.261931,
                    Longitude = 34.863096
                },

                new Station
                {
                    Code = 39112,
                    Name = "יהלום/שני",
                    Latitude = 32.264446,
                    Longitude = 34.862627
                },

                new Station
                {
                    Code = 39113,
                    Name = "מזכירות",
                    Latitude = 32.260481,
                    Longitude = 34.864793
                },

                new Station
                {
                    Code = 39114,
                    Name = "האורזים/האר''י",
                    Latitude = 32.312257,
                    Longitude = 34.869559
                },

                new Station
                {
                    Code = 39115,
                    Name = "איקאה/שדרות גיבורי ישראל",
                    Latitude = 32.274579,
                    Longitude = 34.858842
                },

                new Station
                {
                    Code = 39117,
                    Name = "שד. עובד בן עמי/קהילת צפת",
                    Latitude = 32.306182,
                    Longitude = 34.84364
                },

                new Station
                {
                    Code = 39118,
                    Name = "אירוס הארגמן/חבצלת החוף",
                    Latitude = 32.285717,
                    Longitude = 34.844815
                },

                new Station
                {
                    Code = 39119,
                    Name = "זלמן שז''ר/שד. בן גוריון",
                    Latitude = 32.277813,
                    Longitude = 34.848946
                },

                new Station
                {
                    Code = 39120,
                    Name = "חבצלת החוף/אירוס הארגמן",
                    Latitude = 32.284726,
                    Longitude = 34.846795
                },

                new Station
                {
                    Code = 39121,
                    Name = "שניאור/בן גוריון",
                    Latitude = 32.282745,
                    Longitude = 34.847761
                },

                new Station
                {
                    Code = 39122,
                    Name = "זלמן שניאור/אריה לייב יפה",
                    Latitude = 32.282812,
                    Longitude = 34.847562
                },

                new Station
                {
                    Code = 39123,
                    Name = "ש''י עגנון/קרן היסוד",
                    Latitude = 32.282979,
                    Longitude = 34.84922
                },

                new Station
                {
                    Code = 39124,
                    Name = "ש''י עגנון/קרן היסוד",
                    Latitude = 32.283021,
                    Longitude = 34.849395
                },

                new Station
                {
                    Code = 39125,
                    Name = "גבעת האירוסים/אירוס הארגמן",
                    Latitude = 32.287046,
                    Longitude = 34.846117
                },

                new Station
                {
                    Code = 39126,
                    Name = "חבצלת החוף/אירוס הארגמן",
                    Latitude = 32.286259,
                    Longitude = 34.848222
                },

                new Station
                {
                    Code = 39127,
                    Name = "הבריגדה היהודית/החפץ חיים",
                    Latitude = 32.296835,
                    Longitude = 34.844671
                },

                new Station
                {
                    Code = 39128,
                    Name = "שד. בן גוריון/שמחה ארליך",
                    Latitude = 32.300631,
                    Longitude = 34.844687
                },

                new Station
                {
                    Code = 39129,
                    Name = "דוד רזניק/החפץ חיים",
                    Latitude = 32.295287,
                    Longitude = 34.845476
                },

                new Station
                {
                    Code = 39130,
                    Name = "לוי אשכול/יעקב דורי",
                    Latitude = 32.296581,
                    Longitude = 34.846977
                },

                new Station
                {
                    Code = 39131,
                    Name = "אריה מוצקין/ד''ר סטופ",
                    Latitude = 32.296783,
                    Longitude = 34.848266
                },

                new Station
                {
                    Code = 39132,
                    Name = "אריה מוצקין/ד''ר סטופ",
                    Latitude = 32.29704,
                    Longitude = 34.848792
                },

                new Station
                {
                    Code = 39133,
                    Name = "הבריגדה היהודית/חיים ברלב",
                    Latitude = 32.298042,
                    Longitude = 34.845308
                },

                new Station
                {
                    Code = 39134,
                    Name = "בלומנפלד/יעקב דורי",
                    Latitude = 32.298675,
                    Longitude = 34.846832
                },

                new Station
                {
                    Code = 39135,
                    Name = "שדרות בן גוריון/שמחה ארליך",
                    Latitude = 32.299915,
                    Longitude = 34.846765
                },

                new Station
                {
                    Code = 39136,
                    Name = "לוי אשכול/יעקב דורי",
                    Latitude = 32.298453,
                    Longitude = 34.848019
                },

                new Station
                {
                    Code = 39138,
                    Name = "אריה מוצקין/שמחה ארליך",
                    Latitude = 32.299316,
                    Longitude = 34.849289
                },

                new Station
                {
                    Code = 39139,
                    Name = "בן עמי/שמחה ארליך",
                    Latitude = 32.302867,
                    Longitude = 34.843196
                },

                new Station
                {
                    Code = 39141,
                    Name = "בית ספר אלדד/שדרות גולדה מאיר",
                    Latitude = 32.276979,
                    Longitude = 34.854475
                },

                new Station
                {
                    Code = 39142,
                    Name = "בן עמי/קהילת צפת",
                    Latitude = 32.3052,
                    Longitude = 34.842963
                },

                new Station
                {
                    Code = 39144,
                    Name = "'נאות גולדה א",
                    Latitude = 32.277828,
                    Longitude = 34.851875
                },

                new Station
                {
                    Code = 39145,
                    Name = "'נאות גולדה ב",
                    Latitude = 32.278509,
                    Longitude = 34.85214
                },

                new Station
                {
                    Code = 39146,
                    Name = "נאות גולדה/חזני",
                    Latitude = 32.278717,
                    Longitude = 34.852186
                },

                new Station
                {
                    Code = 39147,
                    Name = "פנקס/57",
                    Latitude = 32.323511,
                    Longitude = 34.874761
                },

                new Station
                {
                    Code = 39148,
                    Name = "ש''י עגנון/יאנוש קורצ'אק",
                    Latitude = 32.280743,
                    Longitude = 34.851721
                },

                new Station
                {
                    Code = 39149,
                    Name = "רופין/שד. פנחס לבון",
                    Latitude = 32.280794,
                    Longitude = 34.852967
                },

                new Station
                {
                    Code = 39150,
                    Name = "ש''י עגנון/יצחק אפשטיין",
                    Latitude = 32.281577,
                    Longitude = 34.850372
                },

                new Station
                {
                    Code = 39151,
                    Name = "ש''י עגנון/יצחק אפשטיין",
                    Latitude = 32.281435,
                    Longitude = 34.850746
                },

                new Station
                {
                    Code = 39152,
                    Name = "זלמן שניאור/ש''י עגנון",
                    Latitude = 32.283894,
                    Longitude = 34.849358
                },

                new Station
                {
                    Code = 39153,
                    Name = "לבון/עגנון",
                    Latitude = 32.282133,
                    Longitude = 34.853363
                },

                new Station
                {
                    Code = 39154,
                    Name = "יצחק קצנלסון/ד''ר וייסלברגר",
                    Latitude = 32.284632,
                    Longitude = 34.852016
                },

                new Station
                {
                    Code = 39155,
                    Name = "רוטנברג/נורדאו",
                    Latitude = 32.279793,
                    Longitude = 34.854338
                },

                new Station
                {
                    Code = 39156,
                    Name = "פנחס לבון/ישראל בר יהודה",
                    Latitude = 32.283161,
                    Longitude = 34.854158
                },

                new Station
                {
                    Code = 39157,
                    Name = "השר ישראל בר יהודה/נורדאו",
                    Latitude = 32.281378,
                    Longitude = 34.856436
                },

                new Station
                {
                    Code = 39158,
                    Name = "מנדלי מוכר ספרים/נורדאו",
                    Latitude = 32.283807,
                    Longitude = 34.85734
                },

                new Station
                {
                    Code = 39160,
                    Name = "יצחק קצנלסון/שדרות פנחס לבון",
                    Latitude = 32.285573,
                    Longitude = 34.85359
                },

                new Station
                {
                    Code = 39161,
                    Name = "לבון/וייסלברגר תאודור",
                    Latitude = 32.285451,
                    Longitude = 34.854552
                },

                new Station
                {
                    Code = 39162,
                    Name = "שד. בן צבי/שד. פנחס לבון",
                    Latitude = 32.28675,
                    Longitude = 34.854833
                },

                new Station
                {
                    Code = 39163,
                    Name = "לבון/שלום עליכם",
                    Latitude = 32.286557,
                    Longitude = 34.855052
                },

                new Station
                {
                    Code = 39164,
                    Name = "הרב אבו חצירא/הרב רפאל אנקווה",
                    Latitude = 32.285705,
                    Longitude = 34.856197
                },

                new Station
                {
                    Code = 39166,
                    Name = "מרכז ביג/שדרות גיבורי ישראל",
                    Latitude = 32.276114,
                    Longitude = 34.859673
                },

                new Station
                {
                    Code = 39167,
                    Name = "שדרות גיבורי ישראל/הבונים",
                    Latitude = 32.279935,
                    Longitude = 34.860652
                },

                new Station
                {
                    Code = 39168,
                    Name = "שד. גיבורי ישראל/הבונים",
                    Latitude = 32.280886,
                    Longitude = 34.861197
                },

                new Station
                {
                    Code = 39170,
                    Name = "שד. גיבורי ישראל/התרופה",
                    Latitude = 32.282477,
                    Longitude = 34.861469
                },

                new Station
                {
                    Code = 39171,
                    Name = "שדרות גיבורי ישראל/האומנות",
                    Latitude = 32.277345,
                    Longitude = 34.860081
                },

                new Station
                {
                    Code = 39172,
                    Name = "האומנות/יד חרוצים",
                    Latitude = 32.278292,
                    Longitude = 34.863479
                },

                new Station
                {
                    Code = 39173,
                    Name = "ת.רכבת ספיר",
                    Latitude = 32.28034,
                    Longitude = 34.864471
                },

                new Station
                {
                    Code = 39174,
                    Name = "שדרות גיבורי ישראל/התרופה",
                    Latitude = 32.283436,
                    Longitude = 34.862011
                },

                new Station
                {
                    Code = 39175,
                    Name = "התרופה/אריה רגב",
                    Latitude = 32.282698,
                    Longitude = 34.863651
                },

                new Station
                {
                    Code = 39176,
                    Name = "יד חרוצים/השיאים",
                    Latitude = 32.2847,
                    Longitude = 34.865661
                },

                new Station
                {
                    Code = 39177,
                    Name = "בית חרושת טבע",
                    Latitude = 32.285635,
                    Longitude = 34.862489
                },

                new Station
                {
                    Code = 39178,
                    Name = "בית חרושת אביק",
                    Latitude = 32.285429,
                    Longitude = 34.862662
                },

                new Station
                {
                    Code = 39179,
                    Name = "הדרים/הראשונים",
                    Latitude = 32.264318,
                    Longitude = 34.931993
                },

                new Station
                {
                    Code = 39180,
                    Name = "שד. גיבורי ישראל/האומנות",
                    Latitude = 32.277657,
                    Longitude = 34.859914
                },

                new Station
                {
                    Code = 39181,
                    Name = "השלום/הגביש",
                    Latitude = 32.286298,
                    Longitude = 34.863673
                },

                new Station
                {
                    Code = 39182,
                    Name = "המחשב/הצורן",
                    Latitude = 32.287621,
                    Longitude = 34.86501
                },

                new Station
                {
                    Code = 39183,
                    Name = "עירייה",
                    Latitude = 32.28872,
                    Longitude = 34.86565
                },

                new Station
                {
                    Code = 39184,
                    Name = "יצחק גרינבוים/אפרים אלנקווה",
                    Latitude = 32.294995,
                    Longitude = 34.849667
                },

                new Station
                {
                    Code = 39185,
                    Name = "יצחק גרינבוים/ד''ר שמורק",
                    Latitude = 32.294705,
                    Longitude = 34.851791
                },

                new Station
                {
                    Code = 39186,
                    Name = "גרינבוים/בן צבי",
                    Latitude = 32.294485,
                    Longitude = 34.851985
                },

                new Station
                {
                    Code = 39187,
                    Name = "בן צבי/גרנבוים",
                    Latitude = 32.293898,
                    Longitude = 34.852491
                },

                new Station
                {
                    Code = 39188,
                    Name = "משטרה/בן צבי",
                    Latitude = 32.294922,
                    Longitude = 34.852932
                },

                new Station
                {
                    Code = 39189,
                    Name = "הנרייטה סולד/סטופ",
                    Latitude = 32.29569,
                    Longitude = 34.851774
                },

                new Station
                {
                    Code = 39190,
                    Name = "הנביאים/שד. בן צבי",
                    Latitude = 32.29686,
                    Longitude = 34.853099
                },

                new Station
                {
                    Code = 39191,
                    Name = "הנביאים/נצח ישראל",
                    Latitude = 32.297185,
                    Longitude = 34.85401
                },

                new Station
                {
                    Code = 39193,
                    Name = "שד. בן צבי/שמחה ארליך",
                    Latitude = 32.297873,
                    Longitude = 34.85357
                },

                new Station
                {
                    Code = 39194,
                    Name = "יחזקאל/הנביאים",
                    Latitude = 32.297381,
                    Longitude = 34.855518000000004
                },

                new Station
                {
                    Code = 39195,
                    Name = "בן צבי/פלוגת מכבי",
                    Latitude = 32.300044,
                    Longitude = 34.853804
                },

                new Station
                {
                    Code = 39196,
                    Name = "בן צבי/נחום",
                    Latitude = 32.301071,
                    Longitude = 34.85428
                },

                new Station
                {
                    Code = 39197,
                    Name = "נחום/ישעיהו",
                    Latitude = 32.300595,
                    Longitude = 34.854651
                },

                new Station
                {
                    Code = 39198,
                    Name = "נחום/ישעיהו",
                    Latitude = 32.30046,
                    Longitude = 34.854938
                },

                new Station
                {
                    Code = 39199,
                    Name = "בית הכנסת הבוכרים/בן בוזי",
                    Latitude = 32.297593,
                    Longitude = 34.855673
                },

                new Station
                {
                    Code = 39200,
                    Name = "יחזקאל בן בוזי/נחום",
                    Latitude = 32.299615,
                    Longitude = 34.856236
                },

                new Station
                {
                    Code = 39201,
                    Name = "'יחזקאל/נחום א",
                    Latitude = 32.299807,
                    Longitude = 34.856413
                },

                new Station
                {
                    Code = 39203,
                    Name = "בן צבי/נחום",
                    Latitude = 32.302485,
                    Longitude = 34.854339
                },

                new Station
                {
                    Code = 39204,
                    Name = "בית ספר טשרניחובסקי/בן צבי",
                    Latitude = 32.309555,
                    Longitude = 34.854597
                },

                new Station
                {
                    Code = 39205,
                    Name = "בית ספר טשרניחובסקי/בן צבי",
                    Latitude = 32.309604,
                    Longitude = 34.854846
                },

                new Station
                {
                    Code = 39207,
                    Name = "מרכז גריאטרי דורות/הנביאים",
                    Latitude = 32.296945,
                    Longitude = 34.858045
                },

                new Station
                {
                    Code = 39208,
                    Name = "בן עמי/קניג",
                    Latitude = 32.310048,
                    Longitude = 34.84455
                },

                new Station
                {
                    Code = 39209,
                    Name = "שדרות בן עמי/צהלה",
                    Latitude = 32.313311,
                    Longitude = 34.845828
                },

                new Station
                {
                    Code = 39210,
                    Name = "בן עמי/צהלה",
                    Latitude = 32.314372,
                    Longitude = 34.845906
                },

                new Station
                {
                    Code = 39211,
                    Name = "שד. עובד בן עמי/ברוך רם",
                    Latitude = 32.316395,
                    Longitude = 34.846121
                },

                new Station
                {
                    Code = 39212,
                    Name = "ז'בוטינסקי/סמילנסקי",
                    Latitude = 32.321443,
                    Longitude = 34.848668
                },

                new Station
                {
                    Code = 39213,
                    Name = "שד. בן אב''י/הרעות",
                    Latitude = 32.319471,
                    Longitude = 34.848923
                },

                new Station
                {
                    Code = 39214,
                    Name = "ז'בוטינסקי/קרליבך",
                    Latitude = 32.324404,
                    Longitude = 34.849462
                },

                new Station
                {
                    Code = 39215,
                    Name = "בן צבי/הגר''א",
                    Latitude = 32.311076,
                    Longitude = 34.854911
                },

                new Station
                {
                    Code = 39216,
                    Name = "שד. בן צבי/היהלומן אברהם",
                    Latitude = 32.310926,
                    Longitude = 34.855105
                },

                new Station
                {
                    Code = 39217,
                    Name = "בית ספר טשרניחובסקי/הגר''א",
                    Latitude = 32.310378,
                    Longitude = 34.856482
                },

                new Station
                {
                    Code = 39218,
                    Name = "עובדיה בן שלום/שלום שבזי",
                    Latitude = 32.313369,
                    Longitude = 34.855772
                },

                new Station
                {
                    Code = 39220,
                    Name = "שד. בנימין/שלום שבזי",
                    Latitude = 32.315191,
                    Longitude = 34.85431
                },

                new Station
                {
                    Code = 39221,
                    Name = "שד. בנימין/הרב טביב",
                    Latitude = 32.315887,
                    Longitude = 34.854722
                },

                new Station
                {
                    Code = 39222,
                    Name = "שד. בן אב''י/שד. בנימין",
                    Latitude = 32.317684,
                    Longitude = 34.854885
                },

                new Station
                {
                    Code = 39223,
                    Name = "עובדיה בן שלום/רמב''ם",
                    Latitude = 32.315389,
                    Longitude = 34.856864
                },

                new Station
                {
                    Code = 39224,
                    Name = "עובדיה בן שלום/רמב''ם",
                    Latitude = 32.315897,
                    Longitude = 34.857335
                },

                new Station
                {
                    Code = 39225,
                    Name = "אליעזר בן יהודה/אברהם אבו שדיד",
                    Latitude = 32.319667,
                    Longitude = 34.851035
                },

                new Station
                {
                    Code = 39226,
                    Name = "סמילנסקי/ירמיהו הלפרין",
                    Latitude = 32.322012,
                    Longitude = 34.8497
                },

                new Station
                {
                    Code = 39228,
                    Name = "סמילנסקי/שדרות ירושלים",
                    Latitude = 32.324634,
                    Longitude = 34.852055
                },

                new Station
                {
                    Code = 39229,
                    Name = "דיזנגוף/ירושלים",
                    Latitude = 32.326185,
                    Longitude = 34.851932
                },

                new Station
                {
                    Code = 39230,
                    Name = "סמילנסקי/שד. ירושלים",
                    Latitude = 32.324914,
                    Longitude = 34.852771
                },

                new Station
                {
                    Code = 39231,
                    Name = "שדרות בן אב''י/שדרות בנימין",
                    Latitude = 32.318227,
                    Longitude = 34.85454
                },

                new Station
                {
                    Code = 39232,
                    Name = "שד. בן אב''י/שד. בנימין",
                    Latitude = 32.318437,
                    Longitude = 34.855357
                },

                new Station
                {
                    Code = 39233,
                    Name = "שד. בנימין/תחכמוני",
                    Latitude = 32.320059,
                    Longitude = 34.855471
                },

                new Station
                {
                    Code = 39234,
                    Name = "שד. בנימין/תחכמוני",
                    Latitude = 32.321473,
                    Longitude = 34.856082
                },

                new Station
                {
                    Code = 39235,
                    Name = "שד.ירושלים/שד. בנימין",
                    Latitude = 32.323668,
                    Longitude = 34.855526
                },

                new Station
                {
                    Code = 39236,
                    Name = "שד. בנימין/שד. ירושלים",
                    Latitude = 32.323959,
                    Longitude = 34.856728
                },

                new Station
                {
                    Code = 39237,
                    Name = "שד. בנימין/שד. ירושלים",
                    Latitude = 32.324201,
                    Longitude = 34.856548
                },

                new Station
                {
                    Code = 39238,
                    Name = "האר''י/הרב חרל''פ",
                    Latitude = 32.312226,
                    Longitude = 34.860947
                },

                new Station
                {
                    Code = 39239,
                    Name = "האר''י/דן שומרון",
                    Latitude = 32.312051,
                    Longitude = 34.860675
                },

                new Station
                {
                    Code = 39240,
                    Name = "פתח תקווה/אצ''ל",
                    Latitude = 32.317886,
                    Longitude = 34.857972
                },

                new Station
                {
                    Code = 39242,
                    Name = "פתח תקווה/בן אבי",
                    Latitude = 32.317978,
                    Longitude = 34.857753
                },

                new Station
                {
                    Code = 39243,
                    Name = "פתח תקווה/אפרים אהרונסון",
                    Latitude = 32.322091,
                    Longitude = 34.859639
                },

                new Station
                {
                    Code = 39244,
                    Name = "פתח תקווה/טיומקין",
                    Latitude = 32.321309,
                    Longitude = 34.859504
                },

                new Station
                {
                    Code = 39245,
                    Name = "פתח תקווה/החלוצים",
                    Latitude = 32.325907,
                    Longitude = 34.860642
                },

                new Station
                {
                    Code = 39246,
                    Name = "הרצל/עולי הגרדום",
                    Latitude = 32.325412,
                    Longitude = 34.864958
                },

                new Station
                {
                    Code = 39247,
                    Name = "הרצל/עולי הגרדום",
                    Latitude = 32.325694,
                    Longitude = 34.864841
                },

                new Station
                {
                    Code = 39248,
                    Name = "אוסישקין/שד. ירושלים",
                    Latitude = 32.326482,
                    Longitude = 34.851215
                },

                new Station
                {
                    Code = 39249,
                    Name = "אוסישקין/ראשון לציון",
                    Latitude = 32.329177,
                    Longitude = 34.851367
                },

                new Station
                {
                    Code = 39250,
                    Name = "דיזנגוף/דוד רמז",
                    Latitude = 32.32771,
                    Longitude = 34.852699
                },

                new Station
                {
                    Code = 39251,
                    Name = "כיכר העצמאות",
                    Latitude = 32.331073,
                    Longitude = 34.851149
                },

                new Station
                {
                    Code = 39252,
                    Name = "דיזנגוף/הרצל",
                    Latitude = 32.329606,
                    Longitude = 34.854308
                },

                new Station
                {
                    Code = 39253,
                    Name = "ששת הימים/ויצמן",
                    Latitude = 32.331575,
                    Longitude = 34.859631
                },

                new Station
                {
                    Code = 39254,
                    Name = "כיכר פרלמן",
                    Latitude = 32.332163,
                    Longitude = 34.856506
                },

                new Station
                {
                    Code = 39255,
                    Name = "דוד המלך/שד. ניצה",
                    Latitude = 32.334329,
                    Longitude = 34.852307
                },

                new Station
                {
                    Code = 39256,
                    Name = "בי''ס יהודה הנשיא",
                    Latitude = 32.337778,
                    Longitude = 34.854235
                },

                new Station
                {
                    Code = 39258,
                    Name = "אנדריוס/בורוכוב",
                    Latitude = 32.335749,
                    Longitude = 34.856207
                },

                new Station
                {
                    Code = 39259,
                    Name = "בורוכוב/יהודה הנשיא",
                    Latitude = 32.33777,
                    Longitude = 34.856468
                },

                new Station
                {
                    Code = 39260,
                    Name = "בורוכוב/יהודה הנשיא",
                    Latitude = 32.337985,
                    Longitude = 34.856332
                },

                new Station
                {
                    Code = 39261,
                    Name = "ד''ר יהושע טהון/ליאון רייך",
                    Latitude = 32.339815,
                    Longitude = 34.856594
                },

                new Station
                {
                    Code = 39262,
                    Name = "הרב הרצוג/שלמה המלך",
                    Latitude = 32.341132,
                    Longitude = 34.854239
                },

                new Station
                {
                    Code = 39263,
                    Name = "יהושע טהון/איכילוב",
                    Latitude = 32.339485,
                    Longitude = 34.856422
                },

                new Station
                {
                    Code = 39264,
                    Name = "יהושע טהון/הרצוג",
                    Latitude = 32.341076,
                    Longitude = 34.856578
                },

                new Station
                {
                    Code = 39265,
                    Name = "ד''ר יהושע טהון/הרב הרצוג",
                    Latitude = 32.341278,
                    Longitude = 34.856356
                },

                new Station
                {
                    Code = 39267,
                    Name = "ת. מרכזית נתניה",
                    Latitude = 32.327371,
                    Longitude = 34.858238
                },

                new Station
                {
                    Code = 39269,
                    Name = "ויצמן/הרצל",
                    Latitude = 32.328953,
                    Longitude = 34.858496
                },

                new Station
                {
                    Code = 39270,
                    Name = "שוק עירוני",
                    Latitude = 32.330186,
                    Longitude = 34.858976
                },

                new Station
                {
                    Code = 39271,
                    Name = "ויצמן/ששת הימים",
                    Latitude = 32.331154,
                    Longitude = 34.858841
                },

                new Station
                {
                    Code = 39272,
                    Name = "מלחמת ששת הימים/ישראל זנגוויל",
                    Latitude = 32.331518,
                    Longitude = 34.861524
                },

                new Station
                {
                    Code = 39273,
                    Name = "הרצל/מינץ",
                    Latitude = 32.326819,
                    Longitude = 34.862473
                },

                new Station
                {
                    Code = 39274,
                    Name = "הרצל/בנימין מינץ",
                    Latitude = 32.326176,
                    Longitude = 34.863338
                },

                new Station
                {
                    Code = 39275,
                    Name = "דרך רזיאל/היהלום",
                    Latitude = 32.329126,
                    Longitude = 34.861776
                },

                new Station
                {
                    Code = 39276,
                    Name = "דרך רזיאל/מלחמת ששת הימים",
                    Latitude = 32.331684,
                    Longitude = 34.863264
                },

                new Station
                {
                    Code = 39277,
                    Name = "דרך רזיאל/מלחמת ששת הימים",
                    Latitude = 32.331629,
                    Longitude = 34.86348
                },

                new Station
                {
                    Code = 39278,
                    Name = "הרב יצחק ניסנבוים/משה גליקסון",
                    Latitude = 32.33108,
                    Longitude = 34.865072
                },

                new Station
                {
                    Code = 39279,
                    Name = "דרך רזיאל/הרב ריינס",
                    Latitude = 32.33235,
                    Longitude = 34.864247
                },

                new Station
                {
                    Code = 39280,
                    Name = "דרך רזיאל/הרב ריינס",
                    Latitude = 32.333132,
                    Longitude = 34.865237
                },

                new Station
                {
                    Code = 39281,
                    Name = "י.ח. ברנר/שד. חיים וייצמן",
                    Latitude = 32.334658,
                    Longitude = 34.859208
                },

                new Station
                {
                    Code = 39282,
                    Name = "ויצמן/שפירא משה",
                    Latitude = 32.334382,
                    Longitude = 34.859411
                },

                new Station
                {
                    Code = 39283,
                    Name = "שדרות וייצמן/יהודה הנשיא",
                    Latitude = 32.337669,
                    Longitude = 34.859541
                }

				#endregion

				//new Station
				//{
				//    Code = 2,
				//    Name = "Beit Inbar/Canfei Nesharim",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 3,
				//    Name = "Merkaz Shetner/Canfei Nesharim",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 4,
				//    Name = "Yamin Avot",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 5,
				//    Name = "Harav Tzvi Yehuda/Shd' Ha'Meiri",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 6,
				//    Name = "Mercaz Harav/Harav Tzvi Yehuda",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 7,
				//    Name = "Gesher Ha'Meitarim/Shd' Hertzel",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 8,
				//    Name = "T. Merkazit Jerusalem/Yafo",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 9,
				//    Name = "Binyanei Ha'uma/Ha'nasi Ha'Shishi",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 10,
				//    Name = "Henyon Ha'leom/Shderot Ha'nasi Ha'Shishi",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 11,
				//    Name = "Misrad Ha'hutz/Shd' Rabin",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 12,
				//    Name = "Shahal/Heler",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 13,
				//    Name = "Shahal Alef",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 14,
				//    Name = "Shahal/Harav Gold",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 15,
				//    Name = "Shahal Beit",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//},

				//new Station
				//{
				//    Code = 16,
				//    Name = "Shahal / Harav Hertzog",
				//    Latitude = (double)rnd.Next(3100000, 33300000) / 1000000,
				//    Longitude = (double)rnd.Next(3430000, 35500000) / 1000000
				//}


			};


            //38831
            int count = 0;
            ListLines = new List<Line>
            {
                new Line
                {
                    Id = 1,
                    Code = 111,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = ListStations[count++ * 0].Code,
                    LastStation = ListStations[StationsInEachBus * count].Code
                },

                new Line
                {
                    Id = 2,
                    Code = 222,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = ListStations[StationsInEachBus * count++ + 1].Code,
                    LastStation = ListStations[StationsInEachBus * count].Code
                },

                new Line
                {
                    Id = 3,
                    Code = 333,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = ListStations[StationsInEachBus * count++ + 1].Code,
                    LastStation = ListStations[StationsInEachBus * count].Code
                },

                new Line
                {
                    Id = 4,
                    Code = 444,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = ListStations[StationsInEachBus * count++ + 1].Code,
                    LastStation = ListStations[StationsInEachBus * count].Code

                },

                new Line
                {
                    Id = 5,
                    Code = 555,
                    Area = (Areas)rnd.Next(0, 5),
                    FirstStation = ListStations[StationsInEachBus * count++ + 1].Code,
                    LastStation = ListStations[StationsInEachBus * count].Code

                },
            };

            ListLineStations = new List<LineStation>();

            for (int i = 0; i < ListLines.Count; i++)
            {
                for (int j = 0; j < StationsInEachBus; j++)
                {
                    ListLineStations.Add(
                    new LineStation
                    {
                        isActive=true,
                        LineId = ListLines[i].Id,
                        LineStationIndex = j,
                        StationId = ListStations[(i * 4 + j + 1) % ListStations.Count].Code,
                        PrevStation = (j == 0) ? ListStations[(i * 4 + j + 1) % ListStations.Count].Code : ListStations[(i * 4 + j) % ListStations.Count].Code,
                        NextStation = (j == StationsInEachBus - 1) ? ListStations[(i * 4 + j + 1) % ListStations.Count].Code : ListStations[(i * 4 + j + 2) % ListStations.Count].Code
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
                            isActive = true,
                            DistFromLastStation = dist,
                            Station1 = ListLineStations[i].StationId,
                            Station2 = ListLineStations[i + 1].StationId,
                            TimeSinceLastStation = DateTime.Now.AddHours(dist * 1000 / rnd.Next(30, 50)) - DateTime.Now
                        });
                }
                if (ListLineStations[i].PrevStation == ListLineStations[i].StationId)
                {
                    ListAdjacentStations.Add(
                        new AdjacentStations
                        {
                            isActive = true,
                            DistFromLastStation = 0,
                            Station1 = ListLineStations[i].StationId,
                            Station2 = ListLineStations[i].StationId,
                            TimeSinceLastStation = new TimeSpan(0)
                        });
                }
                if (ListLineStations[i].NextStation == ListLineStations[i].StationId)
                {
                    ListAdjacentStations.Add(
                    new AdjacentStations
                    {
                        isActive = true,
                        DistFromLastStation = 0,
                        Station1 = ListLineStations[i].StationId,
                        Station2 = ListLineStations[i].StationId,
                        TimeSinceLastStation = new TimeSpan(0)
                    });
                }
            }

            ListUsers = new List<User>()
            {
                new User
                {
                    isActive=true,
                    Admin = true,
                    Password = "Admin",
                    UserName = "Admin"
                },

                 new User
                {
                     isActive=true,
                    Admin = true,
                    Password = "#MAGA",
                    UserName = "Donald J Trump"
                },

                new User
                {
                     isActive=true,
                    Admin = false,
                    UserName = "Noam",
                    Password = "qwerty"
                },

                new User
                {
                     isActive=true,
                    Admin = false,
                    UserName = "Achiya",
                    Password = "12345678"
                },
            };

            ListLineTrips = new List<LineTrip>();
            int id = 0;
            foreach (var line in ListLines)
            {
                TimeSpan t = TimeSpan.Zero;
                var a = ListLineStations.FindAll(p => p.LineId == line.Id);
                for (int i = 0; i < a.Count - 1; i++)
                {
                    t += ListAdjacentStations.Find(p => p.Station1 == a[i].StationId
                            && p.Station2 == a[i + 1].StationId).TimeSinceLastStation;
                }
                var c = new TimeSpan(rnd.Next(0, 8), rnd.Next(0, 59), rnd.Next(0, 59));
                ListLineTrips.Add(new LineTrip
                {
                    isActive = true,
                    StartAt = c,
                    FinishAt = t + c,
                    Frequency = new TimeSpan(0, rnd.Next(30, 50), 0),
                    Id = id++,
                    LineId = line.Id,
                });
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
                    isActive = true,
                    LicenseNum = plateNumber,
                    FromDate = date,
                    LastTreatment = date,
                    FuelRemaining = fuel,
                    Status = Status.Ready,
                    TotalTrip = mileageInLastTreat
                });
            }


            foreach (var item in ListStations)
            {
                item.isActive = true;
            }
            foreach (var item in ListLines)
            {
                item.isActive = true;
            }
            //ListTrips = new List<Trip>(); Shlav 2
            //ListLineTrips = new List<LineTrip>(); shlav 2
            // ListBusesOnTrips = new List<BusOnTrip>(); Shlav 2
            string AdjacentStationsPath = @"AdjacentStationsXml.xml";//XElement
            string BusPath = @"BusXml.xml";//XElement
            string LinePath = @"LineXml.xml";//
            string UserPath = @"UserhXml.xml";//
            string LineTripPath = @"LineTripXml.xml";//XElement
            string StationPath = @"StationXml.xml";//
            string LineStationsPath = @"LineStationsXml.xml";//
            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationsPath);
            XElement adjElement = new XElement("AdjacentStations");
            XElement busElement = new XElement("Busses");
            foreach (var item in ListAdjacentStations)
            {
                adjElement.Add(item.ToXElement());
            }
            XMLTools.SaveListToXMLElement(adjElement, AdjacentStationsPath);
            foreach (var item in ListBuses)
            {
                busElement.Add(item.ToXElement());
            }
            XMLTools.SaveListToXMLElement(busElement, BusPath);
            XElement lineTripsElement = new XElement("LineTrips");
            foreach (var item in ListLineTrips)
            {
                lineTripsElement.Add(item.ToXElement());
            }
            XMLTools.SaveListToXMLElement(lineTripsElement, LineTripPath);
        }


        static double DistanceBetween(Station station1, Station station2)
        {
            return Math.Sqrt(Math.Pow((station1.Latitude - station2.Latitude), 2) +
                Math.Pow((station1.Longitude - station2.Longitude), 2));
        }
    }
}

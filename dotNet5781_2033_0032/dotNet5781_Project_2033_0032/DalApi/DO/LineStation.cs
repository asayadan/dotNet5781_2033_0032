namespace DO
{
    public class LineStation
    {
        public bool isActive { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
    }
}

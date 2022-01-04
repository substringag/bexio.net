using System;

namespace bexio.net.Models.Projects.Timesheet
{
    // TODO maybe this can be merged with its parent class, depending on real world tests. 
    public class TimesheetFetched : Timesheet, IHasPrimaryKey
    {
        public int      Id       { get; set; } = default;
        public DateTime Date     { get; set; }
        public string?  Duration { get; set; }
        public bool     Running  { get; set; }

        public string? TravelTime     { get; set; }
        public string? TravelCharge   { get; set; }
        public int     TravelDistance { get; set; }
    }
}

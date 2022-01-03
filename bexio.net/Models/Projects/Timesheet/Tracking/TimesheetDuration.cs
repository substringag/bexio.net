using System;

namespace bexio.net.Models.Projects.Timesheet.Tracking
{
    public class TimesheetDuration : TrackingBase
    {
        public override string Type => "duration";

        public DateTime Date     { get; set; }
        public string   Duration { get; set; } = "";
    }
}

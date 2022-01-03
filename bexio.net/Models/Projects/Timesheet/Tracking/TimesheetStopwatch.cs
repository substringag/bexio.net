namespace bexio.net.Models.Projects.Timesheet.Tracking
{
    public class TimesheetStopwatch : TrackingBase
    {
        public override string Type => "stopwatch";

        public string Duration { get; set; } = "";
    }
}

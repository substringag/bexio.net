namespace bexio.net.Models.Projects.Timesheet.Tracking;

public record TimesheetStopwatch : TrackingBase
{
    public override string Type => "stopwatch";

    public string Duration { get; set; } = "";
}
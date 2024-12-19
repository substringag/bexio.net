namespace bexio.net.Models.Projects.Timesheet.Tracking;

public record TimesheetRange : TrackingBase
{
    public override string Type => "range";

    public DateTime Start { get; set; }
    public DateTime End   { get; set; }
}
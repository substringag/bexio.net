namespace bexio.net.Models.Projects.Timesheet.Tracking;

public record TimesheetDuration : TrackingBase
{
	public override string Type => "duration";

	public string Date { get; set; } = null!;
	public string Duration { get; set; } = "";
}
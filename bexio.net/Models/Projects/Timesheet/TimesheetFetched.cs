namespace bexio.net.Models.Projects.Timesheet;

public record TimesheetFetched : Timesheet, IHasPrimaryKey
{
    public int      Id       { get; set; } = 0;
    public string   Date     { get; set; } = null!;
    public string?  Duration { get; set; }
    public bool?    Running  { get; set; }

    public string? TravelTime     { get; set; }
    public string? TravelCharge   { get; set; }
    public int?    TravelDistance { get; set; }
}
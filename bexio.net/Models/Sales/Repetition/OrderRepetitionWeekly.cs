namespace bexio.net.Models.Sales.Repetition;

public record OrderRepetitionWeekly : OrderRepetitionIntervalBase
{
    public override string Type => "weekly";

    public int          Interval { get; set; } // in weeks
    public List<string> Weekdays { get; set; } = new();
}
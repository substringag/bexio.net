namespace bexio.net.Models.Sales.Repetition;

public record OrderRepetitionDaily : OrderRepetitionIntervalBase
{
    public override string Type => "daily";

    public int Interval { get; set; } // in days
}
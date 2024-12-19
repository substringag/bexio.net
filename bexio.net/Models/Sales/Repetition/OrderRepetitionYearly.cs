namespace bexio.net.Models.Sales.Repetition;

public record OrderRepetitionYearly : OrderRepetitionIntervalBase
{
    public override string Type => "yearly";

    public int Interval { get; set; } // in years
}
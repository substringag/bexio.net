namespace bexio.net.Models.Sales.Repetition
{
    public class OrderRepetitionYearly : OrderRepetitionIntervalBase
    {
        public override string Type => "yearly";

        public int Interval { get; set; } // in years
    }
}
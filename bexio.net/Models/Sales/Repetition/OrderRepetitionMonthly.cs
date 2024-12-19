namespace bexio.net.Models.Sales.Repetition;

public record OrderRepetitionMonthly : OrderRepetitionIntervalBase
{
    public override string Type => "monthly";

    public int    Interval { get; set; } // in months
    public string Schedule { get; set; } = Schedules.FirstDay; // "fixed_day" "week_day" "first_day" "last_day"

    public static class Schedules
    {
        public const string FixedDay = "fixed_day";
        public const string WeekDay  = "week_day";
        public const string FirstDay = "first_day";
        public const string LastDay  = "last_day";
    }
}
using System;

namespace bexio.net.Models.Sales.Repetition
{
    public class OrderRepetition
    {
        public DateTime? Start { get; set; }
        public DateTime? End   { get; set; }

        public OrderRepetitionIntervalBase? Repetition { get; set; }
    }
}

namespace bexio.net.Models.Sales.Positions
{
    public class PositionDiscountExtended : PositionBase
    {
        public override string? Type { get; set; } = PositionTypes.Discount;

        public string? Text          { get; set; }
        public bool    IsPercentual  { get; set; }
        public string? Value         { get; set; }
        public string? DiscountTotal { get; set; }
        
        public override string ToString()
            => $"DISCOUNT {Text} - {Value} {(IsPercentual ? "%" : "")} => {DiscountTotal}";

    }
}

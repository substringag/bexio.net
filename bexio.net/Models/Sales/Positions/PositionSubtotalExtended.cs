namespace bexio.net.Models.Sales.Positions
{
    public class PositionSubtotalExtended : PositionBase
    {
        public override string? Type { get; set; } = PositionTypes.Subtotal;

        public string? Text        { get; set; }
        public string? Value       { get; set; }
        public int?    InternalPos { get; set; }
        public bool    IsOptional  { get; set; }
        public int?    ParentId    { get; set; }

        public override string ToString()
            => $"{InternalPos}: {Text} {Value}";
    }
}

namespace bexio.net.Models.Sales.Positions;

public record PositionCustomExtended : PositionBase
{
    public override string? Type { get; set; } = PositionTypes.Custom;

    public string?  Amount            { get; set; }
    public int?     UnitId            { get; set; }
    public int?     AccountId         { get; set; }
    public string?  UnitName          { get; set; }
    public int?     TaxId             { get; set; }
    public string?  TaxValue          { get; set; }
    public string?  Text              { get; set; }
    public string?  UnitPrice         { get; set; }
    public string?  DiscountInPercent { get; set; }
    public string?  PositionTotal     { get; set; }
    public decimal? Pos               { get; set; }
    public int?     InternalPos       { get; set; }
    public bool     IsOptional        { get; set; }
    public int?     ParentId          { get; set; }

    public override string ToString()
        => $"{InternalPos}: [{Pos}] {Amount}x {UnitName} {Text} {UnitPrice}";
}
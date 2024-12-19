namespace bexio.net.Models.Sales.Positions;

public record PositionPagebreakExtended : PositionBase
{
    public override string? Type { get; set; } = PositionTypes.Pagebreak;

    public int? InternalPos { get; set; }
    public bool IsOptional  { get; set; }
    public int? ParentId    { get; set; }
        
    public override string ToString()
        => $"{InternalPos}: PAGEBREAK";

}
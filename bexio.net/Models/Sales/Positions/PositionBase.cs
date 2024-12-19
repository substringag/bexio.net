namespace bexio.net.Models.Sales.Positions;

public abstract record PositionBase
{
    public         int?    Id   { get; set; }
    public virtual string? Type { get; set; }
}
namespace bexio.net.Models.Sales.Positions
{
    public abstract class PositionBase
    {
        public         int?    Id   { get; set; }
        public virtual string? Type { get; set; }
    }
}

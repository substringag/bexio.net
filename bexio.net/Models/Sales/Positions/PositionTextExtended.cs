namespace bexio.net.Models.Sales.Positions
{
    public class PositionTextExtended : PositionBase
    {
        public override string? Type { get; set; } = PositionTypes.Text;

        public string? Text        { get; set; }
        public bool    ShowPosNr   { get; set; }
        public string? Pos         { get; set; }
        public int?    InternalPos { get; set; }
        public bool    IsOptional  { get; set; }
        public int?    ParentId    { get; set; }

        public override string ToString()
            => $"{InternalPos}: [{Pos}] {Text}";
    }
}

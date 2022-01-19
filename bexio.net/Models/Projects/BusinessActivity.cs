namespace bexio.net.Models.Projects
{
    public class BusinessActivity : IHasPrimaryKey
    {
        public int      Id                  { get; set; } = default;
        public string   Name                { get; set; } = "";
        public bool?    DefaultIsBillable   { get; set; }
        public decimal? DefaultPricePerHour { get; set; }
        public int?     AccountId           { get; set; }
    }
}

namespace bexio.net.Models.Projects;

public record BusinessActivity : IHasPrimaryKey
{
    public int      Id                  { get; set; } = 0;
    public string   Name                { get; set; } = "";
    public bool?    DefaultIsBillable   { get; set; }
    public decimal? DefaultPricePerHour { get; set; }
    public int?     AccountId           { get; set; }
}
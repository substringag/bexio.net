namespace bexio.net.Models.Projects
{
    public class WorkPackage : IHasPrimaryKey
    {
        public int      Id                   { get; set; } = default;
        public string   Name                 { get; set; } = "";
        public decimal? SpentTimeInHours     { get; set; }
        public decimal? EstimatedTimeInHours { get; set; }
        public string?  Comment              { get; set; }
        public int?     PrMilestoneId        { get; set; }
    }
}

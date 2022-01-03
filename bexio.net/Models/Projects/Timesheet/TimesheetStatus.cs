namespace bexio.net.Models.Projects.Timesheet
{
    public class TimesheetStatus : IHasPrimaryKey
    {
        public int     Id   { get; set; }
        public string? Name { get; set; }
    }
}

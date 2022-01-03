namespace bexio.net.Models.Timesheet
{
    public class Timesheet : TimesheetBase
    {
        public int    id       { get; set; }
        public string date     { get; set; }
        public string duration { get; set; }
    }
}

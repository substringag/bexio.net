using System.Text.Json.Serialization;
using bexio.net.Models.Projects.Timesheet.Tracking;

namespace bexio.net.Models.Projects.Timesheet
{
    public class Timesheet
    {
        public int           UserId          { get; set; }
        public int?          StatusId        { get; set; }
        public int           ClientServiceId { get; set; }
        public string?       Text            { get; set; }
        public bool          AllowableBill   { get; set; }
        public string?       Charge          { get; set; }
        public int?          ContactId       { get; set; }
        public int?          SubContactId    { get; set; }
        public int?          PrProjectId     { get; set; }
        public int?          PrPackageId     { get; set; }
        public int?          PrMilestoneId   { get; set; }
        public string?       EstimatedTime   { get; set; }

        // Todo: @nino fix pls
        public TimesheetDuration? Tracking   { get; set; }
    }
}

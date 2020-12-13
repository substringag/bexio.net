using System.Collections.Generic;

namespace bexio.net.Models
{
    public class TimesheetBase
    {
        public string user_id { get; set; }
        public string status_id { get; set; }
        public string client_service_id { get; set; }
        public string text { get; set; }
        public string allowable_bill { get; set; }
        public string charge { get; set; }
        public string contact_id { get; set; }
        public string sub_contact_id { get; set; }
        public string pr_project_id { get; set; }
        public string pr_package_id { get; set; }
        public string pr_milestone_id { get; set; }
        public string travel_time { get; set; }
        public string travel_charge { get; set; }
        public string travel_distance { get; set; }
        public string estimated_time { get; set; }
        public string date { get; set; }
        public string duration { get; set; }
        public string running { get; set; }
        public Tracking tracking { get; set; }
    }
}

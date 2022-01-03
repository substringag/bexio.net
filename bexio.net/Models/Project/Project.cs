using System.Collections.Generic;

namespace bexio.net.Models
{
    public class Project
    {
        public string id { get; set; }
        public string nr { get; set; }
        public string name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string comment { get; set; }
        public string pr_state_id { get; set; }
        public string pr_project_type_id { get; set; }
        public string contact_id { get; set; }
        public string contact_sub_id { get; set; }
        public string pr_invoice_type_id { get; set; }
        public string pr_invoice_type_amount { get; set; }
        public string pr_budget_type_id { get; set; }
        public string pr_budget_type_amount { get; set; }
    }
}

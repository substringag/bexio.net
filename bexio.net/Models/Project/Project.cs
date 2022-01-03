using System;

namespace bexio.net.Models.Project
{
    public class Project
    {
        public int       id                     { get; set; }
        public string?   nr                     { get; set; }
        public string?   name                   { get; set; }
        public DateTime? start_date             { get; set; }
        public DateTime? end_date               { get; set; }
        public string?   comment                { get; set; }
        public int       pr_state_id            { get; set; }
        public int       pr_project_type_id     { get; set; }
        public int       contact_id             { get; set; }
        public int?      contact_sub_id         { get; set; }
        public int?      pr_invoice_type_id     { get; set; }
        public int?      pr_invoice_type_amount { get; set; }
        public int?      pr_budget_type_id      { get; set; }
        public int?      pr_budget_type_amount  { get; set; }
    }
}

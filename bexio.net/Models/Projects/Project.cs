using System;

namespace bexio.net.Models.Projects
{
    public class Project : IHasPrimaryKey
    {
        public int       Id                  { get; set; } = default;
        public string?   Uuid                { get; set; }
        public string?   Nr                  { get; set; }
        public string    Name                { get; set; } = "";
        public DateTime? StartDate           { get; set; }
        public DateTime? EndDate             { get; set; }
        public string?   Comment             { get; set; }
        public int       PrStateId           { get; set; }
        public int       PrProjectTypeId     { get; set; }
        public int       ContactId           { get; set; }
        public int?      ContactSubId        { get; set; }
        public int?      PrInvoiceTypeId     { get; set; } // 1-4
        public decimal?  PrInvoiceTypeAmount { get; set; }
        public int?      PrBudgetTypeId      { get; set; } // 1-4
        public decimal?  PrBudgetTypeAmount  { get; set; }

        public int? UserId { get; set; } // only used for creation and modification
    }
}

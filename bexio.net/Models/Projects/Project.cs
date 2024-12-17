using System.ComponentModel.DataAnnotations;

namespace bexio.net.Models.Projects
{
    public record Project
    {
        public int?      Id                  { get; set; }
        public string?   Uuid                { get; set; }
        public string?   Nr                  { get; set; }
        
        [MaxLength(800)]
        public string    Name                { get; set; } = null!;
        
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
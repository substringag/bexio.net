using System.ComponentModel.DataAnnotations;

namespace bexio.net.Models.Projects
{
    public record Project
    {
        public int?      Id                  { get; set; }
        public string?   Uuid                { get; set; }
        public string?   Nr                  { get; set; }
        
        [MaxLength(1000)]
        public string    Name                { get; set; } = null!;
        
        public DateTime? StartDate           { get; set; }
        public DateTime? EndDate             { get; set; }
        public string?   Comment             { get; set; }
        public int       PrStateId           { get; set; }
        public int       PrProjectTypeId     { get; set; }
        public int       ContactId           { get; set; }
        public int?      ContactSubId        { get; set; }
        
        [EnumDataType(typeof(Helpers.Enums.ValidPrInvoiceType), ErrorMessage = "PrInvoiceTypeId - Valid values are 1,2,3 or 4")]
        public int?      PrInvoiceTypeId     { get; set; } 
        public decimal?  PrInvoiceTypeAmount { get; set; }
        
        [EnumDataType(typeof(Helpers.Enums.ValidPrBudgetTypeId), ErrorMessage = " PrBudgetTypeId - Valid values are 1,2,3 or 4")]
        public int?      PrBudgetTypeId      { get; set; } 
        public decimal?  PrBudgetTypeAmount  { get; set; }

        public int? UserId { get; set; } // only used for creation and modification
    }
}
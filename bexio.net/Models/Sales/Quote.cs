namespace bexio.net.Models.Sales;

public record Quote : SalesBaseObject
{
    public DateTime? IsValidUntil               { get; set; }
    public int?      DeliveryAddressType        { get; set; }
    public string?   DeliveryAddress            { get; set; }
    public int?      KbTermsOfPaymentTemplateId { get; set; }
    public bool      ShowTotal                  { get; set; }
}
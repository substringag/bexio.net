namespace bexio.net.Models.Sales
{
    public record Delivery : SalesBaseObject
    {
        public int?    DeliveryAddressType { get; set; }
        public string? DeliveryAddress     { get; set; }
    }
}

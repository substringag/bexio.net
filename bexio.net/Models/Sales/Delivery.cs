namespace bexio.net.Models.Sales
{
    public class Delivery : SalesBaseObject
    {
        public int?    DeliveryAddressType { get; set; }
        public string? DeliveryAddress     { get; set; }
    }
}

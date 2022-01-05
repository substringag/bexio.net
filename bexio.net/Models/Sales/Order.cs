namespace bexio.net.Models.Sales
{
    public class Order : SalesBaseObject
    {
        public int?    DeliveryAddressType { get; set; }
        public string? DeliveryAddress     { get; set; }
        public bool    IsRecurring         { get; set; }
    }
}

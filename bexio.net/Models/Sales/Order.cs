namespace bexio.net.Models.Sales
{
    public record Order : SalesBaseObject
    {
        public int?    DeliveryAddressType { get; set; }
        public string? DeliveryAddress     { get; set; }
        public bool?   IsRecurring         { get; set; }
    }
}

namespace bexio.net.Models.Sales
{
    public record Invoice : SalesBaseObject
    {
        public string?   TotalReceivedPayments { get; set; }
        public string?   TotalCreditVouchers   { get; set; }
        public DateTime? IsValidTo             { get; set; }
        public string?   Reference             { get; set; }
    }
}

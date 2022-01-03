namespace bexio.net.Models.BusinessActivity
{
    public class BusinessActivity
    {
        public string id                     { get; set; }
        public string name                   { get; set; }
        public string default_is_billable    { get; set; }
        public string default_price_per_hour { get; set; }
        public string account_id             { get; set; }
    }
}

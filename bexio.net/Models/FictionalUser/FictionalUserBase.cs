namespace bexio.net.Models
{
    //<Entity>Base contains everything expect id and is used as insertType 
    public class FictionalUserBase 
    {
        public string salutation_type { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string title_id { get; set; }
    }
}
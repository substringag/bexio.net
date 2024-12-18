namespace bexio.net.Models.Contacts
{
    public record ContactRelation : IHasPrimaryKey
    {
        public int       Id           { get; set; } = 0;
        public int       ContactId    { get; set; }
        public int       ContactSubId { get; set; }
        public string?   Description  { get; set; }
        public DateTime? UpdatedAt    { get; set; }
    }
}

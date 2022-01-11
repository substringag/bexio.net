using System;

namespace bexio.net.Models.Contacts
{
    public class ContactRelation : IHasPrimaryKey
    {
        public int       Id           { get; set; } = default;
        public int       ContactId    { get; set; }
        public int       ContactSubId { get; set; }
        public string?   Description  { get; set; }
        public DateTime? UpdatedAt    { get; set; }
    }
}

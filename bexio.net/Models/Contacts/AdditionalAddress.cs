namespace bexio.net.Models.Contacts
{
    public class AdditionalAddress : IHasPrimaryKey
    {
        public int     Id          { get; set; } = default;
        public string? Name        { get; set; }
        public string? Address     { get; set; }
        public string? Postcode    { get; set; }
        public string? City        { get; set; }
        public int?    CountryId   { get; set; }
        public string? Subject     { get; set; }
        public string? Description { get; set; }
    }
}

namespace bexio.net.Models
{
    public record SimpleDictionaryEntry
    {
        public int     Id   { get; set; } = 0;
        public string? Name { get; set; } = "";

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}

namespace bexio.net.Models
{
    public class SimpleDictionaryEntry
    {
        public int     Id   { get; set; } = default;
        public string? Name { get; set; } = "";

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}

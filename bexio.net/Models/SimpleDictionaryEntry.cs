namespace bexio.net.Models
{
    public class SimpleDictionaryEntry
    {
        public int    Id   { get; set; }
        public string Name { get; set; } = "";

        public override string ToString()
        {
            return Name;
        }
    }
}

namespace bexio.net.Models.Other.User
{
    public class FictionalUser : IHasPrimaryKey
    {
        public int    Id             { get; set; } = default;
        public string SalutationType { get; set; } = "";
        public string Firstname      { get; set; } = "";
        public string Lastname       { get; set; } = "";
        public string Email          { get; set; } = "";
        public int?   TitleId        { get; set; }
    }
}

namespace bexio.net.Models.Other.User;

public record User : IHasPrimaryKey
{
    public int     Id             { get; set; } = 0;
    public string? SalutationType { get; set; }
    public string? Firstname      { get; set; }
    public string? Lastname       { get; set; }
    public string  Email          { get; set; } = "";
    public bool    IsSuperadmin   { get; set; }
    public bool    IsAccountant   { get; set; }
}
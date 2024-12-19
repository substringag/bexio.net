using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bexio.net.Models.Contacts;

public record Contact
{
    public int?     Id            { get; set; }
    public string? Nr            { get; set; }
        
    [EnumDataType(typeof(Helpers.Enums.ValidContactTypeId), ErrorMessage = "ContactTypeId - Valid values are 1 or 2")]
    public int     ContactTypeId { get; set; } // 1 company, 2 person

    [JsonPropertyName("name_1")]
    public string Name1 { get; set; } = "";

    [JsonPropertyName("name_2")]
    public string? Name2 { get; set; }

    public int?      SalutationId   { get; set; }
    public int?      SalutationForm { get; set; }
    public int?      TitleId        { get; set; }
    public DateTime? Birthday       { get; set; }
    public string?   Address        { get; set; }
    public string?   Postcode       { get; set; }
    public string?   City           { get; set; }
    public int?      CountryId      { get; set; }

    [EmailAddress]
    public string? Mail { get; set; }

    [EmailAddress]
    public string? MailSecond { get; set; }

    public string? PhoneFixed       { get; set; }
    public string? PhoneFixedSecond { get; set; }
    public string? PhoneMobile      { get; set; }
    public string? Fax              { get; set; }
    public string? Url              { get; set; }
    public string? SkypeName        { get; set; }
    public string? Remarks          { get; set; }
    public int?    LanguageId       { get; set; }

    [Obsolete]
    public bool? IsLead { get; set; }

    /// <summary>
    /// A list of foreign keys, comma separated.
    /// </summary>
    public string? ContactGroupIds { get; set; }

    /// <summary>
    /// A list of foreign keys.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<int> ContactGroupIdsList =>
        ContactGroupIds?.Split(',').Select(int.Parse) ?? new List<int>();

    /// <summary>
    /// A list of foreign keys, comma separated.
    /// Also called contact_sector
    /// </summary>
    public string? ContactBranchIds { get; set; }

    /// <summary>
    /// A list of foreign keys.
    /// Also called contact_sector
    /// </summary>
    [JsonIgnore]
    public IEnumerable<int> ContactBranchIdsList =>
        ContactBranchIds?.Split(',').Select(int.Parse) ?? new List<int>();

    public int       UserId       { get; set; }
    public int       OwnerId      { get; set; }
    public DateTime? UpdatedAt    { get; set; }
    public string?   ProfileImage { get; set; } // base64
}
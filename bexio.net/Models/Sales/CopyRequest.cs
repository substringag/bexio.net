using System;
using System.Text.Json.Serialization;

namespace bexio.net.Models.Sales
{
    public class CopyRequest
    {
        public int     ContactId    { get; set; }
        public int?    ContactSubId { get; set; }
        public string? IsValidFrom  => IsValidFromDate?.ToString("yyyy-MM-dd");

        [JsonIgnore]
        public DateTime? IsValidFromDate { get; set; } = DateTime.Today;

        public int?    PrProjectId { get; set; }
        public string? Title       { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace bexio.net.Models.Timesheet
{
    public class TimesheetSearchBody
    {
        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("criteria")]
        public string Criteria { get; set; }
    }
}

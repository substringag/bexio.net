using Newtonsoft.Json;

namespace bexio.net.Models
{
    public class TimesheetSearchBody
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("criteria")]
        public string Criteria { get; set; }
    }
}

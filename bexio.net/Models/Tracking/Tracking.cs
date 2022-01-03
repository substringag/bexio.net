using System.Collections.Generic;
using Newtonsoft.Json;

namespace bexio.net.Models
{
	public class Tracking
	{
		// [JsonIgnore]
		public string type { get; set; }
		public string date { get; set; }
		public string duration { get; set; }
	}
}

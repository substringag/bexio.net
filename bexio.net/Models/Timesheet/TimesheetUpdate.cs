using System;
using System.Collections.Generic;

namespace bexio.net.Models
{
	public class TimesheetUpdate
	{
		public int id { get; set; }
		public int user_id { get; set; }
		public int? status_id { get; set; }
		public int? client_service_id { get; set; }
		public string text { get; set; }
		public Boolean allowable_bill { get; set; }
		public string charge { get; set; }
		public int? contact_id { get; set; }
		public int? sub_contact_id { get; set; }
		public int? pr_project_id { get; set; }
		public int? pr_package_id { get; set; }
		public int? pr_milestone_id { get; set; }
		public string estimated_time { get; set; }
		public TrackingUpdate tracking { get; set; }
	}
}

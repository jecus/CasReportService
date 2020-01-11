using System;

namespace CASReports.Models
{
	public class MaintenanceDirective : BaseModel
	{
		public string ScheduleRef { get; set; }
		public string ScheduleRevisionNum { get; set; }
		public DateTime ScheduleRevisionDate { get; set; }
	}
}
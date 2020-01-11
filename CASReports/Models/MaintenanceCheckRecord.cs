using System;

namespace CASReports.Models
{
	public class MaintenanceCheckRecord : BaseModel
	{
		public DateTime RecordDate { get; set; }
		public MaintenanceCheck ParentCheck { get; set; }
		public string ComplianceCheckName { get; set; }
	}
}
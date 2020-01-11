using System;

namespace CASReports.Models
{
	public class Directive : BaseModel
	{
		public AtaChapter ATAChapter { get; set; }
		public string Description { get; set; }
		public DirectiveWorkType WorkType { get; set; }
		public double ManHours { get; set; }
		public double Cost { get; set; }
		public DateTime? NextPerformanceDate { get; set; }
	}
}
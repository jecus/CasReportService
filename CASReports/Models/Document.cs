using System;

namespace CASReports.Models
{
	public class Document : BaseModel
	{
		public DocumentSubType DocumentSubType { get; set; }
		public bool IssueValidTo { get; set; }
		public DateTime IssueDateValidTo { get; set; }
	}
}
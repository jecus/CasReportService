using System;

namespace CASReports.Models
{
	public class DirectiveRecord : BaseModel
	{
		public DateTime RecordDate { get; set; }
		public string Remarks { get; set; }
	}
}
using System;

namespace CASReports.Models
{
	public class CertificateOfReleaseToService : BaseModel
	{
		public string Station { get; set; }
		public DateTime RecordDate { get; set; }
		public Specialist AuthorizationB1 { get; set; }
	}
}
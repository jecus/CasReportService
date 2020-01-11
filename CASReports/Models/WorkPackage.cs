using System;

namespace CASReports.Models
{
	public class WorkPackage : BaseModel
	{
		public string Title { get; set; }
		public int ParentId { get; set; }
		public DateTime OpeningDate { get; set; }
		public string Number { get; set; }
		public string Station { get; set; }
		public Aircraft Aircraft { get; set; }
		public string Author { get; set; }
		public string PublishedBy { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ClosingDate { get; set; }
		public DateTime PublishingDate { get; set; }
		public WorkPackageStatus Status { get; set; }
		public string ReleaseCertificateNo { get; set; }
		public string CheckType { get; set; }
		public string MaintenanceRepairOrzanization { get; set; }
		public string Remarks { get; set; }
		public string Revision { get; set; }

	}
}
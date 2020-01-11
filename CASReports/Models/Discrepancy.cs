namespace CASReports.Models
{
	public class Discrepancy : BaseModel
	{
		public string Description { get; set; }
		public bool FilledBy { get; set; }
		public CorrectiveAction CorrectiveAction { get; set; }
		public CertificateOfReleaseToService CertificateOfReleaseToService { get; set; }
	}
}
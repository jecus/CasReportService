namespace CASReports.Models
{
	public class CorrectiveAction : BaseModel
	{
		public string AddNo { get; set; }
		public string Description { get; set; }
		public string PartNumberOn { get; set; }
		public string SerialNumberOn { get; set; }
		public string PartNumberOff { get; set; }
		public string SerialNumberOff { get; set; }
	}
}
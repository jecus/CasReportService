namespace CASReports.Models
{
	public class FuelTankCondition : BaseModel
	{
		public string Tank { get; set; }
		public double Remaining { get; set; }
		public double OnBoard { get; set; }
		public double Correction { get; set; }

	}
}
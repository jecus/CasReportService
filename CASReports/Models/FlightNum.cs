namespace CASReports.Models
{
	public class FlightNum : BaseModel
	{
		public string FullName { get; set; }

		public override string ToString()
		{
			return FullName;
		}
		
	}
}
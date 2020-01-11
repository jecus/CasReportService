namespace CASReports.Models
{
	public class AirportsCodes : BaseModel
	{
		public string City { get; set; }
		public string ShortName { get; set; }
		

		public override string ToString()
		{
			return $"{ShortName} {City}";
		}
	}
}
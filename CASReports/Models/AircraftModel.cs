namespace CASReports.Models
{
	public class AircraftModel : BaseModel
	{
		public override string ToString()
		{
			return FullName;
		}

		public string FullName { get; set; }
		public string ShortName { get; set; }
	}
}
namespace CASReports.Models
{
	public class Locations : BaseModel
	{
		public string ShortName { get; set; }
		public LocationsType LocationsType { get; set; }

		public override string ToString()
		{
			return $"{ShortName} ({LocationsType?.ShortName})";
		}
	}
}
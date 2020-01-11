namespace CASReports.Models
{
	public class LocationsType : BaseModel
	{
		public string FullName { get; set; }

		public override string ToString()
		{
			return $"{FullName}";
		}
	}
}
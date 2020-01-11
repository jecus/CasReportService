namespace CASReports.Models
{
	public class MaintenanceCheckType : BaseModel
	{
		public string FullName { get; set; }

		public override string ToString()
		{
			return FullName;
		}
	}
}
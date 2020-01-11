namespace CASReports.Models
{
	public class Store : BaseModel
	{
		public Operator Operator { get; set; }
		public string Location { get; set; }
	}
}
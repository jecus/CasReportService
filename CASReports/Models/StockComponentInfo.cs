namespace CASReports.Models
{
	public class StockComponentInfo : BaseModel
	{
		public string PartNumber { get; set; }
		public string Description { get; set; }
		public double Current { get; set; }
		public double ShouldBeOnStock { get; set; }
	}
}
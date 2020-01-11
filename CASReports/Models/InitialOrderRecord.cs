namespace CASReports.Models
{
	public class InitialOrderRecord : BaseModel
	{
		public string AccessoryDescription { get; set; }
		public Product Product { get; set; }
		public double Quantity { get; set; }
		public string Reference { get; set; }
		public string Remarks { get; set; }
	}
}
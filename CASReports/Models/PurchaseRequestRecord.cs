namespace CASReports.Models
{
	public class PurchaseRequestRecord : BaseModel
	{
		public InitialOrderRecord ParentInitialRecord { get; set; }
		public double Cost { get; set; }
		public double Quantity { get; set; }
		public ComponentStatus CostCondition { get; set; }
		public Product Product { get; set; }
	}
}
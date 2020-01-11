namespace CASReports.Models
{
	public class PurchaseOrder : BaseModel
	{
		public string IncoTermRef { get; set; }
		public Supplier ShipTo { get; set; }
		public Supplier ShipCompany { get; set; }
		public PayTerm PayTerm { get; set; }
		public string Number { get; set; }
		public Supplier Supplier { get; set; }
	}
}
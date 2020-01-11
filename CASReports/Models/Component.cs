using System;

namespace CASReports.Models
{
	public class Component : BaseModel
	{
		public string PartNumber { get; set; }
		public string Description { get; set; }
		public GoodsClass GoodsClass { get; set; }
		public string Remarks { get; set; }
		public double QuantityIn { get; set; }
		public double Quantity { get; set; }
		public double ShouldBeOnStock { get; set; }
		public double NeedWpQuantity { get; set; }
		public AtaChapter ATAChapter { get; set; }
		public Locations Location { get; set; }
		public string SerialNumber { get; set; }
		public string BatchNumber { get; set; }
		public string IdNumber { get; set; }
		public string Position { get; set; }
		public string Manufacturer { get; set; }
		public bool LLPMark { get; set; }
		public bool LLPCategories { get; set; }
		public string MPDItem { get; set; }
		public DateTime? NextPerformanceDate { get; set; }
		public DateTime ManufactureDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public int ParentAircraftId { get; set; }
		public ComponentStatus ComponentStatus { get; set; }
		public AvionicsInventoryMarkType AvionicsInventory { get; set; }
		public double Cost { get; set; }
		public double CostOverhaul { get; set; }
		public double CostServiceable { get; set; }
		public string HiddenRemarks { get; set; }
		public bool IsPOOL { get; set; }
		public bool IsDangerous { get; set; }
	}
}
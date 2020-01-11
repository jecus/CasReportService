namespace CASReports.Models
{
	public class ComponentDirective : BaseModel
	{
		public DirectiveRecord LastPerformance { get; set; }
		public Component ParentComponent { get; set; }
		public double ManHours { get; set; }
		public double Cost { get; set; }

	}
}
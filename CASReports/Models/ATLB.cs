namespace CASReports.Models
{
	public class ATLB : BaseModel
	{
		public int StartPageNo { get; set; }
		//public AircraftFlightCollection AircraftFlightsCollection { get; set; }
		public int ParentAircraftId { get; set; }
	}
}
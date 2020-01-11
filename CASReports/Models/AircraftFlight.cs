using System;

namespace CASReports.Models
{
	public class AircraftFlight : BaseModel
	{
		public ATLB ParentATLB { get; set; }
		public FlightNum FlightNumber { get; set; }
		public DateTime FlightDate { get; set; }
		public AirportsCodes StationFromId { get; set; }
		public AirportsCodes StationToId { get; set; }
		public TimeSpan TimespanOutTime { get; set; }
		public TimeSpan TimespanInTime { get; set; }
		public TimeSpan BlockTime { get; set; }
		public TimeSpan TimespanTakeOffTime { get; set; }
		public TimeSpan TimespanLDGTime { get; set; }
		public TimeSpan FlightTime { get; set; }
		public FuelTankConditionCollection FuelTankCollection { get; set; }
		public FluidsCondition Fluids { get; set; }
	}
}